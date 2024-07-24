using GameVault.ObjectModel.DTOs;
using GameVault.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameVault.REST.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SteamGamesController(ISteamGamesService _steamGamesService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AddGame([FromQuery] int steamGameId, [FromQuery] double price)
        {
            if(steamGameId is 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            await _steamGamesService.AddNewGame(steamGameId, price);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpGet]
        [Authorize]
        [Route("get-all")]
        public async Task<List<SteamGameDTO>> GetSteamGames()
        {
            return await _steamGamesService.GetAll();
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("best/{minCount:int}/{take:int:max(20)}")]
        public async Task<List<SteamGameDTO>> GetBestSellers(int minCount, int take)
        {
            return await _steamGamesService.GetBestSellers(minCount, take);
        }
    }
}
