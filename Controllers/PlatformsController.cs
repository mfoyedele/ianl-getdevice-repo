using Microsoft.AspNetCore.Mvc;
using WebApi.Entities;

namespace WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]   
    public class PlatformsController : ControllerBase
    {
        

        [HttpGet("binder/{id}")]
        public IActionResult GetPlatformById(
            [ModelBinder(Name = "id")] Platform platform)
        {
            if (platform == null)
            {
                return NotFound();
            }

            return Ok(platform);
        }
    }
}