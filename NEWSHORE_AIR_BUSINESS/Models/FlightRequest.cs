using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEWSHORE_AIR_BUSINESS.Models
{
    public class FlightRequest
    {
        public FlightRequest(string origin, string destination)
        {
            Origin = origin;
            Destination = destination;
        }

        string Origin { get; set; }
        string Destination { get; set; }
    }

   
}
