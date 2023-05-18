using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace NEWSHORE_AIR_BUSINESS.Entity
{
    [Table("Journey", Schema = "dbo")]
    public class Journey
    {
        public Journey(string origin, string destination, double price, List<Flight> flights)
        {
            Origin = origin;
            Destination = destination;
            Price = price;
            Flights = flights;
            RouteType = string.Empty;
        }
        public Journey() {
            Origin = string.Empty;
            Destination = string.Empty;
            Price = 0;
            Flights = new List<Flight> ();
            RouteType = string.Empty;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required(ErrorMessage = "JourneyId is required")]
        [Column("JourneyId")]
        [JsonIgnore]
        public int JourneyId { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public double Price { get; set; }
        [NotMapped]
        public List<Flight> Flights { get; set; }
        [JsonIgnore]
        public string RouteType { get; set; }
    }
}
