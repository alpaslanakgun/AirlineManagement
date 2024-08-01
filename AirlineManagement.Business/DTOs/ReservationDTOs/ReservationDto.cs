using AirlineManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement.Business.DTOs.ReservationDTOs
{
    public class ReservationDto: BaseDto
    {
        public string ReservationId { get; set; }
        public string PassengerId { get; set; }
        public string FlightNumber { get; set; }
        public ReservationStatus Status { get; set; }
        public string Seat { get; set; }
    }
}
