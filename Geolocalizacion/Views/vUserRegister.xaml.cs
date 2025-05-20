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
        try
        {
            // Mapear nombre del rol a su ID
            int rolId = rolePicker.SelectedItem switch
            {
                "Admin" => 1,
                "User" => 2,
                _ => 0
            };

            if (rolId == 0)
            {
                await DisplayAlert("Error", "Seleccione un rol válido", "OK");
                return;
            }

            var usuario = new Models.UsuarioRegistro
            {
                email = emailEntry.Text,
                password = passwordEntry.Text,
                status = statusSwitch.IsToggled,
                rol = rolId,
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
                await Shell.Current.GoToAsync(".."); // o "//LoginPage"
            }
            else
            {
                await DisplayAlert("Error", result.Alert ?? "Error desconocido", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Se produjo un error: {ex.Message}", "OK");
        }
    }
}