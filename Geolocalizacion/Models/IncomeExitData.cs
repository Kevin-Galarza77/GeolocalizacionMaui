using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geolocalizacion.Models
{
    public class IncomeExitData
    {
        public int person { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }

        public IncomeExitData(int person, double latitude, double longitude)
        {
            this.person = person;
            this.latitude = latitude;
            this.longitude = longitude;
        }

        public IncomeExitData() { }

    }
}
