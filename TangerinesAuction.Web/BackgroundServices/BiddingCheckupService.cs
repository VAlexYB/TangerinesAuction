
using TangerinesAuction.Core.Abstractions.Services;

namespace TangerinesAuction.Web.BackgroundServices
{
    public class BiddingCheckupService : IHostedService, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private Timer _timer;

        public BiddingCheckupService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(CheckBidding, null, TimeSpan.Zero, TimeSpan.FromHours(1));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        private async void CheckBidding(object state)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var bidsService = scope.ServiceProvider.GetRequiredService<IBidsService>();
                await bidsService.CheckBiddingEndAsync();
            }
        }
    }
}
