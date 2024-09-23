using Microsoft.AspNetCore.Mvc;

namespace TangerinesAuction.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;

        public ImageController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }


        [Route("getImage/{imageUrl}")]
        [HttpGet]
        public IActionResult GetImage(string imageUrl)
        {
            try
            {
                var filePath = Path.Combine(_environment.ContentRootPath, "wwwroot/images/tangerines", imageUrl);

                if (System.IO.File.Exists(filePath))
                {
                    var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    return File(fileStream, "image/jpg"); 
                }
                else
                {
                    return NotFound("Изображение не найдено");
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }
    }
}
