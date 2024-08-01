using AirlineManagement.Business.Common.MessageConstant;
using AirlineManagement.Business.Contracts;
using AirlineManagement.Business.DTOs.CheckInDTOs;
using AirlineManagement.Data.Contracts;
using AirlineManagement.Domain.Entities;
using AirlineManagement.Domain.Enums;
using AirlineManagement.Domain.Results.Abstract;
using AirlineManagement.Domain.Results.ComplexType;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirlineManagement.Business.Services
{
    public class CheckInService : ICheckInService
    {
        private readonly ICheckInRepository _checkInRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IReservationRepository _reservationRepository;

        public CheckInService(ICheckInRepository checkInRepository, IUnitOfWork unitOfWork, IMapper mapper, IReservationRepository reservationRepository)
        {
            _checkInRepository = checkInRepository ?? throw new ArgumentNullException(nameof(checkInRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _reservationRepository = reservationRepository ?? throw new ArgumentNullException(nameof(reservationRepository));
        }
        private async Task<string> GenerateCheckInId()
        {
            var checkIns = await _checkInRepository.GetAllAsync();
            if (checkIns == null || !checkIns.Any())
            {
                return "CI001";
            }

            var lastCheckInId = checkIns
                .OrderByDescending(c => c.CheckInId)
                .Select(c => c.CheckInId)
                .FirstOrDefault();

            if (lastCheckInId == null)
            {
                return "CI001";
            }
            else
            {
                var lastIdNumber = int.Parse(lastCheckInId.Substring(2));
                var newIdNumber = lastIdNumber + 1;
                return $"CI{newIdNumber:D3}";
            }
        }


        public async Task<IDataResult<IEnumerable<CheckInDto>>> GetCheckInsAsync()
        {
            try
            {
                var checkIns = await _checkInRepository.GetAllAsync();
                var checkInDtos = _mapper.Map<IEnumerable<CheckInDto>>(checkIns);
                return new SuccessDataResult<IEnumerable<CheckInDto>>(checkInDtos, Messages.CheckInFetchSuccessful);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<IEnumerable<CheckInDto>>($"{Messages.CheckInFetchFailed}: {ex.Message}");
            }
        }

        public async Task<IDataResult<CheckInDto>> GetCheckInDetailsAsync(string checkInId)
        {
            try
            {
                var checkIn = await _checkInRepository.GetAsync(c => c.CheckInId == checkInId);
                if (checkIn == null)
                {
                    return new ErrorDataResult<CheckInDto>(Messages.CheckInNotFound);
                }
                var checkInDto = _mapper.Map<CheckInDto>(checkIn);
                return new SuccessDataResult<CheckInDto>(checkInDto, Messages.CheckInFetchSuccessful);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<CheckInDto>($"{Messages.CheckInFetchFailed}: {ex.Message}");
            }
        }

        public async Task<IDataResult<CheckInDto>> CreateCheckInAsync(CheckInCreateDto checkInCreateDto)
        {
            try
            {
                var reservation = await _reservationRepository.GetAsync(r => r.ReservationId == checkInCreateDto.ReservationId);
                if (reservation == null)
                {
                    return new ErrorDataResult<CheckInDto>(Messages.ReservationNotFound);
                }

                var checkIn = _mapper.Map<CheckIn>(checkInCreateDto);
                checkIn.CheckInId = await GenerateCheckInId(); 
                checkIn.CreatedDate = DateTime.Now;
                checkIn.UpdatedDate = DateTime.Now;
                checkIn.BoardingTime = DateTime.Now.AddHours(2); 
                checkIn.IsDeleted = false;
                checkIn.IsActive = true;
                checkIn.Status = CheckInStatus.Completed; 

                await _checkInRepository.AddAsync(checkIn);
                await _unitOfWork.CommitAsync();

                var createdCheckInDto = _mapper.Map<CheckInDto>(checkIn);
                return new SuccessDataResult<CheckInDto>(createdCheckInDto, Messages.CheckInCreationSuccessful);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<CheckInDto>($"{Messages.CheckInCreationFailed}: {ex.Message}");
            }
        }
        public async Task<IDataResult<CheckInDto>> UpdateCheckInAsync(CheckInUpdateDto checkInUpdateDto)
        {
            try
            {
                var checkIn = await _checkInRepository.GetAsync(c => c.CheckInId == checkInUpdateDto.CheckInId);

                if (checkIn == null)
                {
                    return new ErrorDataResult<CheckInDto>(Messages.CheckInNotFound);
                }

                _mapper.Map(checkInUpdateDto, checkIn);
                checkIn.UpdatedDate = DateTime.Now;

                if (checkInUpdateDto.Status.HasValue)
                {
                    checkIn.Status = checkInUpdateDto.Status.Value; 
                }

                await _checkInRepository.UpdateAsync(checkIn);
                await _unitOfWork.CommitAsync();

                var updatedCheckInDto = _mapper.Map<CheckInDto>(checkIn);
                return new SuccessDataResult<CheckInDto>(updatedCheckInDto, Messages.CheckInUpdateSuccessful);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<CheckInDto>($"{Messages.CheckInUpdateFailed}: {ex.Message}");
            }
        }

        public async Task<IResult> DeleteCheckInAsync(CheckInDeleteDto checkInDeleteDto)
        {
            try
            {
                var checkIn = await _checkInRepository.GetAsync(c => c.CheckInId == checkInDeleteDto.CheckInId);
                if (checkIn == null)
                {
                    return new ErrorResult(Messages.CheckInNotFound);
                }

                checkIn.IsDeleted = true;
                checkIn.Status = CheckInStatus.Cancelled;
                checkIn.UpdatedDate = DateTime.Now;
                await _checkInRepository.UpdateAsync(checkIn);
                await _unitOfWork.CommitAsync();
                return new SuccessResult(Messages.CheckInDeletionSuccessful);
            }
            catch (Exception ex)
            {
                return new ErrorResult($"{Messages.CheckInDeletionFailed}: {ex.Message}");
            }
        }

        public async Task<IResult> HardDeleteCheckInAsync(CheckInDeleteDto checkInDeleteDto)
        {
            try
            {
                var checkIn = await _checkInRepository.GetAsync(c => c.CheckInId == checkInDeleteDto.CheckInId);
                if (checkIn == null)
                {
                    return new ErrorResult(Messages.CheckInNotFound);
                }

                await _checkInRepository.DeleteAsync(checkIn);
                await _unitOfWork.CommitAsync();
                return new SuccessResult(Messages.CheckInHardDeletionSuccessful);
            }
            catch (Exception ex)
            {
                return new ErrorResult($"{Messages.CheckInHardDeletionFailed}: {ex.Message}");
            }
        }
    }
}
