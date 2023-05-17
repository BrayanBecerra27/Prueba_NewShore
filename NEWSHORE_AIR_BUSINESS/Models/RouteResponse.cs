using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEWSHORE_AIR_BUSINESS.Models
{
    public class RouteResponse
    {
        public RouteResponse() {
            DepartureStation = string.Empty;
            ArrivalStation = string.Empty;
            FlightCarrier = string.Empty;
            FlightNumber = string.Empty;
        }
        //[JsonProperty("departureStation")]
        public string DepartureStation { get; set; }
        //[JsonProperty("arrivalStation")]
        public string ArrivalStation { get; set; }
        //[JsonProperty("flightCarrier")]
        public string FlightCarrier { get; set; }
        //[JsonProperty("flightNumber")]
        public string FlightNumber { get; set; }
        //[JsonProperty("price")]
        public int Price { get; set; }
    }
}
