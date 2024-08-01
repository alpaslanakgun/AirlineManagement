using AirlineManagement.Business.DTOs.PassengerDTOs;
using AirlineManagement.Domain.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement.Business.Contracts
{
    public interface IPassengerService
    {
        Task<IDataResult<IEnumerable<PassengerDto>>> GetPassengersAsync();
        Task<IDataResult<PassengerDto>> GetPassengerDetailsAsync(string passengerId);
        Task<IDataResult<PassengerDto>> CreatePassengerAsync(PassengerCreateDto passengerCreateDto);
        Task<IDataResult<PassengerDto>> UpdatePassengerAsync(PassengerUpdateDto passengerUpdateDto);
        Task<IResult> DeletePassengerAsync(PassengerDeleteDto passengerDeleteDto);
        Task<IResult> HardDeletePassengerAsync(PassengerDeleteDto passengerDeleteDto);
    }
}
