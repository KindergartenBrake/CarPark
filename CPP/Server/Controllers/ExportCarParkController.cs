using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using CP.Server.Services.Interfaces;
using CP.Server.Data;

namespace CP.Server.Controllers
{
    [Authorize(Roles = "Admin")]
    public partial class ExportCarParkController : ExportController
    {
        private readonly CarParkContext context;
        private readonly CarParkService service;

        public ExportCarParkController(CarParkContext context, CarParkService service)
        {
            this.service = service;
            this.context = context;
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
