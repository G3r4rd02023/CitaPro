using CP.Shared.DTOs;

namespace CP.Server.Services
{
    public interface IBusinessService
    {
        Task<IEnumerable<BusinessDto>> GetAllAsync();
        Task<BusinessDto?> GetByIdAsync(Guid id);
        Task<BusinessDto> CreateAsync(CreateBusinessDto createDto, Stream? imageStream, string? imageName);
        Task<BusinessDto?> UpdateAsync(Guid id, UpdateBusinessDto updateDto, Stream? imageStream, string? imageName);
        Task<bool> DeleteAsync(Guid id);
    }
}
