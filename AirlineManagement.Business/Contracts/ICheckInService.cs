using AirlineManagement.Business.DTOs.CheckInDTOs;
using AirlineManagement.Domain.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement.Business.Contracts
{
    public interface ICheckInService
    {
        Task<IDataResult<IEnumerable<CheckInDto>>> GetCheckInsAsync();
        Task<IDataResult<CheckInDto>> GetCheckInDetailsAsync(int checkInId);
        Task<IDataResult<CheckInDto>> CreateCheckInAsync(CheckInCreateDto checkInCreateDto);
        Task<IDataResult<CheckInDto>> UpdateCheckInAsync(CheckInUpdateDto checkInUpdateDto);
        Task<IResult> DeleteCheckInAsync(CheckInDeleteDto checkInDeleteDto);
    }
}
