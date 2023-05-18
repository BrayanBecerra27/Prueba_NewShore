using NEWSHORE_AIR_BUSINESS.Entity;

namespace NEWSHORE_AIR_BUSINESS.Models
{
    public class FlightResponse
    {
        public FlightResponse(string origin, string destination, double price, TransportResponse transport)
        {
            Origin = origin;
            Destination = destination;
            Price = price;
            Transport = transport;
        }
        public FlightResponse() {
            Origin = string.Empty;
            Destination = string.Empty;
            Transport = new TransportResponse(string.Empty, string.Empty);
        }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public double Price { get; set; }
        public TransportResponse Transport { get; set; }
    }
}
