using Geolocalizacion.Models;
using Geolocalizacion.Services;

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

        Preferences.Set("token", "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJqaW1lbmV6a2V2MTA0MEBnbWFpbC5jb20iLCJuYmYiOjE3NDYyMzk5ODQsImlzcyI6IktFQVgtQkFDS0VORCIsImV4cCI6MTc0NjI4MzE4NCwiaWF0IjoxNzQ2MjM5OTg0LCJhdXRob3JpdGllcyI6IkNSRUFURSxERUxFVEUsUkVBRCxST0xFX0FETUlOLFVQREFURSIsImp0aSI6ImQ1YjMyMjcxLWE3NDQtNDk3ZS1hZDE4LTIzZWU5NTkwMTc3NyJ9.7mQEcOIa0XUj21w0_JepzqhXU9vdCjV9gdcUn9FfLOw");
        Preferences.Set("username", "jimenezkev1040@gmail.com");
        Preferences.Set("roleId", 1);
        Preferences.Set("userId", 1);

        //Preferences.Set("token", response.Data.Token);
        //Preferences.Set("username", response.Data.Username);
        //Preferences.Set("roleId", response.Data.RoleId);
        //Preferences.Set("userId", response.Data.UserId);
         

        await Shell.Current.GoToAsync("//HomePage");

        /*

        String password = passwordEntry.Text;
        String email = emailEntry.Text;

        if (await ValidateForm(email, password))
        {
            var response = await loginService.Login(new LoginData(email, password));

            if (response != null && response.Status)
            {

                Preferences.Set("token", "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJqaW1lbmV6a2V2MTA0MEBnbWFpbC5jb20iLCJuYmYiOjE3NDYyMzk5ODQsImlzcyI6IktFQVgtQkFDS0VORCIsImV4cCI6MTc0NjI4MzE4NCwiaWF0IjoxNzQ2MjM5OTg0LCJhdXRob3JpdGllcyI6IkNSRUFURSxERUxFVEUsUkVBRCxST0xFX0FETUlOLFVQREFURSIsImp0aSI6ImQ1YjMyMjcxLWE3NDQtNDk3ZS1hZDE4LTIzZWU5NTkwMTc3NyJ9.7mQEcOIa0XUj21w0_JepzqhXU9vdCjV9gdcUn9FfLOw");
                Preferences.Set("username", "jimenezkev1040@gmail.com");
                Preferences.Set("roleId", 1);
                Preferences.Set("userId", 1);

                //Preferences.Set("token", response.Data.Token);
                //Preferences.Set("username", response.Data.Username);
                //Preferences.Set("roleId", response.Data.RoleId);
                //Preferences.Set("userId", response.Data.UserId);

                Shell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;
                RegisterRoutes();

                await Shell.Current.GoToAsync("//HomePage");

            }
            else
            {
                await AnimateError(responseLR);
            }

        }
        */
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