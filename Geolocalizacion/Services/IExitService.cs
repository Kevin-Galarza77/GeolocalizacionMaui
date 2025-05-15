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
        public Task<ApiResponse<List<ExitResponse>>> getExitByRangeAndUser(String init, string end, int user);

        public Task<ApiResponse<List<RangeExit>>> getExitByRange(string init, string end);

    }
}
