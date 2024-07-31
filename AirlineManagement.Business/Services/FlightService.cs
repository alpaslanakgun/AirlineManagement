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
using System.Text;
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

        public async Task<IDataResult<IEnumerable<FlightDto>>> GetFlightsAsync()
        {
            try
            {
                var flights = await _flightRepository.GetAllAsync();
                var flightDtos = _mapper.Map<IEnumerable<FlightDto>>(flights);
                return new SuccessDataResult<IEnumerable<FlightDto>>(flightDtos, "Uçuşlar başarıyla alındı.");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<IEnumerable<FlightDto>>($"Uçuşlar alınırken bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<IDataResult<FlightDto>> GetFlightDetailsAsync(int flightId)
        {
            try
            {
                var flight = await _flightRepository.GetAsync(f => f.Id == flightId);
                if (flight == null)
                {
                    return new ErrorDataResult<FlightDto>("Uçuş bulunamadı.");
                }
                var flightDto = _mapper.Map<FlightDto>(flight);
                return new SuccessDataResult<FlightDto>(flightDto, "Uçuş detayları başarıyla alındı.");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<FlightDto>($"Uçuş detayları alınırken bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<IDataResult<FlightDto>> CreateFlightAsync(FlightCreateDto flightCreateDto)
        {
            try
            {
                var flight = _mapper.Map<Flight>(flightCreateDto);
                await _flightRepository.AddAsync(flight);
                await _unitOfWork.CommitAsync();
                var createdFlightDto = _mapper.Map<FlightDto>(flight);
                return new SuccessDataResult<FlightDto>(createdFlightDto, "Uçuş başarıyla oluşturuldu.");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<FlightDto>($"Uçuş oluşturulurken bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<IDataResult<FlightDto>> UpdateFlightAsync(FlightUpdateDto flightUpdateDto)
        {
            if (_flightRepository == null)
            {
                throw new ArgumentNullException(nameof(_flightRepository));
            }

            if (_mapper == null)
            {
                throw new ArgumentNullException(nameof(_mapper));
            }

            try
            {
                var flight = await _flightRepository.GetAsync(f => f.Id == flightUpdateDto.Id);

                if (flight == null)
                {
                    return new ErrorDataResult<FlightDto>("Uçuş bulunamadı.");
                }

                _mapper.Map(flightUpdateDto, flight);
                await _flightRepository.UpdateAsync(flight);
                await _unitOfWork.CommitAsync();

                var updatedFlightDto = _mapper.Map<FlightDto>(flight);
                return new SuccessDataResult<FlightDto>(updatedFlightDto, "Uçuş başarıyla güncellendi.");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<FlightDto>($"Uçuş güncellenirken bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<IResult> DeleteFlightAsync(FlightDeleteDto flightDeleteDto)
        {
            try
            {
                var flight = await _flightRepository.GetAsync(f => f.Id == flightDeleteDto.Id);
                if (flight == null)
                {
                    return new ErrorResult("Uçuş bulunamadı.");
                }

                await _flightRepository.DeleteAsync(flight);
                await _unitOfWork.CommitAsync();
                return new SuccessResult("Uçuş başarıyla silindi.");
            }
            catch (Exception ex)
            {
                return new ErrorResult($"Uçuş silinirken bir hata oluştu: {ex.Message}");
            }
        }
        public async Task<IDataResult<IEnumerable<FlightDto>>> SearchFlightsAsync(string search)
        {
            try
            {
                var flights = _flightRepository.GetFlightsMatchingSearch(search).ToList();
                var flightDtos = _mapper.Map<IEnumerable<FlightDto>>(flights);
                return new SuccessDataResult<IEnumerable<FlightDto>>(flightDtos, "Arama sonuçları başarıyla alındı.");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<IEnumerable<FlightDto>>($"Uçuşlar aranırken bir hata oluştu: {ex.Message}");
            }
        }
    }
}

