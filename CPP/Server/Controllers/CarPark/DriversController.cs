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
    [Route("odata/CarPark/Drivers")]
    public partial class DriversController : ODataController
    {
        private CP.Server.Data.CarParkContext context;

        public DriversController(CP.Server.Data.CarParkContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<CP.Server.Models.CarPark.Driver> GetDrivers()
        {
            var items = this.context.Drivers.AsQueryable<CP.Server.Models.CarPark.Driver>();
            this.OnDriversRead(ref items);

            return items;
        }

        partial void OnDriversRead(ref IQueryable<CP.Server.Models.CarPark.Driver> items);

        partial void OnDriverGet(ref SingleResult<CP.Server.Models.CarPark.Driver> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CarPark/Drivers(DriverId={DriverId})")]
        public SingleResult<CP.Server.Models.CarPark.Driver> GetDriver(int key)
        {
            var items = this.context.Drivers.Where(i => i.DriverId == key);
            var result = SingleResult.Create(items);

            OnDriverGet(ref result);

            return result;
        }
        partial void OnDriverDeleted(CP.Server.Models.CarPark.Driver item);
        partial void OnAfterDriverDeleted(CP.Server.Models.CarPark.Driver item);

        [HttpDelete("/odata/CarPark/Drivers(DriverId={DriverId})")]
        public IActionResult DeleteDriver(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.Drivers
                    .Where(i => i.DriverId == key)
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnDriverDeleted(item);
                this.context.Drivers.Remove(item);
                this.context.SaveChanges();
                this.OnAfterDriverDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnDriverUpdated(CP.Server.Models.CarPark.Driver item);
        partial void OnAfterDriverUpdated(CP.Server.Models.CarPark.Driver item);

        [HttpPut("/odata/CarPark/Drivers(DriverId={DriverId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutDriver(int key, [FromBody]CP.Server.Models.CarPark.Driver item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (item == null || (item.DriverId != key))
                {
                    return BadRequest();
                }
                this.OnDriverUpdated(item);
                this.context.Drivers.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Drivers.Where(i => i.DriverId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "User,Vehicle");
                this.OnAfterDriverUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CarPark/Drivers(DriverId={DriverId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchDriver(int key, [FromBody]Delta<CP.Server.Models.CarPark.Driver> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.Drivers.Where(i => i.DriverId == key).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnDriverUpdated(item);
                this.context.Drivers.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Drivers.Where(i => i.DriverId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "User,Vehicle");
                this.OnAfterDriverUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnDriverCreated(CP.Server.Models.CarPark.Driver item);
        partial void OnAfterDriverCreated(CP.Server.Models.CarPark.Driver item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] CP.Server.Models.CarPark.Driver item)
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

                this.OnDriverCreated(item);
                this.context.Drivers.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Drivers.Where(i => i.DriverId == item.DriverId);

                Request.QueryString = Request.QueryString.Add("$expand", "User,Vehicle");

                this.OnAfterDriverCreated(item);

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
