using CP.Server.Data;
using CP.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace CP.Server.Repositories
{
    public class BusinessRepository : IBusinessRepository
    {
        private readonly AppDbContext _context;

        public BusinessRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Business>> GetAllAsync()
        {
            return await _context.Businesses.Include(b => b.User).ToListAsync();
        }

        public async Task<Business?> GetByIdAsync(Guid id)
        {
            return await _context.Businesses.Include(b => b.User).FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Business> AddAsync(Business business)
        {
            _context.Businesses.Add(business);
            await _context.SaveChangesAsync();
            return business;
        }

        public async Task UpdateAsync(Business business)
        {
            _context.Entry(business).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.Businesses.FindAsync(id);
            if (entity != null)
            {
                _context.Businesses.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
