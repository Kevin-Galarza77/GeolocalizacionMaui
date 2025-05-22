using Geolocalizacion.Models;
using Geolocalizacion.Services;
using System.Text;
using System.Text.Json;

namespace Geolocalizacion.ServicesImp
{
    public class RegistroService : IRegistroService
    {
        private string url = UrlService.url + "users";
        private readonly HttpClient client = new HttpClient();
        private readonly string bearerToken = Preferences.Get("token", "");


        public async Task<RegisterResponse> RegistrarUsuario(UsuarioRegistro usuario)
        {
            try
            {
                // Mostrar la URL de la solicitud
                Console.WriteLine($"🌐 URL de solicitud: {url}");

                // Verificar si el token está vacío
                if (string.IsNullOrEmpty(bearerToken))
                {
                    Console.WriteLine("⚠️ Token vacío o no encontrado.");
                    return new RegisterResponse
                    {
                        Status = false,
                        Alert = "Token vacío o no encontrado.",
                        Messages = new List<string>()
                    };
                }

                // Establecer el encabezado de autorización con el token
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", bearerToken);

                // Serializar el cuerpo JSON
                var json = JsonSerializer.Serialize(usuario);
                Console.WriteLine($"📦 Cuerpo JSON: {json}");

                // Crear el contenido de la solicitud
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(url, content);

                // Verificar si la respuesta fue exitosa
                if (!response.IsSuccessStatusCode)
                {
                    // Si la respuesta no es exitosa, registrar el error
                    Console.WriteLine($"❌ Error en la solicitud: {response.StatusCode}");
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Detalles del error: {errorContent}");

                    return new RegisterResponse
                    {
                        Status = false,
                        Alert = $"Error en la solicitud: {response.StatusCode}",
                        Messages = new List<string> { errorContent }
                    };
                }

                // Leer la respuesta del servidor
                var responseBody = await response.Content.ReadAsStringAsync();

                // Verificar si la respuesta está vacía
                if (string.IsNullOrWhiteSpace(responseBody))
                {
                    Console.WriteLine("⚠️ El servidor no devolvió contenido.");
                    return new RegisterResponse
                    {
                        Status = false,
                        Alert = "El servidor no devolvió contenido.",
                        Messages = new List<string>()
                    };
                }

                // Mostrar la respuesta del servidor
                Console.WriteLine($"📨 Respuesta del servidor: {responseBody}");

                // Configuración de opciones para deserialización (caso de propiedad sin importar mayúsculas/minúsculas)
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                // Deserializar la respuesta del servidor a un objeto RegisterResponse
                var data = JsonSerializer.Deserialize<RegisterResponse>(responseBody, options);

                // Si la deserialización falla, retornar un mensaje de error
                return data ?? new RegisterResponse
                {
                    Status = false,
                    Alert = "Error al procesar la respuesta del servidor.",
                    Messages = new List<string>()
                };
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción durante la solicitud
                Console.WriteLine($"❌ Error en la solicitud: {ex.Message}");
                return new RegisterResponse
                {
                    Status = false,
                    Alert = "Error de conexión con el servidor.",
                    Messages = new List<string> { ex.Message }
                };
            }
        }
    }
}

