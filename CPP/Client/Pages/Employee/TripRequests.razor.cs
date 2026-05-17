using Microsoft.AspNetCore.Components;
using CP.Server.DTO;
using System.ComponentModel.DataAnnotations;

namespace CP.Client.Pages.Employee;

public partial class TripRequests
{
    [Inject] protected CP.Client.CarParkService CarParkService { get; set; } = default!;

    private List<string> statuses = new() { "Все", "Pending", "Approved", "Completed", "Rejected" };
    private List<string> vehicleTypes = new() { "легковой", "спецтехника", "грузовой" };

    private string selectedStatus = "Все";
    private bool showCreateModal = false;

    private List<TripRequestDto> requests = new();
    private TripRequestDto? selectedRequest;
    private CreateTripRequestForm newRequest = new();

    private IEnumerable<TripRequestDto> filteredRequests =>
        selectedStatus == "Все"
            ? requests
            : requests.Where(x => x.Status == selectedStatus);

    protected override async Task OnInitializedAsync()
    {
        await LoadRequests();
    }

    private async Task LoadRequests()
    {
        try
        {
            requests = await CarParkService.GetMyTripRequestsAsync();
        }
        catch
        {
            requests = new();
        }
    }

    private void SelectStatus(string status) => selectedStatus = status;

    private void OpenCreateModal()
    {
        newRequest = new CreateTripRequestForm();
        showCreateModal = true;
    }

    private void CloseCreateModal() => showCreateModal = false;

    private async Task SubmitRequest()
    {
        if (newRequest.TripDate < DateTime.Now || newRequest.EndTime <= newRequest.StartTime)
            return;

        try
        {
            var dto = new CreateTripRequestDto
            {
                VehicleType = newRequest.VehicleType,
                TripDate = newRequest.TripDate,
                StartTime = newRequest.StartTime,
                EndTime = newRequest.EndTime,
                Description = newRequest.Description
            };

            await CarParkService.CreateTripRequestAsync(dto);
            showCreateModal = false;
            await LoadRequests();
        }
        catch
        {
        }
    }

    private void OpenDetails(TripRequestDto request) => selectedRequest = request;
    private void CloseDetails() => selectedRequest = null;

    private string GetStatusColor(string status) => status switch
    {
        "Pending" => "#ffd54f",
        "Approved" => "#4caf50",
        "Completed" => "#42a5f5",
        "Rejected" => "#ef5350",
        _ => "#ffffff"
    };

    private string GetStatusBackground(string status) => status switch
    {
        "Pending" => "rgba(255,213,79,0.15)",
        "Approved" => "rgba(76,175,80,0.15)",
        "Completed" => "rgba(66,165,245,0.15)",
        "Rejected" => "rgba(239,83,80,0.15)",
        _ => "rgba(255,255,255,0.08)"
    };
}

public class CreateTripRequestForm
{
    [Required(ErrorMessage = "Выберите тип автомобиля")]
    public string VehicleType { get; set; } = string.Empty;

    [Required(ErrorMessage = "Укажите дату поездки")]
    public DateTime TripDate { get; set; } = DateTime.Now.Date.AddDays(1);

    [Required(ErrorMessage = "Укажите время начала")]
    public DateTime StartTime { get; set; } = DateTime.Now.Date.AddDays(1).AddHours(9);

    [Required(ErrorMessage = "Укажите время окончания")]
    public DateTime EndTime { get; set; } = DateTime.Now.Date.AddDays(1).AddHours(18);

    public string? Description { get; set; }
}