using Microsoft.EntityFrameworkCore;
using TangerinesAuction.Core.Models;

namespace TangerineAuction.Infrastructure.Persistence
{
    public class TADbContext : DbContext
    {
        public TADbContext(DbContextOptions<TADbContext> options) : base(options) { }

        public DbSet<Tangerine> Tangerines { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
