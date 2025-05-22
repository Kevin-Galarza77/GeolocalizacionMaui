using Geolocalizacion.Models; 

namespace Geolocalizacion.Services
{
    public interface IRegistroService
    {
        Task<RegisterResponse> RegistrarUsuario(UsuarioRegistro usuario);
    }
}
