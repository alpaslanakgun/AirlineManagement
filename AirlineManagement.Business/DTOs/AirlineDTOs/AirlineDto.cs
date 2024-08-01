using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement.Business.DTOs.AirlineDTOs
{
    public class AirlineDto:BaseDto
    {
        public string AirlineId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
    }
}
