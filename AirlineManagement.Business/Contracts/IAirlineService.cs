using AirlineManagement.Business.DTOs.AirlineDTOs;
using AirlineManagement.Domain.Results.Abstract;

namespace AirlineManagement.Business.Services
{
    public  interface IAirlineService
    {
        Task<IDataResult<IEnumerable<AirlineDto>>> GetAirlinesAsync();
        Task<IDataResult<AirlineDto>> GetAirlineDetailsAsync(string airlineId);
        Task<IDataResult<AirlineDto>> CreateAirlineAsync(AirlineCreateDto airlineCreateDto);
        Task<IDataResult<AirlineDto>> UpdateAirlineAsync(AirlineUpdateDto airlineUpdateDto);
        Task<IResult> DeleteAirlineAsync(AirlineDeleteDto airlineDeleteDto);
    }
}
