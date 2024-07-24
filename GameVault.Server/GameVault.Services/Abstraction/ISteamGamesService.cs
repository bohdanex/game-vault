using GameVault.ObjectModel.DTOs;
using GameVault.ObjectModel.Entities;

namespace GameVault.Services.Abstraction
{
    public interface ISteamGamesService
    {
        Task AddNewGame(int steamGameId, double price);
        Task<List<SteamGameDTO>> GetAll();
        Task<List<SteamGameDTO>> GetBestSellers(int minBuyCount, int take);
    }
}
