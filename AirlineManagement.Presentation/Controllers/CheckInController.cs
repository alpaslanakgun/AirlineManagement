using AirlineManagement.Business.Contracts;
using AirlineManagement.Business.DTOs.CheckInDTOs;
using AirlineManagement.Domain.Results.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirlineManagement.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckInController : ControllerBase
    {
        private readonly ICheckInService _checkInService;

        public CheckInController(ICheckInService checkInService)
        {
            _checkInService = checkInService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCheckIns()
        {
            var result = await _checkInService.GetCheckInsAsync();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("{checkInId}")]
        public async Task<IActionResult> GetCheckInDetails(string checkInId)
        {
            var result = await _checkInService.GetCheckInDetailsAsync(checkInId);
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCheckIn([FromBody] CheckInCreateDto checkInCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _checkInService.CreateCheckInAsync(checkInCreateDto);
            if (result.Success)
            {
                return CreatedAtAction(nameof(GetCheckInDetails), new { checkInId = result.Data.CheckInId }, result);
            }
            return BadRequest(result);
        }

        [HttpPut("{checkInId}")]
        public async Task<IActionResult> UpdateCheckIn(string checkInId, [FromBody] CheckInUpdateDto checkInUpdateDto)
        {
            if (checkInId != checkInUpdateDto.CheckInId)
            {
                return BadRequest("CheckIn ID mismatch.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _checkInService.UpdateCheckInAsync(checkInUpdateDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("{checkInId}")]
        public async Task<IActionResult> DeleteCheckIn(string checkInId, [FromBody] CheckInDeleteDto checkInDeleteDto)
        {
            if (checkInId != checkInDeleteDto.CheckInId)
            {
                return BadRequest("CheckIn ID mismatch.");
            }

            var result = await _checkInService.DeleteCheckInAsync(checkInDeleteDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("hard/{checkInId}")]
        public async Task<IActionResult> HardDeleteCheckIn(string checkInId, [FromBody] CheckInDeleteDto checkInDeleteDto)
        {
            if (checkInId != checkInDeleteDto.CheckInId)
            {
                return BadRequest("CheckIn ID mismatch.");
            }

            var result = await _checkInService.HardDeleteCheckInAsync(checkInDeleteDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
