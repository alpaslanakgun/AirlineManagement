using AirlineManagement.Business.DTOs.ReservationDTOs;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AirlineManagement.MvcUI.Services
{
    public class ReservationApiService
    {
        private readonly HttpClient _httpClient;

        public ReservationApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ReservationDto> GetByIdAsync(string id)
        {
            var response = await _httpClient.GetFromJsonAsync<ReservationDto>($"reservation/{id}");
            return response;
        }

        public async Task<List<ReservationDto>> GetAllAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<ReservationDto>>("reservation");
            return response;
        }

        public async Task<bool> AddAsync(ReservationCreateDto newReservation)
        {
            var response = await _httpClient.PostAsJsonAsync("reservation", newReservation);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(ReservationUpdateDto reservationUpdateDto)
        {
            var response = await _httpClient.PutAsJsonAsync("reservation", reservationUpdateDto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RemoveAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"reservation/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
