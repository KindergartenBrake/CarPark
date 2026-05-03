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
    [Route("odata/CarPark/AspNetRoleClaims")]
    public partial class AspNetRoleClaimsController : ODataController
    {
        private CP.Server.Data.CarParkContext context;

        public AspNetRoleClaimsController(CP.Server.Data.CarParkContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<CP.Server.Models.CarPark.AspNetRoleClaim> GetAspNetRoleClaims()
        {
            var items = this.context.AspNetRoleClaims.AsQueryable<CP.Server.Models.CarPark.AspNetRoleClaim>();
            this.OnAspNetRoleClaimsRead(ref items);

            return items;
        }

        partial void OnAspNetRoleClaimsRead(ref IQueryable<CP.Server.Models.CarPark.AspNetRoleClaim> items);

        partial void OnAspNetRoleClaimGet(ref SingleResult<CP.Server.Models.CarPark.AspNetRoleClaim> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CarPark/AspNetRoleClaims(Id={Id})")]
        public SingleResult<CP.Server.Models.CarPark.AspNetRoleClaim> GetAspNetRoleClaim(int key)
        {
            var items = this.context.AspNetRoleClaims.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnAspNetRoleClaimGet(ref result);

            return result;
        }
        partial void OnAspNetRoleClaimDeleted(CP.Server.Models.CarPark.AspNetRoleClaim item);
        partial void OnAfterAspNetRoleClaimDeleted(CP.Server.Models.CarPark.AspNetRoleClaim item);

        [HttpDelete("/odata/CarPark/AspNetRoleClaims(Id={Id})")]
        public IActionResult DeleteAspNetRoleClaim(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.AspNetRoleClaims
                    .Where(i => i.Id == key)
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnAspNetRoleClaimDeleted(item);
                this.context.AspNetRoleClaims.Remove(item);
                this.context.SaveChanges();
                this.OnAfterAspNetRoleClaimDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAspNetRoleClaimUpdated(CP.Server.Models.CarPark.AspNetRoleClaim item);
        partial void OnAfterAspNetRoleClaimUpdated(CP.Server.Models.CarPark.AspNetRoleClaim item);

        [HttpPut("/odata/CarPark/AspNetRoleClaims(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutAspNetRoleClaim(int key, [FromBody]CP.Server.Models.CarPark.AspNetRoleClaim item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (item == null || (item.Id != key))
                {
                    return BadRequest();
                }
                this.OnAspNetRoleClaimUpdated(item);
                this.context.AspNetRoleClaims.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AspNetRoleClaims.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "AspNetRole");
                this.OnAfterAspNetRoleClaimUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CarPark/AspNetRoleClaims(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchAspNetRoleClaim(int key, [FromBody]Delta<CP.Server.Models.CarPark.AspNetRoleClaim> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.AspNetRoleClaims.Where(i => i.Id == key).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnAspNetRoleClaimUpdated(item);
                this.context.AspNetRoleClaims.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AspNetRoleClaims.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "AspNetRole");
                this.OnAfterAspNetRoleClaimUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAspNetRoleClaimCreated(CP.Server.Models.CarPark.AspNetRoleClaim item);
        partial void OnAfterAspNetRoleClaimCreated(CP.Server.Models.CarPark.AspNetRoleClaim item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] CP.Server.Models.CarPark.AspNetRoleClaim item)
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

                this.OnAspNetRoleClaimCreated(item);
                this.context.AspNetRoleClaims.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AspNetRoleClaims.Where(i => i.Id == item.Id);

                Request.QueryString = Request.QueryString.Add("$expand", "AspNetRole");

                this.OnAfterAspNetRoleClaimCreated(item);

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
