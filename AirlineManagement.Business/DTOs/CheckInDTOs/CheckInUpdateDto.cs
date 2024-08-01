using AirlineManagement.Domain.Enums;

namespace AirlineManagement.Business.DTOs.CheckInDTOs
{
    public class CheckInUpdateDto: BaseDto
    {
        public string CheckInId { get; set; }
        public string ReservationId { get; set; }
        public CheckInStatus? Status { get; set; } 
        public int BaggageCount { get; set; }
        public DateTime BoardingTime { get; set; }
    }
}
