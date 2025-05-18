
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geolocalizacion.Models
{
    public class Register
    {
        public DateTime DateTime { get; set; }
        public string Fecha { get; set; }
        public string Hora { get; set; }
        public string FullName { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public string Tipo => IncomeId ? "INGRESO" : "SALIDA";
        public Color TipoColor => IncomeId ? Colors.Green : Colors.Red;
        public bool IncomeId { get; set; }

        public Register(string fecha, string hora, double latitud, double longitud, bool incomeId, string fullName)
        {
            DateTime = DateTime.ParseExact(fecha + " " + hora, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            Fecha = fecha;
            Hora = hora;
            Latitud = latitud;
            Longitud = longitud;
            IncomeId = incomeId;
            FullName = fullName;
        }

        public Register() { }

    }
}
