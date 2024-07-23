using GameVault.ObjectModel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameVault.ObjectModel.Models
{
    public class RegistrationRequest
    {
        public string Nickname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Role? Role{ get; set; }
    }
}
