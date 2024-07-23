using GameVault.ObjectModel.Entities;
using GameVault.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace GameVault.Repository.Implementation
{
    public class UserRepository(AppDbContext dbContext) : RepositoryBase<User>(dbContext), IUserRepository
    {
        public async Task<User?> GetByNameOrEmail(string nickname, string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(user => user.Nickname == nickname || user.Email == email);
        }

        public async Task Register(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}
