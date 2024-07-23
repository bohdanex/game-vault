using GameVault.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace GameVault.REST.Controllers
{
    public class SteamGameKeyController(IGameKeyService _gameKeyService) : ControllerBase
    {
        [HttpPost]
        [Route("/{steamAppId}/{lifetimeDays}")]
        public async Task<IActionResult> CreateKey(int steamAppId, int lifetimeDays)
        {
            await _gameKeyService.CreateKey(steamAppId, TimeSpan.FromDays(lifetimeDays));
            return Created();
        }
    }
}
