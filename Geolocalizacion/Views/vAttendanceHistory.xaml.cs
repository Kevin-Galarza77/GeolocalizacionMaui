namespace Geolocalizacion.Views;
using Geolocalizacion.Models;
using Geolocalizacion.Services;
using Geolocalizacion.ServicesImp;
using Microsoft.Maui.Controls; 

public partial class vAttendanceHistory : ContentView
{

    private readonly IIncomeService incomeService = new IncomeService();
    private readonly IExitService exitService = new ExitService();

    public vAttendanceHistory()
	{
		InitializeComponent();
        dateInitPicker.Date = DateTime.Today.AddDays(-7);
        getRegistersIncome();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        if (dateInitPicker.Date > dateEndPicker.Date)
        {
            alertLR.IsVisible = true;
        }
        else
        {
            alertLR.IsVisible = false;
            getRegistersIncome();
        }
    }
    private async void getRegistersIncome()
    {
        try
        {
            int user = Preferences.Get("userId", 0);
            string init = dateInitPicker.Date.ToString("yyyy-MM-dd");
            string end = dateEndPicker.Date.ToString("yyyy-MM-dd");

            var response = await incomeService.getIncomeByRangeAndUser(init, end, user);

            if (response != null)
            {
                List<Register> listRegister = new List<Register>();
                
                if (response.Status)
                {
                    foreach (var item in response.Data)
                    {
                        listRegister.Add(new Register(item.date, item.time, item.latitude, item.longitude, true));
                    }
                }

                getRegistersExit(listRegister);
            }
        }
        catch (Exception ex)
        { 
            Console.WriteLine(ex.Message);
        }
    }


    private async void getRegistersExit(List<Register> listRegister)
    {
        try
        {
            int user = Preferences.Get("userId", 0);
            string init = dateInitPicker.Date.ToString("yyyy-MM-dd");
            string end = dateEndPicker.Date.ToString("yyyy-MM-dd");

            var response = await exitService.getExitByRangeAndUser(init, end, user);

            if (response != null)
            {
                if (response.Status)
                { 
                    foreach (var item in response.Data)
                    {
                        listRegister.Add(new Register(item.date, item.time, item.latitude, item.longitude, false));
                    }

                    recordsView.ItemsSource = listRegister.OrderByDescending(r => r.DateTime).ToList();

                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        var button = sender as Button;
        if (button?.CommandParameter is Register selectedItem)
        {
            await Navigation.PushAsync(new DetailsRegister(selectedItem));
        } 
    }
}