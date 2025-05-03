using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geolocalizacion.Models;

namespace Geolocalizacion.Services
{
    public interface IIncomeService
    {
        public Task<ApiResponse<Object>> createIncome(IncomeExitData incomeExitData);
        public Task<ApiResponse<List<IncomeResponse>>> getIncomeByRangeAndUser(String init, string end, int user);
        public string getUrl();
    }
}
