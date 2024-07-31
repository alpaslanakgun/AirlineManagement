using AirlineManagement.Business.DTOs.AirlineDTOs;
using AirlineManagement.Data.Contracts;
using AirlineManagement.Domain.Entities;
using AirlineManagement.Domain.Results.Abstract;
using AirlineManagement.Domain.Results.ComplexType;
using AutoMapper;

namespace AirlineManagement.Business.Services
{
    public class AirlineService : IAirlineService
    {
        private readonly IAirlineRepository _airlineRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AirlineService(IAirlineRepository airlineRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _airlineRepository = airlineRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IDataResult<IEnumerable<AirlineDto>>> GetAirlinesAsync()
        {
            try
            {
                var airlines = await _airlineRepository.GetAllAsync();
                var airlineDtos = _mapper.Map<IEnumerable<AirlineDto>>(airlines);
                return new SuccessDataResult<IEnumerable<AirlineDto>>(airlineDtos, "Havayolları başarıyla alındı.");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<IEnumerable<AirlineDto>>($"Havayolları alınırken bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<IDataResult<AirlineDto>> GetAirlineDetailsAsync(string airlineId)
        {
            try
            {
                var airline = await _airlineRepository.GetAsync(a => a.AirlineId == airlineId);
                if (airline == null)
                {
                    return new ErrorDataResult<AirlineDto>("Havayolu bulunamadı.");
                }
                var airlineDto = _mapper.Map<AirlineDto>(airline);
                return new SuccessDataResult<AirlineDto>(airlineDto, "Havayolu detayları başarıyla alındı.");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<AirlineDto>($"Havayolu detayları alınırken bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<IDataResult<AirlineDto>> CreateAirlineAsync(AirlineCreateDto airlineCreateDto)
        {
            try
            {
                var airline = _mapper.Map<Airline>(airlineCreateDto);
                await _airlineRepository.AddAsync(airline);
                await _unitOfWork.CommitAsync();
                var createdAirlineDto = _mapper.Map<AirlineDto>(airline);
                return new SuccessDataResult<AirlineDto>(createdAirlineDto, "Havayolu başarıyla oluşturuldu.");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<AirlineDto>($"Havayolu oluşturulurken bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<IDataResult<AirlineDto>> UpdateAirlineAsync(AirlineUpdateDto airlineUpdateDto)
        {
            if (_airlineRepository == null)
            {
                throw new ArgumentNullException(nameof(_airlineRepository));
            }

            if (_mapper == null)
            {
                throw new ArgumentNullException(nameof(_mapper));
            }

            try
            {
                var airline = await _airlineRepository.GetAsync(a => a.AirlineId == airlineUpdateDto.AirlineId);

                if (airline == null)
                {
                    return new ErrorDataResult<AirlineDto>("Havayolu bulunamadı.");
                }

                _mapper.Map(airlineUpdateDto, airline);
                await _airlineRepository.UpdateAsync(airline);
                await _unitOfWork.CommitAsync();

                var updatedAirlineDto = _mapper.Map<AirlineDto>(airline);
                return new SuccessDataResult<AirlineDto>(updatedAirlineDto, "Havayolu başarıyla güncellendi.");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<AirlineDto>($"Havayolu güncellenirken bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<IResult> DeleteAirlineAsync(AirlineDeleteDto airlineDeleteDto)
        {
            try
            {
                var airline = await _airlineRepository.GetAsync(a => a.AirlineId == airlineDeleteDto.AirlineId);
                if (airline == null)
                {
                    return new ErrorResult("Havayolu bulunamadı.");
                }

                await _airlineRepository.DeleteAsync(airline);
                await _unitOfWork.CommitAsync();
                return new SuccessResult("Havayolu başarıyla silindi.");
            }
            catch (Exception ex)
            {
                return new ErrorResult($"Havayolu silinirken bir hata oluştu: {ex.Message}");
            }
        }
    }
}
