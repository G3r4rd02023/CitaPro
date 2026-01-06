using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CP.Shared.DTOs;

namespace CP.Client.Services
{
    public class ReservationService : IReservationService
    {
        private readonly HttpClient _httpClient;

        public ReservationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ReservationDto>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ReservationDto>>("api/reservation") ?? new List<ReservationDto>();
        }

        public async Task<ReservationDto?> GetByIdAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<ReservationDto>($"api/reservation/{id}");
        }

        public async Task<List<ReservationDto>> GetByBusinessIdAsync(Guid businessId)
        {
            return await _httpClient.GetFromJsonAsync<List<ReservationDto>>($"api/reservation/business/{businessId}") ?? new List<ReservationDto>();
        }

        public async Task<List<ReservationDto>> GetMyReservationsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ReservationDto>>("api/reservation/mine") ?? new List<ReservationDto>();
        }

        public async Task<ReservationDto?> CreateAsync(CreateReservationDto reservation)
        {
            var response = await _httpClient.PostAsJsonAsync("api/reservation", reservation);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ReservationDto>();
            }
            return null;
        }

        public async Task<ReservationDto?> UpdateStatusAsync(Guid id, UpdateReservationDto updateDto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/reservation/{id}/status", updateDto);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ReservationDto>();
            }
            return null;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/reservation/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
