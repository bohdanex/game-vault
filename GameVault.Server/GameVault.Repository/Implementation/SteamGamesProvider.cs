using GameVault.ObjectModel.DTOs;
using GameVault.ObjectModel.Entities;
using GameVault.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace GameVault.Repository.Implementation
{
    public class SteamGamesProvider(AppDbContext _dbContext) : RepositoryBase<SteamGame>(_dbContext), ISteamGamesRepository
    {
        public async Task<SteamGame?> GetBySteamAppId(int steamAppId)
        {
            return await _dbContext.SteamGames.FirstOrDefaultAsync(game => game.SteamAppId == steamAppId);
        }
    }
}
