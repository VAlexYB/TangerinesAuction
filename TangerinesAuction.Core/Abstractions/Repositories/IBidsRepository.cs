using TangerinesAuction.Core.Models;

namespace TangerinesAuction.Core.Abstractions.Repositories
{
    public interface IBidsRepository
    {
        Task<Guid> CreateAsync(Bid bid);
        Task<IEnumerable<Bid>> GetBidsForUserAsync(Guid userId);
        Task<Bid> GetByIdAsync(Guid bidId);
    }
}
