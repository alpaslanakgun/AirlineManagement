using AirlineManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement.Business.DTOs.ReservationDTOs
{
    public class ReservationCreateDto: BaseDto
    {
        public string PassengerId { get; set; }
        public string FlightNumber { get; set; }
        public ReservationStatus Status { get; set; } = ReservationStatus.Pending;
        public string Seat { get; set; }
    }
}
