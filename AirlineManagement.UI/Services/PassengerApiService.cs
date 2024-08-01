using AirlineManagement.Business.DTOs.PassengerDTOs;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AirlineManagement.MvcUI.Services
{
    public class PassengerApiService
    {
        private readonly HttpClient _httpClient;

        public PassengerApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PassengerDto> GetByIdAsync(string id)
        {
            var response = await _httpClient.GetFromJsonAsync<PassengerDto>($"passenger/{id}");
            return response;
        }

        public async Task<List<PassengerDto>> GetAllAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<PassengerDto>>("passenger");
            return response;
        }

        public async Task<bool> AddAsync(PassengerCreateDto newPassenger)
        {
            var response = await _httpClient.PostAsJsonAsync("passenger", newPassenger);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(PassengerUpdateDto passengerUpdateDto)
        {
            var response = await _httpClient.PutAsJsonAsync("passenger", passengerUpdateDto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RemoveAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"passenger/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
