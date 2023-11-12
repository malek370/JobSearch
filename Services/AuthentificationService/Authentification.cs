using JobSearch.Data;
using JobSearch.DTOs;
using JobSearch.DTOs.AuthentificationDTOs;
using JobSearch.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace JobSearch.Services.Authentification
{
    public class Authentification : Iauthentification
    {
        const int keySize = 64;
        const int iterations = 350000;
        HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
        private readonly JobDbContext _DbContext;
        private readonly IConfiguration _config;
        public Authentification(JobDbContext dbContext, IConfiguration config)
        {
            _DbContext = dbContext;
            _config = config;
        }

        public async Task<ServiceResponse<bool>>  Register(AddUserDTO addUser)
        {
            ServiceResponse<bool> result = new ServiceResponse<bool>();
            try
            {
                if (!checkUserRegistration(addUser)) throw new Exception("somthing is wrong");
                if(await _DbContext.Users.FirstOrDefaultAsync(u => u.Email == addUser.Email||u.UserName==addUser.UserName)!=null) { throw new Exception("user exists"); }
                byte[] salt;
                string password_hashed=HashPasword(addUser.Password,out salt);
                var user = new User()
                {
                    Email = addUser.Email,
                    Name = addUser.Name,
                    UserName = addUser.UserName,
                    HashedPassword = password_hashed,
                    SaltPassword = salt
                };
                await _DbContext.AddAsync(user);
                await _DbContext.SaveChangesAsync();
                result.Data = true;
                result.Message = "user created successfully";
            }
            catch (Exception ex) { result.Success = false;result.Message = ex.Message;result.Data = false; }
            return result;

        }

        public async Task<ServiceResponse<string>> Login(LoginUserDTO LoginUser)
        {
            ServiceResponse<string> result = new ServiceResponse<string>();
            try
            {
                var user = await _DbContext.Users.FirstOrDefaultAsync(u=>u.Email==LoginUser.Email);
                if (user == null) throw new Exception("user not found!");
                if (!VerifyPassword(LoginUser.Password,user.HashedPassword,user.SaltPassword!)) throw new Exception("user not found!");
                result.Data = CreateToken(user);
            }
            catch (Exception ex) { result.Success = false; result.Message = ex.Message; result.Data =""; }
            return result;
        }

        //--------------------private methodes---------------------------
        private bool checkUserRegistration(AddUserDTO user)
        {
            if (user.Password.Length == 0) return false;
            if(user.UserName.Length == 0) return false;
            if(user.Name.Length == 0) return false;
            if(user.Email.Length == 0)return false;

            return true;
        }
        private string HashPasword(string password, out byte[] salt)
        {
            
            salt = RandomNumberGenerator.GetBytes(keySize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                keySize);
            return Convert.ToHexString(hash);
        }
        private bool VerifyPassword(string password, string hash, byte[] salt)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm, keySize);
            return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
        }
        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _config.GetSection("AppSettings:Token").Value!));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                                   claims: claims,
                                   expires: DateTime.Now.AddDays(1),
                                   signingCredentials: cred
                                     );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

    }
}
