using Geolocalizacion.Models; 

namespace Geolocalizacion.Services
{
    public interface IRegistroService
    {
        Task<ApiResponse<Object>> RegistrarUsuario(UsuarioRegistro usuario);
    }
}
