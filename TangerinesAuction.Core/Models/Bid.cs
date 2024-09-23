using System.Text.Json.Serialization;

namespace TangerinesAuction.Core.Models
{
    public class Bid : BaseModel
    {
        public Guid TangerineId { get; private set; }
        [JsonIgnore]
        public Tangerine Tangerine { get; private set; }
        public Guid BidderId { get; private set; }
        [JsonIgnore]
        public User Bidder { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime CreateDate { get; private set; }

        public Bid() { }

        public Bid(Guid tangerineId, Guid bidderId, decimal amount, Tangerine? tangerine = null, User? user = null)
        {
            Id = Guid.NewGuid();
            TangerineId = tangerineId;
            Tangerine = tangerine;
            BidderId = bidderId;
            Bidder = user;
            Amount = amount;
            CreateDate = DateTime.UtcNow;
        }
    }
}
