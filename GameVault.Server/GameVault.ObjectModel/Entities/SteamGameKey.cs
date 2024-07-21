using System;

namespace GameVault.ObjectModel.Entities
{
    public class SteamGameKey : BaseEntity
    {
        public SteamGame SteamGame { get; set; }
        public User Owner { get; set; }

        public DateTime ExpireDate { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? BuyDate { get; set; }
    }
}
