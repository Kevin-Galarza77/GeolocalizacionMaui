using Geolocalizacion.Models;

namespace Geolocalizacion.Services
{
    public interface ILoginService
    {
        public Task<ApiResponse<LoginResponse>> Login(LoginData loginData);
    }
}
