using TangerinesAuction.Core.Abstractions.Services;

namespace TangerineAuction.Infrastructure.Services
{
    public class ImagesService : IImagesService
    {
        private readonly string[] _imageUrls;

        public ImagesService()
        {
            _imageUrls = Directory.GetFiles("wwwroot/images/tangerines")
                .Select(Path.GetFileName)
                .ToArray();
        }

        public string GetRandomTangerineImage()
        {
            var random = new Random();
            var randomIndex = random.Next(_imageUrls.Length);
            return _imageUrls[randomIndex];
        }
    }
}
