using AirlineManagement.Business.DTOs.ReservationDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement.Business.Contracts
{
    public interface IReservationService
    {
        Task<IEnumerable<ReservationDto>> GetReservationsAsync();
        Task<ReservationDto> GetReservationDetailsAsync(int reservationId);
        Task<ReservationDto> CreateReservationAsync(ReservationCreateDto reservationCreateDto);
        Task<ReservationDto> UpdateReservationAsync(ReservationUpdateDto reservationUpdateDto);
        Task<bool> DeleteReservationAsync(ReservationDeleteDto reservationDeleteDto);
    }
}
