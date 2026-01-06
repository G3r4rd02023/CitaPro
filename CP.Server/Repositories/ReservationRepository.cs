using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CP.Server.Data;
using CP.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace CP.Server.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly AppDbContext _context;

        public ReservationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reservation>> GetAllAsync()
        {
            return await _context.Reservations
                .Include(r => r.Business)
                .Include(r => r.User)
                .Include(r => r.Employee)
                .Include(r => r.Service)
                .ToListAsync();
        }

        public async Task<Reservation?> GetByIdAsync(Guid id)
        {
            return await _context.Reservations
                .Include(r => r.Business)
                .Include(r => r.User)
                .Include(r => r.Employee)
                .Include(r => r.Service)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Reservation>> GetByBusinessIdAsync(Guid businessId)
        {
            return await _context.Reservations
                .Include(r => r.User)
                .Include(r => r.Employee)
                .Include(r => r.Service)
                .Where(r => r.BusinessId == businessId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetByUserIdAsync(Guid userId)
        {
            return await _context.Reservations
                .Include(r => r.Business)
                .Include(r => r.Employee)
                .Include(r => r.Service)
                .Where(r => r.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetByEmployeeIdAsync(Guid employeeId)
        {
            return await _context.Reservations
                .Include(r => r.Business)
                .Include(r => r.Service)
                .Where(r => r.EmployeeId == employeeId)
                .ToListAsync();
        }

        public async Task<Reservation> CreateAsync(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
            return reservation;
        }

        public async Task UpdateAsync(Reservation reservation)
        {
            _context.Entry(reservation).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
                await _context.SaveChangesAsync();
            }
        }
    }
}
