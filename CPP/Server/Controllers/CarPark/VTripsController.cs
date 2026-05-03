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
    [Route("odata/CarPark/VTrips")]
    public partial class VTripsController : ODataController
    {
        private CP.Server.Data.CarParkContext context;

        public VTripsController(CP.Server.Data.CarParkContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<CP.Server.Models.CarPark.VTrip> GetVTrips()
        {
            var items = this.context.VTrips.AsQueryable<CP.Server.Models.CarPark.VTrip>();
            this.OnVTripsRead(ref items);

            return items;
        }

        partial void OnVTripsRead(ref IQueryable<CP.Server.Models.CarPark.VTrip> items);
    }
}
