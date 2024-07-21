using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameVault.ObjectModel.Entities
{
    public class User : BaseEntity
    {
        #region Auth
        [Required, MaxLength(30)] public string Nickname { get; set; }
        [Required, MaxLength(100)] public string Email { get; set; }
        [Required] public string SaltedPassword { get; set; }
        [Required] public string Salt { get; set; }
        #endregion

        public ICollection<SteamGameKey> BoughtKeys { get; set; }
    }
}
