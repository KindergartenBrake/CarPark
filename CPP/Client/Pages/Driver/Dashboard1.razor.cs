using Microsoft.AspNetCore.Components;
using CP.Server.DTO;
using System.Timers;

namespace CP.Client.Pages.Driver;

public partial class Dashboard1
{
    [Inject] protected CarParkService CarParkService { get; set; } = default!;
    [Inject] protected NavigationManager NavigationManager { get; set; } = default!;

    private System.Timers.Timer? timer;
    private string tripDuration = "00:00:00";
    private bool showFinishModal;
    private bool showStartModal;
    private decimal startOdometer;
    private decimal endOdometer;
    private ScheduledTripDto? selectedTrip;

    private DriverDashboardDto data = new();
    private ActiveTripDto? activeTrip => data.ActiveTrip;
    private List<ScheduledTripDto> scheduledTrips => data.ScheduledTrips;
    private List<TripHistoryDto> recentTrips => data.RecentTrips;
    private int completedTrips => data.CompletedTrips;
    private decimal totalMileage => data.TotalMileage;
    private string totalDriveTime => data.TotalDriveTime;

    protected override async Task OnInitializedAsync()
    {
        await LoadDashboard();
        
        timer = new System.Timers.Timer(1000);
        timer.Elapsed += UpdateTripDuration;
        timer.Start();
    }

    private async Task LoadDashboard()
    {
        try
        {
            data = await CarParkService.GetDriverDashboardAsync();
            UpdateDuration();
            StateHasChanged();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading dashboard: {ex.Message}");
        }
    }

    private void UpdateTripDuration(object? sender, ElapsedEventArgs e)
    {
        InvokeAsync(() =>
        {
            UpdateDuration();
            StateHasChanged();
        });
    }

    private void UpdateDuration()
    {
        if (activeTrip == null) return;
        var duration = DateTime.Now - activeTrip.StartTime;
        tripDuration = duration.ToString(@"hh\:mm\:ss");
    }

    private void OpenFinishModal()
    {
        showFinishModal = true;
    }

    private void CloseFinishModal()
    {
        showFinishModal = false;
        endOdometer = 0;
    }

    private async Task FinishTrip()
    {
        if (activeTrip == null) return;
        
        try
        {
            await CarParkService.EndTripAsync(activeTrip.TripId, endOdometer);
            CloseFinishModal();
            await LoadDashboard();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error finishing trip: {ex.Message}");
        }
    }

    private void OpenStartTripModal(ScheduledTripDto trip)
    {
        selectedTrip = trip;
        showStartModal = true;
    }

    private void CloseStartModal()
    {
        showStartModal = false;
        startOdometer = 0;
        selectedTrip = null;
    }

    private async Task StartTrip()
    {
        if (selectedTrip == null) return;
        
        try
        {
            await CarParkService.StartTripAsync(selectedTrip.TripId, startOdometer);
            CloseStartModal();
            await LoadDashboard();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error starting trip: {ex.Message}");
        }
    }

    private void GoToTrips()
    {
        NavigationManager.NavigateTo("/driver/trips1");
    }
}