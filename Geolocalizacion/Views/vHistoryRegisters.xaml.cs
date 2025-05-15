namespace Geolocalizacion.Views
{
    using Geolocalizacion.ServicesImp;
    using Geolocalizacion.ViewModels;

    public partial class vHistoryRegisters : ContentView
    {
        private readonly IncomeViewModel viewModel;

        public vHistoryRegisters()
        {
            InitializeComponent();
            viewModel = new IncomeViewModel(new IncomeService(), new ExitService());
            BindingContext = viewModel;
        }

        private async void LoadRangeDataAsync(object sender, EventArgs e)
        {
            string fechaInicio = dpStartDate.Date.ToString("yyyy-MM-dd");
            string fechaFin = dpEndDate.Date.ToString("yyyy-MM-dd");
            await viewModel.LoadRange(fechaInicio, fechaFin);
        }


        private async void OnRangeClicked(object sender, EventArgs e) =>  LoadRangeDataAsync(sender, e);
        private async void OnSearchClicked(object sender, EventArgs e) =>  LoadRangeDataAsync(sender, e);

        private void OnSearchChanged(object sender, TextChangedEventArgs e)
        {
            viewModel.ApplyPagination(e.NewTextValue);
        }

        private void OnNextPageClicked(object sender, EventArgs e)
        {
            viewModel.NextPage();
        }

        private void OnPrevPageClicked(object sender, EventArgs e)
        {
            viewModel.PrevPage();
        }
    }
}
