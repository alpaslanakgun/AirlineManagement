using AirlineManagement.Business.Common.MessageConstant;
using AirlineManagement.Business.Contracts;
using AirlineManagement.Business.DTOs.PassengerDTOs;
using AirlineManagement.Domain.Results.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirlineManagement.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassengerController : ControllerBase
    {
        private readonly IPassengerService _passengerService;

        public PassengerController(IPassengerService passengerService)
        {
            _passengerService = passengerService;
        }

        /// <summary>
        /// Get all passengers
        /// </summary>
        /// <returns>List of passengers</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllPassengers()
        {
            var result = await _passengerService.GetPassengersAsync();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Get passenger details by ID
        /// </summary>
        /// <param name="passengerId">Passenger ID</param>
        /// <returns>Passenger details</returns>
        [HttpGet("{passengerId}")]
        public async Task<IActionResult> GetPassengerDetails(string passengerId)
        {
            var result = await _passengerService.GetPassengerDetailsAsync(passengerId);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return NotFound(result.Message);
        }

        /// <summary>
        /// Create a new passenger
        /// </summary>
        /// <param name="passengerCreateDto">Passenger creation DTO</param>
        /// <returns>Created passenger</returns>
        [HttpPost]
        public async Task<IActionResult> CreatePassenger([FromBody] PassengerCreateDto passengerCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _passengerService.CreatePassengerAsync(passengerCreateDto);
            if (result.Success)
            {
                return CreatedAtAction(nameof(GetPassengerDetails), new { passengerId = result.Data.PassengerId }, result.Data);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Update an existing passenger
        /// </summary>
        /// <param name="passengerId">Passenger ID</param>
        /// <param name="passengerUpdateDto">Passenger update DTO</param>
        /// <returns>Updated passenger</returns>
        [HttpPut("{passengerId}")]
        public async Task<IActionResult> UpdatePassenger(string passengerId, [FromBody] PassengerUpdateDto passengerUpdateDto)
        {
            if (passengerId != passengerUpdateDto.PassengerId)
            {
                return BadRequest(Messages.PassengerIdMismatch); // Use Messages class for consistency
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _passengerService.UpdatePassengerAsync(passengerUpdateDto);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Delete a passenger (soft delete)
        /// </summary>
        /// <param name="passengerId">Passenger ID</param>
        /// <param name="passengerDeleteDto">Passenger delete DTO</param>
        /// <returns>Result of the deletion</returns>
        [HttpDelete("{passengerId}")]
        public async Task<IActionResult> DeletePassenger(string passengerId, [FromBody] PassengerDeleteDto passengerDeleteDto)
        {
            if (passengerId != passengerDeleteDto.PassengerId)
            {
                return BadRequest(Messages.PassengerIdMismatch); // Use Messages class for consistency
            }

            var result = await _passengerService.DeletePassengerAsync(passengerDeleteDto);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Hard delete a passenger
        /// </summary>
        /// <param name="passengerId">Passenger ID</param>
        /// <param name="passengerDeleteDto">Passenger delete DTO</param>
        /// <returns>Result of the hard deletion</returns>
        [HttpDelete("hard/{passengerId}")]
        public async Task<IActionResult> HardDeletePassenger(string passengerId, [FromBody] PassengerDeleteDto passengerDeleteDto)
        {
            if (passengerId != passengerDeleteDto.PassengerId)
            {
                return BadRequest(Messages.PassengerIdMismatch); // Use Messages class for consistency
            }

            var result = await _passengerService.HardDeletePassengerAsync(passengerDeleteDto);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}
