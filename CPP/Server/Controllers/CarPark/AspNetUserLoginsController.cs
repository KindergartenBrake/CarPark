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
    [Route("odata/CarPark/AspNetUserLogins")]
    public partial class AspNetUserLoginsController : ODataController
    {
        private CP.Server.Data.CarParkContext context;

        public AspNetUserLoginsController(CP.Server.Data.CarParkContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<CP.Server.Models.CarPark.AspNetUserLogin> GetAspNetUserLogins()
        {
            var items = this.context.AspNetUserLogins.AsQueryable<CP.Server.Models.CarPark.AspNetUserLogin>();
            this.OnAspNetUserLoginsRead(ref items);

            return items;
        }

        partial void OnAspNetUserLoginsRead(ref IQueryable<CP.Server.Models.CarPark.AspNetUserLogin> items);

        partial void OnAspNetUserLoginGet(ref SingleResult<CP.Server.Models.CarPark.AspNetUserLogin> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CarPark/AspNetUserLogins(LoginProvider={keyLoginProvider},ProviderKey={keyProviderKey})")]
        public SingleResult<CP.Server.Models.CarPark.AspNetUserLogin> GetAspNetUserLogin([FromODataUri] string keyLoginProvider, [FromODataUri] string keyProviderKey)
        {
            var items = this.context.AspNetUserLogins.Where(i => i.LoginProvider == Uri.UnescapeDataString(keyLoginProvider) && i.ProviderKey == Uri.UnescapeDataString(keyProviderKey));
            var result = SingleResult.Create(items);

            OnAspNetUserLoginGet(ref result);

            return result;
        }
        partial void OnAspNetUserLoginDeleted(CP.Server.Models.CarPark.AspNetUserLogin item);
        partial void OnAfterAspNetUserLoginDeleted(CP.Server.Models.CarPark.AspNetUserLogin item);

        [HttpDelete("/odata/CarPark/AspNetUserLogins(LoginProvider={keyLoginProvider},ProviderKey={keyProviderKey})")]
        public IActionResult DeleteAspNetUserLogin([FromODataUri] string keyLoginProvider, [FromODataUri] string keyProviderKey)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.AspNetUserLogins
                    .Where(i => i.LoginProvider == Uri.UnescapeDataString(keyLoginProvider) && i.ProviderKey == Uri.UnescapeDataString(keyProviderKey))
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnAspNetUserLoginDeleted(item);
                this.context.AspNetUserLogins.Remove(item);
                this.context.SaveChanges();
                this.OnAfterAspNetUserLoginDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAspNetUserLoginUpdated(CP.Server.Models.CarPark.AspNetUserLogin item);
        partial void OnAfterAspNetUserLoginUpdated(CP.Server.Models.CarPark.AspNetUserLogin item);

        [HttpPut("/odata/CarPark/AspNetUserLogins(LoginProvider={keyLoginProvider},ProviderKey={keyProviderKey})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutAspNetUserLogin([FromODataUri] string keyLoginProvider, [FromODataUri] string keyProviderKey, [FromBody]CP.Server.Models.CarPark.AspNetUserLogin item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (item == null || (item.LoginProvider != Uri.UnescapeDataString(keyLoginProvider) && item.ProviderKey != Uri.UnescapeDataString(keyProviderKey)))
                {
                    return BadRequest();
                }
                this.OnAspNetUserLoginUpdated(item);
                this.context.AspNetUserLogins.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AspNetUserLogins.Where(i => i.LoginProvider == Uri.UnescapeDataString(keyLoginProvider) && i.ProviderKey == Uri.UnescapeDataString(keyProviderKey));
                Request.QueryString = Request.QueryString.Add("$expand", "AspNetUser");
                this.OnAfterAspNetUserLoginUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CarPark/AspNetUserLogins(LoginProvider={keyLoginProvider},ProviderKey={keyProviderKey})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchAspNetUserLogin([FromODataUri] string keyLoginProvider, [FromODataUri] string keyProviderKey, [FromBody]Delta<CP.Server.Models.CarPark.AspNetUserLogin> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.AspNetUserLogins.Where(i => i.LoginProvider == Uri.UnescapeDataString(keyLoginProvider) && i.ProviderKey == Uri.UnescapeDataString(keyProviderKey)).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnAspNetUserLoginUpdated(item);
                this.context.AspNetUserLogins.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AspNetUserLogins.Where(i => i.LoginProvider == Uri.UnescapeDataString(keyLoginProvider) && i.ProviderKey == Uri.UnescapeDataString(keyProviderKey));
                Request.QueryString = Request.QueryString.Add("$expand", "AspNetUser");
                this.OnAfterAspNetUserLoginUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAspNetUserLoginCreated(CP.Server.Models.CarPark.AspNetUserLogin item);
        partial void OnAfterAspNetUserLoginCreated(CP.Server.Models.CarPark.AspNetUserLogin item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] CP.Server.Models.CarPark.AspNetUserLogin item)
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

                this.OnAspNetUserLoginCreated(item);
                this.context.AspNetUserLogins.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AspNetUserLogins.Where(i => i.LoginProvider == item.LoginProvider && i.ProviderKey == item.ProviderKey);

                Request.QueryString = Request.QueryString.Add("$expand", "AspNetUser");

                this.OnAfterAspNetUserLoginCreated(item);

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
