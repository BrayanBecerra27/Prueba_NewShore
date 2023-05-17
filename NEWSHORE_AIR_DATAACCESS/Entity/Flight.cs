using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEWSHORE_AIR_DATAACCESS.Entity
{
    public class Flight
    {
        public Flight(string origin, string destination, double price, Transport transport)
        {
            Origin = origin;
            Destination = destination;
            Price = price;
            Transport = transport;
        }
        public Flight() {
            Origin = string.Empty;
            Destination = string.Empty;
            Transport = new  Transport(string.Empty, string.Empty);
        }

        public string Origin { get; set; }
        public string Destination { get; set; }
        public double Price { get; set; }
        public Transport Transport { get; set; }
    }
}
