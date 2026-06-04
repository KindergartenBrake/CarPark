using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;
using Radzen.Blazor;

namespace CP.Client.Pages
{
    public partial class ApplicationUsers
    {
        [Inject]
        protected IJSRuntime JSRuntime { get; set; } = default!;

        [Inject]
        protected NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        protected DialogService DialogService { get; set; } = default!;

        [Inject]
        protected TooltipService TooltipService { get; set; } = default!;

        [Inject]
        protected ContextMenuService ContextMenuService { get; set; } = default!;

        [Inject]
        protected NotificationService NotificationService { get; set; } = default!;

        [Inject]
        protected SecurityService Security { get; set; } = default!;

        protected IEnumerable<CP.Server.Models.ApplicationUser> users = new List<CP.Server.Models.ApplicationUser>();
        protected Dictionary<string, string> userRoles = new Dictionary<string, string>();
        protected string error = "";
        protected bool errorVisible;
        
        // Dialog properties
        protected bool showAddDialog = false;
        protected string dialogEmail = "";
        protected string dialogPassword = "";
        protected string dialogConfirmPassword = "";
        protected string selectedRole = "";
        protected IEnumerable<CP.Server.Models.ApplicationRole> roles = new List<CP.Server.Models.ApplicationRole>();

        protected override async Task OnInitializedAsync()
        {
            await LoadUsers();
            roles = await Security.GetRoles();
        }

        protected async Task LoadUsers()
        {
            try
            {
                users = await Security.GetUsers();
                
                foreach (var user in users)
                {
                    if (user?.Id != null && !userRoles.ContainsKey(user.Id))
                    {
                        var role = await GetUserRoleFromApi(user.Id);
                        userRoles[user.Id] = role;
                    }
                }
                
                await InvokeAsync(StateHasChanged);
            }
            catch (Exception ex)
            {
                errorVisible = true;
                error = ex.Message;
            }
        }

        protected async Task<string> GetUserRoleFromApi(string userId)
        {
            try
            {
                var userWithRoles = await Security.GetUserById(userId);
                var role = userWithRoles?.Roles?.FirstOrDefault()?.Name ?? "Employee";
                
                return role switch
                {
                    "Administrator" => "Администратор",
                    "Admin" => "Администратор",
                    "Driver" => "Водитель",
                    "Employee" => "Сотрудник",
                    _ => "Сотрудник"
                };
            }
            catch
            {
                return "Сотрудник";
            }
        }

        protected string GetUserRole(CP.Server.Models.ApplicationUser user)
        {
            if (user?.Id == null) return "Сотрудник";
            return userRoles.GetValueOrDefault(user.Id, "Сотрудник");
        }

        protected void OpenAddDialog()
        {
            dialogEmail = "";
            dialogPassword = "";
            dialogConfirmPassword = "";
            selectedRole = "";
            showAddDialog = true;
            StateHasChanged();
        }

        protected void CloseAddDialog()
        {
            showAddDialog = false;
            StateHasChanged();
        }
        
        protected void CloseAddDialogIfClickedOutside()
        {
            CloseAddDialog();
        }

        protected async Task SaveNewUser()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dialogEmail))
                {
                    error = "Email обязателен";
                    errorVisible = true;
                    return;
                }
                
                if (string.IsNullOrWhiteSpace(dialogPassword))
                {
                    error = "Пароль обязателен";
                    errorVisible = true;
                    return;
                }
                
                if (dialogPassword != dialogConfirmPassword)
                {
                    error = "Пароли не совпадают";
                    errorVisible = true;
                    return;
                }
                
                if (string.IsNullOrWhiteSpace(selectedRole))
                {
                    error = "Выберите роль";
                    errorVisible = true;
                    return;
                }
                
                var userToCreate = new CP.Server.Models.ApplicationUser
                {
                    Email = dialogEmail,
                    Password = dialogPassword,
                    ConfirmPassword = dialogConfirmPassword
                };
                
                // Назначаем выбранную роль
                var selectedRoleObj = roles.FirstOrDefault(r => r.Id == selectedRole);
                if (selectedRoleObj != null)
                {
                    userToCreate.Roles = new List<CP.Server.Models.ApplicationRole> { selectedRoleObj };
                }
                
                await Security.CreateUser(userToCreate);
                showAddDialog = false;
                await LoadUsers();
                StateHasChanged();
            }
            catch (Exception ex)
            {
                errorVisible = true;
                error = ex.Message;
            }
        }

        protected async Task RowSelect(CP.Server.Models.ApplicationUser user)
        {
            await DialogService.OpenAsync<EditApplicationUser>("Редактирование пользователя", 
                new Dictionary<string, object> { { "Id", user?.Id } });
            await LoadUsers();
        }

        protected async Task DeleteClick(CP.Server.Models.ApplicationUser user)
        {
            try
            {
                if (user?.Id == null) return;
                
                if (await DialogService.Confirm("Вы уверены, что хотите удалить этого пользователя?") == true)
                {
                    await Security.DeleteUser(user.Id);
                    await LoadUsers();
                }
            }
            catch (Exception ex)
            {
                errorVisible = true;
                error = ex.Message;
            }
        }
    }
}