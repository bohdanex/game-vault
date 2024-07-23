using GameVault.ObjectModel.Enums;
using GameVault.Services.Abstraction;
using GameVault.Services.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GameVault.Services
{
    public class JWT_TokenGenerator(IConfiguration _configuration) : ITokenGenerator
    {
        private static readonly TimeSpan ExpirationTime = TimeSpan.FromHours(5);

        public async Task<string> GenerateToken(Guid id, Role role)
        {
            return await Task.Run(() =>
            {
                SecurityKey securityKey = new SymmetricSecurityKey(_configuration["JWT_Settings:Key"]!.GetBytesUTF8());

                // 1. Get token generation data.;
                string issuer = _configuration["JWT_Settings:Issuer"]!;
                string audience = _configuration["JWT_Settings:Audience"]!;
                DateTime expireDate = DateTime.UtcNow.Add(ExpirationTime);

                // 2. Create Credentials.
                SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

                // 3. Add Custom Claims
                List<Claim> claims =
                    [
                        new(ClaimTypes.Role, role.ToString()),
                        new("jti", id.ToString())
                    ];


                // 4. Create JWT Security Token
                JwtSecurityToken securityToken = new(issuer, audience, claims, null, expireDate, signingCredentials);

                // 5. Handle and write the token
                return new JwtSecurityTokenHandler().WriteToken(securityToken);
            });
        }

        public ClaimsPrincipal GetTokenClaims(string jwt)
        {
            SymmetricSecurityKey key = new(_configuration["JWT_Settings:Key"]!.GetBytesUTF8());
            JwtSecurityTokenHandler tokenHandler = new();
            TokenValidationParameters validationParameters = new()
            {
                ValidIssuer = _configuration["JWT_Settings:Issuer"],
                ValidAudience = _configuration["JWT_Settings:Audience"],
                IssuerSigningKey = key,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true
            };

            return tokenHandler.ValidateToken(jwt, validationParameters, out SecurityToken _);
        }
    }
}
