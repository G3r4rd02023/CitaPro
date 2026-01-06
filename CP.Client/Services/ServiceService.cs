using CP.Shared.DTOs;
using System.Net.Http.Json;

namespace CP.Client.Services
{
    public class ServiceService : IServiceService
    {
        private readonly HttpClient _httpClient;

        public ServiceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ServiceDto>> GetByBusinessIdAsync(Guid businessId)
        {
            return await _httpClient.GetFromJsonAsync<List<ServiceDto>>($"api/service/business/{businessId}") ?? new List<ServiceDto>();
        }

        public async Task<List<ServiceDto>> GetActiveServicesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ServiceDto>>("api/service/active") ?? new List<ServiceDto>();
        }

        public async Task<ServiceDto?> GetByIdAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<ServiceDto>($"api/service/{id}");
        }

        public async Task<bool> CreateAsync(CreateServiceDto service)
        {
            var response = await _httpClient.PostAsJsonAsync("api/service", service);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateServiceDto service)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/service/{id}", service);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/service/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
