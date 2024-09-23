using TangerinesAuction.Core.Models;

namespace TangerinesAuction.Core.Abstractions.Services
{
    public interface INotificationsService
    {
        Task SendBidOutbidNotificationAsync(User user, Tangerine tangerine);
        Task SendAuctionWinNotificationAsync(User user, Tangerine tangerine);
    }
}
