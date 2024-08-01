using AirlineManagement.Business.DTOs.CheckInDTOs;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AirlineManagement.MvcUI.Services
{
    public class CheckInApiService
    {
        private readonly HttpClient _httpClient;

        public CheckInApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CheckInDto> GetByIdAsync(string id)
        {
            var response = await _httpClient.GetFromJsonAsync<CheckInDto>($"checkin/{id}");
            return response;
        }

        public async Task<List<CheckInDto>> GetAllAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<CheckInDto>>("checkin");
            return response;
        }

        public async Task<bool> AddAsync(CheckInCreateDto newCheckIn)
        {
            var response = await _httpClient.PostAsJsonAsync("checkin", newCheckIn);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(CheckInUpdateDto checkInUpdateDto)
        {
            var response = await _httpClient.PutAsJsonAsync("checkin", checkInUpdateDto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RemoveAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"checkin/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
