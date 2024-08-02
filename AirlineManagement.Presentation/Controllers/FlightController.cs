using AirlineManagement.Business.Contracts;
using AirlineManagement.Business.DTOs.FlightDTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirlineManagement.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly IFlightService _flightService;

        public FlightController(IFlightService flightService)
        {
            _flightService = flightService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _flightService.GetFlightsAsync();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("{flightNumber}")]
        public async Task<IActionResult> GetById(string flightNumber)
        {
            var result = await _flightService.GetFlightDetailsAsync(flightNumber);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return NotFound(result.Message);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FlightCreateDto flightCreateDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _flightService.CreateFlightAsync(flightCreateDto);
                if (result.Success)
                {
                    return CreatedAtAction(nameof(GetById), new { flightNumber = result.Data.FlightNumber }, result.Data);
                }
                return BadRequest(result.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{flightNumber}")]
        public async Task<IActionResult> Update(string flightNumber, [FromBody] FlightUpdateDto flightUpdateDto)
        {
            if (flightNumber != flightUpdateDto.FlightNumber)
            {
                return BadRequest("Flight number mismatch.");
            }

            if (ModelState.IsValid)
            {
                var result = await _flightService.UpdateFlightAsync(flightUpdateDto);
                if (result.Success)
                {
                    return Ok(result.Data);
                }
                return BadRequest(result.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{flightNumber}")]
        public async Task<IActionResult> Delete(string flightNumber)
        {
            var flightDeleteDto = new FlightDeleteDto { FlightNumber = flightNumber };
            var result = await _flightService.DeleteFlightAsync(flightDeleteDto);
            if (result.Success)
            {
                return NoContent();
            }
            return BadRequest(result.Message);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string departureAirport, string arrivalAirport, DateTime departureDate)
        {
            var result = await _flightService.SearchFlightsAsync(departureAirport, arrivalAirport, departureDate);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("search-with-criteria")]
        public async Task<IActionResult> SearchWithCriteria(string departureAirport, string arrivalAirport, DateTime? departureDate, string status)
        {
            var result = await _flightService.SearchFlightsWithCriteriaAsync(departureAirport, arrivalAirport, departureDate, status);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("details")]
        public async Task<IActionResult> GetFlightsWithDetails()
        {
            var result = await _flightService.GetFlightsWithDetailsAsync();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
    }
}
