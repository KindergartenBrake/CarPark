using System;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Radzen;

using CP.Server.Data;

namespace CP.Server
{
    public partial class CarParkService
    {
        CarParkContext Context
        {
           get
           {
             return this.context;
           }
        }

        private readonly CarParkContext context;
        private readonly NavigationManager navigationManager;

        public CarParkService(CarParkContext context, NavigationManager navigationManager)
        {
            this.context = context;
            this.navigationManager = navigationManager;
        }

        public void Reset() => Context.ChangeTracker.Entries().Where(e => e.Entity != null).ToList().ForEach(e => e.State = EntityState.Detached);

        public void ApplyQuery<T>(ref IQueryable<T> items, Query query = null)
        {
            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }
        }


        public async Task ExportDriversToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/drivers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/drivers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportDriversToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/drivers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/drivers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnDriversRead(ref IQueryable<CP.Server.Models.CarPark.Driver> items);

        public async Task<IQueryable<CP.Server.Models.CarPark.Driver>> GetDrivers(Query query = null)
        {
            var items = Context.Drivers.AsQueryable();

            items = items.Include(i => i.User);
            items = items.Include(i => i.Vehicle);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnDriversRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnDriverGet(CP.Server.Models.CarPark.Driver item);
        partial void OnGetDriverByDriverId(ref IQueryable<CP.Server.Models.CarPark.Driver> items);


        public async Task<CP.Server.Models.CarPark.Driver> GetDriverByDriverId(int driverid)
        {
            var items = Context.Drivers
                              .AsNoTracking()
                              .Where(i => i.DriverId == driverid);

            items = items.Include(i => i.User);
            items = items.Include(i => i.Vehicle);
 
            OnGetDriverByDriverId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnDriverGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnDriverCreated(CP.Server.Models.CarPark.Driver item);
        partial void OnAfterDriverCreated(CP.Server.Models.CarPark.Driver item);

        public async Task<CP.Server.Models.CarPark.Driver> CreateDriver(CP.Server.Models.CarPark.Driver driver)
        {
            OnDriverCreated(driver);

            var existingItem = Context.Drivers
                              .Where(i => i.DriverId == driver.DriverId)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Drivers.Add(driver);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(driver).State = EntityState.Detached;
                throw;
            }

            OnAfterDriverCreated(driver);

            return driver;
        }

        public async Task<CP.Server.Models.CarPark.Driver> CancelDriverChanges(CP.Server.Models.CarPark.Driver item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnDriverUpdated(CP.Server.Models.CarPark.Driver item);
        partial void OnAfterDriverUpdated(CP.Server.Models.CarPark.Driver item);

        public async Task<CP.Server.Models.CarPark.Driver> UpdateDriver(int driverid, CP.Server.Models.CarPark.Driver driver)
        {
            OnDriverUpdated(driver);

            var itemToUpdate = Context.Drivers
                              .Where(i => i.DriverId == driver.DriverId)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(driver);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterDriverUpdated(driver);

            return driver;
        }

        partial void OnDriverDeleted(CP.Server.Models.CarPark.Driver item);
        partial void OnAfterDriverDeleted(CP.Server.Models.CarPark.Driver item);

        public async Task<CP.Server.Models.CarPark.Driver> DeleteDriver(int driverid)
        {
            var itemToDelete = Context.Drivers
                              .Where(i => i.DriverId == driverid)
                              .Include(i => i.Vehicles)
                              .Include(i => i.TripRequests)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnDriverDeleted(itemToDelete);


            Context.Drivers.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterDriverDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportTripRequestsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/triprequests/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/triprequests/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportTripRequestsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/triprequests/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/triprequests/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnTripRequestsRead(ref IQueryable<CP.Server.Models.CarPark.TripRequest> items);

        public async Task<IQueryable<CP.Server.Models.CarPark.TripRequest>> GetTripRequests(Query query = null)
        {
            var items = Context.TripRequests.AsQueryable();

            items = items.Include(i => i.Driver);
            items = items.Include(i => i.User);
            items = items.Include(i => i.Vehicle);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnTripRequestsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnTripRequestGet(CP.Server.Models.CarPark.TripRequest item);
        partial void OnGetTripRequestByRequestId(ref IQueryable<CP.Server.Models.CarPark.TripRequest> items);


        public async Task<CP.Server.Models.CarPark.TripRequest> GetTripRequestByRequestId(int requestid)
        {
            var items = Context.TripRequests
                              .AsNoTracking()
                              .Where(i => i.RequestId == requestid);

            items = items.Include(i => i.Driver);
            items = items.Include(i => i.User);
            items = items.Include(i => i.Vehicle);
 
            OnGetTripRequestByRequestId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnTripRequestGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnTripRequestCreated(CP.Server.Models.CarPark.TripRequest item);
        partial void OnAfterTripRequestCreated(CP.Server.Models.CarPark.TripRequest item);

        public async Task<CP.Server.Models.CarPark.TripRequest> CreateTripRequest(CP.Server.Models.CarPark.TripRequest triprequest)
        {
            OnTripRequestCreated(triprequest);

            var existingItem = Context.TripRequests
                              .Where(i => i.RequestId == triprequest.RequestId)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.TripRequests.Add(triprequest);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(triprequest).State = EntityState.Detached;
                throw;
            }

            OnAfterTripRequestCreated(triprequest);

            return triprequest;
        }

        public async Task<CP.Server.Models.CarPark.TripRequest> CancelTripRequestChanges(CP.Server.Models.CarPark.TripRequest item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnTripRequestUpdated(CP.Server.Models.CarPark.TripRequest item);
        partial void OnAfterTripRequestUpdated(CP.Server.Models.CarPark.TripRequest item);

        public async Task<CP.Server.Models.CarPark.TripRequest> UpdateTripRequest(int requestid, CP.Server.Models.CarPark.TripRequest triprequest)
        {
            OnTripRequestUpdated(triprequest);

            var itemToUpdate = Context.TripRequests
                              .Where(i => i.RequestId == triprequest.RequestId)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(triprequest);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterTripRequestUpdated(triprequest);

            return triprequest;
        }

        partial void OnTripRequestDeleted(CP.Server.Models.CarPark.TripRequest item);
        partial void OnAfterTripRequestDeleted(CP.Server.Models.CarPark.TripRequest item);

        public async Task<CP.Server.Models.CarPark.TripRequest> DeleteTripRequest(int requestid)
        {
            var itemToDelete = Context.TripRequests
                              .Where(i => i.RequestId == requestid)
                              .Include(i => i.Trips)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnTripRequestDeleted(itemToDelete);


            Context.TripRequests.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterTripRequestDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportTripsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/trips/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/trips/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportTripsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/trips/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/trips/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnTripsRead(ref IQueryable<CP.Server.Models.CarPark.Trip> items);

        public async Task<IQueryable<CP.Server.Models.CarPark.Trip>> GetTrips(Query query = null)
        {
            var items = Context.Trips.AsQueryable();

            items = items.Include(i => i.TripRequest);
            items = items.Include(i => i.Vehicle);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnTripsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnTripGet(CP.Server.Models.CarPark.Trip item);
        partial void OnGetTripByTripId(ref IQueryable<CP.Server.Models.CarPark.Trip> items);


        public async Task<CP.Server.Models.CarPark.Trip> GetTripByTripId(int tripid)
        {
            var items = Context.Trips
                              .AsNoTracking()
                              .Where(i => i.TripId == tripid);

            items = items.Include(i => i.TripRequest);
            items = items.Include(i => i.Vehicle);
 
            OnGetTripByTripId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnTripGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnTripCreated(CP.Server.Models.CarPark.Trip item);
        partial void OnAfterTripCreated(CP.Server.Models.CarPark.Trip item);

        public async Task<CP.Server.Models.CarPark.Trip> CreateTrip(CP.Server.Models.CarPark.Trip trip)
        {
            OnTripCreated(trip);

            var existingItem = Context.Trips
                              .Where(i => i.TripId == trip.TripId)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Trips.Add(trip);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(trip).State = EntityState.Detached;
                throw;
            }

            OnAfterTripCreated(trip);

            return trip;
        }

        public async Task<CP.Server.Models.CarPark.Trip> CancelTripChanges(CP.Server.Models.CarPark.Trip item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnTripUpdated(CP.Server.Models.CarPark.Trip item);
        partial void OnAfterTripUpdated(CP.Server.Models.CarPark.Trip item);

        public async Task<CP.Server.Models.CarPark.Trip> UpdateTrip(int tripid, CP.Server.Models.CarPark.Trip trip)
        {
            OnTripUpdated(trip);

            var itemToUpdate = Context.Trips
                              .Where(i => i.TripId == trip.TripId)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(trip);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterTripUpdated(trip);

            return trip;
        }

        partial void OnTripDeleted(CP.Server.Models.CarPark.Trip item);
        partial void OnAfterTripDeleted(CP.Server.Models.CarPark.Trip item);

        public async Task<CP.Server.Models.CarPark.Trip> DeleteTrip(int tripid)
        {
            var itemToDelete = Context.Trips
                              .Where(i => i.TripId == tripid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnTripDeleted(itemToDelete);


            Context.Trips.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterTripDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportUsersToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/users/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/users/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportUsersToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/users/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/users/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnUsersRead(ref IQueryable<CP.Server.Models.CarPark.User> items);

        public async Task<IQueryable<CP.Server.Models.CarPark.User>> GetUsers(Query query = null)
        {
            var items = Context.Users.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnUsersRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnUserGet(CP.Server.Models.CarPark.User item);
        partial void OnGetUserByUserId(ref IQueryable<CP.Server.Models.CarPark.User> items);


        public async Task<CP.Server.Models.CarPark.User> GetUserByUserId(int userid)
        {
            var items = Context.Users
                              .AsNoTracking()
                              .Where(i => i.UserId == userid);

 
            OnGetUserByUserId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnUserGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnUserCreated(CP.Server.Models.CarPark.User item);
        partial void OnAfterUserCreated(CP.Server.Models.CarPark.User item);

        public async Task<CP.Server.Models.CarPark.User> CreateUser(CP.Server.Models.CarPark.User user)
        {
            OnUserCreated(user);

            var existingItem = Context.Users
                              .Where(i => i.UserId == user.UserId)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Users.Add(user);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(user).State = EntityState.Detached;
                throw;
            }

            OnAfterUserCreated(user);

            return user;
        }

        public async Task<CP.Server.Models.CarPark.User> CancelUserChanges(CP.Server.Models.CarPark.User item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnUserUpdated(CP.Server.Models.CarPark.User item);
        partial void OnAfterUserUpdated(CP.Server.Models.CarPark.User item);

        public async Task<CP.Server.Models.CarPark.User> UpdateUser(int userid, CP.Server.Models.CarPark.User user)
        {
            OnUserUpdated(user);

            var itemToUpdate = Context.Users
                              .Where(i => i.UserId == user.UserId)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(user);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterUserUpdated(user);

            return user;
        }

        partial void OnUserDeleted(CP.Server.Models.CarPark.User item);
        partial void OnAfterUserDeleted(CP.Server.Models.CarPark.User item);

        public async Task<CP.Server.Models.CarPark.User> DeleteUser(int userid)
        {
            var itemToDelete = Context.Users
                              .Where(i => i.UserId == userid)
                              .Include(i => i.TripRequests)
                              .Include(i => i.Drivers)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnUserDeleted(itemToDelete);


            Context.Users.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterUserDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportVehiclesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/vehicles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/vehicles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportVehiclesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/vehicles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/vehicles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnVehiclesRead(ref IQueryable<CP.Server.Models.CarPark.Vehicle> items);

        public async Task<IQueryable<CP.Server.Models.CarPark.Vehicle>> GetVehicles(Query query = null)
        {
            var items = Context.Vehicles.AsQueryable();

            items = items.Include(i => i.Driver);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnVehiclesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnVehicleGet(CP.Server.Models.CarPark.Vehicle item);
        partial void OnGetVehicleByVehicleId(ref IQueryable<CP.Server.Models.CarPark.Vehicle> items);


        public async Task<CP.Server.Models.CarPark.Vehicle> GetVehicleByVehicleId(int vehicleid)
        {
            var items = Context.Vehicles
                              .AsNoTracking()
                              .Where(i => i.VehicleId == vehicleid);

            items = items.Include(i => i.Driver);
 
            OnGetVehicleByVehicleId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnVehicleGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnVehicleCreated(CP.Server.Models.CarPark.Vehicle item);
        partial void OnAfterVehicleCreated(CP.Server.Models.CarPark.Vehicle item);

        public async Task<CP.Server.Models.CarPark.Vehicle> CreateVehicle(CP.Server.Models.CarPark.Vehicle vehicle)
        {
            OnVehicleCreated(vehicle);

            var existingItem = Context.Vehicles
                              .Where(i => i.VehicleId == vehicle.VehicleId)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Vehicles.Add(vehicle);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(vehicle).State = EntityState.Detached;
                throw;
            }

            OnAfterVehicleCreated(vehicle);

            return vehicle;
        }

        public async Task<CP.Server.Models.CarPark.Vehicle> CancelVehicleChanges(CP.Server.Models.CarPark.Vehicle item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnVehicleUpdated(CP.Server.Models.CarPark.Vehicle item);
        partial void OnAfterVehicleUpdated(CP.Server.Models.CarPark.Vehicle item);

        public async Task<CP.Server.Models.CarPark.Vehicle> UpdateVehicle(int vehicleid, CP.Server.Models.CarPark.Vehicle vehicle)
        {
            OnVehicleUpdated(vehicle);

            var itemToUpdate = Context.Vehicles
                              .Where(i => i.VehicleId == vehicle.VehicleId)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(vehicle);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterVehicleUpdated(vehicle);

            return vehicle;
        }

        partial void OnVehicleDeleted(CP.Server.Models.CarPark.Vehicle item);
        partial void OnAfterVehicleDeleted(CP.Server.Models.CarPark.Vehicle item);

        public async Task<CP.Server.Models.CarPark.Vehicle> DeleteVehicle(int vehicleid)
        {
            var itemToDelete = Context.Vehicles
                              .Where(i => i.VehicleId == vehicleid)
                              .Include(i => i.Trips)
                              .Include(i => i.TripRequests)
                              .Include(i => i.Drivers)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnVehicleDeleted(itemToDelete);


            Context.Vehicles.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterVehicleDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportVDriversToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/vdrivers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/vdrivers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportVDriversToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/vdrivers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/vdrivers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnVDriversRead(ref IQueryable<CP.Server.Models.CarPark.VDriver> items);

        public async Task<IQueryable<CP.Server.Models.CarPark.VDriver>> GetVDrivers(Query query = null)
        {
            var items = Context.VDrivers.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnVDriversRead(ref items);

            return await Task.FromResult(items);
        }

        public async Task ExportVTripRequestsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/vtriprequests/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/vtriprequests/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportVTripRequestsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/vtriprequests/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/vtriprequests/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnVTripRequestsRead(ref IQueryable<CP.Server.Models.CarPark.VTripRequest> items);

        public async Task<IQueryable<CP.Server.Models.CarPark.VTripRequest>> GetVTripRequests(Query query = null)
        {
            var items = Context.VTripRequests.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnVTripRequestsRead(ref items);

            return await Task.FromResult(items);
        }

        public async Task ExportVTripsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/vtrips/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/vtrips/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportVTripsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/vtrips/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/vtrips/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnVTripsRead(ref IQueryable<CP.Server.Models.CarPark.VTrip> items);

        public async Task<IQueryable<CP.Server.Models.CarPark.VTrip>> GetVTrips(Query query = null)
        {
            var items = Context.VTrips.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnVTripsRead(ref items);

            return await Task.FromResult(items);
        }

        public async Task ExportVUsersToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/vusers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/vusers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportVUsersToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/vusers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/vusers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnVUsersRead(ref IQueryable<CP.Server.Models.CarPark.VUser> items);

        public async Task<IQueryable<CP.Server.Models.CarPark.VUser>> GetVUsers(Query query = null)
        {
            var items = Context.VUsers.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnVUsersRead(ref items);

            return await Task.FromResult(items);
        }

        public async Task ExportVVehiclesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/vvehicles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/vvehicles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportVVehiclesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/vvehicles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/vvehicles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnVVehiclesRead(ref IQueryable<CP.Server.Models.CarPark.VVehicle> items);

        public async Task<IQueryable<CP.Server.Models.CarPark.VVehicle>> GetVVehicles(Query query = null)
        {
            var items = Context.VVehicles.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnVVehiclesRead(ref items);

            return await Task.FromResult(items);
        }

        public async Task ExportAspNetRoleClaimsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/aspnetroleclaims/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/aspnetroleclaims/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAspNetRoleClaimsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/aspnetroleclaims/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/aspnetroleclaims/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAspNetRoleClaimsRead(ref IQueryable<CP.Server.Models.CarPark.AspNetRoleClaim> items);

        public async Task<IQueryable<CP.Server.Models.CarPark.AspNetRoleClaim>> GetAspNetRoleClaims(Query query = null)
        {
            var items = Context.AspNetRoleClaims.AsQueryable();

            items = items.Include(i => i.AspNetRole);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnAspNetRoleClaimsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAspNetRoleClaimGet(CP.Server.Models.CarPark.AspNetRoleClaim item);
        partial void OnGetAspNetRoleClaimById(ref IQueryable<CP.Server.Models.CarPark.AspNetRoleClaim> items);


        public async Task<CP.Server.Models.CarPark.AspNetRoleClaim> GetAspNetRoleClaimById(int id)
        {
            var items = Context.AspNetRoleClaims
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.AspNetRole);
 
            OnGetAspNetRoleClaimById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnAspNetRoleClaimGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnAspNetRoleClaimCreated(CP.Server.Models.CarPark.AspNetRoleClaim item);
        partial void OnAfterAspNetRoleClaimCreated(CP.Server.Models.CarPark.AspNetRoleClaim item);

        public async Task<CP.Server.Models.CarPark.AspNetRoleClaim> CreateAspNetRoleClaim(CP.Server.Models.CarPark.AspNetRoleClaim aspnetroleclaim)
        {
            OnAspNetRoleClaimCreated(aspnetroleclaim);

            var existingItem = Context.AspNetRoleClaims
                              .Where(i => i.Id == aspnetroleclaim.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.AspNetRoleClaims.Add(aspnetroleclaim);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(aspnetroleclaim).State = EntityState.Detached;
                throw;
            }

            OnAfterAspNetRoleClaimCreated(aspnetroleclaim);

            return aspnetroleclaim;
        }

        public async Task<CP.Server.Models.CarPark.AspNetRoleClaim> CancelAspNetRoleClaimChanges(CP.Server.Models.CarPark.AspNetRoleClaim item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAspNetRoleClaimUpdated(CP.Server.Models.CarPark.AspNetRoleClaim item);
        partial void OnAfterAspNetRoleClaimUpdated(CP.Server.Models.CarPark.AspNetRoleClaim item);

        public async Task<CP.Server.Models.CarPark.AspNetRoleClaim> UpdateAspNetRoleClaim(int id, CP.Server.Models.CarPark.AspNetRoleClaim aspnetroleclaim)
        {
            OnAspNetRoleClaimUpdated(aspnetroleclaim);

            var itemToUpdate = Context.AspNetRoleClaims
                              .Where(i => i.Id == aspnetroleclaim.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(aspnetroleclaim);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterAspNetRoleClaimUpdated(aspnetroleclaim);

            return aspnetroleclaim;
        }

        partial void OnAspNetRoleClaimDeleted(CP.Server.Models.CarPark.AspNetRoleClaim item);
        partial void OnAfterAspNetRoleClaimDeleted(CP.Server.Models.CarPark.AspNetRoleClaim item);

        public async Task<CP.Server.Models.CarPark.AspNetRoleClaim> DeleteAspNetRoleClaim(int id)
        {
            var itemToDelete = Context.AspNetRoleClaims
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnAspNetRoleClaimDeleted(itemToDelete);


            Context.AspNetRoleClaims.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAspNetRoleClaimDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportAspNetRolesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/aspnetroles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/aspnetroles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAspNetRolesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/aspnetroles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/aspnetroles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAspNetRolesRead(ref IQueryable<CP.Server.Models.CarPark.AspNetRole> items);

        public async Task<IQueryable<CP.Server.Models.CarPark.AspNetRole>> GetAspNetRoles(Query query = null)
        {
            var items = Context.AspNetRoles.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnAspNetRolesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAspNetRoleGet(CP.Server.Models.CarPark.AspNetRole item);
        partial void OnGetAspNetRoleById(ref IQueryable<CP.Server.Models.CarPark.AspNetRole> items);


        public async Task<CP.Server.Models.CarPark.AspNetRole> GetAspNetRoleById(string id)
        {
            var items = Context.AspNetRoles
                              .AsNoTracking()
                              .Where(i => i.Id == id);

 
            OnGetAspNetRoleById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnAspNetRoleGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnAspNetRoleCreated(CP.Server.Models.CarPark.AspNetRole item);
        partial void OnAfterAspNetRoleCreated(CP.Server.Models.CarPark.AspNetRole item);

        public async Task<CP.Server.Models.CarPark.AspNetRole> CreateAspNetRole(CP.Server.Models.CarPark.AspNetRole aspnetrole)
        {
            OnAspNetRoleCreated(aspnetrole);

            var existingItem = Context.AspNetRoles
                              .Where(i => i.Id == aspnetrole.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.AspNetRoles.Add(aspnetrole);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(aspnetrole).State = EntityState.Detached;
                throw;
            }

            OnAfterAspNetRoleCreated(aspnetrole);

            return aspnetrole;
        }

        public async Task<CP.Server.Models.CarPark.AspNetRole> CancelAspNetRoleChanges(CP.Server.Models.CarPark.AspNetRole item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAspNetRoleUpdated(CP.Server.Models.CarPark.AspNetRole item);
        partial void OnAfterAspNetRoleUpdated(CP.Server.Models.CarPark.AspNetRole item);

        public async Task<CP.Server.Models.CarPark.AspNetRole> UpdateAspNetRole(string id, CP.Server.Models.CarPark.AspNetRole aspnetrole)
        {
            OnAspNetRoleUpdated(aspnetrole);

            var itemToUpdate = Context.AspNetRoles
                              .Where(i => i.Id == aspnetrole.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(aspnetrole);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterAspNetRoleUpdated(aspnetrole);

            return aspnetrole;
        }

        partial void OnAspNetRoleDeleted(CP.Server.Models.CarPark.AspNetRole item);
        partial void OnAfterAspNetRoleDeleted(CP.Server.Models.CarPark.AspNetRole item);

        public async Task<CP.Server.Models.CarPark.AspNetRole> DeleteAspNetRole(string id)
        {
            var itemToDelete = Context.AspNetRoles
                              .Where(i => i.Id == id)
                              .Include(i => i.AspNetRoleClaims)
                              .Include(i => i.AspNetUserRoles)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnAspNetRoleDeleted(itemToDelete);


            Context.AspNetRoles.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAspNetRoleDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportAspNetUserClaimsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/aspnetuserclaims/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/aspnetuserclaims/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAspNetUserClaimsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/aspnetuserclaims/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/aspnetuserclaims/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAspNetUserClaimsRead(ref IQueryable<CP.Server.Models.CarPark.AspNetUserClaim> items);

        public async Task<IQueryable<CP.Server.Models.CarPark.AspNetUserClaim>> GetAspNetUserClaims(Query query = null)
        {
            var items = Context.AspNetUserClaims.AsQueryable();

            items = items.Include(i => i.AspNetUser);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnAspNetUserClaimsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAspNetUserClaimGet(CP.Server.Models.CarPark.AspNetUserClaim item);
        partial void OnGetAspNetUserClaimById(ref IQueryable<CP.Server.Models.CarPark.AspNetUserClaim> items);


        public async Task<CP.Server.Models.CarPark.AspNetUserClaim> GetAspNetUserClaimById(int id)
        {
            var items = Context.AspNetUserClaims
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.AspNetUser);
 
            OnGetAspNetUserClaimById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnAspNetUserClaimGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnAspNetUserClaimCreated(CP.Server.Models.CarPark.AspNetUserClaim item);
        partial void OnAfterAspNetUserClaimCreated(CP.Server.Models.CarPark.AspNetUserClaim item);

        public async Task<CP.Server.Models.CarPark.AspNetUserClaim> CreateAspNetUserClaim(CP.Server.Models.CarPark.AspNetUserClaim aspnetuserclaim)
        {
            OnAspNetUserClaimCreated(aspnetuserclaim);

            var existingItem = Context.AspNetUserClaims
                              .Where(i => i.Id == aspnetuserclaim.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.AspNetUserClaims.Add(aspnetuserclaim);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(aspnetuserclaim).State = EntityState.Detached;
                throw;
            }

            OnAfterAspNetUserClaimCreated(aspnetuserclaim);

            return aspnetuserclaim;
        }

        public async Task<CP.Server.Models.CarPark.AspNetUserClaim> CancelAspNetUserClaimChanges(CP.Server.Models.CarPark.AspNetUserClaim item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAspNetUserClaimUpdated(CP.Server.Models.CarPark.AspNetUserClaim item);
        partial void OnAfterAspNetUserClaimUpdated(CP.Server.Models.CarPark.AspNetUserClaim item);

        public async Task<CP.Server.Models.CarPark.AspNetUserClaim> UpdateAspNetUserClaim(int id, CP.Server.Models.CarPark.AspNetUserClaim aspnetuserclaim)
        {
            OnAspNetUserClaimUpdated(aspnetuserclaim);

            var itemToUpdate = Context.AspNetUserClaims
                              .Where(i => i.Id == aspnetuserclaim.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(aspnetuserclaim);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterAspNetUserClaimUpdated(aspnetuserclaim);

            return aspnetuserclaim;
        }

        partial void OnAspNetUserClaimDeleted(CP.Server.Models.CarPark.AspNetUserClaim item);
        partial void OnAfterAspNetUserClaimDeleted(CP.Server.Models.CarPark.AspNetUserClaim item);

        public async Task<CP.Server.Models.CarPark.AspNetUserClaim> DeleteAspNetUserClaim(int id)
        {
            var itemToDelete = Context.AspNetUserClaims
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnAspNetUserClaimDeleted(itemToDelete);


            Context.AspNetUserClaims.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAspNetUserClaimDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportAspNetUserLoginsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/aspnetuserlogins/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/aspnetuserlogins/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAspNetUserLoginsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/aspnetuserlogins/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/aspnetuserlogins/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAspNetUserLoginsRead(ref IQueryable<CP.Server.Models.CarPark.AspNetUserLogin> items);

        public async Task<IQueryable<CP.Server.Models.CarPark.AspNetUserLogin>> GetAspNetUserLogins(Query query = null)
        {
            var items = Context.AspNetUserLogins.AsQueryable();

            items = items.Include(i => i.AspNetUser);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnAspNetUserLoginsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAspNetUserLoginGet(CP.Server.Models.CarPark.AspNetUserLogin item);
        partial void OnGetAspNetUserLoginByLoginProviderAndProviderKey(ref IQueryable<CP.Server.Models.CarPark.AspNetUserLogin> items);


        public async Task<CP.Server.Models.CarPark.AspNetUserLogin> GetAspNetUserLoginByLoginProviderAndProviderKey(string loginprovider, string providerkey)
        {
            var items = Context.AspNetUserLogins
                              .AsNoTracking()
                              .Where(i => i.LoginProvider == loginprovider && i.ProviderKey == providerkey);

            items = items.Include(i => i.AspNetUser);
 
            OnGetAspNetUserLoginByLoginProviderAndProviderKey(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnAspNetUserLoginGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnAspNetUserLoginCreated(CP.Server.Models.CarPark.AspNetUserLogin item);
        partial void OnAfterAspNetUserLoginCreated(CP.Server.Models.CarPark.AspNetUserLogin item);

        public async Task<CP.Server.Models.CarPark.AspNetUserLogin> CreateAspNetUserLogin(CP.Server.Models.CarPark.AspNetUserLogin aspnetuserlogin)
        {
            OnAspNetUserLoginCreated(aspnetuserlogin);

            var existingItem = Context.AspNetUserLogins
                              .Where(i => i.LoginProvider == aspnetuserlogin.LoginProvider && i.ProviderKey == aspnetuserlogin.ProviderKey)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.AspNetUserLogins.Add(aspnetuserlogin);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(aspnetuserlogin).State = EntityState.Detached;
                throw;
            }

            OnAfterAspNetUserLoginCreated(aspnetuserlogin);

            return aspnetuserlogin;
        }

        public async Task<CP.Server.Models.CarPark.AspNetUserLogin> CancelAspNetUserLoginChanges(CP.Server.Models.CarPark.AspNetUserLogin item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAspNetUserLoginUpdated(CP.Server.Models.CarPark.AspNetUserLogin item);
        partial void OnAfterAspNetUserLoginUpdated(CP.Server.Models.CarPark.AspNetUserLogin item);

        public async Task<CP.Server.Models.CarPark.AspNetUserLogin> UpdateAspNetUserLogin(string loginprovider, string providerkey, CP.Server.Models.CarPark.AspNetUserLogin aspnetuserlogin)
        {
            OnAspNetUserLoginUpdated(aspnetuserlogin);

            var itemToUpdate = Context.AspNetUserLogins
                              .Where(i => i.LoginProvider == aspnetuserlogin.LoginProvider && i.ProviderKey == aspnetuserlogin.ProviderKey)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(aspnetuserlogin);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterAspNetUserLoginUpdated(aspnetuserlogin);

            return aspnetuserlogin;
        }

        partial void OnAspNetUserLoginDeleted(CP.Server.Models.CarPark.AspNetUserLogin item);
        partial void OnAfterAspNetUserLoginDeleted(CP.Server.Models.CarPark.AspNetUserLogin item);

        public async Task<CP.Server.Models.CarPark.AspNetUserLogin> DeleteAspNetUserLogin(string loginprovider, string providerkey)
        {
            var itemToDelete = Context.AspNetUserLogins
                              .Where(i => i.LoginProvider == loginprovider && i.ProviderKey == providerkey)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnAspNetUserLoginDeleted(itemToDelete);


            Context.AspNetUserLogins.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAspNetUserLoginDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportAspNetUserRolesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/aspnetuserroles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/aspnetuserroles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAspNetUserRolesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/aspnetuserroles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/aspnetuserroles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAspNetUserRolesRead(ref IQueryable<CP.Server.Models.CarPark.AspNetUserRole> items);

        public async Task<IQueryable<CP.Server.Models.CarPark.AspNetUserRole>> GetAspNetUserRoles(Query query = null)
        {
            var items = Context.AspNetUserRoles.AsQueryable();

            items = items.Include(i => i.AspNetRole);
            items = items.Include(i => i.AspNetUser);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnAspNetUserRolesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAspNetUserRoleGet(CP.Server.Models.CarPark.AspNetUserRole item);
        partial void OnGetAspNetUserRoleByUserIdAndRoleId(ref IQueryable<CP.Server.Models.CarPark.AspNetUserRole> items);


        public async Task<CP.Server.Models.CarPark.AspNetUserRole> GetAspNetUserRoleByUserIdAndRoleId(string userid, string roleid)
        {
            var items = Context.AspNetUserRoles
                              .AsNoTracking()
                              .Where(i => i.UserId == userid && i.RoleId == roleid);

            items = items.Include(i => i.AspNetRole);
            items = items.Include(i => i.AspNetUser);
 
            OnGetAspNetUserRoleByUserIdAndRoleId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnAspNetUserRoleGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnAspNetUserRoleCreated(CP.Server.Models.CarPark.AspNetUserRole item);
        partial void OnAfterAspNetUserRoleCreated(CP.Server.Models.CarPark.AspNetUserRole item);

        public async Task<CP.Server.Models.CarPark.AspNetUserRole> CreateAspNetUserRole(CP.Server.Models.CarPark.AspNetUserRole aspnetuserrole)
        {
            OnAspNetUserRoleCreated(aspnetuserrole);

            var existingItem = Context.AspNetUserRoles
                              .Where(i => i.UserId == aspnetuserrole.UserId && i.RoleId == aspnetuserrole.RoleId)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.AspNetUserRoles.Add(aspnetuserrole);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(aspnetuserrole).State = EntityState.Detached;
                throw;
            }

            OnAfterAspNetUserRoleCreated(aspnetuserrole);

            return aspnetuserrole;
        }

        public async Task<CP.Server.Models.CarPark.AspNetUserRole> CancelAspNetUserRoleChanges(CP.Server.Models.CarPark.AspNetUserRole item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAspNetUserRoleUpdated(CP.Server.Models.CarPark.AspNetUserRole item);
        partial void OnAfterAspNetUserRoleUpdated(CP.Server.Models.CarPark.AspNetUserRole item);

        public async Task<CP.Server.Models.CarPark.AspNetUserRole> UpdateAspNetUserRole(string userid, string roleid, CP.Server.Models.CarPark.AspNetUserRole aspnetuserrole)
        {
            OnAspNetUserRoleUpdated(aspnetuserrole);

            var itemToUpdate = Context.AspNetUserRoles
                              .Where(i => i.UserId == aspnetuserrole.UserId && i.RoleId == aspnetuserrole.RoleId)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(aspnetuserrole);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterAspNetUserRoleUpdated(aspnetuserrole);

            return aspnetuserrole;
        }

        partial void OnAspNetUserRoleDeleted(CP.Server.Models.CarPark.AspNetUserRole item);
        partial void OnAfterAspNetUserRoleDeleted(CP.Server.Models.CarPark.AspNetUserRole item);

        public async Task<CP.Server.Models.CarPark.AspNetUserRole> DeleteAspNetUserRole(string userid, string roleid)
        {
            var itemToDelete = Context.AspNetUserRoles
                              .Where(i => i.UserId == userid && i.RoleId == roleid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnAspNetUserRoleDeleted(itemToDelete);


            Context.AspNetUserRoles.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAspNetUserRoleDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportAspNetUsersToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/aspnetusers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/aspnetusers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAspNetUsersToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/aspnetusers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/aspnetusers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAspNetUsersRead(ref IQueryable<CP.Server.Models.CarPark.AspNetUser> items);

        public async Task<IQueryable<CP.Server.Models.CarPark.AspNetUser>> GetAspNetUsers(Query query = null)
        {
            var items = Context.AspNetUsers.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnAspNetUsersRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAspNetUserGet(CP.Server.Models.CarPark.AspNetUser item);
        partial void OnGetAspNetUserById(ref IQueryable<CP.Server.Models.CarPark.AspNetUser> items);


        public async Task<CP.Server.Models.CarPark.AspNetUser> GetAspNetUserById(string id)
        {
            var items = Context.AspNetUsers
                              .AsNoTracking()
                              .Where(i => i.Id == id);

 
            OnGetAspNetUserById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnAspNetUserGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnAspNetUserCreated(CP.Server.Models.CarPark.AspNetUser item);
        partial void OnAfterAspNetUserCreated(CP.Server.Models.CarPark.AspNetUser item);

        public async Task<CP.Server.Models.CarPark.AspNetUser> CreateAspNetUser(CP.Server.Models.CarPark.AspNetUser aspnetuser)
        {
            OnAspNetUserCreated(aspnetuser);

            var existingItem = Context.AspNetUsers
                              .Where(i => i.Id == aspnetuser.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.AspNetUsers.Add(aspnetuser);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(aspnetuser).State = EntityState.Detached;
                throw;
            }

            OnAfterAspNetUserCreated(aspnetuser);

            return aspnetuser;
        }

        public async Task<CP.Server.Models.CarPark.AspNetUser> CancelAspNetUserChanges(CP.Server.Models.CarPark.AspNetUser item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAspNetUserUpdated(CP.Server.Models.CarPark.AspNetUser item);
        partial void OnAfterAspNetUserUpdated(CP.Server.Models.CarPark.AspNetUser item);

        public async Task<CP.Server.Models.CarPark.AspNetUser> UpdateAspNetUser(string id, CP.Server.Models.CarPark.AspNetUser aspnetuser)
        {
            OnAspNetUserUpdated(aspnetuser);

            var itemToUpdate = Context.AspNetUsers
                              .Where(i => i.Id == aspnetuser.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(aspnetuser);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterAspNetUserUpdated(aspnetuser);

            return aspnetuser;
        }

        partial void OnAspNetUserDeleted(CP.Server.Models.CarPark.AspNetUser item);
        partial void OnAfterAspNetUserDeleted(CP.Server.Models.CarPark.AspNetUser item);

        public async Task<CP.Server.Models.CarPark.AspNetUser> DeleteAspNetUser(string id)
        {
            var itemToDelete = Context.AspNetUsers
                              .Where(i => i.Id == id)
                              .Include(i => i.AspNetUserTokens)
                              .Include(i => i.AspNetUserRoles)
                              .Include(i => i.AspNetUserLogins)
                              .Include(i => i.AspNetUserClaims)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnAspNetUserDeleted(itemToDelete);


            Context.AspNetUsers.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAspNetUserDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportAspNetUserTokensToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/aspnetusertokens/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/aspnetusertokens/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAspNetUserTokensToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/carpark/aspnetusertokens/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/carpark/aspnetusertokens/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAspNetUserTokensRead(ref IQueryable<CP.Server.Models.CarPark.AspNetUserToken> items);

        public async Task<IQueryable<CP.Server.Models.CarPark.AspNetUserToken>> GetAspNetUserTokens(Query query = null)
        {
            var items = Context.AspNetUserTokens.AsQueryable();

            items = items.Include(i => i.AspNetUser);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnAspNetUserTokensRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAspNetUserTokenGet(CP.Server.Models.CarPark.AspNetUserToken item);
        partial void OnGetAspNetUserTokenByUserIdAndLoginProviderAndName(ref IQueryable<CP.Server.Models.CarPark.AspNetUserToken> items);


        public async Task<CP.Server.Models.CarPark.AspNetUserToken> GetAspNetUserTokenByUserIdAndLoginProviderAndName(string userid, string loginprovider, string name)
        {
            var items = Context.AspNetUserTokens
                              .AsNoTracking()
                              .Where(i => i.UserId == userid && i.LoginProvider == loginprovider && i.Name == name);

            items = items.Include(i => i.AspNetUser);
 
            OnGetAspNetUserTokenByUserIdAndLoginProviderAndName(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnAspNetUserTokenGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnAspNetUserTokenCreated(CP.Server.Models.CarPark.AspNetUserToken item);
        partial void OnAfterAspNetUserTokenCreated(CP.Server.Models.CarPark.AspNetUserToken item);

        public async Task<CP.Server.Models.CarPark.AspNetUserToken> CreateAspNetUserToken(CP.Server.Models.CarPark.AspNetUserToken aspnetusertoken)
        {
            OnAspNetUserTokenCreated(aspnetusertoken);

            var existingItem = Context.AspNetUserTokens
                              .Where(i => i.UserId == aspnetusertoken.UserId && i.LoginProvider == aspnetusertoken.LoginProvider && i.Name == aspnetusertoken.Name)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.AspNetUserTokens.Add(aspnetusertoken);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(aspnetusertoken).State = EntityState.Detached;
                throw;
            }

            OnAfterAspNetUserTokenCreated(aspnetusertoken);

            return aspnetusertoken;
        }

        public async Task<CP.Server.Models.CarPark.AspNetUserToken> CancelAspNetUserTokenChanges(CP.Server.Models.CarPark.AspNetUserToken item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAspNetUserTokenUpdated(CP.Server.Models.CarPark.AspNetUserToken item);
        partial void OnAfterAspNetUserTokenUpdated(CP.Server.Models.CarPark.AspNetUserToken item);

        public async Task<CP.Server.Models.CarPark.AspNetUserToken> UpdateAspNetUserToken(string userid, string loginprovider, string name, CP.Server.Models.CarPark.AspNetUserToken aspnetusertoken)
        {
            OnAspNetUserTokenUpdated(aspnetusertoken);

            var itemToUpdate = Context.AspNetUserTokens
                              .Where(i => i.UserId == aspnetusertoken.UserId && i.LoginProvider == aspnetusertoken.LoginProvider && i.Name == aspnetusertoken.Name)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(aspnetusertoken);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterAspNetUserTokenUpdated(aspnetusertoken);

            return aspnetusertoken;
        }

        partial void OnAspNetUserTokenDeleted(CP.Server.Models.CarPark.AspNetUserToken item);
        partial void OnAfterAspNetUserTokenDeleted(CP.Server.Models.CarPark.AspNetUserToken item);

        public async Task<CP.Server.Models.CarPark.AspNetUserToken> DeleteAspNetUserToken(string userid, string loginprovider, string name)
        {
            var itemToDelete = Context.AspNetUserTokens
                              .Where(i => i.UserId == userid && i.LoginProvider == loginprovider && i.Name == name)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnAspNetUserTokenDeleted(itemToDelete);


            Context.AspNetUserTokens.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAspNetUserTokenDeleted(itemToDelete);

            return itemToDelete;
        }
        }
}