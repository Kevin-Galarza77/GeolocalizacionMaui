using Geolocalizacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geolocalizacion.Services
{
    public interface IUsersList
    {
       public Task<ApiResponse<List<UsersList>>> ObtenerUsuarios();
    }
}
