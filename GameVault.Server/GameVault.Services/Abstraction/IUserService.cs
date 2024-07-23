using GameVault.ObjectModel.Enums;

namespace GameVault.Services.Abstraction
{
    public interface IUserService
    {
        Task<ObjectModel.DTOs.UserDTO?> Authenticate(string emailOrNickname, string password);
        Task<ObjectModel.DTOs.UserDTO?> Authenticate(string JWT);
        Task<ObjectModel.Models.RegistrationResponse> Register(string nickname, string email, string password, Role role = Role.User);
    }
}
