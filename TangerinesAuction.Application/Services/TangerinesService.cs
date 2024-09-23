using TangerinesAuction.Core.Abstractions.Repositories;
using TangerinesAuction.Core.Abstractions.Services;
using TangerinesAuction.Core.Models;

namespace TangerinesAuction.Application.Services
{
    public class TangerinesService : ITangerinesService
    {
        private readonly ITangerinesRepository _tangerinesRepository;

        public TangerinesService(ITangerinesRepository tangerinesRepository)
        {
            _tangerinesRepository = tangerinesRepository;
        }

        public async Task CheckAndMarkAsSpoiled()
        {
            var tangerines = await _tangerinesRepository.GetTangerinesAsync(); //проверяем только не испорченные мандарины, находящиеся в аукционе

            foreach (var tangerine in tangerines)
            {
                if (tangerine.CheckIfSpoiled())
                {
                    await _tangerinesRepository.UpdateAsync(tangerine);
                }
            }
        }

        public async Task CreateTangerineAsync(string imageUrl, decimal startPrive, DateTime expiryDate)
        {
            var tangerine = new Tangerine(imageUrl, startPrive, expiryDate);
            await _tangerinesRepository.CreateAsync(tangerine);
        }

        public async Task<IEnumerable<Tangerine>> GetTangerinesAsync()
        {
            return await _tangerinesRepository.GetTangerinesAsync();
        }

        public async Task RemoveSpoiledTangerinesAsync()
        {
            var spoiledTanegrines = await _tangerinesRepository.GetSpoiledTangerinesAsync();

            foreach (var tangerine in spoiledTanegrines)
            {
                await _tangerinesRepository.DeleteAsync(tangerine.Id);
            }
        }
    }
}
