using Geolocalizacion.Models;
using Geolocalizacion.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Geolocalizacion.ViewModels
{
    public class ViewUsers
    {
        private readonly IUsersList _usersList;
        private int currentPage = 1;
        private int pageSize = 10;
        private List<UsersList> allData = new();


        public ObservableCollection<UsersList> PagedData { get; set; } = new();
        public string PageInfo => $"Página {currentPage}";

        public ViewUsers(IUsersList usersList)
        {
            _usersList = usersList;
        }

        public async Task LoadUsers()
        {
            try
            {
                var exitResponse = await _usersList.ObtenerUsuarios();

                if (exitResponse?.Data == null)
                {
                    Debug.WriteLine("❌ No se recibió data del API");
                    return;
                }

                Debug.WriteLine($"✅ Usuarios recibidos: {exitResponse.Data.Count}");

                allData = exitResponse.Data;
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
