
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
using Radzen;

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


        public async System.Threading.Tasks.Task ExportDriversToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/drivers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/drivers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportDriversToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/drivers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/drivers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetDrivers(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<CP.Server.Models.CarPark.Driver>> GetDrivers(Query query)
        {
            return await GetDrivers(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<CP.Server.Models.CarPark.Driver>> GetDrivers(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string), string apply = default(string))
        {
            var uri = new Uri(baseUri, $"Drivers");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count, apply:apply);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetDrivers(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<CP.Server.Models.CarPark.Driver>>(response);
        }

        partial void OnCreateDriver(HttpRequestMessage requestMessage);

        public async Task<CP.Server.Models.CarPark.Driver> CreateDriver(CP.Server.Models.CarPark.Driver driver = default(CP.Server.Models.CarPark.Driver))
        {
            var uri = new Uri(baseUri, $"Drivers");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(driver), Encoding.UTF8, "application/json");

            OnCreateDriver(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CP.Server.Models.CarPark.Driver>(response);
        }

        partial void OnDeleteDriver(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteDriver(int driverId = default(int))
        {
            var uri = new Uri(baseUri, $"Drivers({driverId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteDriver(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetDriverByDriverId(HttpRequestMessage requestMessage);

        public async Task<CP.Server.Models.CarPark.Driver> GetDriverByDriverId(string expand = default(string), int driverId = default(int))
        {
            var uri = new Uri(baseUri, $"Drivers({driverId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetDriverByDriverId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CP.Server.Models.CarPark.Driver>(response);
        }

        partial void OnUpdateDriver(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateDriver(int driverId = default(int), CP.Server.Models.CarPark.Driver driver = default(CP.Server.Models.CarPark.Driver))
        {
            var uri = new Uri(baseUri, $"Drivers({driverId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(driver), Encoding.UTF8, "application/json");

            OnUpdateDriver(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportTripRequestsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/triprequests/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/triprequests/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportTripRequestsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/triprequests/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/triprequests/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetTripRequests(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<CP.Server.Models.CarPark.TripRequest>> GetTripRequests(Query query)
        {
            return await GetTripRequests(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<CP.Server.Models.CarPark.TripRequest>> GetTripRequests(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string), string apply = default(string))
        {
            var uri = new Uri(baseUri, $"TripRequests");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count, apply:apply);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTripRequests(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<CP.Server.Models.CarPark.TripRequest>>(response);
        }

        partial void OnCreateTripRequest(HttpRequestMessage requestMessage);

        public async Task<CP.Server.Models.CarPark.TripRequest> CreateTripRequest(CP.Server.Models.CarPark.TripRequest tripRequest = default(CP.Server.Models.CarPark.TripRequest))
        {
            var uri = new Uri(baseUri, $"TripRequests");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(tripRequest), Encoding.UTF8, "application/json");

            OnCreateTripRequest(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CP.Server.Models.CarPark.TripRequest>(response);
        }

        partial void OnDeleteTripRequest(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteTripRequest(int requestId = default(int))
        {
            var uri = new Uri(baseUri, $"TripRequests({requestId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteTripRequest(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetTripRequestByRequestId(HttpRequestMessage requestMessage);

        public async Task<CP.Server.Models.CarPark.TripRequest> GetTripRequestByRequestId(string expand = default(string), int requestId = default(int))
        {
            var uri = new Uri(baseUri, $"TripRequests({requestId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTripRequestByRequestId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CP.Server.Models.CarPark.TripRequest>(response);
        }

        partial void OnUpdateTripRequest(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateTripRequest(int requestId = default(int), CP.Server.Models.CarPark.TripRequest tripRequest = default(CP.Server.Models.CarPark.TripRequest))
        {
            var uri = new Uri(baseUri, $"TripRequests({requestId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(tripRequest), Encoding.UTF8, "application/json");

            OnUpdateTripRequest(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportTripsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/trips/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/trips/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportTripsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/trips/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/trips/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetTrips(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<CP.Server.Models.CarPark.Trip>> GetTrips(Query query)
        {
            return await GetTrips(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<CP.Server.Models.CarPark.Trip>> GetTrips(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string), string apply = default(string))
        {
            var uri = new Uri(baseUri, $"Trips");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count, apply:apply);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTrips(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<CP.Server.Models.CarPark.Trip>>(response);
        }

        partial void OnCreateTrip(HttpRequestMessage requestMessage);

        public async Task<CP.Server.Models.CarPark.Trip> CreateTrip(CP.Server.Models.CarPark.Trip trip = default(CP.Server.Models.CarPark.Trip))
        {
            var uri = new Uri(baseUri, $"Trips");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(trip), Encoding.UTF8, "application/json");

            OnCreateTrip(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CP.Server.Models.CarPark.Trip>(response);
        }

        partial void OnDeleteTrip(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteTrip(int tripId = default(int))
        {
            var uri = new Uri(baseUri, $"Trips({tripId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteTrip(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetTripByTripId(HttpRequestMessage requestMessage);

        public async Task<CP.Server.Models.CarPark.Trip> GetTripByTripId(string expand = default(string), int tripId = default(int))
        {
            var uri = new Uri(baseUri, $"Trips({tripId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTripByTripId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CP.Server.Models.CarPark.Trip>(response);
        }

        partial void OnUpdateTrip(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateTrip(int tripId = default(int), CP.Server.Models.CarPark.Trip trip = default(CP.Server.Models.CarPark.Trip))
        {
            var uri = new Uri(baseUri, $"Trips({tripId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(trip), Encoding.UTF8, "application/json");

            OnUpdateTrip(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportUsersToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/users/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/users/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportUsersToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/users/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/users/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetUsers(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<CP.Server.Models.CarPark.User>> GetUsers(Query query)
        {
            return await GetUsers(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<CP.Server.Models.CarPark.User>> GetUsers(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string), string apply = default(string))
        {
            var uri = new Uri(baseUri, $"Users");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count, apply:apply);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetUsers(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<CP.Server.Models.CarPark.User>>(response);
        }

        partial void OnCreateUser(HttpRequestMessage requestMessage);

        public async Task<CP.Server.Models.CarPark.User> CreateUser(CP.Server.Models.CarPark.User user = default(CP.Server.Models.CarPark.User))
        {
            var uri = new Uri(baseUri, $"Users");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(user), Encoding.UTF8, "application/json");

            OnCreateUser(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CP.Server.Models.CarPark.User>(response);
        }

        partial void OnDeleteUser(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteUser(int userId = default(int))
        {
            var uri = new Uri(baseUri, $"Users({userId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteUser(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetUserByUserId(HttpRequestMessage requestMessage);

        public async Task<CP.Server.Models.CarPark.User> GetUserByUserId(string expand = default(string), int userId = default(int))
        {
            var uri = new Uri(baseUri, $"Users({userId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetUserByUserId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CP.Server.Models.CarPark.User>(response);
        }

        partial void OnUpdateUser(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateUser(int userId = default(int), CP.Server.Models.CarPark.User user = default(CP.Server.Models.CarPark.User))
        {
            var uri = new Uri(baseUri, $"Users({userId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(user), Encoding.UTF8, "application/json");

            OnUpdateUser(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportVehiclesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/vehicles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/vehicles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportVehiclesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/vehicles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/vehicles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetVehicles(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<CP.Server.Models.CarPark.Vehicle>> GetVehicles(Query query)
        {
            return await GetVehicles(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<CP.Server.Models.CarPark.Vehicle>> GetVehicles(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string), string apply = default(string))
        {
            var uri = new Uri(baseUri, $"Vehicles");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count, apply:apply);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetVehicles(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<CP.Server.Models.CarPark.Vehicle>>(response);
        }

        partial void OnCreateVehicle(HttpRequestMessage requestMessage);

        public async Task<CP.Server.Models.CarPark.Vehicle> CreateVehicle(CP.Server.Models.CarPark.Vehicle vehicle = default(CP.Server.Models.CarPark.Vehicle))
        {
            var uri = new Uri(baseUri, $"Vehicles");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(vehicle), Encoding.UTF8, "application/json");

            OnCreateVehicle(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CP.Server.Models.CarPark.Vehicle>(response);
        }

        partial void OnDeleteVehicle(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteVehicle(int vehicleId = default(int))
        {
            var uri = new Uri(baseUri, $"Vehicles({vehicleId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteVehicle(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetVehicleByVehicleId(HttpRequestMessage requestMessage);

        public async Task<CP.Server.Models.CarPark.Vehicle> GetVehicleByVehicleId(string expand = default(string), int vehicleId = default(int))
        {
            var uri = new Uri(baseUri, $"Vehicles({vehicleId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetVehicleByVehicleId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CP.Server.Models.CarPark.Vehicle>(response);
        }

        partial void OnUpdateVehicle(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateVehicle(int vehicleId = default(int), CP.Server.Models.CarPark.Vehicle vehicle = default(CP.Server.Models.CarPark.Vehicle))
        {
            var uri = new Uri(baseUri, $"Vehicles({vehicleId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(vehicle), Encoding.UTF8, "application/json");

            OnUpdateVehicle(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportVDriversToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/vdrivers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/vdrivers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportVDriversToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/vdrivers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/vdrivers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetVDrivers(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<CP.Server.Models.CarPark.VDriver>> GetVDrivers(Query query)
        {
            return await GetVDrivers(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<CP.Server.Models.CarPark.VDriver>> GetVDrivers(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string), string apply = default(string))
        {
            var uri = new Uri(baseUri, $"VDrivers");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count, apply:apply);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetVDrivers(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<CP.Server.Models.CarPark.VDriver>>(response);
        }

        public async System.Threading.Tasks.Task ExportVTripRequestsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/vtriprequests/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/vtriprequests/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportVTripRequestsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/vtriprequests/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/vtriprequests/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetVTripRequests(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<CP.Server.Models.CarPark.VTripRequest>> GetVTripRequests(Query query)
        {
            return await GetVTripRequests(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<CP.Server.Models.CarPark.VTripRequest>> GetVTripRequests(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string), string apply = default(string))
        {
            var uri = new Uri(baseUri, $"VTripRequests");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count, apply:apply);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetVTripRequests(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<CP.Server.Models.CarPark.VTripRequest>>(response);
        }

        public async System.Threading.Tasks.Task ExportVTripsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/vtrips/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/vtrips/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportVTripsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/vtrips/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/vtrips/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetVTrips(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<CP.Server.Models.CarPark.VTrip>> GetVTrips(Query query)
        {
            return await GetVTrips(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<CP.Server.Models.CarPark.VTrip>> GetVTrips(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string), string apply = default(string))
        {
            var uri = new Uri(baseUri, $"VTrips");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count, apply:apply);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetVTrips(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<CP.Server.Models.CarPark.VTrip>>(response);
        }

        public async System.Threading.Tasks.Task ExportVUsersToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/vusers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/vusers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportVUsersToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/vusers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/vusers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetVUsers(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<CP.Server.Models.CarPark.VUser>> GetVUsers(Query query)
        {
            return await GetVUsers(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<CP.Server.Models.CarPark.VUser>> GetVUsers(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string), string apply = default(string))
        {
            var uri = new Uri(baseUri, $"VUsers");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count, apply:apply);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetVUsers(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<CP.Server.Models.CarPark.VUser>>(response);
        }

        public async System.Threading.Tasks.Task ExportVVehiclesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/vvehicles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/vvehicles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportVVehiclesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/vvehicles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/vvehicles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetVVehicles(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<CP.Server.Models.CarPark.VVehicle>> GetVVehicles(Query query)
        {
            return await GetVVehicles(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<CP.Server.Models.CarPark.VVehicle>> GetVVehicles(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string), string apply = default(string))
        {
            var uri = new Uri(baseUri, $"VVehicles");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count, apply:apply);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetVVehicles(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<CP.Server.Models.CarPark.VVehicle>>(response);
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
            return await GetAspNetRoleClaims(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<CP.Server.Models.CarPark.AspNetRoleClaim>> GetAspNetRoleClaims(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string), string apply = default(string))
        {
            var uri = new Uri(baseUri, $"AspNetRoleClaims");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count, apply:apply);

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

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

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
            return await GetAspNetRoles(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<CP.Server.Models.CarPark.AspNetRole>> GetAspNetRoles(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string), string apply = default(string))
        {
            var uri = new Uri(baseUri, $"AspNetRoles");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count, apply:apply);

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

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

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
            return await GetAspNetUserClaims(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<CP.Server.Models.CarPark.AspNetUserClaim>> GetAspNetUserClaims(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string), string apply = default(string))
        {
            var uri = new Uri(baseUri, $"AspNetUserClaims");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count, apply:apply);

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

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

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
            return await GetAspNetUserLogins(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<CP.Server.Models.CarPark.AspNetUserLogin>> GetAspNetUserLogins(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string), string apply = default(string))
        {
            var uri = new Uri(baseUri, $"AspNetUserLogins");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count, apply:apply);

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

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

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
            return await GetAspNetUserRoles(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<CP.Server.Models.CarPark.AspNetUserRole>> GetAspNetUserRoles(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string), string apply = default(string))
        {
            var uri = new Uri(baseUri, $"AspNetUserRoles");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count, apply:apply);

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

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

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
            return await GetAspNetUsers(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<CP.Server.Models.CarPark.AspNetUser>> GetAspNetUsers(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string), string apply = default(string))
        {
            var uri = new Uri(baseUri, $"AspNetUsers");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count, apply:apply);

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

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

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
            return await GetAspNetUserTokens(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<CP.Server.Models.CarPark.AspNetUserToken>> GetAspNetUserTokens(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string), string apply = default(string))
        {
            var uri = new Uri(baseUri, $"AspNetUserTokens");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count, apply:apply);

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

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

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
    }
}