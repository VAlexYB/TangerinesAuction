using TangerinesAuction.Core.Abstractions.Repositories;
using TangerinesAuction.Core.Abstractions.Services;
using TangerinesAuction.Core.Models;

namespace TangerinesAuction.Application.Services
{
    public class BidsService : IBidsService
    {
        private readonly IBidsRepository _bidsRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly ITangerinesRepository _tangerinesRepository;
        private readonly INotificationsService _notificationsService;

        public BidsService(
            IBidsRepository bidsRepository, 
            IUsersRepository usersRepository, 
            ITangerinesRepository tangerinesRepository, 
            INotificationsService notificationsService
        )
        {
            _bidsRepository = bidsRepository;
            _usersRepository = usersRepository;
            _tangerinesRepository = tangerinesRepository;
            _notificationsService = notificationsService;
        }

        public async Task<IEnumerable<Bid>> GetUserBidsAsync(Guid userId)
        {
            return await _bidsRepository.GetBidsForUserAsync(userId);
        }

        public async Task PlaceBidAsync(Guid userId, Guid tangerineId, decimal amount)
        {
            var user = await _usersRepository.GetByIdAsync(userId);
            var tangerine = await _tangerinesRepository.GetByIdAsync(tangerineId);

            if (user == null) throw new InvalidOperationException("Пользователь без id пытается сделать ставку!!!!");
            if (tangerine == null) throw new InvalidOperationException("Пользователь пытается сделать ставку по id мандарина, которого нет в бд");

            var bid = new Bid(tangerineId, userId, amount, tangerine, user);
            

            tangerine.PlaceBid(bid);
            user.PlaceBid(bid);

            await _bidsRepository.CreateAsync(bid);

            var outbidUsers = tangerine.Bids
                .Select(b => b.Bidder)
                .Distinct()
                .Where(u => u.Id != userId)
                .ToList(); /* сообщение должно отправиться всем остальным участникам аукциона,
                             так как пользователь, вызывающий данный метод и перебивает ставку */

            foreach (var outbidUser in outbidUsers)
            {
                await _notificationsService.SendBidOutbidNotificationAsync(outbidUser, tangerine);
            }
        }

        public async Task CheckBiddingEndAsync()
        {
            var expiredTangerines = await _tangerinesRepository.GetBiddingExpiredTangerinesAsync();

            foreach (var tangerine in expiredTangerines)
            {
                if (tangerine.Bids.Any() && !tangerine.IsWinnerNotified)
                {
                    var winningBid = tangerine.Bids.OrderByDescending(b => b.Amount).FirstOrDefault();
                    var winningBidder = winningBid.Bidder;

                    await _notificationsService.SendAuctionWinNotificationAsync(winningBidder, tangerine);

                    tangerine.MarkAsWinnerNotified();
                    await _tangerinesRepository.UpdateAsync(tangerine);
                }
            }
        }
    }
}
