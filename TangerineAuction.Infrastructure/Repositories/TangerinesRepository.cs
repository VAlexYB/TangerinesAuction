using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using TangerineAuction.Infrastructure.Persistence;
using TangerinesAuction.Core.Abstractions.Repositories;
using TangerinesAuction.Core.Models;

namespace TangerineAuction.Infrastructure.Repositories
{
    public class TangerinesRepository : ITangerinesRepository
    {
        private readonly TADbContext _context;

        public TangerinesRepository(TADbContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateAsync(Tangerine tangerine)
        {
            await _context.Tangerines.AddAsync(tangerine);
            await _context.SaveChangesAsync();
            return tangerine.Id;
        }

        public async Task<Tangerine> GetByIdAsync(Guid tangerineId)
        {
            return await _context.Tangerines
                .Include(t => t.Bids)
                .ThenInclude(b => b.Bidder)
                .FirstOrDefaultAsync(t => t.Id == tangerineId);

        }
        public async Task<IEnumerable<Tangerine>> GetTangerinesAsync()
        {
            return await _context.Tangerines
                .AsNoTracking()
                .Where(t => t.ExpiryDate > DateTime.UtcNow)
                .Where(t => !t.IsSpoiled)
                .Include(t => t.Bids)
                //.ThenInclude(b => b.Bidder)
                .ToListAsync();
        }

        public async Task<Guid> UpdateAsync(Tangerine tangerine)
        {
            var existEntity = await _context.Tangerines.FindAsync(tangerine.Id);

            _context.Entry(existEntity).CurrentValues.SetValues(tangerine);
            await _context.SaveChangesAsync();
            return tangerine.Id;
        }

        public async Task<Guid> DeleteAsync(Guid tangerineId)
        {
            var entity = await _context.Tangerines.FindAsync(tangerineId);
            if (entity != null)
            {
                _context.Tangerines.Remove(entity);
                await _context.SaveChangesAsync();
            }
            return tangerineId;
            
        }

        public async Task<IEnumerable<Tangerine>> GetSpoiledTangerinesAsync()
        {
            return await _context.Tangerines
                .AsNoTracking()
                .Where(t => t.IsSpoiled)
                .Include(t => t.Bids)
                .ThenInclude(b => b.Bidder)
                .ToListAsync();
        }

        public async Task<IEnumerable<Tangerine>> GetBiddingExpiredTangerinesAsync()
        {
            return await _context.Tangerines
                .AsNoTracking()
                .Where(t => t.ExpiryDate < DateTime.UtcNow)
                .Include(t => t.Bids)
                .ThenInclude(b => b.Bidder)
                .ToListAsync();
        }
    }
}
