namespace Geolocalizacion.Views;

public partial class vHome : ContentPage
{

    private bool isMenuVisible = false;

    public vHome()
	{
		InitializeComponent();
        MainContent.Content = new vHistoryRegisters();
    }

    private async void OnMenuTapped(object sender, EventArgs e)
    {
        if (!isMenuVisible)
        {
            SideMenu.IsVisible = true;
            SideMenu.TranslationX = -250;
            await SideMenu.TranslateTo(0, 0, 250, Easing.SinOut);
            isMenuVisible = true;
        }
        else
        {
            await SideMenu.TranslateTo(-250, 0, 250, Easing.SinIn);
            SideMenu.IsVisible = false;
            isMenuVisible = false;
        }
    }

    private void OnAttendanceTapped(object sender, EventArgs e)
    { 
        MainContent.Content= new vAttendance(this);
        OnMenuTapped(sender, e);
    }

    private void OnAttendanceHistoryTapped(object sender, EventArgs e)
    {
        MainContent.Content = new vAttendanceHistory();
        OnMenuTapped(sender, e);
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        OnMenuTapped(sender, e);
        await Shell.Current.GoToAsync("//LoginPage");
    }

    private void TapGestureRecognizer_Tapped_1(object sender, TappedEventArgs e)
    {
        MainContent.Content = new vHistoryRegisters();
        OnMenuTapped(sender, e);
    }

    private void TapGestureRecognizer_Tapped_2(object sender, TappedEventArgs e)
    {
        MainContent.Content = new vUserManagement();
        OnMenuTapped(sender, e);
    }

}