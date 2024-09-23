
using TangerinesAuction.Application.Services;
using TangerinesAuction.Core.Abstractions.Services;
using TangerinesAuction.Core.Models;

namespace TangerinesAuction.Web.BackgroundServices
{
    public class TangerineGenerationService : IHostedService, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IImagesService _imagesService;
        private Timer _timer;

        
        private readonly TimeSpan _interval = TimeSpan.FromHours(3);

        public TangerineGenerationService(IServiceProvider serviceProvider, IImagesService imagesService)
        {
            _serviceProvider = serviceProvider;
            _imagesService = imagesService;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(GenerateTangerine, null, TimeSpan.Zero, _interval);
           return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        private async void GenerateTangerine(object state)
        {
            string imageUrl = _imagesService.GetRandomTangerineImage();
            decimal price = Math.Round((decimal)new Random().NextDouble() * 100, 2);
            using (var scope = _serviceProvider.CreateScope())
            {
                var tangerinesService = scope.ServiceProvider.GetRequiredService<ITangerinesService>();
                await tangerinesService.CreateTangerineAsync(imageUrl, price, DateTime.UtcNow.AddDays(3));
            }
                
        }
    }
}
