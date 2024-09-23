using TangerinesAuction.Web.BackgroundServices;

namespace TangerinesAuction.Web
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBackgroundJobs(this IServiceCollection services)
        {
            services.AddHostedService<BiddingCheckupService>();
            services.AddHostedService<TangerineCleanupService>();
            services.AddHostedService<TangerineGenerationService>();
            return services;
        }
    }
}
