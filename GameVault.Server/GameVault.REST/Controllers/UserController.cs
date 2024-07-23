using GameVault.ObjectModel.DTOs;
using GameVault.ObjectModel.Models;
using GameVault.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameVault.REST.Controllers
{
    [Route("[controller]")]
    public class UserController(IUserService _userService) : Controller
    {
        [Route("register")]
        [HttpPost]
        public async Task<RegistrationResponse> RegisterUser([FromBody] RegistrationRequest registrationRequest)
        {
            return await _userService.Register(
                registrationRequest.Nickname, 
                registrationRequest.Email, 
                registrationRequest.Password, 
                registrationRequest.Role ?? ObjectModel.Enums.Role.User);
        }

        [HttpGet]
        [Route("login")]
        public async Task<UserDTO?> LogIn([FromBody] LoginRequest loginRequest)
        {
            return await _userService.Authenticate(loginRequest.EmailOrNickname, loginRequest.Password);
        }

        [HttpGet]
        [Route("login/{jwt}")]
        public async Task<UserDTO?> LogIn(string jwt)
        {
            return await _userService.Authenticate(jwt);
        }
    }
}
