using AirlineManagement.Domain.Common;
using AirlineManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement.Domain.Entities
{
    public class Reservation : BaseEntity, IEntity
    {
        public string ReservationId { get; set; }
        public string PassengerId { get; set; }
        public string FlightNumber { get; set; }
        public ReservationStatus Status { get; set; }
        public string Seat { get; set; }
        public Passenger Passenger { get; set; } 
        public Flight Flight { get; set; } 
        public ICollection<CheckIn> CheckIns { get; set; } 
    }
}
