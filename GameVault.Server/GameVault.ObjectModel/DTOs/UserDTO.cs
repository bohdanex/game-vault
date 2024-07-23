using GameVault.ObjectModel.Entities;
using GameVault.ObjectModel.Enums;
using System.Collections.Generic;

namespace GameVault.ObjectModel.DTOs
{
    public class UserDTO(User user)
    {
        public string Nickname { get; set; } = user.Nickname;
        public string Email { get; set; } = user.Email;
        public ICollection<SteamGameKey> BoughtKeys { get; set; } = user.BoughtKeys;
        public Role Role { get; set; } = user.Role;
        public string JWT { get; set; }
    }
}
