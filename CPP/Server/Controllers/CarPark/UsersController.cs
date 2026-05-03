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
    [Route("odata/CarPark/Users")]
    public partial class UsersController : ODataController
    {
        private CP.Server.Data.CarParkContext context;

        public UsersController(CP.Server.Data.CarParkContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<CP.Server.Models.CarPark.User> GetUsers()
        {
            var items = this.context.Users.AsQueryable<CP.Server.Models.CarPark.User>();
            this.OnUsersRead(ref items);

            return items;
        }

        partial void OnUsersRead(ref IQueryable<CP.Server.Models.CarPark.User> items);

        partial void OnUserGet(ref SingleResult<CP.Server.Models.CarPark.User> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CarPark/Users(UserId={UserId})")]
        public SingleResult<CP.Server.Models.CarPark.User> GetUser(int key)
        {
            var items = this.context.Users.Where(i => i.UserId == key);
            var result = SingleResult.Create(items);

            OnUserGet(ref result);

            return result;
        }
        partial void OnUserDeleted(CP.Server.Models.CarPark.User item);
        partial void OnAfterUserDeleted(CP.Server.Models.CarPark.User item);

        [HttpDelete("/odata/CarPark/Users(UserId={UserId})")]
        public IActionResult DeleteUser(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.Users
                    .Where(i => i.UserId == key)
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnUserDeleted(item);
                this.context.Users.Remove(item);
                this.context.SaveChanges();
                this.OnAfterUserDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnUserUpdated(CP.Server.Models.CarPark.User item);
        partial void OnAfterUserUpdated(CP.Server.Models.CarPark.User item);

        [HttpPut("/odata/CarPark/Users(UserId={UserId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutUser(int key, [FromBody]CP.Server.Models.CarPark.User item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (item == null || (item.UserId != key))
                {
                    return BadRequest();
                }
                this.OnUserUpdated(item);
                this.context.Users.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Users.Where(i => i.UserId == key);
                
                this.OnAfterUserUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CarPark/Users(UserId={UserId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchUser(int key, [FromBody]Delta<CP.Server.Models.CarPark.User> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.Users.Where(i => i.UserId == key).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnUserUpdated(item);
                this.context.Users.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Users.Where(i => i.UserId == key);
                
                this.OnAfterUserUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnUserCreated(CP.Server.Models.CarPark.User item);
        partial void OnAfterUserCreated(CP.Server.Models.CarPark.User item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] CP.Server.Models.CarPark.User item)
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

                this.OnUserCreated(item);
                this.context.Users.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Users.Where(i => i.UserId == item.UserId);

                

                this.OnAfterUserCreated(item);

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
