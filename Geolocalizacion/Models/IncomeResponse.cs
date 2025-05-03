using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geolocalizacion.Models
{
    public class IncomeResponse
    {
        public int incomeId { get; set; }

        public string date { get; set; }

        public string time { get; set; }

        public Double latitude { get; set; }

        public Double longitude { get; set; }

    }
}
