using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

using CP.Server.Data;

namespace CP.Server.Controllers
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

        [HttpGet("/export/CarPark/aspnetroleclaims/csv")]
        [HttpGet("/export/CarPark/aspnetroleclaims/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAspNetRoleClaimsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetAspNetRoleClaims(), Request.Query, false), fileName);
        }

        [HttpGet("/export/CarPark/aspnetroleclaims/excel")]
        [HttpGet("/export/CarPark/aspnetroleclaims/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAspNetRoleClaimsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetAspNetRoleClaims(), Request.Query, false), fileName);
        }

        [HttpGet("/export/CarPark/aspnetroles/csv")]
        [HttpGet("/export/CarPark/aspnetroles/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAspNetRolesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetAspNetRoles(), Request.Query, false), fileName);
        }

        [HttpGet("/export/CarPark/aspnetroles/excel")]
        [HttpGet("/export/CarPark/aspnetroles/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAspNetRolesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetAspNetRoles(), Request.Query, false), fileName);
        }

        [HttpGet("/export/CarPark/aspnetuserclaims/csv")]
        [HttpGet("/export/CarPark/aspnetuserclaims/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAspNetUserClaimsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetAspNetUserClaims(), Request.Query, false), fileName);
        }

        [HttpGet("/export/CarPark/aspnetuserclaims/excel")]
        [HttpGet("/export/CarPark/aspnetuserclaims/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAspNetUserClaimsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetAspNetUserClaims(), Request.Query, false), fileName);
        }

        [HttpGet("/export/CarPark/aspnetuserlogins/csv")]
        [HttpGet("/export/CarPark/aspnetuserlogins/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAspNetUserLoginsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetAspNetUserLogins(), Request.Query, false), fileName);
        }

        [HttpGet("/export/CarPark/aspnetuserlogins/excel")]
        [HttpGet("/export/CarPark/aspnetuserlogins/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAspNetUserLoginsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetAspNetUserLogins(), Request.Query, false), fileName);
        }

        [HttpGet("/export/CarPark/aspnetuserroles/csv")]
        [HttpGet("/export/CarPark/aspnetuserroles/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAspNetUserRolesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetAspNetUserRoles(), Request.Query, false), fileName);
        }

        [HttpGet("/export/CarPark/aspnetuserroles/excel")]
        [HttpGet("/export/CarPark/aspnetuserroles/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAspNetUserRolesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetAspNetUserRoles(), Request.Query, false), fileName);
        }

        [HttpGet("/export/CarPark/aspnetusers/csv")]
        [HttpGet("/export/CarPark/aspnetusers/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAspNetUsersToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetAspNetUsers(), Request.Query, false), fileName);
        }

        [HttpGet("/export/CarPark/aspnetusers/excel")]
        [HttpGet("/export/CarPark/aspnetusers/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAspNetUsersToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetAspNetUsers(), Request.Query, false), fileName);
        }

        [HttpGet("/export/CarPark/aspnetusertokens/csv")]
        [HttpGet("/export/CarPark/aspnetusertokens/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAspNetUserTokensToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetAspNetUserTokens(), Request.Query, false), fileName);
        }

        [HttpGet("/export/CarPark/aspnetusertokens/excel")]
        [HttpGet("/export/CarPark/aspnetusertokens/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAspNetUserTokensToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetAspNetUserTokens(), Request.Query, false), fileName);
        }
    }
}
