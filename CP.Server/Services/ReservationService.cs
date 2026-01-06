using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CP.Server.Repositories;
using CP.Shared.DTOs;
using CP.Shared.Entities;
using CP.Shared.Enums;

namespace CP.Server.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _repository;

        public ReservationService(IReservationRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ReservationDto>> GetAllAsync()
        {
            var reservations = await _repository.GetAllAsync();
            return reservations.Select(MapToDto);
        }

        public async Task<ReservationDto?> GetByIdAsync(Guid id)
        {
            var reservation = await _repository.GetByIdAsync(id);
            return reservation == null ? null : MapToDto(reservation);
        }

        public async Task<IEnumerable<ReservationDto>> GetByBusinessIdAsync(Guid businessId)
        {
            var reservations = await _repository.GetByBusinessIdAsync(businessId);
            return reservations.Select(MapToDto);
        }

        public async Task<IEnumerable<ReservationDto>> GetByUserIdAsync(Guid userId)
        {
            var reservations = await _repository.GetByUserIdAsync(userId);
            return reservations.Select(MapToDto);
        }

        public async Task<ReservationDto> CreateAsync(CreateReservationDto createDto)
        {
            var reservation = new Reservation
            {
                BusinessId = createDto.BusinessId,
                UserId = createDto.UserId,
                EmployeeId = createDto.EmployeeId,
                ServiceId = createDto.ServiceId,
                StartTime = createDto.StartTime,
                EndTime = createDto.EndTime,
                Status = ReservationStatus.Pending
            };

            var created = await _repository.CreateAsync(reservation);
            
            // To return full names, we might need to re-fetch or include them in Repo
            var result = await _repository.GetByIdAsync(created.Id);
            return MapToDto(result!);
        }

        public async Task<ReservationDto?> UpdateStatusAsync(Guid id, UpdateReservationDto updateDto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return null;

            existing.Status = updateDto.Status;
            await _repository.UpdateAsync(existing);
            
            return MapToDto(existing);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;

            await _repository.DeleteAsync(id);
            return true;
        }

        private static ReservationDto MapToDto(Reservation r)
        {
            return new ReservationDto
            {
                Id = r.Id,
                BusinessId = r.BusinessId,
                BusinessName = r.Business?.Name ?? string.Empty,
                UserId = r.UserId,
                UserName = r.User?.FullName ?? string.Empty,
                EmployeeId = r.EmployeeId,
                EmployeeName = r.Employee?.FullName ?? string.Empty,
                ServiceId = r.ServiceId,
                ServiceName = r.Service?.Name ?? string.Empty,
                StartTime = r.StartTime,
                EndTime = r.EndTime,
                Status = r.Status,
                CreatedAt = r.CreatedAt
            };
        }
    }
}
