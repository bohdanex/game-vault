using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace GameVault.Services.Abstraction
{
    public interface ITokenGenerator
    {
        Task<string> GenerateToken(Guid id, ObjectModel.Enums.Role role);
        ClaimsPrincipal GetTokenClaims(string jwt);
    }
}
