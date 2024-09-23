using TangerinesAuction.Core.Models;

namespace TangerinesAuction.Core.Abstractions.Services
{
    public interface IBidsService
    {
        Task PlaceBidAsync(Guid userId, Guid tangerineId, decimal amount);
        Task<IEnumerable<Bid>> GetUserBidsAsync(Guid userId);
        Task CheckBiddingEndAsync();
    }
}
