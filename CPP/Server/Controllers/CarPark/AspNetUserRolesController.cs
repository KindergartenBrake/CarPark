using System;
using System.Net;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CP.Server.Controllers.CarPark
{
    [Authorize(Roles = "Admin")]
    [Route("odata/CarPark/AspNetUserRoles")]
    public partial class AspNetUserRolesController : ODataController
    {
        private CP.Server.Data.CarParkContext context;

        public AspNetUserRolesController(CP.Server.Data.CarParkContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<CP.Server.Models.CarPark.AspNetUserRole> GetAspNetUserRoles()
        {
            var items = this.context.AspNetUserRoles.AsQueryable<CP.Server.Models.CarPark.AspNetUserRole>();
            this.OnAspNetUserRolesRead(ref items);

            return items;
        }

        partial void OnAspNetUserRolesRead(ref IQueryable<CP.Server.Models.CarPark.AspNetUserRole> items);

        partial void OnAspNetUserRoleGet(ref SingleResult<CP.Server.Models.CarPark.AspNetUserRole> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CarPark/AspNetUserRoles(UserId={keyUserId},RoleId={keyRoleId})")]
        public SingleResult<CP.Server.Models.CarPark.AspNetUserRole> GetAspNetUserRole([FromODataUri] string keyUserId, [FromODataUri] string keyRoleId)
        {
            var items = this.context.AspNetUserRoles.Where(i => i.UserId == Uri.UnescapeDataString(keyUserId) && i.RoleId == Uri.UnescapeDataString(keyRoleId));
            var result = SingleResult.Create(items);

            OnAspNetUserRoleGet(ref result);

            return result;
        }
        partial void OnAspNetUserRoleDeleted(CP.Server.Models.CarPark.AspNetUserRole item);
        partial void OnAfterAspNetUserRoleDeleted(CP.Server.Models.CarPark.AspNetUserRole item);

        [HttpDelete("/odata/CarPark/AspNetUserRoles(UserId={keyUserId},RoleId={keyRoleId})")]
        public IActionResult DeleteAspNetUserRole([FromODataUri] string keyUserId, [FromODataUri] string keyRoleId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.AspNetUserRoles
                    .Where(i => i.UserId == Uri.UnescapeDataString(keyUserId) && i.RoleId == Uri.UnescapeDataString(keyRoleId))
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnAspNetUserRoleDeleted(item);
                this.context.AspNetUserRoles.Remove(item);
                this.context.SaveChanges();
                this.OnAfterAspNetUserRoleDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAspNetUserRoleUpdated(CP.Server.Models.CarPark.AspNetUserRole item);
        partial void OnAfterAspNetUserRoleUpdated(CP.Server.Models.CarPark.AspNetUserRole item);

        [HttpPut("/odata/CarPark/AspNetUserRoles(UserId={keyUserId},RoleId={keyRoleId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutAspNetUserRole([FromODataUri] string keyUserId, [FromODataUri] string keyRoleId, [FromBody]CP.Server.Models.CarPark.AspNetUserRole item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (item == null || (item.UserId != Uri.UnescapeDataString(keyUserId) && item.RoleId != Uri.UnescapeDataString(keyRoleId)))
                {
                    return BadRequest();
                }
                this.OnAspNetUserRoleUpdated(item);
                this.context.AspNetUserRoles.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AspNetUserRoles.Where(i => i.UserId == Uri.UnescapeDataString(keyUserId) && i.RoleId == Uri.UnescapeDataString(keyRoleId));
                Request.QueryString = Request.QueryString.Add("$expand", "AspNetRole,AspNetUser");
                this.OnAfterAspNetUserRoleUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CarPark/AspNetUserRoles(UserId={keyUserId},RoleId={keyRoleId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchAspNetUserRole([FromODataUri] string keyUserId, [FromODataUri] string keyRoleId, [FromBody]Delta<CP.Server.Models.CarPark.AspNetUserRole> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.AspNetUserRoles.Where(i => i.UserId == Uri.UnescapeDataString(keyUserId) && i.RoleId == Uri.UnescapeDataString(keyRoleId)).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnAspNetUserRoleUpdated(item);
                this.context.AspNetUserRoles.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AspNetUserRoles.Where(i => i.UserId == Uri.UnescapeDataString(keyUserId) && i.RoleId == Uri.UnescapeDataString(keyRoleId));
                Request.QueryString = Request.QueryString.Add("$expand", "AspNetRole,AspNetUser");
                this.OnAfterAspNetUserRoleUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAspNetUserRoleCreated(CP.Server.Models.CarPark.AspNetUserRole item);
        partial void OnAfterAspNetUserRoleCreated(CP.Server.Models.CarPark.AspNetUserRole item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] CP.Server.Models.CarPark.AspNetUserRole item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (item == null)
                {
                    return BadRequest();
                }

                this.OnAspNetUserRoleCreated(item);
                this.context.AspNetUserRoles.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AspNetUserRoles.Where(i => i.UserId == item.UserId && i.RoleId == item.RoleId);

                Request.QueryString = Request.QueryString.Add("$expand", "AspNetRole,AspNetUser");

                this.OnAfterAspNetUserRoleCreated(item);

                return new ObjectResult(SingleResult.Create(itemToReturn))
                {
                    StatusCode = 201
                };
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }
    }
}
