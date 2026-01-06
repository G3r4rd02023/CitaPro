using CP.Shared.Entities;

namespace CP.Server.Repositories
{
    public interface IServiceRepository
    {
        Task<IEnumerable<Service>> GetAllAsync();
        Task<IEnumerable<Service>> GetByBusinessIdAsync(Guid businessId);
        Task<Service?> GetByIdAsync(Guid id);
        Task<Service> CreateAsync(Service service);
        Task UpdateAsync(Service service);
        Task DeleteAsync(Guid id);
    }
}
