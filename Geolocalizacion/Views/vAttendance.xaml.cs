using Geolocalizacion.Models;
using Geolocalizacion.Services;
using Geolocalizacion.ServicesImp;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;

namespace Geolocalizacion.Views;

public partial class vAttendance : ContentView
{
     
    private readonly IIncomeService incomeService = new IncomeService();
    private readonly IExitService exitService= new ExitService();
    private ContentPage contentPage;
	
    public vAttendance(ContentPage contentPage)
	{
		InitializeComponent();
        showLocation();
        this.contentPage = contentPage;
    }

    private async void showLocation()
    {
        try
        { 
            var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            if (status != PermissionStatus.Granted)
            {
                alertLR.IsVisible = true;
                return;
            }
             
            var ubicacion = await Geolocation.GetLastKnownLocationAsync(); 

            if (ubicacion == null)
            {
                ubicacion = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Medium));
            }
               
            if (ubicacion != null)
            {
                var posicion = new Location(ubicacion.Latitude, ubicacion.Longitude);

                map.MoveToRegion(MapSpan.FromCenterAndRadius(posicion, Distance.FromKilometers(0.5)));
                 
                map.Pins.Add(new Pin
                {
                    Label = "Ubicación actual",
                    Location = posicion 
                });

                alertLR.IsVisible = false;
            }
        }
        catch (Exception ex)
        {
            alertLR.IsVisible = true;
            Console.WriteLine(ex.Message);
        }
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var data = await getLocationAndUser();
        create(data, incomeService.createIncome(data));
    }

    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        var data = await getLocationAndUser();
        create(data, exitService.createExit(data));
    }

    private async void create(IncomeExitData data,Task<ApiResponse<Object>> apiTask)
    {
        if (data != null)
        {
            try
            {
                var response = await apiTask;

                if (response != null)
                {
                    if (response.Status)
                    {
                        await contentPage.DisplayAlert("Éxito", response.Alert, "OK");
                    }
                    else
                    {
                        string sms = "";

                        foreach (var messege in response.Messages)
                        {
                            sms += messege + "\n";
                        }

                        if (sms == "") sms = response.Alert;

                        await contentPage.DisplayAlert("Advertencia", sms, "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                await contentPage.DisplayAlert("Error", "Se produjo un error!", "OK");
                Console.WriteLine(ex.Message);
            }
        }
    }

    public async Task<IncomeExitData> getLocationAndUser()
    {
        try
        {
            var location = await Geolocation.GetLastKnownLocationAsync();

            if (location == null)
            {
                location = await Geolocation.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Medium,
                    Timeout = TimeSpan.FromSeconds(10)
                });
            }

            if (location != null)
            {
                Preferences.Get("userId", 0);

                return new IncomeExitData(Preferences.Get("userId", 0), location.Latitude, location.Longitude) ;
            }

            return null;
        }
        catch (Exception ex)
        {
            alertLR.IsVisible = true;
            Console.WriteLine(ex.Message);
            return null;
        }
    }

}