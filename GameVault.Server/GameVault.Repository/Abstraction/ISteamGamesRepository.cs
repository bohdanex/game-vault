using GameVault.ObjectModel.DTOs;
using GameVault.ObjectModel.Entities;

namespace GameVault.Repository.Abstraction
{
    public interface ISteamGamesRepository : IRepositoryBase<SteamGame>
    {
        Task<SteamGame?> GetBySteamAppId(int steamAppId);
    }
}
