using TangerinesAuction.Core.Models;

namespace TangerinesAuction.Core.Abstractions.Repositories
{
    public interface IUsersRepository
    {
        Task<User> GetByIdAsync(Guid id);
        Task<User> GetByEmailAsync(string email);
        Task<Guid> AddAsync(User user);
    }
}
