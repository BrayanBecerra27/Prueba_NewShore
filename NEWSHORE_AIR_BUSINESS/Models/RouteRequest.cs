using NEWSHORE_AIR_BUSINESS.Enumerator;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEWSHORE_AIR_BUSINESS.Models
{
    public class RouteRequest
    {
        public RouteRequest()
        {
            Origin = string.Empty;
            Destination = string.Empty;

        }
        [Required]
        public string Origin { get; set; }
        [Required]
        public string Destination { get; set; }
        public int Scale { get; set; }
        [Required]
        public RouteType RouteType { get; set; }

    }
}
