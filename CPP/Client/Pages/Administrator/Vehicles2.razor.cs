using Microsoft.AspNetCore.Components;
using CP.Server.DTO;
using Radzen;

namespace CP.Client.Pages.Administrator;

public partial class Vehicles2
{
    [Inject] protected CP.Client.CarParkService CarParkService { get; set; } = default!;
    [Inject] protected DialogService DialogService { get; set; } = default!;

    private string search = "";
    private bool showModal;
    private bool showDriverWarning;
    private CreateVehicleDto editingVehicle = new();
    private AdminVehicleDto? selectedVehicle;
    private List<AvailableDriverDto> drivers = new();
    private int? editingVehicleId;

    private List<AdminVehicleDto> vehicles = new();

    private IEnumerable<AdminVehicleDto> FilteredVehicles =>
        vehicles.Where(v =>
            string.IsNullOrWhiteSpace(search)
            || (v.Brand?.Contains(search, StringComparison.OrdinalIgnoreCase) ?? false)
            || (v.Model?.Contains(search, StringComparison.OrdinalIgnoreCase) ?? false)
            || (v.LicensePlate?.Contains(search, StringComparison.OrdinalIgnoreCase) ?? false)
            || (v.Vin?.Contains(search, StringComparison.OrdinalIgnoreCase) ?? false));

    protected override async Task OnInitializedAsync()
    {
        await LoadVehicles();
        await LoadDrivers();
    }

    private async Task LoadVehicles()
    {
        try
        {
            vehicles = await CarParkService.GetAdminVehiclesAsync();
        }
        catch
        {
            vehicles = new();
        }
    }

    private async Task LoadDrivers()
    {
        try
        {
            drivers = await CarParkService.GetAvailableDriversAsync();
        }
        catch
        {
            drivers = new();
        }
    }

    private void OpenCreateModal()
    {
        editingVehicleId = null;
        editingVehicle = new CreateVehicleDto
        {
            Year = DateTime.Now.Year,
            Status = "Available",
            InsuranceExpire = DateTime.Now.AddYears(1)
        };
        showModal = true;
        showDriverWarning = false;
    }

    private void OpenEditModal(AdminVehicleDto vehicle)
    {
        editingVehicleId = vehicle.VehicleId;
        editingVehicle = new CreateVehicleDto
        {
            Brand = vehicle.Brand,
            Model = vehicle.Model,
            Year = vehicle.Year,
            LicensePlate = vehicle.LicensePlate,
            Vin = vehicle.Vin,
            FuelType = vehicle.FuelType,
            VehicleType = vehicle.VehicleType,
            Mileage = vehicle.Mileage,
            Status = vehicle.Status,
            InsuranceExpire = vehicle.InsuranceExpire,
            DriverId = vehicle.DriverId
        };
        showModal = true;
        showDriverWarning = false;
    }

    private async Task SaveVehicle()
    {
        try
        {
            if (editingVehicleId == null)
            {
                await CarParkService.CreateVehicleAsync(editingVehicle);
            }
            else
            {
                await CarParkService.UpdateVehicleAsync(editingVehicleId.Value, editingVehicle);
            }
            showModal = false;
            await LoadVehicles();
        }
        catch (Exception ex)
        {
            var errorMessage = ex.Message;
            Console.WriteLine($"Ошибка: {errorMessage}");
            await DialogService.Alert(errorMessage, "Ошибка", new AlertOptions());
        }
    }

    private async Task DeleteVehicle(AdminVehicleDto vehicle)
    {
        try
        {
            await CarParkService.DeleteVehicleAsync(vehicle.VehicleId);
            await LoadVehicles();
        }
        catch (Exception ex)
        {
            await DialogService.Alert(ex.Message, "Ошибка", new AlertOptions());
        }
    }

    private async Task DeactivateVehicle(AdminVehicleDto vehicle)
    {
        try
        {
            await CarParkService.DeactivateVehicleAsync(vehicle.VehicleId);
            await LoadVehicles();
        }
        catch (Exception ex)
        {
            await DialogService.Alert(ex.Message, "Ошибка", new AlertOptions());
        }
    }

    private void OpenDetails(AdminVehicleDto vehicle)
    {
        selectedVehicle = vehicle;
    }

    private void CloseDetails()
    {
        selectedVehicle = null;
    }

    private void CloseModal()
    {
        showModal = false;
        showDriverWarning = false;
    }

    private void CheckDriverWarning()
    {
        showDriverWarning = selectedVehicle?.HasActiveTrips ?? false;
    }
}