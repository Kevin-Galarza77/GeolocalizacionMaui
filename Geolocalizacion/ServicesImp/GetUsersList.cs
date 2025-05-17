using Geolocalizacion.Models;
using Geolocalizacion.Services;
using System.Diagnostics;
using System.Text;
using System.Text.Json;


namespace Geolocalizacion.ServicesImp
{
    public class GetUsersList : IUsersList
    {
        private readonly string url = UrlService.url + "users";
        private readonly HttpClient client = new HttpClient();
        private readonly string bearerToken = Preferences.Get("token", "");

        public async Task<ApiResponse<List<UsersList>>> ObtenerUsuarios()
        {
            string url_ = url + "/all";
            Debug.WriteLine("Llamando a: " + url_);
            Console.WriteLine("Token usado: [" + bearerToken + "]");
            //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", bearerToken);

            var response = await client.GetAsync(url_);
            Console.WriteLine($"StatusCode: {response.StatusCode}");
            var responseBody = await response.Content.ReadAsStringAsync();

            Console.WriteLine("✅ Respuesta del API:");
            Console.WriteLine(responseBody); // 🔍 AQUÍ VES EL JSON REAL

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var data = JsonSerializer.Deserialize<ApiResponse<List<UsersList>>>(responseBody, options);

            return data;
        }
    
    }
}
