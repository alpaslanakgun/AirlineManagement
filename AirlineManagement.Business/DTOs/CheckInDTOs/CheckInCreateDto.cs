using AirlineManagement.Domain.Enums;

namespace AirlineManagement.Business.DTOs.CheckInDTOs
{
    public class CheckInCreateDto 
    {
        public string ReservationId { get; set; }
        public int BaggageCount { get; set; }
    }
}
