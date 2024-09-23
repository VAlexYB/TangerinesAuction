using Microsoft.Extensions.DependencyInjection;
using TangerinesAuction.Application.Services;
using TangerinesAuction.Core.Abstractions.Services;

namespace TangerinesAuction.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IBidsService, BidsService>();
            services.AddTransient<ITangerinesService, TangerinesService>();
            return services;
        }
    }
}
