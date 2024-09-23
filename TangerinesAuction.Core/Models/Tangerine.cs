using TangerinesAuction.Core.Exceptions;

namespace TangerinesAuction.Core.Models
{
    public class Tangerine : BaseModel
    {
        public string ImageUrl { get; private set; } 
        public decimal StartPrice { get; private set; }
        public decimal CurrentPrice { get; private set; }
        public DateTime CreateDate { get; private set; }
        public DateTime ExpiryDate { get; private set; }
        public bool IsSpoiled { get; private set; }
        public bool IsWinnerNotified { get; private set; }

        public bool IsAuctionEnded => DateTime.UtcNow > ExpiryDate;

        private readonly List<Bid> _bids = new List<Bid>();

        public IReadOnlyCollection<Bid> Bids => _bids.AsReadOnly();

        public Tangerine() { }
        public Tangerine(string imageUrl, decimal startPrice, DateTime expiryDate)
        {
            Id = Guid.NewGuid();
            ImageUrl = imageUrl;
            StartPrice = startPrice;
            CurrentPrice = startPrice;
            CreateDate = DateTime.UtcNow;
            ExpiryDate = expiryDate;
            IsSpoiled = false;
            IsWinnerNotified = false;
        }

        public void PlaceBid(Bid newBid)
        {
            if (DateTime.UtcNow > ExpiryDate) throw new TangerineExpiredException("Пользователь попытался купить мандарин. А аукцион уже прошёл");

            if (IsSpoiled) throw new TangerineSpoiledException("Пользователь попытался купить испорченный мандарин");

            if (newBid.Amount < CurrentPrice) throw new InvalidBidException("Ставка должна быть больше, чтобы перебить чужую ставку");

            CurrentPrice = newBid.Amount;
            _bids.Add(newBid);
        }

        public bool CheckIfSpoiled()
        {
            if (!IsSpoiled && (DateTime.UtcNow - CreateDate).TotalHours >= 24)
            {
                if (Bids.Count == 0)
                {
                    IsSpoiled = true;
                }
            }

            return IsSpoiled;
        }

        public Bid GetWinningBid()
        {
            return Bids?.OrderByDescending(b => b.Amount).FirstOrDefault();
        }

        public void MarkAsWinnerNotified()
        {
            IsWinnerNotified = true;
        }
    }
}
