using GameVault.ObjectModel.DTOs;
using GameVault.ObjectModel.Entities;
using GameVault.ObjectModel.Models;
using GameVault.Repository.Abstraction;
using GameVault.Services.Abstraction;
using GameVault.Services.Extensions;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace GameVault.Services
{
    public class SteamGamesService(
        ISteamGamesRepository _provider, 
        HttpClient _httpClient, 
        IConfiguration _configuration,
        IDistributedCache _distributedCache) : ISteamGamesService
    {
        public async Task AddNewGame(int steamGameId, double price)
        {
            SteamGame steamGame = new() { SteamAppId = steamGameId, OurPriceInUSD = price};
            await _provider.Add(steamGame);
        }

        private async Task<List<SteamGameDTO>> GetFromSteamApiAndDeserealize(List<SteamGame> games)
        {
            const int DelayBetweenRequestsMS = 5;
            string steamGamesApiURI = _configuration["APIs:Steam Games"]!;

            List<SteamGameDTO> steamGameDTOs = new(games.Count);
            JsonSerializerOptions jsonSerializerOptions = new() { PropertyNameCaseInsensitive = true };

            foreach (SteamGame game in games)
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
                        Name = responseObject.Data.Name,
                        Description = responseObject.Data.Description,
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

        public async Task<List<SteamGameDTO>> GetAll()
            => await GetFromSteamApiAndDeserealize(await _provider.GetAll());

        public async Task<List<SteamGameDTO>> GetBestSellers(int minBuyCount, int take)
        {
            CacheKey bestSellersCacheKey = CacheKeys.BestSellers;
            string? bestSellersJson = await _distributedCache.GetStringAsync(bestSellersCacheKey.Name);
            List<SteamGameDTO>? bestSellers = [];

            if (!string.IsNullOrEmpty(bestSellersJson))
            {
                using MemoryStream stream = new(bestSellersJson.GetBytesUTF8());
                bestSellers = await JsonSerializer.DeserializeAsync<List<SteamGameDTO>>(stream);
            }
            else
            {
                bestSellers = await GetFromSteamApiAndDeserealize(await _provider.GetBestSellers(minBuyCount, take));
                await _distributedCache.SetStringAsync(bestSellersCacheKey.Name, JsonSerializer.Serialize(bestSellers), bestSellersCacheKey.Options);
            }
            
            return bestSellers ?? [];
        }
    }
}
