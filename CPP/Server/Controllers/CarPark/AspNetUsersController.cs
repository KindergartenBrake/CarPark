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
    [Route("odata/CarPark/AspNetUsers")]
    public partial class AspNetUsersController : ODataController
    {
        private CP.Server.Data.CarParkContext context;

        public AspNetUsersController(CP.Server.Data.CarParkContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<CP.Server.Models.CarPark.AspNetUser> GetAspNetUsers()
        {
            var items = this.context.AspNetUsers.AsQueryable<CP.Server.Models.CarPark.AspNetUser>();
            this.OnAspNetUsersRead(ref items);

            return items;
        }

        partial void OnAspNetUsersRead(ref IQueryable<CP.Server.Models.CarPark.AspNetUser> items);

        partial void OnAspNetUserGet(ref SingleResult<CP.Server.Models.CarPark.AspNetUser> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CarPark/AspNetUsers(Id={Id})")]
        public SingleResult<CP.Server.Models.CarPark.AspNetUser> GetAspNetUser(string key)
        {
            var items = this.context.AspNetUsers.Where(i => i.Id == Uri.UnescapeDataString(key));
            var result = SingleResult.Create(items);

            OnAspNetUserGet(ref result);

            return result;
        }
        partial void OnAspNetUserDeleted(CP.Server.Models.CarPark.AspNetUser item);
        partial void OnAfterAspNetUserDeleted(CP.Server.Models.CarPark.AspNetUser item);

        [HttpDelete("/odata/CarPark/AspNetUsers(Id={Id})")]
        public IActionResult DeleteAspNetUser(string key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.AspNetUsers
                    .Where(i => i.Id == Uri.UnescapeDataString(key))
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnAspNetUserDeleted(item);
                this.context.AspNetUsers.Remove(item);
                this.context.SaveChanges();
                this.OnAfterAspNetUserDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAspNetUserUpdated(CP.Server.Models.CarPark.AspNetUser item);
        partial void OnAfterAspNetUserUpdated(CP.Server.Models.CarPark.AspNetUser item);

        [HttpPut("/odata/CarPark/AspNetUsers(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutAspNetUser(string key, [FromBody]CP.Server.Models.CarPark.AspNetUser item)
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
                this.OnAspNetUserUpdated(item);
                this.context.AspNetUsers.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AspNetUsers.Where(i => i.Id == Uri.UnescapeDataString(key));
                
                this.OnAfterAspNetUserUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CarPark/AspNetUsers(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchAspNetUser(string key, [FromBody]Delta<CP.Server.Models.CarPark.AspNetUser> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.AspNetUsers.Where(i => i.Id == Uri.UnescapeDataString(key)).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnAspNetUserUpdated(item);
                this.context.AspNetUsers.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AspNetUsers.Where(i => i.Id == Uri.UnescapeDataString(key));
                
                this.OnAfterAspNetUserUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAspNetUserCreated(CP.Server.Models.CarPark.AspNetUser item);
        partial void OnAfterAspNetUserCreated(CP.Server.Models.CarPark.AspNetUser item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] CP.Server.Models.CarPark.AspNetUser item)
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

                this.OnAspNetUserCreated(item);
                this.context.AspNetUsers.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AspNetUsers.Where(i => i.Id == item.Id);

                

                this.OnAfterAspNetUserCreated(item);

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
