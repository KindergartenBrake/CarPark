using Microsoft.AspNetCore.Components;
using CP.Server.DTO;

namespace CP.Client.Pages.Administrator;

public partial class Dashboard2
{
    [Inject] protected CP.Client.CarParkService CarParkService { get; set; } = default!;
    [Inject] protected NavigationManager Navigation { get; set; } = default!;

    private bool showRejectModal;
    private string rejectReason = "";
    private RecentRequestDto? selectedRequest;
    
    private AdminDashboardDto stats = new();
    
    // Свойства для привязки к разметке
    private List<RecentRequestDto> requests => stats.RecentRequests;
    private List<ActivityPointDto> chartData => stats.WeeklyActivity;
    
    private int PendingRequests => stats.PendingRequests;
    private int ActiveTrips => stats.ActiveTrips;
    private int AvailableVehicles => stats.AvailableVehicles;
    private int ActiveDrivers => stats.ActiveDrivers;
    private int TotalVehicles => stats.TotalVehicles;
    private int VehiclesInRepair => stats.VehiclesInRepair;
    private int CompletedTripsToday => stats.CompletedTripsToday;
    private double AverageLoad => stats.AverageLoad;

    protected override async Task OnInitializedAsync()
    {
        await LoadStats();
    }

    private async Task LoadStats()
    {
        try
        {
            stats = await CarParkService.GetAdminDashboardStatsAsync();
            StateHasChanged();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading dashboard: {ex.Message}");
            stats = new AdminDashboardDto();
        }
    }

    private void GoToRequests()
    {
        Navigation.NavigateTo("/administrator/trip-requests2");
    }

    private void GoToVehicles()
    {
        Navigation.NavigateTo("/administrator/vehicles2");
    }

    private async Task AssignRequest(RecentRequestDto request)
    {
        // Перенаправляем на страницу заявок с выделением конкретной заявки
        Navigation.NavigateTo($"/administrator/trip-requests2?requestId={request.RequestId}");
    }

    private void OpenRejectModal(RecentRequestDto request)
    {
        selectedRequest = request;
        showRejectModal = true;
    }

    private void CloseRejectModal()
    {
        showRejectModal = false;
        rejectReason = "";
        selectedRequest = null;
    }

    private async Task RejectRequest()
    {
        if (selectedRequest is not null)
        {
            try
            {
                await CarParkService.RejectTripRequestAsync(selectedRequest.RequestId, 
                    new RejectTripRequestDto { Reason = rejectReason });
                await LoadStats();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error rejecting request: {ex.Message}");
            }
        }
        CloseRejectModal();
    }
}