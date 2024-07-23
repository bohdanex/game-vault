using GameVault.ObjectModel.Entities;
using GameVault.Repository.Abstraction;

namespace GameVault.Repository.Implementation
{
    public class GameKeyRepository(AppDbContext _dbContext) : RepositoryBase<SteamGameKey>(_dbContext), IGameKeyRepository
    {
    }
}
