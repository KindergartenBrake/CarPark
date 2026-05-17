using Microsoft.AspNetCore.Components;
using CP.Server.DTO;

namespace CP.Client.Pages.Employee;

public partial class Vehicles
{
    [Inject] protected CP.Client.CarParkService CarParkService { get; set; } = default!;

    private List<VehicleDto> vehicles = new();
    private string? selectedType = null;

    private List<VehicleDto> filteredVehicles =>
        selectedType == null
            ? vehicles
            : vehicles.Where(v => v.VehicleType == selectedType).ToList();

    private readonly string[] types = { null!, "Седан", "Универсал", "Микроавтобус", "Грузовой" };

    protected override async Task OnInitializedAsync()
    {
        vehicles = await CarParkService.GetVehiclesAsync();
    }

    private async Task SelectType(string? type)
    {
        selectedType = type;
        vehicles = await CarParkService.GetVehiclesAsync(type);
    }
}