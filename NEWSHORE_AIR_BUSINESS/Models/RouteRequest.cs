using NEWSHORE_AIR_BUSINESS.Enumerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEWSHORE_AIR_BUSINESS.Models
{
    public class RouteRequest
    {
        public RouteRequest()
        {
            Origin = string.Empty;
            Destination = string.Empty;

        }

        public string Origin { get; set; }
        public string Destination { get; set; }
        public int Scale { get; set; }
        public RouteType RouteType { get; set; }

    }
}
