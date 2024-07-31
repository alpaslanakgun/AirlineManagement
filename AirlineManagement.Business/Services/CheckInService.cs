using AirlineManagement.Business.Contracts;
using AirlineManagement.Business.DTOs.CheckInDTOs;
using AirlineManagement.Data.Contracts;
using AirlineManagement.Domain.Entities;
using AirlineManagement.Domain.Results.Abstract;
using AirlineManagement.Domain.Results.ComplexType;
using AutoMapper;

namespace AirlineManagement.Business.Services
{
    public class CheckInService: ICheckInService
    {
        private readonly ICheckInRepository _checkInRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CheckInService(ICheckInRepository checkInRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _checkInRepository = checkInRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IDataResult<IEnumerable<CheckInDto>>> GetCheckInsAsync()
        {
            try
            {
                var checkIns = await _checkInRepository.GetAllAsync();
                var checkInDtos = _mapper.Map<IEnumerable<CheckInDto>>(checkIns);
                return new SuccessDataResult<IEnumerable<CheckInDto>>(checkInDtos, "Check-in'ler başarıyla alındı.");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<IEnumerable<CheckInDto>>($"Check-in'ler alınırken bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<IDataResult<CheckInDto>> GetCheckInDetailsAsync(int checkInId)
        {
            try
            {
                var checkIn = await _checkInRepository.GetAsync(c => c.Id == checkInId);
                if (checkIn == null)
                {
                    return new ErrorDataResult<CheckInDto>("Check-in bulunamadı.");
                }
                var checkInDto = _mapper.Map<CheckInDto>(checkIn);
                return new SuccessDataResult<CheckInDto>(checkInDto, "Check-in detayları başarıyla alındı.");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<CheckInDto>($"Check-in detayları alınırken bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<IDataResult<CheckInDto>> CreateCheckInAsync(CheckInCreateDto checkInCreateDto)
        {
            try
            {
                var checkIn = _mapper.Map<CheckIn>(checkInCreateDto);
                await _checkInRepository.AddAsync(checkIn);
                await _unitOfWork.CommitAsync();
                var createdCheckInDto = _mapper.Map<CheckInDto>(checkIn);
                return new SuccessDataResult<CheckInDto>(createdCheckInDto, "Check-in başarıyla oluşturuldu.");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<CheckInDto>($"Check-in oluşturulurken bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<IDataResult<CheckInDto>> UpdateCheckInAsync(CheckInUpdateDto checkInUpdateDto)
        {
            if (_checkInRepository == null)
            {
                throw new ArgumentNullException(nameof(_checkInRepository));
            }

            if (_mapper == null)
            {
                throw new ArgumentNullException(nameof(_mapper));
            }

            try
            {
                var checkIn = await _checkInRepository.GetAsync(c => c.Id == checkInUpdateDto.Id);

                if (checkIn == null)
                {
                    return new ErrorDataResult<CheckInDto>("Check-in bulunamadı.");
                }

                _mapper.Map(checkInUpdateDto, checkIn);
                await _checkInRepository.UpdateAsync(checkIn);
                await _unitOfWork.CommitAsync();

                var updatedCheckInDto = _mapper.Map<CheckInDto>(checkIn);
                return new SuccessDataResult<CheckInDto>(updatedCheckInDto, "Check-in başarıyla güncellendi.");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<CheckInDto>($"Check-in güncellenirken bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<IResult> DeleteCheckInAsync(CheckInDeleteDto checkInDeleteDto)
        {
            try
            {
                var checkIn = await _checkInRepository.GetAsync(c => c.Id == checkInDeleteDto.Id);
                if (checkIn == null)
                {
                    return new ErrorResult("Check-in bulunamadı.");
                }

                await _checkInRepository.DeleteAsync(checkIn);
                await _unitOfWork.CommitAsync();
                return new SuccessResult("Check-in başarıyla silindi.");
            }
            catch (Exception ex)
            {
                return new ErrorResult($"Check-in silinirken bir hata oluştu: {ex.Message}");
            }
        }
    }
}
