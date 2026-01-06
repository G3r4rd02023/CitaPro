using CP.Shared.DTOs;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Json;
using System.Net.Http.Headers;

namespace CP.Client.Services
{
    public class BusinessService : IBusinessService
    {
        private readonly HttpClient _httpClient;

        public BusinessService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<BusinessDto>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<BusinessDto>>("api/business") ?? new List<BusinessDto>();
        }

        public async Task<BusinessDto?> GetByIdAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<BusinessDto>($"api/business/{id}");
        }

        public async Task<bool> CreateAsync(CreateBusinessDto business, IBrowserFile? imageFile)
        {
            var content = new MultipartFormDataContent();
            content.Add(new StringContent(business.Name), "Name");
            content.Add(new StringContent(business.UserId.ToString()), "UserId");

            if (imageFile != null)
            {
                var fileContent = new StreamContent(imageFile.OpenReadStream(maxAllowedSize: 1024 * 1024 * 5)); // 5MB max
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(imageFile.ContentType);
                content.Add(fileContent, "image", imageFile.Name);
            }

            var response = await _httpClient.PostAsync("api/business", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateBusinessDto business, IBrowserFile? imageFile)
        {
            var content = new MultipartFormDataContent();
            content.Add(new StringContent(business.Id.ToString()), "Id");
            content.Add(new StringContent(business.Name), "Name");
            content.Add(new StringContent(business.IsActive.ToString()), "IsActive");

            if (imageFile != null)
            {
                var fileContent = new StreamContent(imageFile.OpenReadStream(maxAllowedSize: 1024 * 1024 * 5));
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(imageFile.ContentType);
                content.Add(fileContent, "image", imageFile.Name);
            }

            var response = await _httpClient.PutAsync($"api/business/{id}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/business/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<BusinessDto>> GetMyBusinessesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<BusinessDto>>("api/business/mine") ?? new List<BusinessDto>();
        }

        public async Task<List<BusinessDto>> GetActiveBusinessesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<BusinessDto>>("api/business/active") ?? new List<BusinessDto>();
        }
    }
}
