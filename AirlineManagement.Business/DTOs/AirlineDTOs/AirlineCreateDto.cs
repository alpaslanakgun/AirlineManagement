using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement.Business.DTOs.AirlineDTOs
{
    public class AirlineCreateDto: BaseDto
    {
        public string Name { get; set; }
        public string Country { get; set; }
    }
}
