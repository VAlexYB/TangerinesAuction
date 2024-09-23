using Microsoft.Extensions.DependencyInjection;
using TangerineAuction.Infrastructure.Repositories;
using TangerineAuction.Infrastructure.Security;
using TangerineAuction.Infrastructure.Services;
using TangerinesAuction.Core.Abstractions;
using TangerinesAuction.Core.Abstractions.Repositories;
using TangerinesAuction.Core.Abstractions.Services;

namespace TangerineAuction.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IImagesService, ImagesService>();

            services.AddHttpClient<INotificationsService, NotificationService>();

            services.AddTransient<IBidsRepository, BidsRepository>();
            services.AddTransient<ITangerinesRepository, TangerinesRepository>();
            services.AddTransient<IUsersRepository, UsersRepository>();

            services.AddTransient<IPasswordHasher, PasswordHasher>();
            services.AddTransient<IPasswordVerifier, PasswordVerifier>();
            services.AddTransient<IJwtProvider, JwtProvider>();

            return services;
        }
    }
}
