namespace AirlineManagement.Business.DTOs.CheckInDTOs
{
    public class CheckInUpdateDto
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public DateTime CheckInTime { get; set; }
        public string Status { get; set; }
        public int BaggageCount { get; set; }
    }
}
