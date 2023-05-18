using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NEWSHORE_AIR_BUSINESS.Entity
{
    [Table("Flight", Schema = "dbo")]
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

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required(ErrorMessage = "FlightId is required")]
        [Column("FlightId")]
        [JsonIgnore]
        public int FlightId { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public double Price { get; set; }
        [NotMapped]
        public Transport Transport { get; set; }
        [JsonIgnore]
        public int JourneyId { get; set; }
        [JsonIgnore]
        public int TransportId { get; set; }
        [JsonIgnore]
        [NotMapped]
        public Journey Journeys { get; set; }
    }
}
