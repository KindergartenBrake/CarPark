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
    [Route("odata/CarPark/TripRequests")]
    public partial class TripRequestsController : ODataController
    {
        private CP.Server.Data.CarParkContext context;

        public TripRequestsController(CP.Server.Data.CarParkContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<CP.Server.Models.CarPark.TripRequest> GetTripRequests()
        {
            var items = this.context.TripRequests.AsQueryable<CP.Server.Models.CarPark.TripRequest>();
            this.OnTripRequestsRead(ref items);

            return items;
        }

        partial void OnTripRequestsRead(ref IQueryable<CP.Server.Models.CarPark.TripRequest> items);

        partial void OnTripRequestGet(ref SingleResult<CP.Server.Models.CarPark.TripRequest> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CarPark/TripRequests(RequestId={RequestId})")]
        public SingleResult<CP.Server.Models.CarPark.TripRequest> GetTripRequest(int key)
        {
            var items = this.context.TripRequests.Where(i => i.RequestId == key);
            var result = SingleResult.Create(items);

            OnTripRequestGet(ref result);

            return result;
        }
        partial void OnTripRequestDeleted(CP.Server.Models.CarPark.TripRequest item);
        partial void OnAfterTripRequestDeleted(CP.Server.Models.CarPark.TripRequest item);

        [HttpDelete("/odata/CarPark/TripRequests(RequestId={RequestId})")]
        public IActionResult DeleteTripRequest(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.TripRequests
                    .Where(i => i.RequestId == key)
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnTripRequestDeleted(item);
                this.context.TripRequests.Remove(item);
                this.context.SaveChanges();
                this.OnAfterTripRequestDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnTripRequestUpdated(CP.Server.Models.CarPark.TripRequest item);
        partial void OnAfterTripRequestUpdated(CP.Server.Models.CarPark.TripRequest item);

        [HttpPut("/odata/CarPark/TripRequests(RequestId={RequestId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutTripRequest(int key, [FromBody]CP.Server.Models.CarPark.TripRequest item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (item == null || (item.RequestId != key))
                {
                    return BadRequest();
                }
                this.OnTripRequestUpdated(item);
                this.context.TripRequests.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.TripRequests.Where(i => i.RequestId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Driver,User,Vehicle");
                this.OnAfterTripRequestUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CarPark/TripRequests(RequestId={RequestId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchTripRequest(int key, [FromBody]Delta<CP.Server.Models.CarPark.TripRequest> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.TripRequests.Where(i => i.RequestId == key).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnTripRequestUpdated(item);
                this.context.TripRequests.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.TripRequests.Where(i => i.RequestId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Driver,User,Vehicle");
                this.OnAfterTripRequestUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnTripRequestCreated(CP.Server.Models.CarPark.TripRequest item);
        partial void OnAfterTripRequestCreated(CP.Server.Models.CarPark.TripRequest item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] CP.Server.Models.CarPark.TripRequest item)
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

                this.OnTripRequestCreated(item);
                this.context.TripRequests.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.TripRequests.Where(i => i.RequestId == item.RequestId);

                Request.QueryString = Request.QueryString.Add("$expand", "Driver,User,Vehicle");

                this.OnAfterTripRequestCreated(item);

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
