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
    [Route("odata/CarPark/Trips")]
    public partial class TripsController : ODataController
    {
        private CP.Server.Data.CarParkContext context;

        public TripsController(CP.Server.Data.CarParkContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<CP.Server.Models.CarPark.Trip> GetTrips()
        {
            var items = this.context.Trips.AsQueryable<CP.Server.Models.CarPark.Trip>();
            this.OnTripsRead(ref items);

            return items;
        }

        partial void OnTripsRead(ref IQueryable<CP.Server.Models.CarPark.Trip> items);

        partial void OnTripGet(ref SingleResult<CP.Server.Models.CarPark.Trip> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CarPark/Trips(TripId={TripId})")]
        public SingleResult<CP.Server.Models.CarPark.Trip> GetTrip(int key)
        {
            var items = this.context.Trips.Where(i => i.TripId == key);
            var result = SingleResult.Create(items);

            OnTripGet(ref result);

            return result;
        }
        partial void OnTripDeleted(CP.Server.Models.CarPark.Trip item);
        partial void OnAfterTripDeleted(CP.Server.Models.CarPark.Trip item);

        [HttpDelete("/odata/CarPark/Trips(TripId={TripId})")]
        public IActionResult DeleteTrip(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.Trips
                    .Where(i => i.TripId == key)
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnTripDeleted(item);
                this.context.Trips.Remove(item);
                this.context.SaveChanges();
                this.OnAfterTripDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnTripUpdated(CP.Server.Models.CarPark.Trip item);
        partial void OnAfterTripUpdated(CP.Server.Models.CarPark.Trip item);

        [HttpPut("/odata/CarPark/Trips(TripId={TripId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutTrip(int key, [FromBody]CP.Server.Models.CarPark.Trip item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (item == null || (item.TripId != key))
                {
                    return BadRequest();
                }
                this.OnTripUpdated(item);
                this.context.Trips.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Trips.Where(i => i.TripId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "TripRequest,Vehicle");
                this.OnAfterTripUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CarPark/Trips(TripId={TripId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchTrip(int key, [FromBody]Delta<CP.Server.Models.CarPark.Trip> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.Trips.Where(i => i.TripId == key).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnTripUpdated(item);
                this.context.Trips.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Trips.Where(i => i.TripId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "TripRequest,Vehicle");
                this.OnAfterTripUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnTripCreated(CP.Server.Models.CarPark.Trip item);
        partial void OnAfterTripCreated(CP.Server.Models.CarPark.Trip item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] CP.Server.Models.CarPark.Trip item)
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

                this.OnTripCreated(item);
                this.context.Trips.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Trips.Where(i => i.TripId == item.TripId);

                Request.QueryString = Request.QueryString.Add("$expand", "TripRequest,Vehicle");

                this.OnAfterTripCreated(item);

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
