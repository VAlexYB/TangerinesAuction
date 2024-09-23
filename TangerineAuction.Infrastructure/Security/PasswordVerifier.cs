using TangerinesAuction.Core.Abstractions;

namespace TangerineAuction.Infrastructure.Security
{
    public class PasswordVerifier : IPasswordVerifier
    {
        public bool Verify(string password, string hashPassword) =>
             BCrypt.Net.BCrypt.EnhancedVerify(password, hashPassword);
    }
}
