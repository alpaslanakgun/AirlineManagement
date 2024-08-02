using AirlineManagement.Business.DTOs.AirlineDTOs;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AirlineManagement.MvcUI.Services
{
    public class AirlineApiService
    {
        private readonly HttpClient _httpClient;

        public AirlineApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AirlineDto> GetByIdAsync(string id)
        {
            var response = await _httpClient.GetFromJsonAsync<AirlineDto>($"airline/{id}");
            return response;
        }

        public async Task<List<AirlineDto>> GetAllAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<AirlineDto>>("airline");
            return response;
        }

        public async Task<bool> AddAsync(AirlineCreateDto newAirline)
        {
            var response = await _httpClient.PostAsJsonAsync("airline", newAirline);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(AirlineUpdateDto airlineUpdateDto)
        {
            var response = await _httpClient.PutAsJsonAsync("airline", airlineUpdateDto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RemoveAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"airline/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
