using TangerinesAuction.Core.Models;

namespace TangerinesAuction.Core.Abstractions.Services
{
    public interface IAuthService
    {
        Task<User> RegisterAsync(string email, string passwordHash);
        Task<string> AuthenticateAsync(string email, string password);
    }
}
