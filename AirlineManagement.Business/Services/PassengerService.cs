using AirlineManagement.Business.Common.MessageConstant;
using AirlineManagement.Business.Contracts;
using AirlineManagement.Business.DTOs.PassengerDTOs;
using AirlineManagement.Data.Contracts;
using AirlineManagement.Domain.Entities;
using AirlineManagement.Domain.Results.Abstract;
using AirlineManagement.Domain.Results.ComplexType;
using AutoMapper;

namespace AirlineManagement.Business.Services
{
    public class PassengerService : IPassengerService
    {
        private readonly IPassengerRepository _passengerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PassengerService(IPassengerRepository passengerRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _passengerRepository = passengerRepository ?? throw new ArgumentNullException(nameof(passengerRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        private async Task<string> GeneratePassengerId()
        {
            var lastPassengerId = await _passengerRepository.GetAllAsync()
                .ContinueWith(task => task.Result
                    .OrderByDescending(p => p.PassengerId)
                    .Select(p => p.PassengerId)
                    .FirstOrDefault());

            if (lastPassengerId == null)
            {
                return "P001";
            }
            else
            {
                var lastIdNumber = int.Parse(lastPassengerId.Substring(1));
                var newIdNumber = lastIdNumber + 1;
                return $"P{newIdNumber:D3}";
            }
        }
        public async Task<IDataResult<IEnumerable<PassengerDto>>> GetPassengersAsync()
        {
            try
            {
                var passengers = await _passengerRepository.GetAllAsync();
                var passengerDtos = _mapper.Map<IEnumerable<PassengerDto>>(passengers);
                return new SuccessDataResult<IEnumerable<PassengerDto>>(passengerDtos, Messages.PassengerFetchSuccessful);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<IEnumerable<PassengerDto>>($"{Messages.PassengerFetchFailed}: {ex.Message}");
            }
        }

        public async Task<IDataResult<PassengerDto>> GetPassengerDetailsAsync(string passengerId)
        {
            try
            {
                var passenger = await _passengerRepository.GetAsync(p => p.PassengerId == passengerId);
                if (passenger == null)
                {
                    return new ErrorDataResult<PassengerDto>(Messages.PassengerNotFound);
                }
                var passengerDto = _mapper.Map<PassengerDto>(passenger);
                return new SuccessDataResult<PassengerDto>(passengerDto, Messages.PassengerFetchSuccessful);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<PassengerDto>($"{Messages.PassengerFetchFailed}: {ex.Message}");
            }
        }
        public async Task<IDataResult<PassengerDto>> CreatePassengerAsync(PassengerCreateDto passengerCreateDto)
        {
            try
            {
                var passenger = _mapper.Map<Passenger>(passengerCreateDto);
                passenger.PassengerId = await GeneratePassengerId(); 
                passenger.CreatedDate = DateTime.Now;
                passenger.UpdatedDate = DateTime.Now;
                passenger.IsDeleted = false;
                passenger.IsActive = true;

                await _passengerRepository.AddAsync(passenger);
                await _unitOfWork.CommitAsync();

                var createdPassengerDto = _mapper.Map<PassengerDto>(passenger);
                return new SuccessDataResult<PassengerDto>(createdPassengerDto, Messages.PassengerCreationSuccessful);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<PassengerDto>($"{Messages.PassengerCreationFailed}: {ex.Message}");
            }
        }

        public async Task<IDataResult<PassengerDto>> UpdatePassengerAsync(PassengerUpdateDto passengerUpdateDto)
        {
            try
            {
                var passenger = await _passengerRepository.GetAsync(p => p.PassengerId == passengerUpdateDto.PassengerId);

                if (passenger == null)
                {
                    return new ErrorDataResult<PassengerDto>(Messages.PassengerNotFound);
                }

                _mapper.Map(passengerUpdateDto, passenger);
                passenger.UpdatedDate = DateTime.Now;

                await _passengerRepository.UpdateAsync(passenger);
                await _unitOfWork.CommitAsync();

                var updatedPassengerDto = _mapper.Map<PassengerDto>(passenger);
                return new SuccessDataResult<PassengerDto>(updatedPassengerDto, Messages.PassengerUpdateSuccessful);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<PassengerDto>($"{Messages.PassengerUpdateFailed}: {ex.Message}");
            }
        }

        public async Task<IResult> DeletePassengerAsync(PassengerDeleteDto passengerDeleteDto)
        {
            try
            {
                var passenger = await _passengerRepository.GetAsync(p => p.PassengerId == passengerDeleteDto.PassengerId);
                if (passenger == null)
                {
                    return new ErrorResult(Messages.PassengerNotFound);
                }

                passenger.IsDeleted = true;
                passenger.UpdatedDate = DateTime.Now;
                await _passengerRepository.UpdateAsync(passenger);
                await _unitOfWork.CommitAsync();
                return new SuccessResult(Messages.PassengerDeletionSuccessful);
            }
            catch (Exception ex)
            {
                return new ErrorResult($"{Messages.PassengerDeletionFailed}: {ex.Message}");
            }
        }

        public async Task<IResult> HardDeletePassengerAsync(PassengerDeleteDto passengerDeleteDto)
        {
            try
            {
                var passenger = await _passengerRepository.GetAsync(p => p.PassengerId == passengerDeleteDto.PassengerId);
                if (passenger == null)
                {
                    return new ErrorResult(Messages.PassengerNotFound);
                }

                await _passengerRepository.DeleteAsync(passenger);
                await _unitOfWork.CommitAsync();
                return new SuccessResult(Messages.PassengerHardDeletionSuccessful);
            }
            catch (Exception ex)
            {
                return new ErrorResult($"{Messages.PassengerHardDeletionFailed}: {ex.Message}");
            }
        }
    }
}
