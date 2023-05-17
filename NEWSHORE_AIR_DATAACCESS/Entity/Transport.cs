using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEWSHORE_AIR_DATAACCESS.Entity
{
    public  class Transport
    {
        public Transport(string flightCarrier, string flightNumber)
        {
            FlightCarrier = flightCarrier;
            FlightNumber = flightNumber;
        }
        public string FlightCarrier { get; set; }
        public string FlightNumber { get; set; }
    }
}
