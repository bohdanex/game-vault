namespace GameVault.ObjectModel.Entities
{
    public class SteamGame : BaseEntity
    {
        public int SteamAppId { get; set; }
        public double OurPriceInUSD { get; set; } 
    }
}
