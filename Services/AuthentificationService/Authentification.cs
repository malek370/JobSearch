using JobSearch.Data;
using JobSearch.DTOs;
using JobSearch.DTOs.AuthentificationDTOs;
using JobSearch.Models;
using Microsoft.EntityFrameworkCore;
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
        public Authentification(JobDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public async Task<ServiceResponse<bool>>  Register(AddUserDTO addUser)
        {
            ServiceResponse<bool> result = new ServiceResponse<bool>();
            try
            {
                if (!checkUserRegistration(addUser)) throw new Exception("somthing is wrong");
                
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
                result.Data = "jwt token";
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

        
    }
}
