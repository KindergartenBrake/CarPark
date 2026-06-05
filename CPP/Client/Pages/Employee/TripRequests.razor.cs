using Microsoft.AspNetCore.Components;
using CP.Server.DTO;
using Radzen;

namespace CP.Client.Pages.Employee;

public partial class TripRequests
{
    [Inject] protected CP.Client.CarParkService CarParkService { get; set; } = default!;
    [Inject] protected DialogService DialogService { get; set; } = default!;

    private List<string> statuses = new() { "Все", "Pending", "Approved", "Completed", "Rejected" };
    private List<string> vehicleTypes = new() { "легковой", "спецтехника", "грузовой" };

    private string selectedStatus = "Все";
    private bool showCreateModal = false;
    private string startTimeString = "09:00";
    private string endTimeString = "18:00";

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
            StateHasChanged();
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
        startTimeString = "09:00";
        endTimeString = "18:00";
        showCreateModal = true;
    }

    private void CloseCreateModal() => showCreateModal = false;

    private async Task SubmitRequest()
    {
        if (string.IsNullOrWhiteSpace(newRequest.VehicleType))
        {
            await DialogService.Alert("Выберите тип автомобиля", "Ошибка");
            return;
        }
        
        if (newRequest.TripDate < DateTime.Now.Date)
        {
            await DialogService.Alert("Дата поездки не может быть в прошлом", "Ошибка");
            return;
        }
        
        if (!TimeSpan.TryParse(startTimeString, out var startTime))
        {
            await DialogService.Alert("Неверный формат времени начала", "Ошибка");
            return;
        }
        
        if (!TimeSpan.TryParse(endTimeString, out var endTime))
        {
            await DialogService.Alert("Неверный формат времени окончания", "Ошибка");
            return;
        }
        
        if (endTime <= startTime)
        {
            await DialogService.Alert("Время окончания должно быть позже времени начала", "Ошибка");
            return;
        }

        try
        {
            var dto = new CreateTripRequestDto
            {
                VehicleType = newRequest.VehicleType,
                TripDate = newRequest.TripDate,
                StartTime = startTime,
                EndTime = endTime,
                Description = newRequest.Description
            };

            await CarParkService.CreateTripRequestAsync(dto);
            CloseCreateModal();
            await LoadRequests();
            await DialogService.Alert("Заявка успешно создана!", "Успех");
        }
        catch (Exception ex)
        {
            await DialogService.Alert($"Ошибка: {ex.Message}", "Ошибка");
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
    public string VehicleType { get; set; } = string.Empty;
    public DateTime TripDate { get; set; } = DateTime.Now.Date.AddDays(1);
    public string? Description { get; set; }
}