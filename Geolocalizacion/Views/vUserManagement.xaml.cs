using Geolocalizacion.Services;
using Geolocalizacion.ServicesImp;
using Geolocalizacion.ViewModels;

namespace Geolocalizacion.Views;

public partial class vUserManagement : ContentView
{

    private readonly ViewUsers viewUsers;
    public vUserManagement()
    { 
        InitializeComponent();
        viewUsers = new ViewUsers(new GetUsersList());
        BindingContext = viewUsers;

    }

    private async void UsersData(object sender, EventArgs e)
    {
      await viewUsers.LoadUsers();
    }


    private async void OnRangeClicked(object sender, EventArgs e) => UsersData(sender, e);
    private async void OnSearchClicked(object sender, EventArgs e) => UsersData(sender, e);

    private void OnSearchChanged(object sender, TextChangedEventArgs e)
    {
        viewUsers.ApplyPagination(e.NewTextValue);
    }

    private void OnNextPageClicked(object sender, EventArgs e)
    {
        viewUsers.NextPage();
    }

    private void OnPrevPageClicked(object sender, EventArgs e)
    {
        viewUsers.PrevPage();
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new vUserRegister());
    }
}