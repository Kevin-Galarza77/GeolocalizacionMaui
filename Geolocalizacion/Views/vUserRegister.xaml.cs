using Microsoft.Maui.Storage;
using Geolocalizacion.Models;
using Geolocalizacion.Services;
using Geolocalizacion.ServicesImp;
namespace Geolocalizacion.Views;
using System.Text.RegularExpressions;

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
            // Validaciones manuales
            List<string> errores = new List<string>();

            if (string.IsNullOrWhiteSpace(emailEntry.Text))
                errores.Add("El email es requerido");
            else if (!Regex.IsMatch(emailEntry.Text, @"^[A-Za-z0-9+_.-]+@[A-Za-z0-9.-]+$"))
                errores.Add("El formato del email es inv�lido");

            if (string.IsNullOrWhiteSpace(passwordEntry.Text))
                errores.Add("La contrase�a es requerida");
            else if (!Regex.IsMatch(passwordEntry.Text, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&]).{8,}$"))
                errores.Add("La contrase�a debe tener al menos 8 caracteres, incluyendo may�sculas, min�sculas, un n�mero y un car�cter especial");

            if (string.IsNullOrWhiteSpace(confirmPasswordEntry.Text))
                errores.Add("Debe confirmar la contrase�a");
            else if (passwordEntry.Text != confirmPasswordEntry.Text)
                errores.Add("Las contrase�as no coinciden");

            if (string.IsNullOrWhiteSpace(firstNameEntry.Text))
                errores.Add("El nombre es requerido");

            if (string.IsNullOrWhiteSpace(lastNameEntry.Text))
                errores.Add("El apellido es requerido");

            if (string.IsNullOrWhiteSpace(cardIdEntry.Text))
                errores.Add("El n�mero de c�dula es requerido");
            else if (!Regex.IsMatch(cardIdEntry.Text, @"^\d{10}$"))
                errores.Add("El n�mero de c�dula debe tener 10 d�gitos");

            if (string.IsNullOrWhiteSpace(phoneEntry.Text))
                errores.Add("El celular es requerido");
            else if (!Regex.IsMatch(phoneEntry.Text, @"^\d{10}$"))
                errores.Add("El celular debe tener 10 d�gitos");

            if (string.IsNullOrWhiteSpace(addressEntry.Text))
                errores.Add("La direcci�n es requerida");

            int rolId = rolePicker.SelectedItem switch
            {
                "Admin" => 1,
                "User" => 2,
                _ => 0
            };

            if (rolId == 0)
                errores.Add("Seleccione un rol v�lido");

            // Mostrar errores si existen
            if (errores.Count > 0)
            {
                string mensaje = string.Join("\n", errores);
                await DisplayAlert("Errores de validaci�n", mensaje, "OK");
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

            if (!result.Status)
            {
                var mensajeCompleto = result.Alert;

                if (result.Messages != null && result.Messages.Any())
                {
                    mensajeCompleto += "\n� " + string.Join("\n� ", result.Messages);
                }

                await DisplayAlert("Error", mensajeCompleto, "OK");
            }
            else
            {
                await DisplayAlert("�xito", "Usuario registrado correctamente", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Se produjo un error: {ex.Message}", "OK");
        }

    }
}