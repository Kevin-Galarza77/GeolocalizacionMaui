using Geolocalizacion.Models;
using Geolocalizacion.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Geolocalizacion.ServicesImp
{
    public class RegistroService : IRegistroService
    {
        private string url = UrlService.url + "api/users";

        public async Task<ApiResponse<Object>> RegistrarUsuario(UsuarioRegistro usuario)
        {

            var json = JsonSerializer.Serialize(usuario);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var client = new HttpClient();
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

