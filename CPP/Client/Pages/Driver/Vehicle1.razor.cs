using Microsoft.AspNetCore.Components;
using CP.Server.DTO;

namespace CP.Client.Pages.Driver;

public partial class Vehicle1
{
    [Inject] protected CP.Client.CarParkService CarParkService { get; set; } = default!;

    private DriverVehicleDto? vehicle;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            vehicle = await CarParkService.GetDriverVehicleAsync();
        }
        catch
        {
            vehicle = null;
        }
    }

    private string GetVehicleIcon(string type) => type switch
    {
        "Седан" => "🚗",
        "Универсал" => "🚙",
        "Микроавтобус" => "🚐",
        "Грузовой" => "🚛",
        _ => "🚘"
    };
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