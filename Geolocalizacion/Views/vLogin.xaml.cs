namespace Geolocalizacion.Views;

public partial class vLogin : ContentPage
{
	public vLogin()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        String password = passwordEntry.Text;
        String email = emailEntry.Text;

        if (await ValidateForm(email, password))
        {
            
        }

    }

    private async Task<Boolean> ValidateForm(string email, string password)
    {
        Boolean validate = true;

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

}