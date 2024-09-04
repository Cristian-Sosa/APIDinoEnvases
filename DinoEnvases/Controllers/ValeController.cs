using DinoEnvases.Models;
using DinoEnvases.Models.Requests;
using DinoEnvases.Models.Responses;
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
            if(modelo.Sucursal != "csosa" && modelo.Sucursal != "mcastillo")
            {
                Vale? datosVale = new ValeRule().ObtenerDatosVale(modelo);

                var _ = new ValeRule().AddEnvase(modelo.Items!, datosVale!.Id!, datosVale.EAN!);

                var __ = new ValeRule().AddVale(datosVale);

                if (datosVale.EAN != null)
                    datosVale.EAN = datosVale.EAN?[..^1];

                return Ok(datosVale);
            }

            return Ok();
        }

        [HttpPost("BulkAdd")]
        public IActionResult BulkAddVale([FromBody] List<ValeRequest> modelo)
        {
            for (int i = 0; i < modelo.Count; i++)
            {
                Vale? datosVale = new ValeRule().ObtenerDatosVale(modelo[i]);

                var _ = new ValeRule().AddEnvase(modelo[i].Items!, datosVale!.Id!, datosVale.EAN!);

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

        [HttpPatch("ConsumirTicket")]
        public IActionResult ConsumirVale([FromBody] TicketRequest vale)
        {
            string isValeRendido = new ValeRule().ValidarValeRendido(vale.NroTransaccion);

            if (isValeRendido != "")
                return BadRequest(isValeRendido);

            Task<bool> rule = new ValeRule().ConsumirTicket(vale);

            List<EnvaseFacturable>? listaEnvases = new ValeRule().ObtenerDetalleVale(vale.NroTransaccion);

            return Ok(listaEnvases);



        }
    }
}
