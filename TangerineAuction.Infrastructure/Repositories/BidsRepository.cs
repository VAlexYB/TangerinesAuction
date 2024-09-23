using Microsoft.EntityFrameworkCore;
using TangerineAuction.Infrastructure.Persistence;
using TangerinesAuction.Core.Abstractions.Repositories;
using TangerinesAuction.Core.Models;

namespace TangerineAuction.Infrastructure.Repositories
{
    public class BidsRepository : IBidsRepository
    {
        private readonly TADbContext _context;

        public BidsRepository(TADbContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateAsync(Bid bid)
        {
            await _context.Bids.AddAsync(bid);
            await _context.SaveChangesAsync();
            return bid.Id;
        }

        public async Task<IEnumerable<Bid>> GetBidsForUserAsync(Guid userId)
        {
            return await _context.Bids.AsNoTracking().Where(b => b.BidderId == userId).ToListAsync();
        }

        public async Task<Bid> GetByIdAsync(Guid bidId)
        {
            return await _context.Bids.FindAsync(bidId);
        }
    }
}
