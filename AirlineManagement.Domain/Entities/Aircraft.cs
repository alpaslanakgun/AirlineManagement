using AirlineManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement.Domain.Entities
{
    public class Aircraft: IEntity
    {
        public string AircraftId { get; set; }
        public string Model { get; set; }
        public int Capacity { get; set; }
        public string AirlineId { get; set; }
    }
}
