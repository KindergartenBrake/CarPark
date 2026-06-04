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

    private readonly string[] types = { null!, "легковой", "спецтехника", "грузовой" };

    protected override async Task OnInitializedAsync()
    {
        vehicles = await CarParkService.GetVehiclesAsync();
    }

    private async Task SelectType(string? type)
    {
        selectedType = type;
        vehicles = await CarParkService.GetVehiclesAsync(type);
    }
    private string GetImage(int vehicle_id)
    {
    var key = $"{vehicle_id}".ToLower();

    return key switch
    {
        var k when k.Contains("1") => "/images/toyota.jpg",
        var k when k.Contains("2") => "/images/solaris.jpg",
        var k when k.Contains("3") => "/images/rio.jpg",
        var k when k.Contains("4") => "/images/fordfocus.jpg",
        var k when k.Contains("5") => "/images/GAZ.jpg",
        var k when k.Contains("6") => "/images/MAN.jpg",
        var k when k.Contains("7") => "/images/cat.jpg",
        _ => "/images/hamam.jpg",
    };
}
}