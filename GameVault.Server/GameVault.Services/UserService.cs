using GameVault.ObjectModel.DTOs;
using GameVault.ObjectModel.Entities;
using GameVault.ObjectModel.Enums;
using GameVault.ObjectModel.Models;
using GameVault.Repository.Abstraction;
using GameVault.Services.Abstraction;
using System.Text;

namespace GameVault.Services
{
    public class UserService(IUserRepository _repository, IPasswordHasher _passwordHasher, ITokenGenerator _tokenGenerator) : IUserService
    {
        public async Task<UserDTO?> Authenticate(string emailOrNickname, string password)
        {
            User? user = await _repository.GetByNameOrEmail(emailOrNickname, emailOrNickname);

            if(user is not null && await _passwordHasher.VerifyPassword(user.SaltedPassword, password, Encoding.UTF8.GetBytes(user.Salt)))
            {
                return new UserDTO(user) { JWT = await _tokenGenerator.GenerateToken(user.Id, user.Role)};
            }

            return null;
        }

        public async Task<UserDTO?> Authenticate(string JWT)
        {
            string? claimIdString = _tokenGenerator.GetTokenClaims(JWT).FindFirst("jti")?.Value;
            if(claimIdString is not null)
            {
                Guid id = Guid.Parse(claimIdString);
                return new UserDTO(await _repository.GetById(id));
            }

            return null;
        }

        public async Task<RegistrationResponse> Register(string nickname, string email, string password, Role role = Role.User)
        {
            if(await _repository.GetByNameOrEmail(nickname, email) is not null)
            {
                return new RegistrationResponse() { RegistrationInfo = RegistrationInfo.UserAlreadyExists };
            }

            string salt = Guid.NewGuid().ToString();
            string saltedPassword = await _passwordHasher.GetHashedPassword(password, Encoding.UTF8.GetBytes(salt));
            Guid id = Guid.NewGuid();

            User newUser = new()
            {
                Id = id,
                Nickname = nickname,
                Email = email,
                Role = role,
                Salt = salt,
                SaltedPassword = saltedPassword
            };

            await _repository.Register(newUser);

            RegistrationResponse response = new()
            {
                User = new UserDTO(newUser) { JWT = await _tokenGenerator.GenerateToken(id, role)},
                RegistrationInfo = RegistrationInfo.Success
            };

            return response;
        }
    }
}
