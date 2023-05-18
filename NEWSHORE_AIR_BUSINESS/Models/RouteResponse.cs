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
        public string DepartureStation { get; set; }
        public string ArrivalStation { get; set; }
        public string FlightCarrier { get; set; }
        public string FlightNumber { get; set; }
        public int Price { get; set; }
    }
}
