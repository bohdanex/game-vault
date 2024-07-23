using Microsoft.EntityFrameworkCore;

namespace GameVault.ObjectModel.Entities
{
    [Index(nameof(SteamAppId))]
    public class SteamGame : BaseEntity
    {
        public int SteamAppId { get; set; }
        public double OurPriceInUSD { get; set; } 
    }
}
