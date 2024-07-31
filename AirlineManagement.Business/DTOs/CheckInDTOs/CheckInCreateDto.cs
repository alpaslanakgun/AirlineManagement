namespace AirlineManagement.Business.DTOs.CheckInDTOs
{
    public class CheckInCreateDto
    {
        public int ReservationId { get; set; }
        public DateTime CheckInTime { get; set; }
        public string Status { get; set; }
        public int BaggageCount { get; set; }
    }
}
