using CP.Shared.DTOs;
using Microsoft.AspNetCore.Components.Forms;

namespace CP.Client.Services
{
    public interface IBusinessService
    {
        Task<List<BusinessDto>> GetAllAsync();
        Task<BusinessDto?> GetByIdAsync(Guid id);
        Task<bool> CreateAsync(CreateBusinessDto business, IBrowserFile? imageFile);
        Task<bool> UpdateAsync(Guid id, UpdateBusinessDto business, IBrowserFile? imageFile);
        Task<bool> DeleteAsync(Guid id);
    }
}
