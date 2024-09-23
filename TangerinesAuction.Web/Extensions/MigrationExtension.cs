using Microsoft.EntityFrameworkCore;
using TangerineAuction.Infrastructure.Persistence;

namespace TangerinesAuction.Web.Extensions
{
    public static class MigrationExtension
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            using TADbContext dbContext = scope.ServiceProvider.GetRequiredService<TADbContext>();

            dbContext.Database.Migrate();
        }
    }
}
