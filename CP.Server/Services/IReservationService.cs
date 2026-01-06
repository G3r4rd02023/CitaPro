using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CP.Shared.DTOs;

namespace CP.Server.Services
{
    public interface IReservationService
    {
        Task<IEnumerable<ReservationDto>> GetAllAsync();
        Task<ReservationDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<ReservationDto>> GetByBusinessIdAsync(Guid businessId);
        Task<IEnumerable<ReservationDto>> GetByUserIdAsync(Guid userId);
        Task<ReservationDto> CreateAsync(CreateReservationDto createDto);
        Task<ReservationDto?> UpdateStatusAsync(Guid id, UpdateReservationDto updateDto);
        Task<bool> DeleteAsync(Guid id);
    }
}
