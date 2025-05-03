using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geolocalizacion.Models
{
    public class LoginData
    { 
        public string email { get; set; }
        public string password { get; set; }

        public LoginData(string email, string password)
        {
            this.email = email;
            this.password = password;
        }
    }
}
