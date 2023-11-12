using JobSearch.DTOs.AuthentificationDTOs;
using JobSearch.Services.Authentification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobSearch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthentificationController : ControllerBase
    {
        private readonly Iauthentification _authentification;
        public AuthentificationController(Iauthentification authentification)
        {
            _authentification = authentification;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(AddUserDTO addUserDTO)
        {
            return Ok(await _authentification.Register(addUserDTO));
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDTO loginUserDTO)
        {
            return Ok(await _authentification.Login(loginUserDTO));
        }
    }
}
