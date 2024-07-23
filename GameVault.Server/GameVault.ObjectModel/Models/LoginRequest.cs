using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameVault.ObjectModel.Models
{
    public class LoginRequest
    {
        public string EmailOrNickname { get; set; }
        public string Password { get; set; }
    }
}
