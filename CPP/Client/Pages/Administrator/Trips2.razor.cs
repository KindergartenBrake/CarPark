using Microsoft.AspNetCore.Components;
using CP.Server.DTO;

namespace CP.Client.Pages.Administrator;

public partial class Trips2
{
    [Inject] protected CarParkService CarParkService { get; set; } = default!;

    private string selectedStatus = "All";
    private string selectedDriver = "";
    private string selectedVehicle = "";
    private DateTime? dateFrom;
    private DateTime? dateTo;

    private TripDto? selectedTrip;
    private bool showForceCompleteModal;
    private TripDto? forceTrip;
    private string forceComment = "";

    private List<TripDto> trips = new();
    private List<string> drivers = new();
    private List<string> vehicles = new();

    private IEnumerable<TripDto> FilteredTrips =>
        trips.Where(t =>
            (selectedStatus == "All" || t.Status == selectedStatus)
            && (string.IsNullOrWhiteSpace(selectedDriver) || t.DriverName == selectedDriver)
            && (string.IsNullOrWhiteSpace(selectedVehicle) || t.VehicleName == selectedVehicle)
            && (!dateFrom.HasValue || t.TripDate >= dateFrom.Value)
            && (!dateTo.HasValue || t.TripDate <= dateTo.Value));

    protected override async Task OnInitializedAsync()
    {
        await LoadData();
    }

    private async Task LoadData()
    {
        await LoadTrips();
        await LoadDrivers();
        await LoadVehicles();
    }

    private async Task LoadTrips()
    {
        try
        {
            trips = await CarParkService.GetAllTripsAsync();
            StateHasChanged();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка загрузки поездок: {ex.Message}");
            trips = new();
        }
    }

    private async Task LoadDrivers()
    {
        try
        {
            drivers = await CarParkService.GetTripsDriversAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка загрузки водителей: {ex.Message}");
            drivers = new();
        }
    }

    private async Task LoadVehicles()
    {
        try
        {
            vehicles = await CarParkService.GetTripsVehiclesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка загрузки автомобилей: {ex.Message}");
            vehicles = new();
        }
    }

    private void OpenDetails(TripDto trip)
    {
        selectedTrip = trip;
    }

    private void CloseDetails()
    {
        selectedTrip = null;
    }

    private void OpenForceComplete(TripDto trip)
    {
        forceTrip = trip;
        showForceCompleteModal = true;
    }

    private void CloseForceModal()
    {
        showForceCompleteModal = false;
        forceComment = "";
        forceTrip = null;
    }

    private async Task ForceCompleteTrip()
    {
        if (forceTrip == null) return;

        try
        {
            await CarParkService.ForceCompleteTripAsync(forceTrip.TripId, forceComment);
            await LoadTrips();
            CloseForceModal();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка принудительного завершения: {ex.Message}");
        }
    }

    private async Task CancelTrip(TripDto trip)
    {
        try
        {
            await CarParkService.CancelTripAsync(trip.TripId);
            await LoadTrips();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка отмены поездки: {ex.Message}");
        }
    }

    private string GetStartTimeString(DateTime? time) => time?.ToString("HH:mm") ?? "—";
    private string GetEndTimeString(DateTime? time) => time?.ToString("HH:mm") ?? "—";
private Task RestoreTripStub()
{
    return Task.CompletedTask;
}
    //private async Task RestoreTrip(TripDto trip)
//{
  //  trip.Status = "Scheduled";

    // если есть связанная заявка
  //  var request = Requests.FirstOrDefault(x => x.Id == trip.RequestId);
  //  if (request != null)
  //  {
  //      request.Status = "Approved"; // или нужный статус
   // }

   // await TripService.UpdateTripAsync(trip);

   // StateHasChanged();
//}
}