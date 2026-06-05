using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Radzen;
using Microsoft.JSInterop;

namespace CP.Client.Pages
{
    public partial class Login
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

        protected string redirectUrl = "";
        protected string error = "";
        protected string info = "";
        protected bool errorVisible;
        protected bool infoVisible;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var uri = new Uri(NavigationManager.ToAbsoluteUri(NavigationManager.Uri).ToString());
                var query = System.Web.HttpUtility.ParseQueryString(uri.Query);
                
                error = query.Get("error") ?? "";
                info = query.Get("info") ?? "";
                redirectUrl = query.Get("redirectUrl") ?? "";
                
                errorVisible = !string.IsNullOrEmpty(error);
                infoVisible = !string.IsNullOrEmpty(info);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing query: {ex.Message}");
            }
        }
        
    }
}