using TangerinesAuction.Core.Models;

namespace TangerinesAuction.Core.Abstractions
{
    public interface IJwtProvider
    {
        string GenerateToken(User user);
    }
}
