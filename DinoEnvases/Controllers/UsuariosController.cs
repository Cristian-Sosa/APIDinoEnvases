using DinoEnvases.Models.DTO;
using DinoEnvases.Rules;
using DinoEnvases.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace DinoEnvases.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        [HttpPost("Auth")]
        public IActionResult Login(LoginRequest modelo)
        {
            UsuarioDTO? rule = new UsuarioRule().Login(modelo);
            
            if (rule == null)
            {
                return NotFound();
            }
                return Ok(rule);
        }

        [HttpGet("AllActives")]
        public IActionResult GetActiveUsers()
        {
            var rule = new UsuarioRule().ActiveUsers();

            return Ok(rule);
        }
    };
};
