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
    private List<UserLookupDto> allUsers = new();  // для редактирования
    private List<UserLookupDto> availableUsers = new();  // для создания
    private List<UserLookupDto> currentUsers = new();



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
        await LoadAllUsers(); 
        await LoadAvailableUsers();
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

    private async Task LoadAllUsers()
    {
        try
        {
            allUsers = await CarParkService.GetAllUsersAsync();
        }
        catch
        {
            allUsers = new();
        }
    }


    private async Task LoadAvailableUsers()
    {
        try
        {
            availableUsers = await CarParkService.GetAvailableUsersAsync();
        }
        catch
        {
            availableUsers = new();
        }
    }

    private async Task OpenCreateModal()
    {
        editingDriverId = null;
        currentUsers = await CarParkService.GetAvailableUsersAsync();

        await LoadAvailableUsers();  // загружаем пользователей
        
        editingDriver = new CreateDriverDto
        {
            IsActive = true,
            LicenseExpireDate = DateTime.Now.AddYears(1)
        };
        showModal = true;
    }

    private async Task OpenEditModal(AdminDriverDto driver)
    {
        editingDriverId = driver.DriverId;
        currentUsers = await CarParkService.GetAllUsersAsync();


        await LoadAllUsers();  // загружаем пользователей
        await LoadVehicles();

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
            UserId = driver.IdentityUser,
            VehicleId = GetVehicleIdByName(driver.VehicleName)
        };
        showModal = true;
    }
    
    private int? GetVehicleIdByName(string? vehicleName)
    {
        if (string.IsNullOrEmpty(vehicleName)) return null;
        
        var vehicle = vehicles.FirstOrDefault(v => 
            $"{v.Brand} {v.Model} ({v.LicensePlate})" == vehicleName ||
            $"{v.Brand} {v.Model}" == vehicleName);
        
        return vehicle?.VehicleId;
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