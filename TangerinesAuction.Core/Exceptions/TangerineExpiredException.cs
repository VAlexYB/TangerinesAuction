namespace TangerinesAuction.Core.Exceptions
{
    public class TangerineExpiredException : Exception
    {
        public TangerineExpiredException(string message) : base(message) { }
    }
}
