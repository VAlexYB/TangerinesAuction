using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TangerinesAuction.Application.Services;
using TangerinesAuction.Core.Abstractions.Services;
using TangerinesAuction.Core.Exceptions;
using TangerinesAuction.Web.Contracts.Requests;

namespace TangerinesAuction.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BidsController : ControllerBase
    {
        private readonly IBidsService _bidsService;

        public BidsController(IBidsService bidsService)
        {
            _bidsService = bidsService;
        }

        [HttpPost]
        public async Task<IActionResult> PlaceBid([FromBody] CreateBidRequest request)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (userId == null)
                {
                    return Unauthorized();
                }
                await _bidsService.PlaceBidAsync(Guid.Parse(userId), request.TangerineId, request.Amount);
                return Ok();
            }
            catch (UserException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
            
        }
    }
}
