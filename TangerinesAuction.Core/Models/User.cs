namespace TangerinesAuction.Core.Models
{
    public class User : BaseModel
    {
        public string Email { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;
        public List<Bid> Bids { get; set; } = new List<Bid>();
        public User() { }
        public User(string email, string passwordHash)
        {
            Id = Guid.NewGuid();
            Email = email;
            PasswordHash = passwordHash;
        }

        public void PlaceBid(Bid bid) 
        {
            if(bid == null) throw new ArgumentNullException(nameof(bid));
            Bids.Add(bid);
        }
    }
}
