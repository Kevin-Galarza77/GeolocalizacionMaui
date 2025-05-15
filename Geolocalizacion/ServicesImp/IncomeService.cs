using Geolocalizacion.Models;
using Geolocalizacion.Services;
using System.Text;
using System.Text.Json;

namespace Geolocalizacion.ServicesImp
{
    public class IncomeService : IIncomeService
    {
        private readonly string url = UrlService.url + "income";
        private readonly HttpClient client = new HttpClient();
        private readonly string bearerToken = Preferences.Get("token", "");

        public async Task<ApiResponse<Object>> createIncome(IncomeExitData incomeExitData)
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

        public async Task<ApiResponse<List<IncomeResponse>>> getIncomeByRangeAndUser(string init, string end, int user)
        {
            string url_ = url + "/history/" + user + "?fecha_inicio=" + init + "&fecha_fin=" + end;
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", bearerToken);

            var response = await client.GetAsync(url_);
            var responseBody = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var data = JsonSerializer.Deserialize<ApiResponse<List<IncomeResponse>>>(responseBody, options);

            return data;
        }

        public string getUrl(){
            return url;
        }

        public async Task<ApiResponse<List<RangeIncome>>> getIncomeByRange(string init, string end)
        {
            var endpoint = $"{UrlService.url}income/historyRange?fecha_inicio={init}&fecha_fin={end}";

            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", bearerToken);

            var response = await client.GetAsync(endpoint);
            var responseBody = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var apiResponse = JsonSerializer.Deserialize<ApiResponse<List<RangeIncome>>>(responseBody, options);

            return apiResponse;
        }





    }
}
