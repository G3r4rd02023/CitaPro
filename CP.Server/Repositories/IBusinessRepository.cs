using CP.Shared.Entities;

namespace CP.Server.Repositories
{
    public interface IBusinessRepository
    {
        Task<IEnumerable<Business>> GetAllAsync();
        Task<Business?> GetByIdAsync(Guid id);
        Task<Business> AddAsync(Business business);
        Task UpdateAsync(Business business);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<Business>> GetByUserIdAsync(Guid userId);
    }
}
