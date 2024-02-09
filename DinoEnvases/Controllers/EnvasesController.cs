using DinoEnvases.Rules;
using Microsoft.AspNetCore.Mvc;

namespace DinoEnvases.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnvasesController : ControllerBase
    {
        [HttpGet("AllActives")]
        public IActionResult ActiveEnvases() 
        {
            var rule = new EnvaseRule().ActiveEnvases();

            return Ok(rule);
        }
    }
}
