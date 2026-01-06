using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CP.Shared.Entities;

namespace CP.Server.Repositories
{
    public interface IReservationRepository
    {
        Task<IEnumerable<Reservation>> GetAllAsync();
        Task<Reservation?> GetByIdAsync(Guid id);
        Task<IEnumerable<Reservation>> GetByBusinessIdAsync(Guid businessId);
        Task<IEnumerable<Reservation>> GetByUserIdAsync(Guid userId);
        Task<IEnumerable<Reservation>> GetByEmployeeIdAsync(Guid employeeId);
        Task<Reservation> CreateAsync(Reservation reservation);
        Task UpdateAsync(Reservation reservation);
        Task DeleteAsync(Guid id);
    }
}
