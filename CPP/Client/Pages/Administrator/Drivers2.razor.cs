using Microsoft.AspNetCore.Components;
using CP.Server.DTO;

namespace CP.Client.Pages.Administrator;

public partial class Drivers2
{
    [Inject] protected CP.Client.CarParkService CarParkService { get; set; } = default!;

    private string search = "";
    private bool showModal;
    private CreateDriverDto editingDriver = new();
    private AdminDriverDto? selectedDriver;
    private int? editingDriverId;

    private List<AdminDriverDto> drivers = new();
    private List<AdminVehicleDto> vehicles = new();

    private IEnumerable<AdminDriverDto> FilteredDrivers =>
        drivers.Where(x =>
            string.IsNullOrWhiteSpace(search)
            || (x.FullName?.Contains(search, StringComparison.OrdinalIgnoreCase) ?? false)
            || (x.Phone?.Contains(search, StringComparison.OrdinalIgnoreCase) ?? false)
            || (x.LicenseNumber?.Contains(search, StringComparison.OrdinalIgnoreCase) ?? false));

    protected override async Task OnInitializedAsync()
    {
        await LoadDrivers();
        await LoadVehicles();
    }

    private async Task LoadDrivers()
    {
        try
        {
            drivers = await CarParkService.GetAdminDriversAsync();
        }
        catch
        {
            drivers = new();
        }
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

    private void OpenCreateModal()
    {
        editingDriverId = null;
        editingDriver = new CreateDriverDto
        {
            IsActive = true,
            LicenseExpireDate = DateTime.Now.AddYears(1)
        };
        showModal = true;
    }

    private void OpenEditModal(AdminDriverDto driver)
    {
        editingDriverId = driver.DriverId;

        var nameParts = driver.FullName?.Split(' ') ?? new[] { "", "" };
        editingDriver = new CreateDriverDto
        {
            LastName = nameParts.Length > 0 ? nameParts[0] : "",
            FirstName = nameParts.Length > 1 ? nameParts[1] : "",
            MiddleName = nameParts.Length > 2 ? nameParts[2] : null,
            LicenseNumber = driver.LicenseNumber,
            LicenseExpireDate = driver.LicenseExpireDate,
            Phone = driver.Phone ?? "",
            IsActive = driver.IsActive,
            UserId = driver.IdentityUser
        };
        showModal = true;
    }

    private async Task SaveDriver()
    {
        try
        {
            if (editingDriverId == null)
            {
                await CarParkService.CreateDriverAsync(editingDriver);
            }
            else
            {
                await CarParkService.UpdateDriverAsync(editingDriverId.Value, editingDriver);
            }
            showModal = false;
            await LoadDrivers();
        }
        catch { }
    }

    private void OpenDetails(AdminDriverDto driver)
    {
        selectedDriver = driver;
    }

    private void CloseDetails()
    {
        selectedDriver = null;
    }

    private void CloseModal()
    {
        showModal = false;
    }

    private async Task Deactivate(AdminDriverDto driver)
    {
        try
        {
            await CarParkService.DeactivateDriverAsync(driver.DriverId);
            await LoadDrivers();
        }
        catch { }
    }
}