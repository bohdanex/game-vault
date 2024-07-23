using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameVault.Services.Abstraction
{
    public interface IPasswordHasher
    {
        Task<string> GetHashedPassword(string password, byte[] salt);
        Task<bool> VerifyPassword(string userSaltedPass, string incomePassword, byte[] salt);
    }
}
