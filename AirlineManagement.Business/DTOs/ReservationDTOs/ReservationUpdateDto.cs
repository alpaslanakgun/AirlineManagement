using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement.Business.DTOs.ReservationDTOs
{
    public class ReservationUpdateDto
    {
        public int Id { get; set; }
        public int FlightId { get; set; }
        public string PassengerName { get; set; }
        public DateTime ReservationDate { get; set; }
        public string Seat { get; set; }
        public string Status { get; set; }
    }
}
