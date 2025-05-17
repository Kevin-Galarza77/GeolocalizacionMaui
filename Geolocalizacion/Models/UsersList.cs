using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geolocalizacion.Models
{
    public class UsersList
    {
        public int userId { get; set; }
        public string username { get; set; }
        public bool status { get; set; }
        public string cardId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public int roleId { get; set; }
        public string roleName { get; set; }
    }
}
