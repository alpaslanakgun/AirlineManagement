using AirlineManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement.Domain.Entities
{
    public class Flight: BaseEntity, IEntity
    {
        public string FlightNumber { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public string AircraftId { get; set; }
        public string Status { get; set; }
        public Airport DepartureAirportNavigation { get; set; } 
        public Airport ArrivalAirportNavigation { get; set; } 
        public Aircraft Aircraft { get; set; } 
        public ICollection<Reservation> Reservations { get; set; }
    }
}
