using Microsoft.AspNetCore.Components;
using CP.Server.DTO;

namespace CP.Client.Pages.Employee;

public partial class Dashboard
{
    [Inject] protected CarParkService CarParkService { get; set; } = default!;
    [Inject] protected NavigationManager NavigationManager { get; set; } = default!;

    private EmployeeDashboardDto data = new();

    private int PendingRequests => data.PendingRequests;
    private int ApprovedRequests => data.ApprovedRequests;
    private int CompletedRequests => data.CompletedRequests;
    private int RejectedRequests => data.RejectedRequests;
    private UpcomingTripDto? UpcomingTrip => data.UpcomingTrip;
    private List<RecentRequestDto> RecentRequests => data.RecentRequests;

    protected override async Task OnInitializedAsync()
    {
        await LoadDashboard();
    }

    private async Task LoadDashboard()
    {
        try
        {
            data = await CarParkService.GetEmployeeDashboardAsync();
            StateHasChanged();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading dashboard: {ex.Message}");
        }
    }
}