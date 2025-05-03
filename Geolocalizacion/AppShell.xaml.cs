using Geolocalizacion.Views;

namespace Geolocalizacion
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            RegisterRoutes();
        }

        private void RegisterRoutes()
        {
            Routing.RegisterRoute("HomePage", typeof(vHome));
            Routing.RegisterRoute("AsistenciaPage", typeof(vAttendance));
            Routing.RegisterRoute("HistorialAsistenciaPage", typeof(vAttendanceHistory));
        }

    }
}
