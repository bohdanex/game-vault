using GameVault.ObjectModel.DTOs;
using GameVault.ObjectModel.Enums;

namespace GameVault.ObjectModel.Models
{
    public class RegistrationResponse
    {
        public UserDTO User { get; set; }
        public RegistrationInfo RegistrationInfo { get; set; }
    }
}
