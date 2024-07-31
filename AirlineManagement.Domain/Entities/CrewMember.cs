using AirlineManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement.Domain.Entities
{
    public class CrewMember: IEntity
    {
        public string MemberId { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string AirlineId { get; set; }
    }
}
