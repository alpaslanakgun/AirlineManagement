using AirlineManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement.Domain.Entities
{
    public class Airline:BaseEntity, IEntity
    {
        public string AirlineId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public ICollection<Aircraft> Aircrafts { get; set; } 
        public ICollection<CrewMember> CrewMembers { get; set; } 
    }
}
