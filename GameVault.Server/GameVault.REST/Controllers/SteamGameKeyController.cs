using GameVault.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace GameVault.REST.Controllers
{
    [Route("[controller]")]
    public class SteamGameKeyController(IGameKeyService _gameKeyService) : ControllerBase
    {
        [HttpPost]
        [Route("[action]/{steamAppId}/{lifetimeDays}")]
        public async Task<IActionResult> CreateKey(int steamAppId, int lifetimeDays)
        {
            await _gameKeyService.CreateKey(steamAppId, TimeSpan.FromDays(lifetimeDays));
            return Created();
        }
    }
}
