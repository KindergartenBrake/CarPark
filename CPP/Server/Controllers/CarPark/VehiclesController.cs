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
    [Route("odata/CarPark/Vehicles")]
    public partial class VehiclesController : ODataController
    {
        private CP.Server.Data.CarParkContext context;

        public VehiclesController(CP.Server.Data.CarParkContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<CP.Server.Models.CarPark.Vehicle> GetVehicles()
        {
            var items = this.context.Vehicles.AsQueryable<CP.Server.Models.CarPark.Vehicle>();
            this.OnVehiclesRead(ref items);

            return items;
        }

        partial void OnVehiclesRead(ref IQueryable<CP.Server.Models.CarPark.Vehicle> items);

        partial void OnVehicleGet(ref SingleResult<CP.Server.Models.CarPark.Vehicle> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CarPark/Vehicles(VehicleId={VehicleId})")]
        public SingleResult<CP.Server.Models.CarPark.Vehicle> GetVehicle(int key)
        {
            var items = this.context.Vehicles.Where(i => i.VehicleId == key);
            var result = SingleResult.Create(items);

            OnVehicleGet(ref result);

            return result;
        }
        partial void OnVehicleDeleted(CP.Server.Models.CarPark.Vehicle item);
        partial void OnAfterVehicleDeleted(CP.Server.Models.CarPark.Vehicle item);

        [HttpDelete("/odata/CarPark/Vehicles(VehicleId={VehicleId})")]
        public IActionResult DeleteVehicle(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.Vehicles
                    .Where(i => i.VehicleId == key)
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnVehicleDeleted(item);
                this.context.Vehicles.Remove(item);
                this.context.SaveChanges();
                this.OnAfterVehicleDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnVehicleUpdated(CP.Server.Models.CarPark.Vehicle item);
        partial void OnAfterVehicleUpdated(CP.Server.Models.CarPark.Vehicle item);

        [HttpPut("/odata/CarPark/Vehicles(VehicleId={VehicleId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutVehicle(int key, [FromBody]CP.Server.Models.CarPark.Vehicle item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (item == null || (item.VehicleId != key))
                {
                    return BadRequest();
                }
                this.OnVehicleUpdated(item);
                this.context.Vehicles.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Vehicles.Where(i => i.VehicleId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Driver");
                this.OnAfterVehicleUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CarPark/Vehicles(VehicleId={VehicleId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchVehicle(int key, [FromBody]Delta<CP.Server.Models.CarPark.Vehicle> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.Vehicles.Where(i => i.VehicleId == key).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnVehicleUpdated(item);
                this.context.Vehicles.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Vehicles.Where(i => i.VehicleId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Driver");
                this.OnAfterVehicleUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnVehicleCreated(CP.Server.Models.CarPark.Vehicle item);
        partial void OnAfterVehicleCreated(CP.Server.Models.CarPark.Vehicle item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] CP.Server.Models.CarPark.Vehicle item)
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

                this.OnVehicleCreated(item);
                this.context.Vehicles.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Vehicles.Where(i => i.VehicleId == item.VehicleId);

                Request.QueryString = Request.QueryString.Add("$expand", "Driver");

                this.OnAfterVehicleCreated(item);

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
