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
}