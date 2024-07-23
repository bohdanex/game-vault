using GameVault.ObjectModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameVault.Services.Abstraction
{
    public interface IGameKeyService
    {
        Task<SteamGameKey?> CreateKey(int steamAppId, TimeSpan lifetime);
    }
}
