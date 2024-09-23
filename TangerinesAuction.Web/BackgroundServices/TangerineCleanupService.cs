
using TangerinesAuction.Core.Abstractions.Services;

namespace TangerinesAuction.Web.BackgroundServices
{
    public class TangerineCleanupService : IHostedService, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private Timer _timer;

        public TangerineCleanupService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(RemoveSpoiledTangerines, null, TimeSpan.Zero, TimeSpan.FromDays(1));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        private async void RemoveSpoiledTangerines(object state)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var tangerinesService = scope.ServiceProvider.GetRequiredService<ITangerinesService>();
                await tangerinesService.CheckAndMarkAsSpoiled();
                await tangerinesService.RemoveSpoiledTangerinesAsync();
            }
        }
    }
}
