using GameVault.ObjectModel.Enums;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameVault.ObjectModel.Entities
{
    [Index(nameof(Nickname), nameof(Email), IsUnique = true)]
    public class User : BaseEntity
    {
        #region Auth
        [Required, MaxLength(30)] public string Nickname { get; set; }
        [Required, MaxLength(100)] public string Email { get; set; }
        [Required] public string SaltedPassword { get; set; }
        [Required] public string Salt { get; set; }
        public Role Role { get; set; }
        #endregion

        public ICollection<SteamGameKey> BoughtKeys { get; set; }
    }
}
