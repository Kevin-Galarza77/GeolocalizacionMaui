using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json; 
using Geolocalizacion.Models;
using Geolocalizacion.Services;

namespace Geolocalizacion.ServicesImp
{
    public class ExitService:IExitService
    {
        private readonly string url = UrlService.url + "exit";
        private readonly HttpClient client = new HttpClient();
        private readonly string bearerToken = Preferences.Get("token", "");

        public async Task<ApiResponse<Object>> createExit(IncomeExitData incomeExitData)
        {
            var json = JsonSerializer.Serialize(incomeExitData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", bearerToken);

            var response = await client.PostAsync(url, content);
            var responseBody = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var data = JsonSerializer.Deserialize<ApiResponse<Object>>(responseBody, options);

            return data;
        }
    }
}
