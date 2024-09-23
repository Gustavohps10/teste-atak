using Microsoft.AspNetCore.Mvc;

namespace teste_atak.Server.Controllers
{
    [Route("health")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Check()
        {
            return Ok(new { Status = "API está no ar!" });
        }
    }
}
