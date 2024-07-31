using AirlineManagement.Business.DTOs.FlightDTOs;
using AirlineManagement.Domain.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement.Business.Contracts
{
    public interface IFlightService
    {

        Task<IDataResult<IEnumerable<FlightDto>>> GetFlightsAsync();
        Task<IDataResult<FlightDto>> GetFlightDetailsAsync(int flightId);
        Task<IDataResult<FlightDto>> CreateFlightAsync(FlightCreateDto flightCreateDto);
        Task<IDataResult<FlightDto>> UpdateFlightAsync(FlightUpdateDto flightUpdateDto);
        Task<IResult> DeleteFlightAsync(FlightDeleteDto flightDeleteDto);
        Task<IDataResult<IEnumerable<FlightDto>>> SearchFlightsAsync(string search);
    }
}
