using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Radzen;

namespace CP.Client.Layout
{
    public partial class MainLayout
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
        protected AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

        [Inject]
        protected SecurityService SecurityService { get; set; } = default!;

        private bool sidebarExpanded = true;
        private string userRole = "";

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.IsInRole("Admin"))
                userRole = "Administrator";
            else if (user.IsInRole("Driver"))
                userRole = "Driver";
            else if (user.IsInRole("Employee"))
                userRole = "Employee";
        }

        void SidebarToggleClick()
        {
            sidebarExpanded = !sidebarExpanded;
        }

        protected void Logout()
        {
            SecurityService?.Logout();
        }

        // Метод для отображения роли
        public string GetRoleDisplayName() => userRole switch
        {
            "Administrator" => "Администратор",
            "Driver" => "Водитель",
            "Employee" => "Сотрудник",
            _ => "Сотрудник"
        };
    }
}