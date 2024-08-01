using AirlineManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement.Domain.Entities
{
    public class Passenger : BaseEntity, IEntity
    {
        public string PassengerId { get; set; }
        public string Name { get; set; }
        public ICollection<Reservation> Reservations { get; set; } 

    }
}
