using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geolocalizacion.Models;

namespace Geolocalizacion.Services
{
    public interface IExitService
    {
        public Task<ApiResponse<Object>> createExit(IncomeExitData incomeExitData);
    }
}
