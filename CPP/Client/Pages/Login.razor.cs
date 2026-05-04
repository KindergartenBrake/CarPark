using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;

namespace CP.Client.Pages
{
    public partial class Login
    {
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected ContextMenuService ContextMenuService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        protected string redirectUrl; // Куда перенаправить после входа
        protected string error; // Текст ошибки (если есть)
        protected string info; // Информационное сообщение
        protected bool errorVisible;  // Показывать ли ошибку
        protected bool infoVisible; // Показывать ли информацию

        [Inject]
        protected SecurityService Security { get; set; }

        // Получаем текущий URL и парсим его параметры
        protected override async Task OnInitializedAsync()
        {
            var query = System.Web.HttpUtility.ParseQueryString(new Uri(NavigationManager.ToAbsoluteUri(NavigationManager.Uri).ToString()).Query);

            error = query.Get("error"); // Получаем параметр "error"

            info = query.Get("info"); // Получаем параметр "info"

            redirectUrl = query.Get("redirectUrl"); // Получаем параметр "redirectUrl"

            errorVisible = !string.IsNullOrEmpty(error); // Есть ли ошибка

            infoVisible = !string.IsNullOrEmpty(info); // Есть ли информация
        }
    }
}