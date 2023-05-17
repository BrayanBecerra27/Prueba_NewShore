using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEWSHORE_AIR_DATAACCESS.Entity
{
    public class Journey
    {
        public Journey(string origin, string destination, double price, List<Flight> flights)
        {
            Origin = origin;
            Destination = destination;
            Price = price;
            Flights = flights;
        }

        public string Origin { get; set; }
        public string Destination { get; set; }
        public double Price { get; set; }
        public List<Flight> Flights { get; set; }
    }
}
