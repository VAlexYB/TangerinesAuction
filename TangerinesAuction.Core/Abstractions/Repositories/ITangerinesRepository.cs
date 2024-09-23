using TangerinesAuction.Core.Models;

namespace TangerinesAuction.Core.Abstractions.Repositories
{
    public interface ITangerinesRepository
    {
        Task<Tangerine> GetByIdAsync(Guid tangerineId);
        Task<IEnumerable<Tangerine>> GetTangerinesAsync();
        Task<IEnumerable<Tangerine>> GetSpoiledTangerinesAsync();
        Task<IEnumerable<Tangerine>> GetBiddingExpiredTangerinesAsync();
        Task<Guid> CreateAsync(Tangerine tangerine);
        Task<Guid> UpdateAsync(Tangerine tangerine);
        Task<Guid> DeleteAsync(Guid tangerineId);
    }
}
