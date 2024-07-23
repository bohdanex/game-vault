using GameVault.ObjectModel.DTOs;
using GameVault.ObjectModel.Entities;
using GameVault.ObjectModel.Models;
using GameVault.Repository.Abstraction;
using GameVault.Services.Abstraction;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using System.Text.Json;

namespace GameVault.Services
{
    public class SteamGamesService(ISteamGamesRepository _provider, HttpClient _httpClient, IConfiguration _configuration) : ISteamGamesService
    {
        public async Task AddNewGame(int steamGameId, double price)
        {
            SteamGame steamGame = new() { SteamAppId = steamGameId, OurPriceInUSD = price};
            await _provider.Add(steamGame);
        }

        public async Task<List<SteamGameDTO>> GetAll()
        {
            const int DelayBetweenRequestsMS = 5;
            string steamGamesApiURI = _configuration["APIs:Steam Games"]!;

            List<SteamGame> steamGames = await _provider.GetAll();
            List<SteamGameDTO> steamGameDTOs = new(steamGames.Count);
            JsonSerializerOptions jsonSerializerOptions = new() { PropertyNameCaseInsensitive = true };

            foreach (SteamGame game in steamGames) 
            {
                string fullUri = $"{steamGamesApiURI}/appdetails?appids={game.SteamAppId}";

                string responseString = await ((await _httpClient.GetAsync(fullUri)).Content).ReadAsStringAsync();
                int startIndex = responseString.IndexOf(':') + 1;
                string responseJson = responseString[startIndex..^1];

                SteamApiGameInfoResponse? responseObject = JsonSerializer.Deserialize<SteamApiGameInfoResponse>(responseJson, jsonSerializerOptions);

                if (responseObject is not null)
                {
                    SteamGameDTO steamGameDTO = new()
                    {
                        OurPrice = game.OurPriceInUSD,
                        SteamPrice = responseObject.Data.PriceOverview.Final,
                        Currency = responseObject.Data.PriceOverview.Currency,
                        SteamAppId = game.SteamAppId,
                        ImageURI = responseObject.Data.HeaderImage
                    };

                    steamGameDTOs.Add(steamGameDTO);
                }
                
                await Task.Delay(DelayBetweenRequestsMS);
            }

            return steamGameDTOs;
        }

        //public async Task<(double price, string currency)> GetPrice(int steamAppId)
        //{

        //}
    }
}
