using GameVault.Services.Abstraction;
using GameVault.Services.Extensions;
using System.Security.Cryptography;
using System.Text;

namespace GameVault.Services
{
    public class PBKDF2PasswordHasher : IPasswordHasher
    {
        private const int iterations = 10000;
        private const int keyLength = 64;
        private readonly HashAlgorithmName HashingAlgorithm = HashAlgorithmName.SHA512;

        private async Task<byte[]> GetHashedPasswordBytes(string password, byte[] salt)
        {
            byte[] passwordBytes = password.GetBytesUTF8();

            return await Task.Run(() => Rfc2898DeriveBytes.Pbkdf2(passwordBytes, salt, iterations, HashingAlgorithm, keyLength));
        }

        public async Task<string> GetHashedPassword(string password, byte[] salt) 
            => Convert.ToHexString(await GetHashedPasswordBytes(password, salt));

        public async Task<bool> VerifyPassword(string userSaltedPass, string incomePassword, byte[] salt)
        {
            byte[] incomePassSalted = await GetHashedPasswordBytes(incomePassword, salt);
            return CryptographicOperations.FixedTimeEquals(Convert.FromHexString(userSaltedPass), incomePassSalted);
        }
    }
}
