using TangerinesAuction.Core.Models;

namespace TangerinesAuction.Core.Abstractions.Services
{
    public interface ITangerinesService
    {
        Task CreateTangerineAsync(string imageUrl, decimal startPrive, DateTime expiryDate);
        Task<IEnumerable<Tangerine>> GetTangerinesAsync();
        Task CheckAndMarkAsSpoiled();
        Task RemoveSpoiledTangerinesAsync();
    }
}
