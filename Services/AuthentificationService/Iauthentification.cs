using JobSearch.DTOs;
using JobSearch.DTOs.AuthentificationDTOs;

namespace JobSearch.Services.Authentification
{
    public interface Iauthentification
    {
        Task<ServiceResponse<bool>> Register(AddUserDTO AddUser);
        Task<ServiceResponse<string>> Login(LoginUserDTO LoginUser);
    }
}
