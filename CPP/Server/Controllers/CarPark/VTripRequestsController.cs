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
    [Route("odata/CarPark/VTripRequests")]
    public partial class VTripRequestsController : ODataController
    {
        private CP.Server.Data.CarParkContext context;

        public VTripRequestsController(CP.Server.Data.CarParkContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<CP.Server.Models.CarPark.VTripRequest> GetVTripRequests()
        {
            var items = this.context.VTripRequests.AsQueryable<CP.Server.Models.CarPark.VTripRequest>();
            this.OnVTripRequestsRead(ref items);

            return items;
        }

        partial void OnVTripRequestsRead(ref IQueryable<CP.Server.Models.CarPark.VTripRequest> items);
    }
}
