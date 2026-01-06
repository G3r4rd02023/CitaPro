using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CP.Shared.DTOs;

namespace CP.Client.Services
{
    public interface IReservationService
    {
        Task<List<ReservationDto>> GetAllAsync();
        Task<ReservationDto?> GetByIdAsync(Guid id);
        Task<List<ReservationDto>> GetByBusinessIdAsync(Guid businessId);
        Task<List<ReservationDto>> GetMyReservationsAsync();
        Task<ReservationDto?> CreateAsync(CreateReservationDto reservation);
        Task<ReservationDto?> UpdateStatusAsync(Guid id, UpdateReservationDto updateDto);
        Task<bool> DeleteAsync(Guid id);
    }
}
