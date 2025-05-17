using Microsoft.Maui.Storage;
using Geolocalizacion.Models;
using Geolocalizacion.Services;
using Geolocalizacion.ServicesImp;
namespace Geolocalizacion.Views;

public partial class vUserRegister : ContentPage
{
    private readonly IRegistroService registroService = new RegistroService();
    public vUserRegister()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var usuario = new Models.UsuarioRegistro
        {
            email = emailEntry.Text,
            password = passwordEntry.Text,
            status = statusSwitch.IsToggled,
            rol = int.Parse(rolePicker.SelectedItem.ToString()),
            first_name = firstNameEntry.Text,
            last_name = lastNameEntry.Text,
            card_id = cardIdEntry.Text,
            phone = phoneEntry.Text,
            address = addressEntry.Text
        };

        var result = await registroService.RegistrarUsuario(usuario);

        if (result.Status)
        {
            await DisplayAlert("Éxito", "Usuario registrado correctamente", "OK");
            Shell.Current.GoToAsync(".."); // o "//LoginPage"
        }
        else
        {
            await DisplayAlert("Error", "Error desconocido", "OK");
        }

    }
}