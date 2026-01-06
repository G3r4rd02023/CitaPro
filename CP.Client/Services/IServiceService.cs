using CP.Shared.DTOs;

namespace CP.Client.Services
{
    public interface IServiceService
    {
        Task<List<ServiceDto>> GetByBusinessIdAsync(Guid businessId);
        Task<List<ServiceDto>> GetActiveServicesAsync();
        Task<ServiceDto?> GetByIdAsync(Guid id);
        Task<bool> CreateAsync(CreateServiceDto service);
        Task<bool> UpdateAsync(Guid id, UpdateServiceDto service);
        Task<bool> DeleteAsync(Guid id);
    }
}
