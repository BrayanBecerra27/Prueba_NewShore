using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace NEWSHORE_AIR_BUSINESS.Entity
{
    [Table("Transport", Schema = "dbo")]
    public  class Transport
    {
        public Transport(string flightCarrier, string flightNumber)
        {
            FlightCarrier = flightCarrier;
            FlightNumber = flightNumber;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required(ErrorMessage = "TransportId is required")]
        [Column("TransportId")]
        [JsonIgnore()]
        public int TransportId { get; set; }
        public string FlightCarrier { get; set; }
        public string FlightNumber { get; set; }
    }
}
