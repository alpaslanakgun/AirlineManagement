using AirlineManagement.Business.Contracts;
using AirlineManagement.Business.DTOs.ReservationDTOs;
using AirlineManagement.Domain.Results.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirlineManagement.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        /// <summary>
        /// Get all reservations
        /// </summary>
        /// <returns>List of reservations</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllReservations()
        {
            var result = await _reservationService.GetReservationsAsync();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Get reservation details by ID
        /// </summary>
        /// <param name="reservationId">Reservation ID</param>
        /// <returns>Reservation details</returns>
        [HttpGet("{reservationId}")]
        public async Task<IActionResult> GetReservationDetails(string reservationId)
        {
            var result = await _reservationService.GetReservationDetailsAsync(reservationId);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return NotFound(result.Message);
        }

        /// <summary>
        /// Create a new reservation
        /// </summary>
        /// <param name="reservationCreateDto">Reservation creation DTO</param>
        /// <returns>Created reservation</returns>
        [HttpPost]
        public async Task<IActionResult> CreateReservation([FromBody] ReservationCreateDto reservationCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _reservationService.CreateReservationAsync(reservationCreateDto);
            if (result.Success)
            {
                return CreatedAtAction(nameof(GetReservationDetails), new { reservationId = result.Data.ReservationId }, result.Data);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Update an existing reservation
        /// </summary>
        /// <param name="reservationId">Reservation ID</param>
        /// <param name="reservationUpdateDto">Reservation update DTO</param>
        /// <returns>Updated reservation</returns>
        [HttpPut("{reservationId}")]
        public async Task<IActionResult> UpdateReservation(string reservationId, [FromBody] ReservationUpdateDto reservationUpdateDto)
        {
            if (reservationId != reservationUpdateDto.ReservationId)
            {
                return BadRequest("Reservation ID mismatch.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _reservationService.UpdateReservationAsync(reservationUpdateDto);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Delete a reservation (soft delete)
        /// </summary>
        /// <param name="reservationId">Reservation ID</param>
        /// <param name="reservationDeleteDto">Reservation delete DTO</param>
        /// <returns>Result of the deletion</returns>
        [HttpDelete("{reservationId}")]
        public async Task<IActionResult> DeleteReservation(string reservationId, [FromBody] ReservationDeleteDto reservationDeleteDto)
        {
            if (reservationId != reservationDeleteDto.ReservationId)
            {
                return BadRequest("Reservation ID mismatch.");
            }

            var result = await _reservationService.DeleteReservationAsync(reservationDeleteDto);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Hard delete a reservation
        /// </summary>
        /// <param name="reservationId">Reservation ID</param>
        /// <param name="reservationDeleteDto">Reservation delete DTO</param>
        /// <returns>Result of the hard deletion</returns>
        [HttpDelete("hard/{reservationId}")]
        public async Task<IActionResult> HardDeleteReservation(string reservationId, [FromBody] ReservationDeleteDto reservationDeleteDto)
        {
            if (reservationId != reservationDeleteDto.ReservationId)
            {
                return BadRequest("Reservation ID mismatch.");
            }

            var result = await _reservationService.HardDeleteReservationAsync(reservationDeleteDto);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}
