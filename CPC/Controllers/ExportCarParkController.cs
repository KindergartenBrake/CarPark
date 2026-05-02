using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

using CPC.Data;

namespace CPC.Controllers
{
    public partial class ExportCarParkController : ExportController
    {
        private readonly CarParkContext context;
        private readonly CarParkService service;

        public ExportCarParkController(CarParkContext context, CarParkService service)
        {
            this.service = service;
            this.context = context;
        }

        [HttpGet("/export/CarPark/drivers/csv")]
        [HttpGet("/export/CarPark/drivers/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportDriversToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetDrivers(), Request.Query, false), fileName);
        }

        [HttpGet("/export/CarPark/drivers/excel")]
        [HttpGet("/export/CarPark/drivers/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportDriversToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetDrivers(), Request.Query, false), fileName);
        }

        [HttpGet("/export/CarPark/triprequests/csv")]
        [HttpGet("/export/CarPark/triprequests/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportTripRequestsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetTripRequests(), Request.Query, false), fileName);
        }

        [HttpGet("/export/CarPark/triprequests/excel")]
        [HttpGet("/export/CarPark/triprequests/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportTripRequestsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetTripRequests(), Request.Query, false), fileName);
        }

        [HttpGet("/export/CarPark/trips/csv")]
        [HttpGet("/export/CarPark/trips/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportTripsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetTrips(), Request.Query, false), fileName);
        }

        [HttpGet("/export/CarPark/trips/excel")]
        [HttpGet("/export/CarPark/trips/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportTripsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetTrips(), Request.Query, false), fileName);
        }

        [HttpGet("/export/CarPark/users/csv")]
        [HttpGet("/export/CarPark/users/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportUsersToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetUsers(), Request.Query, false), fileName);
        }

        [HttpGet("/export/CarPark/users/excel")]
        [HttpGet("/export/CarPark/users/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportUsersToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetUsers(), Request.Query, false), fileName);
        }

        [HttpGet("/export/CarPark/vehicles/csv")]
        [HttpGet("/export/CarPark/vehicles/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportVehiclesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetVehicles(), Request.Query, false), fileName);
        }

        [HttpGet("/export/CarPark/vehicles/excel")]
        [HttpGet("/export/CarPark/vehicles/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportVehiclesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetVehicles(), Request.Query, false), fileName);
        }

        [HttpGet("/export/CarPark/vdrivers/csv")]
        [HttpGet("/export/CarPark/vdrivers/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportVDriversToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetVDrivers(), Request.Query, true), fileName);
        }

        [HttpGet("/export/CarPark/vdrivers/excel")]
        [HttpGet("/export/CarPark/vdrivers/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportVDriversToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetVDrivers(), Request.Query, true), fileName);
        }

        [HttpGet("/export/CarPark/vtriprequests/csv")]
        [HttpGet("/export/CarPark/vtriprequests/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportVTripRequestsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetVTripRequests(), Request.Query, true), fileName);
        }

        [HttpGet("/export/CarPark/vtriprequests/excel")]
        [HttpGet("/export/CarPark/vtriprequests/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportVTripRequestsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetVTripRequests(), Request.Query, true), fileName);
        }

        [HttpGet("/export/CarPark/vtrips/csv")]
        [HttpGet("/export/CarPark/vtrips/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportVTripsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetVTrips(), Request.Query, true), fileName);
        }

        [HttpGet("/export/CarPark/vtrips/excel")]
        [HttpGet("/export/CarPark/vtrips/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportVTripsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetVTrips(), Request.Query, true), fileName);
        }

        [HttpGet("/export/CarPark/vusers/csv")]
        [HttpGet("/export/CarPark/vusers/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportVUsersToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetVUsers(), Request.Query, true), fileName);
        }

        [HttpGet("/export/CarPark/vusers/excel")]
        [HttpGet("/export/CarPark/vusers/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportVUsersToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetVUsers(), Request.Query, true), fileName);
        }

        [HttpGet("/export/CarPark/vvehicles/csv")]
        [HttpGet("/export/CarPark/vvehicles/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportVVehiclesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetVVehicles(), Request.Query, true), fileName);
        }

        [HttpGet("/export/CarPark/vvehicles/excel")]
        [HttpGet("/export/CarPark/vvehicles/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportVVehiclesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetVVehicles(), Request.Query, true), fileName);
        }
    }
}
