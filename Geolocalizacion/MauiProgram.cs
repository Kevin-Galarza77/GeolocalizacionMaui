using Geolocalizacion.Services;
using Geolocalizacion.ServicesImp;
using Geolocalizacion.Views;
using Microsoft.Extensions.Logging;

namespace Geolocalizacion
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("fa-solid-900.ttf", "FontAwesome");
                });

            builder.Services.AddSingleton<ILoginService, LoginService>();
            builder.Services.AddTransient<vLogin>();

            builder.UseMauiApp<App>().UseMauiMaps();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
