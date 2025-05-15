using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Geolocalizacion.Models
{
    public class Ranges
    {
        public string date { get; set; }
        public string time { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string cardId { get; set; }
        public string Tipo => incomeId ? "ingreso" : "salida";
        public Color TipoColor => incomeId ? Colors.Green : Colors.Red;
        public bool incomeId { get; set; }
        public DateTime DateTime { get; set; }

        public Ranges(string _date, string _time, double _latitude, double _longitude, bool _incomeId, string _firstName, string _lastName, string _cardId)
        {
            date = _date;
            time = _time;
            latitude = _latitude;
            longitude = _longitude;
            incomeId = _incomeId;

            firstName = _firstName;
            lastName = _lastName;
            cardId = _cardId;

            DateTime = DateTime.ParseExact($"{date} {time}", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
        }
    }
}
