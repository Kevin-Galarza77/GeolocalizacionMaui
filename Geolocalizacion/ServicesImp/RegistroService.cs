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
        private string url = UrlService.url + "users";

        public async Task<ApiResponse<Object>> RegistrarUsuario(UsuarioRegistro usuario)
        {
            try
            {
                // Mostrar URL
                Console.WriteLine($"🌐 URL de solicitud: {url}");

                // Serializar el cuerpo JSON
                var json = JsonSerializer.Serialize(usuario);
                Console.WriteLine($"📦 Cuerpo JSON: {json}");

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                using var client = new HttpClient();
                var response = await client.PostAsync(url, content);

                // Leer la respuesta
                var responseBody = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(responseBody))
                {
                    Console.WriteLine("⚠️ El servidor no devolvió contenido.");
                    return new ApiResponse<object>
                    {
                        Status = false,
                        Alert = "El servidor no devolvió contenido.",
                        Data = null
                    };
                }

                Console.WriteLine($"📨 Respuesta del servidor: {responseBody}");

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var data = JsonSerializer.Deserialize<ApiResponse<object>>(responseBody, options);

                if (data == null)
                {
                    return new ApiResponse<object>
                    {
                        Status = false,
                        Alert = "Error al procesar la respuesta del servidor.",
                        Data = null
                    };
                }

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error en la solicitud: {ex.Message}");
                return new ApiResponse<object>
                {
                    Status = false,
                    Alert = "Error de conexión con el servidor.",
                    Data = null
                };
            }
        }
    }
}

