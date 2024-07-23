using GameVault.ObjectModel.Entities;
using GameVault.Repository.Abstraction;
using GameVault.Services.Abstraction;

namespace GameVault.Services
{
    public class GameKeyService(IGameKeyRepository _repository, ISteamGamesRepository _steamGameRepo) : IGameKeyService
    {
        public async Task<SteamGameKey?> CreateKey(int steamAppId, TimeSpan lifetime)
        {
            SteamGame? game = await _steamGameRepo.GetBySteamAppId(steamAppId);

            if(game is not null)
            {
                SteamGameKey key = new()
                {
                    SteamGame = game,
                    CreationDate = DateTime.Now,
                    ExpireDate = DateTime.Now.Add(lifetime),
                };

                await _repository.Add(key);
                return key;
            }

            return null;
        }
    }
}
