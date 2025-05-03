namespace Geolocalizacion.Views;
using Geolocalizacion.Models;
public partial class vAttendanceHistory : ContentView
{
	public vAttendanceHistory()
	{
		InitializeComponent();
        dateInitPicker.Date = DateTime.Today.AddDays(-7);
        var registros = ObtenerRegistrosAsync();
        recordsView.ItemsSource = registros;
    }


    private List<Register> ObtenerRegistrosAsync()
    {
        return new List<Register>
        {
            new Register { Fecha = "2025-05-03", Hora = "08:00", Latitud = -0.123, Longitud = -78.456, IncomeId = true },
            new Register { Fecha = "2025-05-03", Hora = "17:00", Latitud = -0.123, Longitud = -78.456, IncomeId = false },
        };
    }


}