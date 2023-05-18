namespace NEWSHORE_AIR_BUSINESS.Models
{
    public class TransportResponse
    {
        public TransportResponse(string flightCarrier, string flightNumber)
        {
            FlightCarrier = flightCarrier;
            FlightNumber = flightNumber;
        }
        public string FlightCarrier { get; set; }
        public string FlightNumber { get; set; }
    }
}
