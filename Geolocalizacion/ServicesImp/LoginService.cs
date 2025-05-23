﻿using Geolocalizacion.Models;
using Geolocalizacion.Services;
using System.Text;
using System.Text.Json; 

namespace Geolocalizacion.ServicesImp
{
    public class LoginService: ILoginService
    {
        private string url =  UrlService.url + "auth/login";

        public async Task<ApiResponse<LoginResponse>> Login(LoginData loginData)
        {
            var json = JsonSerializer.Serialize(loginData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var client = new HttpClient();
            var response = await client.PostAsync(url, content);
            var responseBody = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var data = JsonSerializer.Deserialize<ApiResponse<LoginResponse>>(responseBody, options);

            return data;
        }

        public string getUrl()
        {
            return url;
        }

    }
}
