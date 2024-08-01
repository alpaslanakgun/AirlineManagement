using AirlineManagement.Business.Common.MessageConstant;
using AirlineManagement.Business.Contracts;
using AirlineManagement.Business.DTOs.ReservationDTOs;
using AirlineManagement.Data.Contracts;
using AirlineManagement.Domain.Entities;
using AirlineManagement.Domain.Enums;
using AirlineManagement.Domain.Results.Abstract;
using AirlineManagement.Domain.Results.ComplexType;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineManagement.Business.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReservationService(IReservationRepository reservationRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _reservationRepository = reservationRepository ?? throw new ArgumentNullException(nameof(reservationRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        private async Task<string> GenerateReservationId()
        {
            var lastReservationId = await _reservationRepository.GetAllAsync()
                .ContinueWith(task => task.Result
                    .OrderByDescending(r => r.ReservationId)
                    .Select(r => r.ReservationId)
                    .FirstOrDefault());

            if (lastReservationId == null)
            {
                return "R001";
            }
            else
            {
                var lastIdNumber = int.Parse(lastReservationId.Substring(1));
                var newIdNumber = lastIdNumber + 1;
                return $"R{newIdNumber:D3}";
            }
        }

        public async Task<IDataResult<IEnumerable<ReservationDto>>> GetReservationsAsync()
        {
            try
            {
                var reservations = await _reservationRepository.GetAllAsync();
                var reservationDtos = _mapper.Map<IEnumerable<ReservationDto>>(reservations);
                return new SuccessDataResult<IEnumerable<ReservationDto>>(reservationDtos, Messages.ReservationSearchSuccessful);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<IEnumerable<ReservationDto>>($"{Messages.ReservationSearchFailed}: {ex.Message}");
            }
        }

        public async Task<IDataResult<ReservationDto>> GetReservationDetailsAsync(string reservationId)
        {
            try
            {
                var reservation = await _reservationRepository.GetAsync(r => r.ReservationId == reservationId);
                if (reservation == null)
                {
                    return new ErrorDataResult<ReservationDto>(Messages.ReservationNotFound);
                }
                var reservationDto = _mapper.Map<ReservationDto>(reservation);
                return new SuccessDataResult<ReservationDto>(reservationDto, Messages.ReservationSearchSuccessful);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<ReservationDto>($"{Messages.ReservationSearchFailed}: {ex.Message}");
            }
        }

        public async Task<IDataResult<ReservationDto>> CreateReservationAsync(ReservationCreateDto reservationCreateDto)
        {
            try
            {
                var reservation = _mapper.Map<Reservation>(reservationCreateDto);
                reservation.ReservationId = await GenerateReservationId();
                reservation.CreatedDate = DateTime.Now;
                reservation.UpdatedDate = DateTime.Now;
                reservation.IsDeleted = false;
                reservation.IsActive = true;

                await _reservationRepository.AddAsync(reservation);
                await _unitOfWork.CommitAsync();

                var createdReservationDto = _mapper.Map<ReservationDto>(reservation);
                return new SuccessDataResult<ReservationDto>(createdReservationDto, Messages.ReservationCreationSuccessful);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<ReservationDto>($"{Messages.ReservationCreationFailed}: {ex.Message}");
            }
        }

        public async Task<IDataResult<ReservationDto>> UpdateReservationAsync(ReservationUpdateDto reservationUpdateDto)
        {
            try
            {
                var reservation = await _reservationRepository.GetAsync(r => r.ReservationId == reservationUpdateDto.ReservationId);

                if (reservation == null)
                {
                    return new ErrorDataResult<ReservationDto>(Messages.ReservationNotFound);
                }

                _mapper.Map(reservationUpdateDto, reservation);
                reservation.UpdatedDate = DateTime.Now;

                await _reservationRepository.UpdateAsync(reservation);
                await _unitOfWork.CommitAsync();

                var updatedReservationDto = _mapper.Map<ReservationDto>(reservation);
                return new SuccessDataResult<ReservationDto>(updatedReservationDto, Messages.ReservationUpdateSuccessful);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<ReservationDto>($"{Messages.ReservationUpdateFailed}: {ex.Message}");
            }
        }

        public async Task<IResult> DeleteReservationAsync(ReservationDeleteDto reservationDeleteDto)
        {
            try
            {
                var reservation = await _reservationRepository.GetAsync(r => r.ReservationId == reservationDeleteDto.ReservationId);
                if (reservation == null)
                {
                    return new ErrorResult(Messages.ReservationAlreadyDeleted);
                }

                reservation.IsDeleted = true;
                reservation.Status = ReservationStatus.Cancelled; 
                reservation.UpdatedDate = DateTime.Now;
                await _reservationRepository.UpdateAsync(reservation);
                await _unitOfWork.CommitAsync();
                return new SuccessResult(Messages.ReservationDeletionSuccessful);
            }
            catch (Exception ex)
            {
                return new ErrorResult($"{Messages.ReservationDeletionFailed}: {ex.Message}");
            }
        }

        public async Task<IResult> HardDeleteReservationAsync(ReservationDeleteDto reservationDeleteDto)
        {
            try
            {
                var reservation = await _reservationRepository.GetAsync(r => r.ReservationId == reservationDeleteDto.ReservationId);
                if (reservation == null)
                {
                    return new ErrorResult(Messages.ReservationAlreadyDeleted);
                }

                await _reservationRepository.DeleteAsync(reservation);
                await _unitOfWork.CommitAsync();
                return new SuccessResult(Messages.ReservationHardDeletionSuccessful);
            }
            catch (Exception ex)
            {
                return new ErrorResult($"{Messages.ReservationDeletionFailed}: {ex.Message}");
            }
        }
    }
}
