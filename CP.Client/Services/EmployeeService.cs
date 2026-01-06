using CP.Shared.DTOs;
using System.Net.Http.Json;

namespace CP.Client.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient _httpClient;

        public EmployeeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<EmployeeDto>> GetByBusinessIdAsync(Guid businessId)
        {
            return await _httpClient.GetFromJsonAsync<List<EmployeeDto>>($"api/employee/business/{businessId}") ?? new List<EmployeeDto>();
        }

        public async Task<List<EmployeeDto>> GetActiveEmployeesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<EmployeeDto>>("api/employee/active") ?? new List<EmployeeDto>();
        }

        public async Task<EmployeeDto?> GetByIdAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<EmployeeDto>($"api/employee/{id}");
        }

        public async Task<bool> CreateAsync(CreateEmployeeDto employee)
        {
            var response = await _httpClient.PostAsJsonAsync("api/employee", employee);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateEmployeeDto employee)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/employee/{id}", employee);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/employee/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
