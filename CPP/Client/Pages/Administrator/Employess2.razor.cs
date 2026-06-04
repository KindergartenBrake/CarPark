using Microsoft.AspNetCore.Components;
using CP.Server.DTO;

namespace CP.Client.Pages.Administrator;

public partial class Employess2
{
    [Inject] protected CarParkService CarParkService { get; set; } = default!;

    private string search = "";
    private EmployeeDto? selectedEmployee;
    private List<EmployeeDto> employees = new();

    private IEnumerable<EmployeeDto> FilteredEmployees =>
        employees.Where(x =>
            string.IsNullOrWhiteSpace(search)
            || x.FullName.Contains(search, StringComparison.OrdinalIgnoreCase)
            || x.Email.Contains(search, StringComparison.OrdinalIgnoreCase));

    protected override async Task OnInitializedAsync()
    {
        await LoadEmployees();
    }

    private async Task LoadEmployees()
    {
        try
        {
            employees = await CarParkService.GetEmployeesAsync();
            StateHasChanged();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка загрузки сотрудников: {ex.Message}");
            employees = new();
        }
    }

    private async Task OpenDetails(EmployeeDto employee)
    {
        try
        {
            selectedEmployee = await CarParkService.GetEmployeeByIdAsync(employee.UserId);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка загрузки деталей: {ex.Message}");
        }
    }

    private void CloseDetails()
    {
        selectedEmployee = null;
    }

    private async Task Deactivate(EmployeeDto employee)
{
    try
    {
        await CarParkService.DeactivateEmployeeAsync(employee.UserId);
        await LoadEmployees();

        if (selectedEmployee?.UserId == employee.UserId)
        {
            selectedEmployee = await CarParkService.GetEmployeeByIdAsync(employee.UserId);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Ошибка деактивации: {ex.Message}");
    }
}
    //private async Task Activate(EmployeeDto employee)
//{
    //try
    //{
       // await CarParkService.ActivateEmployeeAsync(employee.UserId);
      //  await LoadEmployees();

      //  if (selectedEmployee?.UserId == employee.UserId)
      //  {
      //      selectedEmployee = await CarParkService.GetEmployeeByIdAsync(employee.UserId);
       // }
   // }
  //  catch (Exception ex)
  //  {
      //  Console.WriteLine($"Ошибка активации: {ex.Message}");
   // }
//}
}