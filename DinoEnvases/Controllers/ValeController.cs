using DinoEnvases.Models;
using DinoEnvases.Models.Requests;
using DinoEnvases.Rules;
using Microsoft.AspNetCore.Mvc;

namespace DinoEnvases.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValeController : ControllerBase
    {
        [HttpPost("Add")]
        public IActionResult AddVale(ValeRequest modelo)
        {
            Vale? datosVale = new ValeRule().ObtenerDatosVale(modelo);

            Task<bool> envases = new ValeRule().AddEnvase(modelo.Items!, datosVale!.Id!);

            Task<bool> vale = new ValeRule().AddVale(datosVale);

            return Ok(datosVale);
        }
    }
}
