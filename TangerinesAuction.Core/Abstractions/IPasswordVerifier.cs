namespace TangerinesAuction.Core.Abstractions
{
    public interface IPasswordVerifier
    {
        bool Verify(string password, string hashPassword);
    }
}
