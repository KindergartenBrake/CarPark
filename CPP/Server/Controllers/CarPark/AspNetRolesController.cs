using System;
using System.Net;
using System.Data;
using System.Linq;
using System.Collections.Generic;
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
    [Route("odata/CarPark/AspNetRoles")]
    public partial class AspNetRolesController : ODataController
    {
        private CP.Server.Data.CarParkContext context;

        public AspNetRolesController(CP.Server.Data.CarParkContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<CP.Server.Models.CarPark.AspNetRole> GetAspNetRoles()
        {
            var items = this.context.AspNetRoles.AsQueryable<CP.Server.Models.CarPark.AspNetRole>();
            this.OnAspNetRolesRead(ref items);

            return items;
        }

        partial void OnAspNetRolesRead(ref IQueryable<CP.Server.Models.CarPark.AspNetRole> items);

        partial void OnAspNetRoleGet(ref SingleResult<CP.Server.Models.CarPark.AspNetRole> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CarPark/AspNetRoles(Id={Id})")]
        public SingleResult<CP.Server.Models.CarPark.AspNetRole> GetAspNetRole(string key)
        {
            var items = this.context.AspNetRoles.Where(i => i.Id == Uri.UnescapeDataString(key));
            var result = SingleResult.Create(items);

            OnAspNetRoleGet(ref result);

            return result;
        }
        partial void OnAspNetRoleDeleted(CP.Server.Models.CarPark.AspNetRole item);
        partial void OnAfterAspNetRoleDeleted(CP.Server.Models.CarPark.AspNetRole item);

        [HttpDelete("/odata/CarPark/AspNetRoles(Id={Id})")]
        public IActionResult DeleteAspNetRole(string key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.AspNetRoles
                    .Where(i => i.Id == Uri.UnescapeDataString(key))
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnAspNetRoleDeleted(item);
                this.context.AspNetRoles.Remove(item);
                this.context.SaveChanges();
                this.OnAfterAspNetRoleDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAspNetRoleUpdated(CP.Server.Models.CarPark.AspNetRole item);
        partial void OnAfterAspNetRoleUpdated(CP.Server.Models.CarPark.AspNetRole item);

        [HttpPut("/odata/CarPark/AspNetRoles(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutAspNetRole(string key, [FromBody]CP.Server.Models.CarPark.AspNetRole item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (item == null || (item.Id != Uri.UnescapeDataString(key)))
                {
                    return BadRequest();
                }
                this.OnAspNetRoleUpdated(item);
                this.context.AspNetRoles.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AspNetRoles.Where(i => i.Id == Uri.UnescapeDataString(key));
                
                this.OnAfterAspNetRoleUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CarPark/AspNetRoles(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchAspNetRole(string key, [FromBody]Delta<CP.Server.Models.CarPark.AspNetRole> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.AspNetRoles.Where(i => i.Id == Uri.UnescapeDataString(key)).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnAspNetRoleUpdated(item);
                this.context.AspNetRoles.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AspNetRoles.Where(i => i.Id == Uri.UnescapeDataString(key));
                
                this.OnAfterAspNetRoleUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAspNetRoleCreated(CP.Server.Models.CarPark.AspNetRole item);
        partial void OnAfterAspNetRoleCreated(CP.Server.Models.CarPark.AspNetRole item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] CP.Server.Models.CarPark.AspNetRole item)
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

                this.OnAspNetRoleCreated(item);
                this.context.AspNetRoles.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AspNetRoles.Where(i => i.Id == item.Id);

                

                this.OnAfterAspNetRoleCreated(item);

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
