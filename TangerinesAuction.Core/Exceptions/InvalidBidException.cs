namespace TangerinesAuction.Core.Exceptions
{
    public class InvalidBidException : UserException
    {
        public InvalidBidException(string message) : base(message) { }
    }
}
