using Microsoft.AspNetCore.Mvc;
using TangerinesAuction.Core.Abstractions.Services;

namespace TangerinesAuction.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TangerinesController : ControllerBase
    {
        private readonly ITangerinesService _tangerinesService;

        public TangerinesController(ITangerinesService tangerinesService)
        {
            _tangerinesService = tangerinesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTangerines()
        {
            var tangerines = await _tangerinesService.GetTangerinesAsync();
            return Ok(tangerines);
        }
    }
}
