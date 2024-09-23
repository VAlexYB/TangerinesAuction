using Microsoft.EntityFrameworkCore;
using TangerineAuction.Infrastructure.Persistence;
using TangerinesAuction.Core.Abstractions.Repositories;
using TangerinesAuction.Core.Models;

namespace TangerineAuction.Infrastructure.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly TADbContext _context;

        public UsersRepository(TADbContext context)
        {
            _context = context;
        }

        public async Task<Guid> AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user.Id;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Id == id);
        }
    }
}
