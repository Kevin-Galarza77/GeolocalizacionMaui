using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geolocalizacion.Models
{
    public class RegisterResponse
    {
        public string Alert { get; set; }
        public List<string> Messages { get; set; }
        public bool Status { get; set; }
    }
}
