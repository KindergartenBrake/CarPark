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
    [Route("odata/CarPark/AspNetUserClaims")]
    public partial class AspNetUserClaimsController : ODataController
    {
        private CP.Server.Data.CarParkContext context;

        public AspNetUserClaimsController(CP.Server.Data.CarParkContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<CP.Server.Models.CarPark.AspNetUserClaim> GetAspNetUserClaims()
        {
            var items = this.context.AspNetUserClaims.AsQueryable<CP.Server.Models.CarPark.AspNetUserClaim>();
            this.OnAspNetUserClaimsRead(ref items);

            return items;
        }

        partial void OnAspNetUserClaimsRead(ref IQueryable<CP.Server.Models.CarPark.AspNetUserClaim> items);

        partial void OnAspNetUserClaimGet(ref SingleResult<CP.Server.Models.CarPark.AspNetUserClaim> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CarPark/AspNetUserClaims(Id={Id})")]
        public SingleResult<CP.Server.Models.CarPark.AspNetUserClaim> GetAspNetUserClaim(int key)
        {
            var items = this.context.AspNetUserClaims.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnAspNetUserClaimGet(ref result);

            return result;
        }
        partial void OnAspNetUserClaimDeleted(CP.Server.Models.CarPark.AspNetUserClaim item);
        partial void OnAfterAspNetUserClaimDeleted(CP.Server.Models.CarPark.AspNetUserClaim item);

        [HttpDelete("/odata/CarPark/AspNetUserClaims(Id={Id})")]
        public IActionResult DeleteAspNetUserClaim(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.AspNetUserClaims
                    .Where(i => i.Id == key)
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnAspNetUserClaimDeleted(item);
                this.context.AspNetUserClaims.Remove(item);
                this.context.SaveChanges();
                this.OnAfterAspNetUserClaimDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAspNetUserClaimUpdated(CP.Server.Models.CarPark.AspNetUserClaim item);
        partial void OnAfterAspNetUserClaimUpdated(CP.Server.Models.CarPark.AspNetUserClaim item);

        [HttpPut("/odata/CarPark/AspNetUserClaims(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutAspNetUserClaim(int key, [FromBody]CP.Server.Models.CarPark.AspNetUserClaim item)
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
                this.OnAspNetUserClaimUpdated(item);
                this.context.AspNetUserClaims.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AspNetUserClaims.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "AspNetUser");
                this.OnAfterAspNetUserClaimUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CarPark/AspNetUserClaims(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchAspNetUserClaim(int key, [FromBody]Delta<CP.Server.Models.CarPark.AspNetUserClaim> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.AspNetUserClaims.Where(i => i.Id == key).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnAspNetUserClaimUpdated(item);
                this.context.AspNetUserClaims.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AspNetUserClaims.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "AspNetUser");
                this.OnAfterAspNetUserClaimUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAspNetUserClaimCreated(CP.Server.Models.CarPark.AspNetUserClaim item);
        partial void OnAfterAspNetUserClaimCreated(CP.Server.Models.CarPark.AspNetUserClaim item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] CP.Server.Models.CarPark.AspNetUserClaim item)
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

                this.OnAspNetUserClaimCreated(item);
                this.context.AspNetUserClaims.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AspNetUserClaims.Where(i => i.Id == item.Id);

                Request.QueryString = Request.QueryString.Add("$expand", "AspNetUser");

                this.OnAfterAspNetUserClaimCreated(item);

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
