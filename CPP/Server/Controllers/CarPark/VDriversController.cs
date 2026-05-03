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
    [Route("odata/CarPark/VDrivers")]
    public partial class VDriversController : ODataController
    {
        private CP.Server.Data.CarParkContext context;

        public VDriversController(CP.Server.Data.CarParkContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<CP.Server.Models.CarPark.VDriver> GetVDrivers()
        {
            var items = this.context.VDrivers.AsQueryable<CP.Server.Models.CarPark.VDriver>();
            this.OnVDriversRead(ref items);

            return items;
        }

        partial void OnVDriversRead(ref IQueryable<CP.Server.Models.CarPark.VDriver> items);
    }
}
