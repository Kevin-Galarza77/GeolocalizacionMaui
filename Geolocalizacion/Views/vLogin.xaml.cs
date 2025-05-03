using Geolocalizacion.Models;
using Geolocalizacion.Services;
using Microsoft.Maui.Controls;

namespace Geolocalizacion.Views;

public partial class vLogin : ContentPage
{

    private readonly ILoginService loginService;

	public vLogin(ILoginService loginService)
	{
        this.loginService = loginService;
		InitializeComponent();
        //VerifyLogin();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        String password = passwordEntry.Text;
        String email = emailEntry.Text;

        if (await ValidateForm(email, password))
        {
            try
            {
                var response = await loginService.Login(new LoginData(email, password));

                if (response != null && response.Status)
                {
                    Preferences.Set("token", response.Data.Token);
                    Preferences.Set("username", response.Data.Username);
                    Preferences.Set("roleId", response.Data.RoleId);
                    Preferences.Set("userId", response.Data.UserId);

                    await Shell.Current.GoToAsync("//HomePage");

                }
                else
                {
                    await AnimateError(responseLR);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message + "  >>> " + loginService.getUrl(), "OK");
                Console.WriteLine(ex.Message);
            }

        } 
    }

    private async Task<Boolean> ValidateForm(string email, string password)
    {
        Boolean validate = true;

        responseLR.IsVisible = false;

        if (string.IsNullOrWhiteSpace(email) || IsValidEmail(email))
        {
            emailEntry.Focus();
            await AnimateError(emailLR);
            validate = false;
        }
        else
        {
            emailLR.IsVisible = false;
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            passwordEntry.Focus();
            await AnimateError(passwordLR);
            validate = false;
        }
        else
        {
            passwordLR.IsVisible = false;
        }

        return validate;

    }

    private Boolean IsValidEmail(string email)
    {
        return !System.Text.RegularExpressions.Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
    }

    private async Task AnimateError(Label label)
    {
        label.Opacity = 0;
        label.TranslationY = -10;
        label.IsVisible = true;
        await Task.WhenAll(
            label.FadeTo(1, 250),
            label.TranslateTo(0, 0, 250, Easing.CubicOut)
        );
    }


    private async void VerifyLogin()
    {
        String token = Preferences.Get("token", string.Empty);
        String username = Preferences.Get("username", string.Empty);
        String roleId = Preferences.Get("roleId", string.Empty);
        String userId = Preferences.Get("userId", string.Empty);


        if (!string.IsNullOrEmpty(token) && !string.IsNullOrEmpty(username) 
            && !string.IsNullOrEmpty(roleId) && !string.IsNullOrEmpty(userId))
        {
            await Shell.Current.GoToAsync("//HomePage");
        }

    }
 
}