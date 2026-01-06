using CP.Shared.DTOs;

namespace CP.Server.Services
{
    public interface IServiceService
    {
        Task<IEnumerable<ServiceDto>> GetAllAsync();
        Task<IEnumerable<ServiceDto>> GetByBusinessIdAsync(Guid businessId);
        Task<IEnumerable<ServiceDto>> GetActiveAsync();
        Task<ServiceDto?> GetByIdAsync(Guid id);
        Task<ServiceDto> CreateAsync(CreateServiceDto createDto);
        Task<ServiceDto?> UpdateAsync(Guid id, UpdateServiceDto updateDto);
        Task<bool> DeleteAsync(Guid id);
    }
}
