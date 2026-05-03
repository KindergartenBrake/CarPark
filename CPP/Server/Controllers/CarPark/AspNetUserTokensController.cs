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
    [Route("odata/CarPark/AspNetUserTokens")]
    public partial class AspNetUserTokensController : ODataController
    {
        private CP.Server.Data.CarParkContext context;

        public AspNetUserTokensController(CP.Server.Data.CarParkContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<CP.Server.Models.CarPark.AspNetUserToken> GetAspNetUserTokens()
        {
            var items = this.context.AspNetUserTokens.AsQueryable<CP.Server.Models.CarPark.AspNetUserToken>();
            this.OnAspNetUserTokensRead(ref items);

            return items;
        }

        partial void OnAspNetUserTokensRead(ref IQueryable<CP.Server.Models.CarPark.AspNetUserToken> items);

        partial void OnAspNetUserTokenGet(ref SingleResult<CP.Server.Models.CarPark.AspNetUserToken> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CarPark/AspNetUserTokens(UserId={keyUserId},LoginProvider={keyLoginProvider},Name={keyName})")]
        public SingleResult<CP.Server.Models.CarPark.AspNetUserToken> GetAspNetUserToken([FromODataUri] string keyUserId, [FromODataUri] string keyLoginProvider, [FromODataUri] string keyName)
        {
            var items = this.context.AspNetUserTokens.Where(i => i.UserId == Uri.UnescapeDataString(keyUserId) && i.LoginProvider == Uri.UnescapeDataString(keyLoginProvider) && i.Name == Uri.UnescapeDataString(keyName));
            var result = SingleResult.Create(items);

            OnAspNetUserTokenGet(ref result);

            return result;
        }
        partial void OnAspNetUserTokenDeleted(CP.Server.Models.CarPark.AspNetUserToken item);
        partial void OnAfterAspNetUserTokenDeleted(CP.Server.Models.CarPark.AspNetUserToken item);

        [HttpDelete("/odata/CarPark/AspNetUserTokens(UserId={keyUserId},LoginProvider={keyLoginProvider},Name={keyName})")]
        public IActionResult DeleteAspNetUserToken([FromODataUri] string keyUserId, [FromODataUri] string keyLoginProvider, [FromODataUri] string keyName)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.AspNetUserTokens
                    .Where(i => i.UserId == Uri.UnescapeDataString(keyUserId) && i.LoginProvider == Uri.UnescapeDataString(keyLoginProvider) && i.Name == Uri.UnescapeDataString(keyName))
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnAspNetUserTokenDeleted(item);
                this.context.AspNetUserTokens.Remove(item);
                this.context.SaveChanges();
                this.OnAfterAspNetUserTokenDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAspNetUserTokenUpdated(CP.Server.Models.CarPark.AspNetUserToken item);
        partial void OnAfterAspNetUserTokenUpdated(CP.Server.Models.CarPark.AspNetUserToken item);

        [HttpPut("/odata/CarPark/AspNetUserTokens(UserId={keyUserId},LoginProvider={keyLoginProvider},Name={keyName})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutAspNetUserToken([FromODataUri] string keyUserId, [FromODataUri] string keyLoginProvider, [FromODataUri] string keyName, [FromBody]CP.Server.Models.CarPark.AspNetUserToken item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (item == null || (item.UserId != Uri.UnescapeDataString(keyUserId) && item.LoginProvider != Uri.UnescapeDataString(keyLoginProvider) && item.Name != Uri.UnescapeDataString(keyName)))
                {
                    return BadRequest();
                }
                this.OnAspNetUserTokenUpdated(item);
                this.context.AspNetUserTokens.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AspNetUserTokens.Where(i => i.UserId == Uri.UnescapeDataString(keyUserId) && i.LoginProvider == Uri.UnescapeDataString(keyLoginProvider) && i.Name == Uri.UnescapeDataString(keyName));
                Request.QueryString = Request.QueryString.Add("$expand", "AspNetUser");
                this.OnAfterAspNetUserTokenUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CarPark/AspNetUserTokens(UserId={keyUserId},LoginProvider={keyLoginProvider},Name={keyName})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchAspNetUserToken([FromODataUri] string keyUserId, [FromODataUri] string keyLoginProvider, [FromODataUri] string keyName, [FromBody]Delta<CP.Server.Models.CarPark.AspNetUserToken> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.AspNetUserTokens.Where(i => i.UserId == Uri.UnescapeDataString(keyUserId) && i.LoginProvider == Uri.UnescapeDataString(keyLoginProvider) && i.Name == Uri.UnescapeDataString(keyName)).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnAspNetUserTokenUpdated(item);
                this.context.AspNetUserTokens.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AspNetUserTokens.Where(i => i.UserId == Uri.UnescapeDataString(keyUserId) && i.LoginProvider == Uri.UnescapeDataString(keyLoginProvider) && i.Name == Uri.UnescapeDataString(keyName));
                Request.QueryString = Request.QueryString.Add("$expand", "AspNetUser");
                this.OnAfterAspNetUserTokenUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAspNetUserTokenCreated(CP.Server.Models.CarPark.AspNetUserToken item);
        partial void OnAfterAspNetUserTokenCreated(CP.Server.Models.CarPark.AspNetUserToken item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] CP.Server.Models.CarPark.AspNetUserToken item)
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

                this.OnAspNetUserTokenCreated(item);
                this.context.AspNetUserTokens.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AspNetUserTokens.Where(i => i.UserId == item.UserId && i.LoginProvider == item.LoginProvider && i.Name == item.Name);

                Request.QueryString = Request.QueryString.Add("$expand", "AspNetUser");

                this.OnAfterAspNetUserTokenCreated(item);

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
