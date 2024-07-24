using GameVault.ObjectModel.DTOs;
using GameVault.ObjectModel.Entities;
using GameVault.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace GameVault.Repository.Implementation
{
    public class SteamGamesRepository(AppDbContext _dbContext) : RepositoryBase<SteamGame>(_dbContext), ISteamGamesRepository
    {
        public async Task<SteamGame?> GetBySteamAppId(int steamAppId)
        {
            return await _dbContext.SteamGames.FirstOrDefaultAsync(game => game.SteamAppId == steamAppId);
        }

        public async Task<List<SteamGame>> GetBestSellers(int minBuyCount, int take)
        {
            return await _dbContext.SteamGameKeys
                .GroupBy(keys => keys.SteamGame)
                .Where(g => g.Count() >= minBuyCount)
                .OrderByDescending(g => g.Count())
                .Take(take)
                .Select(k => k.Key).ToListAsync();
        }
    }
}
