using NEWSHORE_AIR_BUSINESS.Entity;

namespace NEWSHORE_AIR_BUSINESS.Models
{
    public class JourneyResponse
    {

        public JourneyResponse(string origin, string destination, double price, List<FlightResponse> flights)
        {
            Origin = origin;
            Destination = destination;
            Price = price;
            Flights = flights;
        }
        public JourneyResponse()
        {
            Origin = string.Empty;
            Destination = string.Empty;
            Price = 0;
            Flights = new List<FlightResponse>();
        }

        public string Origin { get; set; }
        public string Destination { get; set; }
        public double Price { get; set; }
        public List<FlightResponse> Flights { get; set; }
    }
}
