using System.Text.Json.Serialization;

namespace GameVault.ObjectModel.Models
{
    public class SteamApiGameInfoResponse
    {
        public bool Success { get; set; }
        public GameData Data { get; set; }

        public class GameData
        {
            public string Name { get; set; }
            public bool IsFree { get; set; }
            [JsonPropertyName("header_image")]
            public string HeaderImage { get; set; }
            [JsonPropertyName("price_overview")]
            public PriceData PriceOverview { get; set; }
        }

        public class PriceData
        {
            public string Currency { get; set; }
            public double Initial { get; set; }
            public double Final { get; set; }
        }
    }
}
