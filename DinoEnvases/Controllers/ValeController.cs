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
        public IActionResult AddVale([FromBody] ValeRequest modelo)
        {
            Vale? datosVale = new ValeRule().ObtenerDatosVale(modelo);

            var _ = new ValeRule().AddEnvase(modelo.Items!, datosVale!.Id!);

            var __ = new ValeRule().AddVale(datosVale);

            return Ok(datosVale);
        }

        [HttpPost("BulkAdd")]
        public IActionResult BulkAddVale([FromBody] List<ValeRequest> modelo)
        {
            for (int i = 0; i < modelo.Count; i++)
            {
                Vale? datosVale = new ValeRule().ObtenerDatosVale(modelo[i]);

                var _ = new ValeRule().AddEnvase(modelo[i].Items!, datosVale!.Id!);

                var __ = new ValeRule().AddVale(datosVale);
            }

            return Ok(modelo);
        }

        [HttpPost("Anular")]
        public IActionResult AnularVale(string NroVale, string Username)
        {
            Task<bool> rule = new ValeRule().AnularVale2(NroVale, Username);

            return Ok(true);
        }
    }
}
