using AirlineManagement.Business.DTOs.FlightDTOs;
using AirlineManagement.Domain.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirlineManagement.Business.Contracts
{
    public interface IFlightService
    {
        Task<IDataResult<IEnumerable<FlightDto>>> GetFlightsAsync();
        Task<IDataResult<FlightDto>> GetFlightDetailsAsync(string flightNumber);
        Task<IDataResult<FlightDto>> CreateFlightAsync(FlightCreateDto flightCreateDto);
        Task<IDataResult<FlightDto>> UpdateFlightAsync(FlightUpdateDto flightUpdateDto);
        Task<IResult> DeleteFlightAsync(FlightDeleteDto flightDeleteDto);
        Task<IResult> HardDeleteFlightAsync(FlightDeleteDto flightDeleteDto);
        Task<IDataResult<IEnumerable<FlightDto>>> SearchFlightsAsync(string departureAirport, string arrivalAirport, DateTime departureDate);
        Task<IDataResult<IEnumerable<FlightDto>>> SearchFlightsWithCriteriaAsync(string departureAirport, string arrivalAirport, DateTime? departureDate, string status);
        Task<IDataResult<IEnumerable<FlightDto>>> GetFlightsWithDetailsAsync();
    }
}
