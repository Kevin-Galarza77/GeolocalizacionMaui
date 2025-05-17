using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geolocalizacion.Models
{
    public class UsuarioRegistro
    {
        public string email { get; set; }
        public string password { get; set; }
        public bool status { get; set; }
        public int rol { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string card_id { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
    }
}
