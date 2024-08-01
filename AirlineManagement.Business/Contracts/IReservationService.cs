using AirlineManagement.Business.DTOs.ReservationDTOs;
using AirlineManagement.Domain.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement.Business.Contracts
{
    public interface IReservationService
    {
        Task<IDataResult<IEnumerable<ReservationDto>>> GetReservationsAsync();
        Task<IDataResult<ReservationDto>> GetReservationDetailsAsync(string reservationId);
        Task<IDataResult<ReservationDto>> CreateReservationAsync(ReservationCreateDto reservationCreateDto);
        Task<IDataResult<ReservationDto>> UpdateReservationAsync(ReservationUpdateDto reservationUpdateDto);
        Task<IResult> DeleteReservationAsync(ReservationDeleteDto reservationDeleteDto);
        Task<IResult> HardDeleteReservationAsync(ReservationDeleteDto reservationDeleteDto);
    }
}
