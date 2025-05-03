using Geolocalizacion.Models;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;

namespace Geolocalizacion.Views;

public partial class DetailsRegister : ContentPage
{ 
	public DetailsRegister(Register register)
	{
		InitializeComponent();
		this.BindingContext = register;
        MarkPoint(register);
	}

	private void MarkPoint(Register register)
	{ 
        var position = new Location(register.Latitud, register.Longitud);
         
        var pin = new Pin
        {
            Label = "Ubicación",
            Location = position,
            Type = PinType.Place
        };

        map.Pins.Add(pin);
         
        map.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMeters(250)));
    }

}