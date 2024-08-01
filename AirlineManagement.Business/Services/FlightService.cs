using AirlineManagement.Business.Common.MessageConstant;
using AirlineManagement.Business.Contracts;
using AirlineManagement.Business.DTOs.FlightDTOs;
using AirlineManagement.Data.Contracts;
using AirlineManagement.Domain.Entities;
using AirlineManagement.Domain.Results.Abstract;
using AirlineManagement.Domain.Results.ComplexType;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineManagement.Business.Services
{
    public class FlightService : IFlightService
    {
        private readonly IFlightRepository _flightRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FlightService(IFlightRepository flightRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _flightRepository = flightRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        private async Task<string> GenerateFlightNumber()
        {
            var lastFlightNumber = await _flightRepository.GetAllAsync()
                .ContinueWith(task => task.Result
                    .OrderByDescending(f => f.FlightNumber)
                    .Select(f => f.FlightNumber)
                    .FirstOrDefault());

            if (lastFlightNumber == null)
            {
                return "FL001";
            }
            else
            {
                var prefix = lastFlightNumber.Substring(0, 2);
                var lastIdNumber = int.Parse(lastFlightNumber.Substring(2));
                var newIdNumber = lastIdNumber + 1;
                return $"{prefix}{newIdNumber:D3}";
            }
        }

        public async Task<IDataResult<IEnumerable<FlightDto>>> GetFlightsAsync()
        {
            try
            {
                var flights = await _flightRepository.GetAllAsync();
                var flightDtos = _mapper.Map<IEnumerable<FlightDto>>(flights);
                return new SuccessDataResult<IEnumerable<FlightDto>>(flightDtos, Messages.FlightSearchSuccessful);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<IEnumerable<FlightDto>>($"{Messages.FlightSearchFailed}: {ex.Message}");
            }
        }

        public async Task<IDataResult<FlightDto>> GetFlightDetailsAsync(string flightNumber)
        {
            try
            {
                var flight = await _flightRepository.GetAsync(f => f.FlightNumber == flightNumber);
                if (flight == null)
                {
                    return new ErrorDataResult<FlightDto>(Messages.FlightNotFound);
                }
                var flightDto = _mapper.Map<FlightDto>(flight);
                return new SuccessDataResult<FlightDto>(flightDto, Messages.FlightSearchSuccessful);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<FlightDto>($"{Messages.FlightSearchFailed}: {ex.Message}");
            }
        }

        public async Task<IDataResult<FlightDto>> CreateFlightAsync(FlightCreateDto flightCreateDto)
        {
            try
            {
                var flight = _mapper.Map<Flight>(flightCreateDto);
                flight.FlightNumber = await GenerateFlightNumber();
                flight.CreatedDate = DateTime.Now;
                flight.UpdatedDate = DateTime.Now;
                flight.IsDeleted = false;
                flight.IsActive = true;

                await _flightRepository.AddAsync(flight);
                await _unitOfWork.CommitAsync();
                var createdFlightDto = _mapper.Map<FlightDto>(flight);
                return new SuccessDataResult<FlightDto>(createdFlightDto, Messages.FlightSearchSuccessful);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<FlightDto>($"{Messages.FlightSearchFailed}: {ex.Message}");
            }
        }

        public async Task<IDataResult<FlightDto>> UpdateFlightAsync(FlightUpdateDto flightUpdateDto)
        {
            try
            {
                var flight = await _flightRepository.GetAsync(f => f.FlightNumber == flightUpdateDto.FlightNumber);

                if (flight == null)
                {
                    return new ErrorDataResult<FlightDto>(Messages.FlightNotFound);
                }

                _mapper.Map(flightUpdateDto, flight);
                flight.UpdatedDate = DateTime.Now;
                await _flightRepository.UpdateAsync(flight);
                await _unitOfWork.CommitAsync();

                var updatedFlightDto = _mapper.Map<FlightDto>(flight);
                return new SuccessDataResult<FlightDto>(updatedFlightDto, Messages.FlightUpdateSuccessful);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<FlightDto>($"{Messages.FlightUpdateFailed}: {ex.Message}");
            }
        }

        public async Task<IResult> DeleteFlightAsync(FlightDeleteDto flightDeleteDto)
        {
            try
            {
                var flight = await _flightRepository.GetAsync(f => f.FlightNumber == flightDeleteDto.FlightNumber);
                if (flight == null)
                {
                    return new ErrorResult(Messages.FlightNotFound);
                }

                flight.IsDeleted = true;
                flight.UpdatedDate = DateTime.Now;
                await _flightRepository.UpdateAsync(flight);
                await _unitOfWork.CommitAsync();
                return new SuccessResult(Messages.FlightDeletionSuccessful);
            }
            catch (Exception ex)
            {
                return new ErrorResult($"{Messages.FlightDeletionFailed}: {ex.Message}");
            }
        }

        public async Task<IResult> HardDeleteFlightAsync(FlightDeleteDto flightDeleteDto)
        {
            try
            {
                var flight = await _flightRepository.GetAsync(f => f.FlightNumber == flightDeleteDto.FlightNumber);
                if (flight == null)
                {
                    return new ErrorResult(Messages.FlightNotFound);
                }

                await _flightRepository.DeleteAsync(flight);
                await _unitOfWork.CommitAsync();
                return new SuccessResult(Messages.FlightDeletionSuccessful);
            }
            catch (Exception ex)
            {
                return new ErrorResult($"{Messages.FlightDeletionFailed}: {ex.Message}");
            }
        }

        public async Task<IDataResult<IEnumerable<FlightDto>>> SearchFlightsAsync(string departureAirport, string arrivalAirport, DateTime departureDate)
        {
            try
            {
                var flights = await _flightRepository.SearchFlightsAsync(departureAirport, arrivalAirport, departureDate);
                var flightDtos = _mapper.Map<IEnumerable<FlightDto>>(flights);
                return new SuccessDataResult<IEnumerable<FlightDto>>(flightDtos, Messages.FlightSearchSuccessful);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<IEnumerable<FlightDto>>($"{Messages.FlightSearchFailed}: {ex.Message}");
            }
        }

        public async Task<IDataResult<IEnumerable<FlightDto>>> SearchFlightsWithCriteriaAsync(string departureAirport, string arrivalAirport, DateTime? departureDate, string status)
        {
            try
            {
                var flights = await _flightRepository.SearchFlightsAsync(departureAirport, arrivalAirport, departureDate, status);
                var flightDtos = _mapper.Map<IEnumerable<FlightDto>>(flights);
                return new SuccessDataResult<IEnumerable<FlightDto>>(flightDtos, Messages.FlightSearchSuccessful);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<IEnumerable<FlightDto>>($"{Messages.FlightSearchFailed}: {ex.Message}");
            }
        }

        public async Task<IDataResult<IEnumerable<FlightDto>>> GetFlightsWithDetailsAsync()
        {
            try
            {
                var flights = await _flightRepository.GetFlightsWithDetailsAsync();
                var flightDtos = _mapper.Map<IEnumerable<FlightDto>>(flights);
                return new SuccessDataResult<IEnumerable<FlightDto>>(flightDtos, Messages.FlightSearchSuccessful);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<IEnumerable<FlightDto>>($"{Messages.FlightSearchFailed}: {ex.Message}");
            }
        }
    }
}
