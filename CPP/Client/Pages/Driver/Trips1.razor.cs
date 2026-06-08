using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using CP.Server.DTO;

namespace CP.Client.Pages.Driver
{
    public partial class Trips1
    {
        [Inject] protected HttpClient Http { get; set; } = default!;
        [Inject] protected NavigationManager NavigationManager { get; set; } = default!;

        private List<DriverTripDto> trips = new();
        private string selectedFilter = "All";
        private bool showStartModal;
        private bool showEndModal;
        private DriverTripDto? selectedTrip;
        private StartTripDto startModel = new();
        private EndTripDto endModel = new();

        private List<string> tripFilters = new() { "All", "Scheduled", "InProgress", "Completed" };

        private IEnumerable<DriverTripDto> filteredTrips =>
            selectedFilter == "All"
                ? trips
                : trips.Where(t => t.Status == selectedFilter);

        protected override async Task OnInitializedAsync()
        {
            await LoadTrips();
        }

        private async Task LoadTrips()
        {
            try
            {
                var result = await Http.GetFromJsonAsync<List<DriverTripDto>>("api/driver/trips/my");
                trips = result ?? new();
                
                // Нормализация статусов
                foreach (var trip in trips)
                {
                    trip.Status = trip.Status?.ToLower() switch
                    {
                        "scheduled" => "Scheduled",
                        "inprogress" => "InProgress",
                        "completed" => "Completed",
                        _ => trip.Status ?? "Scheduled"
                    };
                }
                
                StateHasChanged();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading trips: {ex.Message}");
                trips = new();
            }
        }

        private void SelectFilter(string filter)
        {
            selectedFilter = filter;
        }

        private string GetFilterLabel(string filter) => filter switch
        {
            "All" => "Все",
            "Scheduled" => "Назначенные",
            "InProgress" => "Активные",
            "Completed" => "Завершённые",
            _ => filter
        };

        private string GetStatusText(string status) => status switch
        {
            "Scheduled" => "Назначена",
            "InProgress" => "В пути",
            "Completed" => "Завершена",
            _ => status
        };

        private string GetStatusStyle(string status) => status switch
        {
            "Scheduled" => "background: rgba(245, 158, 11, 0.15); color: #fbbf24; padding: 6px 14px; border-radius: 20px; font-weight: 700; font-size: 13px;",
            "InProgress" => "background: rgba(59, 130, 246, 0.15); color: #60a5fa; padding: 6px 14px; border-radius: 20px; font-weight: 700; font-size: 13px;",
            "Completed" => "background: rgba(34, 197, 94, 0.15); color: #4ade80; padding: 6px 14px; border-radius: 20px; font-weight: 700; font-size: 13px;",
            _ => "background: rgba(100, 116, 139, 0.15); color: #94a3b8; padding: 6px 14px; border-radius: 20px; font-weight: 700; font-size: 13px;"
        };

        private string ActionButtonStyle(string color) =>
            $"background: {color}; border: none; color: white; padding: 8px 14px; border-radius: 8px; cursor: pointer; font-weight: 600; font-size: 13px; transition: 0.2s;";

        private string GetOdometerValue(decimal? value) => value?.ToString() ?? "—";

        private void OpenStartModal(DriverTripDto trip)
        {
            selectedTrip = trip;
            startModel = new();
            showStartModal = true;
        }

        private void OpenEndModal(DriverTripDto trip)
        {
            selectedTrip = trip;
            endModel = new();
            showEndModal = true;
        }

        private void CloseModals()
        {
            showStartModal = false;
            showEndModal = false;
            selectedTrip = null;
        }

        private async Task StartTrip()
        {
            if (selectedTrip == null) return;

            try
            {
                var response = await Http.PutAsJsonAsync($"api/driver/trips/{selectedTrip.Id}/start", startModel);

                if (response.IsSuccessStatusCode)
                {
                    selectedTrip.Status = "InProgress";
                    selectedTrip.StartOdometer = startModel.StartOdometer;
                    CloseModals();
                    await LoadTrips();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error starting trip: {ex.Message}");
            }
        }

        private async Task EndTrip()
        {
            if (selectedTrip == null) return;

            if (endModel.EndOdometer <= (selectedTrip.StartOdometer ?? 0))
            {
                Console.WriteLine("End odometer must be greater than start odometer");
                return;
            }

            try
            {
                var response = await Http.PutAsJsonAsync($"api/driver/trips/{selectedTrip.Id}/end", endModel);

                if (response.IsSuccessStatusCode)
                {
                    selectedTrip.Status = "Completed";
                    selectedTrip.EndOdometer = endModel.EndOdometer;
                    CloseModals();
                    await LoadTrips();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error ending trip: {ex.Message}");
            }
        }

        private void ShowDetails(DriverTripDto trip)
        {
            // TODO: Открыть модалку с деталями
            Console.WriteLine($"Show details for trip {trip.Id}");
        }
    }
}