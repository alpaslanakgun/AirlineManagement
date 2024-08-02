using AirlineManagement.Business.DTOs.FlightDTOs;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AirlineManagement.MvcUI.Services
{
    public class FlightApiService
    {
        private readonly HttpClient _httpClient;

        public FlightApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<FlightDto> GetByIdAsync(string flightNumber)
        {
            var response = await _httpClient.GetFromJsonAsync<FlightDto>($"api/flight/{flightNumber}");
            return response;
        }

        public async Task<List<FlightDto>> GetAllAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<FlightDto>>("api/flight");
            return response;
        }

        public async Task<bool> AddAsync(FlightCreateDto newFlight)
        {
            var response = await _httpClient.PostAsJsonAsync("api/flight", newFlight);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(FlightUpdateDto flightUpdateDto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/flight/{flightUpdateDto.FlightNumber}", flightUpdateDto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RemoveAsync(string flightNumber)
        {
            var response = await _httpClient.DeleteAsync($"api/flight/{flightNumber}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<FlightDto>> SearchAsync(string departureAirport, string arrivalAirport, DateTime departureDate)
        {
            var response = await _httpClient.GetFromJsonAsync<List<FlightDto>>($"api/flight/search?departureAirport={departureAirport}&arrivalAirport={arrivalAirport}&departureDate={departureDate}");
            return response;
        }

        public async Task<List<FlightDto>> SearchWithCriteriaAsync(string departureAirport, string arrivalAirport, DateTime? departureDate, string status)
        {
            var response = await _httpClient.GetFromJsonAsync<List<FlightDto>>($"api/flight/search-with-criteria?departureAirport={departureAirport}&arrivalAirport={arrivalAirport}&departureDate={departureDate}&status={status}");
            return response;
        }

        public async Task<List<FlightDto>> GetFlightsWithDetailsAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<FlightDto>>("api/flight/details");
            return response;
        }
    }
}
