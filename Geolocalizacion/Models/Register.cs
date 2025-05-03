
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geolocalizacion.Models
{
    public class Register
    {
        public string Fecha { get; set; }
        public string Hora { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public string Tipo => IncomeId ? "INGRESO" : "SALIDA";
        public Color TipoColor => IncomeId ? Colors.Green : Colors.Red;
        public bool IncomeId { get; set; }
    }
}
