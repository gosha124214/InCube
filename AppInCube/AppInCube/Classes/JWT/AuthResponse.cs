using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppInCube.Classes.JWT
{
    public class AuthResponse
    {
        public string Message { get; set; }
        public uint UserId { get; set; }
        public string Token { get; set; }
    }
}
