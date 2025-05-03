using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geolocalizacion.Models
{
    public class LoginResponse
    {
        public int RoleId { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
        public string Username { get; set; }
    }
}
