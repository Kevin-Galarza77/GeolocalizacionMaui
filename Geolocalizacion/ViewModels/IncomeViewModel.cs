using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Geolocalizacion.Models;
using Geolocalizacion.Services;

namespace Geolocalizacion.ViewModels
{
    public class IncomeViewModel : INotifyPropertyChanged
    {
        private readonly IIncomeService _incomeService;
        private readonly IExitService _exitService;
        private int currentPage = 1;
        private int pageSize = 10;
        private List<Ranges> allData = new();


        public ObservableCollection<Ranges> PagedData { get; set; } = new();
        public string PageInfo => $"Página {currentPage}";

        public IncomeViewModel(IIncomeService incomeService, IExitService exitService)
        {
            _incomeService = incomeService;
            _exitService = exitService;
        }

        public async Task LoadRange(string startDate, string endDate)
        {
            try
            {

                var incomeResponse = await _incomeService.getIncomeByRange(startDate, endDate);
                var exitResponse = await _exitService.getExitByRange(startDate, endDate);

                List<Ranges> combined = new();

                if (incomeResponse?.Status == true)
                {
                    foreach (var item in incomeResponse.Data)
                    {
                        combined.Add(new Ranges(
                            item.date, item.time, item.latitude, item.longitude,
                            true, item.firstName, item.lastName, item.cardId
                        ));
                    }
                }

                if (exitResponse?.Status == true)
                {
                    foreach (var item in exitResponse.Data)
                    {
                        combined.Add(new Ranges(
                            item.date, item.time, item.latitude, item.longitude,
                            false, item.firstName, item.lastName, item.cardId
                        ));
                    }
                }

                allData = combined.OrderByDescending(x => x.DateTime).ToList();
                currentPage = 1;
                ApplyPagination();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error cargando registros: " + ex.Message);
            }
        }


        public void ApplyPagination(string? filter = null)
        {
            var filtered = allData;

            if (!string.IsNullOrWhiteSpace(filter))
            {
                filtered = filtered.Where(x =>
                    (x.firstName?.Contains(filter, StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (x.lastName?.Contains(filter, StringComparison.OrdinalIgnoreCase) ?? false)
                ).ToList();
            }

            var paged = filtered
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            PagedData.Clear();
            foreach (var item in paged)
                PagedData.Add(item);

            OnPropertyChanged(nameof(PageInfo));
        }

        public void NextPage()
        {
            if ((currentPage * pageSize) < allData.Count)
            {
                currentPage++;
                ApplyPagination();
            }
        }

        public void PrevPage()
        {
            if (currentPage > 1)
            {
                currentPage--;
                ApplyPagination();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
