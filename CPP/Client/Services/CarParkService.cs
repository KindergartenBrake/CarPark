
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Web;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using System.Net.Http.Json;
using Radzen;
using CP.Client.Pages.Driver;


using CP.Server.DTO;

namespace CP.Client
{
    public partial class CarParkService
    {
        private readonly HttpClient httpClient;
        private readonly Uri baseUri;
        private readonly NavigationManager navigationManager;

        public CarParkService(NavigationManager navigationManager, HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;

            this.navigationManager = navigationManager;
            this.baseUri = new Uri($"{navigationManager.BaseUri}odata/CarPark/");
        }


        public async System.Threading.Tasks.Task ExportAspNetRoleClaimsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/aspnetroleclaims/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/aspnetroleclaims/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportAspNetRoleClaimsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/aspnetroleclaims/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/aspnetroleclaims/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetAspNetRoleClaims(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<CP.Server.Models.CarPark.AspNetRoleClaim>> GetAspNetRoleClaims(Query query)
        {
            return await GetAspNetRoleClaims(filter: $"{query.Filter}", orderby: $"{query.OrderBy}", top: query.Top, skip: query.Skip, count: query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<CP.Server.Models.CarPark.AspNetRoleClaim>> GetAspNetRoleClaims(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string), string apply = default(string))
        {
            var uri = new Uri(baseUri, $"AspNetRoleClaims");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter: filter, top: top, skip: skip, orderby: orderby, expand: expand, select: select, count: count, apply: apply);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAspNetRoleClaims(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<CP.Server.Models.CarPark.AspNetRoleClaim>>(response);
        }

        partial void OnCreateAspNetRoleClaim(HttpRequestMessage requestMessage);

        public async Task<CP.Server.Models.CarPark.AspNetRoleClaim> CreateAspNetRoleClaim(CP.Server.Models.CarPark.AspNetRoleClaim aspNetRoleClaim = default(CP.Server.Models.CarPark.AspNetRoleClaim))
        {
            var uri = new Uri(baseUri, $"AspNetRoleClaims");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(aspNetRoleClaim), Encoding.UTF8, "application/json");

            OnCreateAspNetRoleClaim(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CP.Server.Models.CarPark.AspNetRoleClaim>(response);
        }

        partial void OnDeleteAspNetRoleClaim(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteAspNetRoleClaim(int id = default(int))
        {
            var uri = new Uri(baseUri, $"AspNetRoleClaims({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteAspNetRoleClaim(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetAspNetRoleClaimById(HttpRequestMessage requestMessage);

        public async Task<CP.Server.Models.CarPark.AspNetRoleClaim> GetAspNetRoleClaimById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"AspNetRoleClaims({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter: null, top: null, skip: null, orderby: null, expand: expand, select: null, count: null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAspNetRoleClaimById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CP.Server.Models.CarPark.AspNetRoleClaim>(response);
        }

        partial void OnUpdateAspNetRoleClaim(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> UpdateAspNetRoleClaim(int id = default(int), CP.Server.Models.CarPark.AspNetRoleClaim aspNetRoleClaim = default(CP.Server.Models.CarPark.AspNetRoleClaim))
        {
            var uri = new Uri(baseUri, $"AspNetRoleClaims({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(aspNetRoleClaim), Encoding.UTF8, "application/json");

            OnUpdateAspNetRoleClaim(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportAspNetRolesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/aspnetroles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/aspnetroles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportAspNetRolesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/aspnetroles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/aspnetroles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetAspNetRoles(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<CP.Server.Models.CarPark.AspNetRole>> GetAspNetRoles(Query query)
        {
            return await GetAspNetRoles(filter: $"{query.Filter}", orderby: $"{query.OrderBy}", top: query.Top, skip: query.Skip, count: query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<CP.Server.Models.CarPark.AspNetRole>> GetAspNetRoles(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string), string apply = default(string))
        {
            var uri = new Uri(baseUri, $"AspNetRoles");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter: filter, top: top, skip: skip, orderby: orderby, expand: expand, select: select, count: count, apply: apply);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAspNetRoles(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<CP.Server.Models.CarPark.AspNetRole>>(response);
        }

        partial void OnCreateAspNetRole(HttpRequestMessage requestMessage);

        public async Task<CP.Server.Models.CarPark.AspNetRole> CreateAspNetRole(CP.Server.Models.CarPark.AspNetRole aspNetRole = default(CP.Server.Models.CarPark.AspNetRole))
        {
            var uri = new Uri(baseUri, $"AspNetRoles");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(aspNetRole), Encoding.UTF8, "application/json");

            OnCreateAspNetRole(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CP.Server.Models.CarPark.AspNetRole>(response);
        }

        partial void OnDeleteAspNetRole(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteAspNetRole(string id = default(string))
        {
            var uri = new Uri(baseUri, $"AspNetRoles('{Uri.EscapeDataString(id.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteAspNetRole(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetAspNetRoleById(HttpRequestMessage requestMessage);

        public async Task<CP.Server.Models.CarPark.AspNetRole> GetAspNetRoleById(string expand = default(string), string id = default(string))
        {
            var uri = new Uri(baseUri, $"AspNetRoles('{Uri.EscapeDataString(id.Trim().Replace("'", "''"))}')");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter: null, top: null, skip: null, orderby: null, expand: expand, select: null, count: null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAspNetRoleById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CP.Server.Models.CarPark.AspNetRole>(response);
        }

        partial void OnUpdateAspNetRole(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> UpdateAspNetRole(string id = default(string), CP.Server.Models.CarPark.AspNetRole aspNetRole = default(CP.Server.Models.CarPark.AspNetRole))
        {
            var uri = new Uri(baseUri, $"AspNetRoles('{Uri.EscapeDataString(id.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(aspNetRole), Encoding.UTF8, "application/json");

            OnUpdateAspNetRole(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportAspNetUserClaimsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/aspnetuserclaims/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/aspnetuserclaims/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportAspNetUserClaimsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/aspnetuserclaims/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/aspnetuserclaims/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetAspNetUserClaims(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<CP.Server.Models.CarPark.AspNetUserClaim>> GetAspNetUserClaims(Query query)
        {
            return await GetAspNetUserClaims(filter: $"{query.Filter}", orderby: $"{query.OrderBy}", top: query.Top, skip: query.Skip, count: query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<CP.Server.Models.CarPark.AspNetUserClaim>> GetAspNetUserClaims(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string), string apply = default(string))
        {
            var uri = new Uri(baseUri, $"AspNetUserClaims");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter: filter, top: top, skip: skip, orderby: orderby, expand: expand, select: select, count: count, apply: apply);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAspNetUserClaims(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<CP.Server.Models.CarPark.AspNetUserClaim>>(response);
        }

        partial void OnCreateAspNetUserClaim(HttpRequestMessage requestMessage);

        public async Task<CP.Server.Models.CarPark.AspNetUserClaim> CreateAspNetUserClaim(CP.Server.Models.CarPark.AspNetUserClaim aspNetUserClaim = default(CP.Server.Models.CarPark.AspNetUserClaim))
        {
            var uri = new Uri(baseUri, $"AspNetUserClaims");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(aspNetUserClaim), Encoding.UTF8, "application/json");

            OnCreateAspNetUserClaim(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CP.Server.Models.CarPark.AspNetUserClaim>(response);
        }

        partial void OnDeleteAspNetUserClaim(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteAspNetUserClaim(int id = default(int))
        {
            var uri = new Uri(baseUri, $"AspNetUserClaims({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteAspNetUserClaim(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetAspNetUserClaimById(HttpRequestMessage requestMessage);

        public async Task<CP.Server.Models.CarPark.AspNetUserClaim> GetAspNetUserClaimById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"AspNetUserClaims({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter: null, top: null, skip: null, orderby: null, expand: expand, select: null, count: null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAspNetUserClaimById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CP.Server.Models.CarPark.AspNetUserClaim>(response);
        }

        partial void OnUpdateAspNetUserClaim(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> UpdateAspNetUserClaim(int id = default(int), CP.Server.Models.CarPark.AspNetUserClaim aspNetUserClaim = default(CP.Server.Models.CarPark.AspNetUserClaim))
        {
            var uri = new Uri(baseUri, $"AspNetUserClaims({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(aspNetUserClaim), Encoding.UTF8, "application/json");

            OnUpdateAspNetUserClaim(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportAspNetUserLoginsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/aspnetuserlogins/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/aspnetuserlogins/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportAspNetUserLoginsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/aspnetuserlogins/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/aspnetuserlogins/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetAspNetUserLogins(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<CP.Server.Models.CarPark.AspNetUserLogin>> GetAspNetUserLogins(Query query)
        {
            return await GetAspNetUserLogins(filter: $"{query.Filter}", orderby: $"{query.OrderBy}", top: query.Top, skip: query.Skip, count: query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<CP.Server.Models.CarPark.AspNetUserLogin>> GetAspNetUserLogins(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string), string apply = default(string))
        {
            var uri = new Uri(baseUri, $"AspNetUserLogins");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter: filter, top: top, skip: skip, orderby: orderby, expand: expand, select: select, count: count, apply: apply);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAspNetUserLogins(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<CP.Server.Models.CarPark.AspNetUserLogin>>(response);
        }

        partial void OnCreateAspNetUserLogin(HttpRequestMessage requestMessage);

        public async Task<CP.Server.Models.CarPark.AspNetUserLogin> CreateAspNetUserLogin(CP.Server.Models.CarPark.AspNetUserLogin aspNetUserLogin = default(CP.Server.Models.CarPark.AspNetUserLogin))
        {
            var uri = new Uri(baseUri, $"AspNetUserLogins");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(aspNetUserLogin), Encoding.UTF8, "application/json");

            OnCreateAspNetUserLogin(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CP.Server.Models.CarPark.AspNetUserLogin>(response);
        }

        partial void OnDeleteAspNetUserLogin(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteAspNetUserLogin(string loginProvider = default(string), string providerKey = default(string))
        {
            var uri = new Uri(baseUri, $"AspNetUserLogins(LoginProvider='{Uri.EscapeDataString(loginProvider.Trim().Replace("'", "''"))}',ProviderKey='{Uri.EscapeDataString(providerKey.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteAspNetUserLogin(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetAspNetUserLoginByLoginProviderAndProviderKey(HttpRequestMessage requestMessage);

        public async Task<CP.Server.Models.CarPark.AspNetUserLogin> GetAspNetUserLoginByLoginProviderAndProviderKey(string expand = default(string), string loginProvider = default(string), string providerKey = default(string))
        {
            var uri = new Uri(baseUri, $"AspNetUserLogins(LoginProvider='{Uri.EscapeDataString(loginProvider.Trim().Replace("'", "''"))}',ProviderKey='{Uri.EscapeDataString(providerKey.Trim().Replace("'", "''"))}')");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter: null, top: null, skip: null, orderby: null, expand: expand, select: null, count: null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAspNetUserLoginByLoginProviderAndProviderKey(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CP.Server.Models.CarPark.AspNetUserLogin>(response);
        }

        partial void OnUpdateAspNetUserLogin(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> UpdateAspNetUserLogin(string loginProvider = default(string), string providerKey = default(string), CP.Server.Models.CarPark.AspNetUserLogin aspNetUserLogin = default(CP.Server.Models.CarPark.AspNetUserLogin))
        {
            var uri = new Uri(baseUri, $"AspNetUserLogins(LoginProvider='{Uri.EscapeDataString(loginProvider.Trim().Replace("'", "''"))}',ProviderKey='{Uri.EscapeDataString(providerKey.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(aspNetUserLogin), Encoding.UTF8, "application/json");

            OnUpdateAspNetUserLogin(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportAspNetUserRolesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/aspnetuserroles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/aspnetuserroles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportAspNetUserRolesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/aspnetuserroles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/aspnetuserroles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetAspNetUserRoles(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<CP.Server.Models.CarPark.AspNetUserRole>> GetAspNetUserRoles(Query query)
        {
            return await GetAspNetUserRoles(filter: $"{query.Filter}", orderby: $"{query.OrderBy}", top: query.Top, skip: query.Skip, count: query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<CP.Server.Models.CarPark.AspNetUserRole>> GetAspNetUserRoles(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string), string apply = default(string))
        {
            var uri = new Uri(baseUri, $"AspNetUserRoles");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter: filter, top: top, skip: skip, orderby: orderby, expand: expand, select: select, count: count, apply: apply);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAspNetUserRoles(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<CP.Server.Models.CarPark.AspNetUserRole>>(response);
        }

        partial void OnCreateAspNetUserRole(HttpRequestMessage requestMessage);

        public async Task<CP.Server.Models.CarPark.AspNetUserRole> CreateAspNetUserRole(CP.Server.Models.CarPark.AspNetUserRole aspNetUserRole = default(CP.Server.Models.CarPark.AspNetUserRole))
        {
            var uri = new Uri(baseUri, $"AspNetUserRoles");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(aspNetUserRole), Encoding.UTF8, "application/json");

            OnCreateAspNetUserRole(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CP.Server.Models.CarPark.AspNetUserRole>(response);
        }

        partial void OnDeleteAspNetUserRole(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteAspNetUserRole(string userId = default(string), string roleId = default(string))
        {
            var uri = new Uri(baseUri, $"AspNetUserRoles(UserId='{Uri.EscapeDataString(userId.Trim().Replace("'", "''"))}',RoleId='{Uri.EscapeDataString(roleId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteAspNetUserRole(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetAspNetUserRoleByUserIdAndRoleId(HttpRequestMessage requestMessage);

        public async Task<CP.Server.Models.CarPark.AspNetUserRole> GetAspNetUserRoleByUserIdAndRoleId(string expand = default(string), string userId = default(string), string roleId = default(string))
        {
            var uri = new Uri(baseUri, $"AspNetUserRoles(UserId='{Uri.EscapeDataString(userId.Trim().Replace("'", "''"))}',RoleId='{Uri.EscapeDataString(roleId.Trim().Replace("'", "''"))}')");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter: null, top: null, skip: null, orderby: null, expand: expand, select: null, count: null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAspNetUserRoleByUserIdAndRoleId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CP.Server.Models.CarPark.AspNetUserRole>(response);
        }

        partial void OnUpdateAspNetUserRole(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> UpdateAspNetUserRole(string userId = default(string), string roleId = default(string), CP.Server.Models.CarPark.AspNetUserRole aspNetUserRole = default(CP.Server.Models.CarPark.AspNetUserRole))
        {
            var uri = new Uri(baseUri, $"AspNetUserRoles(UserId='{Uri.EscapeDataString(userId.Trim().Replace("'", "''"))}',RoleId='{Uri.EscapeDataString(roleId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(aspNetUserRole), Encoding.UTF8, "application/json");

            OnUpdateAspNetUserRole(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportAspNetUsersToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/aspnetusers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/aspnetusers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportAspNetUsersToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/aspnetusers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/aspnetusers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetAspNetUsers(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<CP.Server.Models.CarPark.AspNetUser>> GetAspNetUsers(Query query)
        {
            return await GetAspNetUsers(filter: $"{query.Filter}", orderby: $"{query.OrderBy}", top: query.Top, skip: query.Skip, count: query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<CP.Server.Models.CarPark.AspNetUser>> GetAspNetUsers(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string), string apply = default(string))
        {
            var uri = new Uri(baseUri, $"AspNetUsers");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter: filter, top: top, skip: skip, orderby: orderby, expand: expand, select: select, count: count, apply: apply);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAspNetUsers(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<CP.Server.Models.CarPark.AspNetUser>>(response);
        }

        partial void OnCreateAspNetUser(HttpRequestMessage requestMessage);

        public async Task<CP.Server.Models.CarPark.AspNetUser> CreateAspNetUser(CP.Server.Models.CarPark.AspNetUser aspNetUser = default(CP.Server.Models.CarPark.AspNetUser))
        {
            var uri = new Uri(baseUri, $"AspNetUsers");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(aspNetUser), Encoding.UTF8, "application/json");

            OnCreateAspNetUser(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CP.Server.Models.CarPark.AspNetUser>(response);
        }

        partial void OnDeleteAspNetUser(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteAspNetUser(string id = default(string))
        {
            var uri = new Uri(baseUri, $"AspNetUsers('{Uri.EscapeDataString(id.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteAspNetUser(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetAspNetUserById(HttpRequestMessage requestMessage);

        public async Task<CP.Server.Models.CarPark.AspNetUser> GetAspNetUserById(string expand = default(string), string id = default(string))
        {
            var uri = new Uri(baseUri, $"AspNetUsers('{Uri.EscapeDataString(id.Trim().Replace("'", "''"))}')");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter: null, top: null, skip: null, orderby: null, expand: expand, select: null, count: null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAspNetUserById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CP.Server.Models.CarPark.AspNetUser>(response);
        }

        partial void OnUpdateAspNetUser(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> UpdateAspNetUser(string id = default(string), CP.Server.Models.CarPark.AspNetUser aspNetUser = default(CP.Server.Models.CarPark.AspNetUser))
        {
            var uri = new Uri(baseUri, $"AspNetUsers('{Uri.EscapeDataString(id.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(aspNetUser), Encoding.UTF8, "application/json");

            OnUpdateAspNetUser(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportAspNetUserTokensToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/aspnetusertokens/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/aspnetusertokens/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportAspNetUserTokensToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/aspnetusertokens/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/aspnetusertokens/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetAspNetUserTokens(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<CP.Server.Models.CarPark.AspNetUserToken>> GetAspNetUserTokens(Query query)
        {
            return await GetAspNetUserTokens(filter: $"{query.Filter}", orderby: $"{query.OrderBy}", top: query.Top, skip: query.Skip, count: query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<CP.Server.Models.CarPark.AspNetUserToken>> GetAspNetUserTokens(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string), string apply = default(string))
        {
            var uri = new Uri(baseUri, $"AspNetUserTokens");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter: filter, top: top, skip: skip, orderby: orderby, expand: expand, select: select, count: count, apply: apply);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAspNetUserTokens(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<CP.Server.Models.CarPark.AspNetUserToken>>(response);
        }

        partial void OnCreateAspNetUserToken(HttpRequestMessage requestMessage);

        public async Task<CP.Server.Models.CarPark.AspNetUserToken> CreateAspNetUserToken(CP.Server.Models.CarPark.AspNetUserToken aspNetUserToken = default(CP.Server.Models.CarPark.AspNetUserToken))
        {
            var uri = new Uri(baseUri, $"AspNetUserTokens");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(aspNetUserToken), Encoding.UTF8, "application/json");

            OnCreateAspNetUserToken(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CP.Server.Models.CarPark.AspNetUserToken>(response);
        }

        partial void OnDeleteAspNetUserToken(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteAspNetUserToken(string userId = default(string), string loginProvider = default(string), string name = default(string))
        {
            var uri = new Uri(baseUri, $"AspNetUserTokens(UserId='{Uri.EscapeDataString(userId.Trim().Replace("'", "''"))}',LoginProvider='{Uri.EscapeDataString(loginProvider.Trim().Replace("'", "''"))}',Name='{Uri.EscapeDataString(name.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteAspNetUserToken(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetAspNetUserTokenByUserIdAndLoginProviderAndName(HttpRequestMessage requestMessage);

        public async Task<CP.Server.Models.CarPark.AspNetUserToken> GetAspNetUserTokenByUserIdAndLoginProviderAndName(string expand = default(string), string userId = default(string), string loginProvider = default(string), string name = default(string))
        {
            var uri = new Uri(baseUri, $"AspNetUserTokens(UserId='{Uri.EscapeDataString(userId.Trim().Replace("'", "''"))}',LoginProvider='{Uri.EscapeDataString(loginProvider.Trim().Replace("'", "''"))}',Name='{Uri.EscapeDataString(name.Trim().Replace("'", "''"))}')");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter: null, top: null, skip: null, orderby: null, expand: expand, select: null, count: null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAspNetUserTokenByUserIdAndLoginProviderAndName(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CP.Server.Models.CarPark.AspNetUserToken>(response);
        }

        partial void OnUpdateAspNetUserToken(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> UpdateAspNetUserToken(string userId = default(string), string loginProvider = default(string), string name = default(string), CP.Server.Models.CarPark.AspNetUserToken aspNetUserToken = default(CP.Server.Models.CarPark.AspNetUserToken))
        {
            var uri = new Uri(baseUri, $"AspNetUserTokens(UserId='{Uri.EscapeDataString(userId.Trim().Replace("'", "''"))}',LoginProvider='{Uri.EscapeDataString(loginProvider.Trim().Replace("'", "''"))}',Name='{Uri.EscapeDataString(name.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(aspNetUserToken), Encoding.UTF8, "application/json");

            OnUpdateAspNetUserToken(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async Task<List<CP.Server.DTO.VehicleDto>> GetVehiclesAsync(string? type = null)
        {
            var query = string.IsNullOrEmpty(type) ? "" : $"?type={Uri.EscapeDataString(type)}";
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/vehicles{query}");
            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<CP.Server.DTO.VehicleDto>>() ?? new();
        }

        public async Task<List<CP.Server.DTO.TripRequestDto>> GetMyTripRequestsAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "api/triprequests/my");
            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<CP.Server.DTO.TripRequestDto>>() ?? new();
        }

        public async Task CreateTripRequestAsync(CP.Server.DTO.CreateTripRequestDto dto)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "api/triprequests")
            {
                Content = JsonContent.Create(dto)
            };
            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

        public async Task<List<AdminVehicleDto>> GetAdminVehiclesAsync(string? search = null)
        {
            var query = string.IsNullOrEmpty(search) ? "" : $"?search={Uri.EscapeDataString(search)}";
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/admin/vehicles{query}");
            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<AdminVehicleDto>>() ?? new();
        }

        public async Task CreateVehicleAsync(CreateVehicleDto dto)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "api/admin/vehicles")
            {
                Content = JsonContent.Create(dto)
            };
            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateVehicleAsync(int id, CreateVehicleDto dto)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"api/admin/vehicles/{id}")
            {
                Content = JsonContent.Create(dto)
            };
            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteVehicleAsync(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"api/admin/vehicles/{id}");
            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeactivateVehicleAsync(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"api/admin/vehicles/{id}/deactivate");
            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

        public async Task<List<AvailableDriverDto>> GetAvailableDriversAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "api/admin/trip-requests/drivers");
            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<AvailableDriverDto>>() ?? new();
        }

        public async Task<List<AdminDriverDto>> GetAdminDriversAsync(string? search = null)
        {
            var query = string.IsNullOrEmpty(search) ? "" : $"?search={Uri.EscapeDataString(search)}";
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/admin/drivers{query}");
            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<AdminDriverDto>>() ?? new();
        }

        public async Task CreateDriverAsync(CreateDriverDto dto)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "api/admin/drivers")
            {
                Content = JsonContent.Create(dto)
            };
            var response = await httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                // Читаем тело ответа с ошибкой до того как выбросить исключение
                var errorContent = await response.Content.ReadAsStringAsync();
                
                try
                {
                    // Пытаемся распарсить ошибку с сервера (формат { "error": "текст" })
                    var errorObj = JsonSerializer.Deserialize<Dictionary<string, string>>(errorContent);
                    if (errorObj != null && errorObj.TryGetValue("error", out var errorMessage))
                    {
                        throw new Exception(errorMessage);
                    }
                }
                catch (JsonException)
                {
                    throw new Exception($"Ошибка {response.StatusCode}: {errorContent}");
                }
                
                throw new Exception($"Ошибка сервера: {response.StatusCode}");
            }
        }

        public async Task UpdateDriverAsync(int id, CreateDriverDto dto)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"api/admin/drivers/{id}")
            {
                Content = JsonContent.Create(dto)
            };
            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeactivateDriverAsync(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"api/admin/drivers/{id}/deactivate");
            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

        public async Task<DriverVehicleDto?> GetDriverVehicleAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "api/driver/trips/vehicle");
            var response = await httpClient.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<DriverVehicleDto>();
        }

        public async Task<AdminDashboardDto> GetAdminDashboardStatsAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "api/admin/Dashboard/stats");
            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<AdminDashboardDto>() ?? new AdminDashboardDto();
        }

        public async Task RejectTripRequestAsync(int requestId, RejectTripRequestDto dto)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"api/admin/trip-requests/{requestId}/reject")
            {
                Content = JsonContent.Create(dto)
            };
            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

        public async Task AssignTripRequestAsync(int requestId, AssignTripDto dto)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"api/admin/trip-requests/{requestId}/assign")
            {
                Content = JsonContent.Create(dto)
            };
            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

        public async Task<List<TripDto>> GetAllTripsAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "api/admin/trips");
            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<TripDto>>() ?? new();
        }

        public async Task<List<string>> GetTripsDriversAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "api/admin/trips/drivers");
            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<string>>() ?? new();
        }

        public async Task<List<string>> GetTripsVehiclesAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "api/admin/trips/vehicles");
            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<string>>() ?? new();
        }

        public async Task ForceCompleteTripAsync(int tripId, string comment)
        {
            var dto = new { Comment = comment };
            var request = new HttpRequestMessage(HttpMethod.Put, $"api/admin/trips/{tripId}/force-complete")
            {
                Content = JsonContent.Create(dto)
            };
            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

        public async Task CancelTripAsync(int tripId)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"api/admin/trips/{tripId}/cancel");
            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

        public async Task<List<EmployeeDto>> GetEmployeesAsync(string? search = null)
        {
            var query = string.IsNullOrEmpty(search) ? "" : $"?search={Uri.EscapeDataString(search)}";
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/admin/employees{query}");
            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<EmployeeDto>>() ?? new();
        }

        public async Task<EmployeeDto?> GetEmployeeByIdAsync(string userId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/admin/employees/{userId}");
            var response = await httpClient.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound) return null;
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<EmployeeDto>();
        }

        public async Task DeactivateEmployeeAsync(string userId)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"api/admin/employees/{userId}/deactivate");
            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

        public async Task<DriverDashboardDto> GetDriverDashboardAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "api/driver/trips/dashboard");
            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<DriverDashboardDto>() ?? new();
        }

        public async Task StartTripAsync(int tripId, decimal startOdometer)
        {
            var dto = new { StartOdometer = startOdometer };
            var request = new HttpRequestMessage(HttpMethod.Put, $"api/driver/trips/{tripId}/start")
            {
                Content = JsonContent.Create(dto)
            };
            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

        public async Task EndTripAsync(int tripId, decimal endOdometer)
        {
            var dto = new { EndOdometer = endOdometer };
            var request = new HttpRequestMessage(HttpMethod.Put, $"api/driver/trips/{tripId}/end")
            {
                Content = JsonContent.Create(dto)
            };
            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

        public async Task<EmployeeDashboardDto> GetEmployeeDashboardAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "api/trip-requests/dashboard");
            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<EmployeeDashboardDto>() ?? new();
        }

        public async Task<List<UserLookupDto>> GetAvailableUsersAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "api/admin/drivers/users");
            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<UserLookupDto>>() ?? new();
        }

        public async Task<List<UserLookupDto>> GetAllUsersAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "api/admin/drivers/all-users");
            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<UserLookupDto>>() ?? new();
        }
        public async Task RestoreTripAsync(int tripId)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"api/admin/trips/{tripId}/restore");
            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

        public async Task<DriverProfileDto> GetDriverProfileAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "api/profile/driver");
            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<DriverProfileDto>() ?? new DriverProfileDto();
        }

        public async Task UpdateDriverProfileAsync(DriverProfileDto dto)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, "api/profile/driver")
            {
                Content = JsonContent.Create(dto)
            };
            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

        public async Task ChangeDriverPasswordAsync(string oldPassword, string newPassword)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "api/profile/change-password")
            {
                Content = JsonContent.Create(new { OldPassword = oldPassword, NewPassword = newPassword })  // ← заглавные буквы
            };
            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

        private async Task<string> GetErrorMessageAsync(HttpResponseMessage response)
        {
            try
            {
                var content = await response.Content.ReadAsStringAsync();
                var error = JsonSerializer.Deserialize<Dictionary<string, string>>(content);
                return error?.GetValueOrDefault("error") ?? content;
            }
            catch
            {
                return response.ReasonPhrase ?? "Неизвестная ошибка";
            }
        }

     
    }
}