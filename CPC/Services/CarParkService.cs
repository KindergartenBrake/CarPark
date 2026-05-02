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

using CPC.Data;

namespace CPC
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

        partial void OnDriversRead(ref IQueryable<CPC.Models.CarPark.Driver> items);

        public async Task<IQueryable<CPC.Models.CarPark.Driver>> GetDrivers(Query query = null)
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

        partial void OnDriverGet(CPC.Models.CarPark.Driver item);
        partial void OnGetDriverByDriverId(ref IQueryable<CPC.Models.CarPark.Driver> items);


        public async Task<CPC.Models.CarPark.Driver> GetDriverByDriverId(int driverid)
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

        partial void OnDriverCreated(CPC.Models.CarPark.Driver item);
        partial void OnAfterDriverCreated(CPC.Models.CarPark.Driver item);

        public async Task<CPC.Models.CarPark.Driver> CreateDriver(CPC.Models.CarPark.Driver driver)
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

        public async Task<CPC.Models.CarPark.Driver> CancelDriverChanges(CPC.Models.CarPark.Driver item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnDriverUpdated(CPC.Models.CarPark.Driver item);
        partial void OnAfterDriverUpdated(CPC.Models.CarPark.Driver item);

        public async Task<CPC.Models.CarPark.Driver> UpdateDriver(int driverid, CPC.Models.CarPark.Driver driver)
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

        partial void OnDriverDeleted(CPC.Models.CarPark.Driver item);
        partial void OnAfterDriverDeleted(CPC.Models.CarPark.Driver item);

        public async Task<CPC.Models.CarPark.Driver> DeleteDriver(int driverid)
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

        partial void OnTripRequestsRead(ref IQueryable<CPC.Models.CarPark.TripRequest> items);

        public async Task<IQueryable<CPC.Models.CarPark.TripRequest>> GetTripRequests(Query query = null)
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

        partial void OnTripRequestGet(CPC.Models.CarPark.TripRequest item);
        partial void OnGetTripRequestByRequestId(ref IQueryable<CPC.Models.CarPark.TripRequest> items);


        public async Task<CPC.Models.CarPark.TripRequest> GetTripRequestByRequestId(int requestid)
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

        partial void OnTripRequestCreated(CPC.Models.CarPark.TripRequest item);
        partial void OnAfterTripRequestCreated(CPC.Models.CarPark.TripRequest item);

        public async Task<CPC.Models.CarPark.TripRequest> CreateTripRequest(CPC.Models.CarPark.TripRequest triprequest)
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

        public async Task<CPC.Models.CarPark.TripRequest> CancelTripRequestChanges(CPC.Models.CarPark.TripRequest item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnTripRequestUpdated(CPC.Models.CarPark.TripRequest item);
        partial void OnAfterTripRequestUpdated(CPC.Models.CarPark.TripRequest item);

        public async Task<CPC.Models.CarPark.TripRequest> UpdateTripRequest(int requestid, CPC.Models.CarPark.TripRequest triprequest)
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

        partial void OnTripRequestDeleted(CPC.Models.CarPark.TripRequest item);
        partial void OnAfterTripRequestDeleted(CPC.Models.CarPark.TripRequest item);

        public async Task<CPC.Models.CarPark.TripRequest> DeleteTripRequest(int requestid)
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

        partial void OnTripsRead(ref IQueryable<CPC.Models.CarPark.Trip> items);

        public async Task<IQueryable<CPC.Models.CarPark.Trip>> GetTrips(Query query = null)
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

        partial void OnTripGet(CPC.Models.CarPark.Trip item);
        partial void OnGetTripByTripId(ref IQueryable<CPC.Models.CarPark.Trip> items);


        public async Task<CPC.Models.CarPark.Trip> GetTripByTripId(int tripid)
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

        partial void OnTripCreated(CPC.Models.CarPark.Trip item);
        partial void OnAfterTripCreated(CPC.Models.CarPark.Trip item);

        public async Task<CPC.Models.CarPark.Trip> CreateTrip(CPC.Models.CarPark.Trip trip)
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

        public async Task<CPC.Models.CarPark.Trip> CancelTripChanges(CPC.Models.CarPark.Trip item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnTripUpdated(CPC.Models.CarPark.Trip item);
        partial void OnAfterTripUpdated(CPC.Models.CarPark.Trip item);

        public async Task<CPC.Models.CarPark.Trip> UpdateTrip(int tripid, CPC.Models.CarPark.Trip trip)
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

        partial void OnTripDeleted(CPC.Models.CarPark.Trip item);
        partial void OnAfterTripDeleted(CPC.Models.CarPark.Trip item);

        public async Task<CPC.Models.CarPark.Trip> DeleteTrip(int tripid)
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

        partial void OnUsersRead(ref IQueryable<CPC.Models.CarPark.User> items);

        public async Task<IQueryable<CPC.Models.CarPark.User>> GetUsers(Query query = null)
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

        partial void OnUserGet(CPC.Models.CarPark.User item);
        partial void OnGetUserByUserId(ref IQueryable<CPC.Models.CarPark.User> items);


        public async Task<CPC.Models.CarPark.User> GetUserByUserId(int userid)
        {
            var items = Context.Users
                              .AsNoTracking()
                              .Where(i => i.UserId == userid);

 
            OnGetUserByUserId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnUserGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnUserCreated(CPC.Models.CarPark.User item);
        partial void OnAfterUserCreated(CPC.Models.CarPark.User item);

        public async Task<CPC.Models.CarPark.User> CreateUser(CPC.Models.CarPark.User user)
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

        public async Task<CPC.Models.CarPark.User> CancelUserChanges(CPC.Models.CarPark.User item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnUserUpdated(CPC.Models.CarPark.User item);
        partial void OnAfterUserUpdated(CPC.Models.CarPark.User item);

        public async Task<CPC.Models.CarPark.User> UpdateUser(int userid, CPC.Models.CarPark.User user)
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

        partial void OnUserDeleted(CPC.Models.CarPark.User item);
        partial void OnAfterUserDeleted(CPC.Models.CarPark.User item);

        public async Task<CPC.Models.CarPark.User> DeleteUser(int userid)
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

        partial void OnVehiclesRead(ref IQueryable<CPC.Models.CarPark.Vehicle> items);

        public async Task<IQueryable<CPC.Models.CarPark.Vehicle>> GetVehicles(Query query = null)
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

        partial void OnVehicleGet(CPC.Models.CarPark.Vehicle item);
        partial void OnGetVehicleByVehicleId(ref IQueryable<CPC.Models.CarPark.Vehicle> items);


        public async Task<CPC.Models.CarPark.Vehicle> GetVehicleByVehicleId(int vehicleid)
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

        partial void OnVehicleCreated(CPC.Models.CarPark.Vehicle item);
        partial void OnAfterVehicleCreated(CPC.Models.CarPark.Vehicle item);

        public async Task<CPC.Models.CarPark.Vehicle> CreateVehicle(CPC.Models.CarPark.Vehicle vehicle)
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

        public async Task<CPC.Models.CarPark.Vehicle> CancelVehicleChanges(CPC.Models.CarPark.Vehicle item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnVehicleUpdated(CPC.Models.CarPark.Vehicle item);
        partial void OnAfterVehicleUpdated(CPC.Models.CarPark.Vehicle item);

        public async Task<CPC.Models.CarPark.Vehicle> UpdateVehicle(int vehicleid, CPC.Models.CarPark.Vehicle vehicle)
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

        partial void OnVehicleDeleted(CPC.Models.CarPark.Vehicle item);
        partial void OnAfterVehicleDeleted(CPC.Models.CarPark.Vehicle item);

        public async Task<CPC.Models.CarPark.Vehicle> DeleteVehicle(int vehicleid)
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

        partial void OnVDriversRead(ref IQueryable<CPC.Models.CarPark.VDriver> items);

        public async Task<IQueryable<CPC.Models.CarPark.VDriver>> GetVDrivers(Query query = null)
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

        partial void OnVTripRequestsRead(ref IQueryable<CPC.Models.CarPark.VTripRequest> items);

        public async Task<IQueryable<CPC.Models.CarPark.VTripRequest>> GetVTripRequests(Query query = null)
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

        partial void OnVTripsRead(ref IQueryable<CPC.Models.CarPark.VTrip> items);

        public async Task<IQueryable<CPC.Models.CarPark.VTrip>> GetVTrips(Query query = null)
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

        partial void OnVUsersRead(ref IQueryable<CPC.Models.CarPark.VUser> items);

        public async Task<IQueryable<CPC.Models.CarPark.VUser>> GetVUsers(Query query = null)
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

        partial void OnVVehiclesRead(ref IQueryable<CPC.Models.CarPark.VVehicle> items);

        public async Task<IQueryable<CPC.Models.CarPark.VVehicle>> GetVVehicles(Query query = null)
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
    }
}