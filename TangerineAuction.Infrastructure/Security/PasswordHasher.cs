using TangerinesAuction.Core.Abstractions;

namespace TangerineAuction.Infrastructure.Security
{
    public class PasswordHasher : IPasswordHasher
    {
         public string Generate(string password) =>
            BCrypt.Net.BCrypt.EnhancedHashPassword(password);
    }
}
