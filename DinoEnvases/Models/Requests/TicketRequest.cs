using System.ComponentModel.DataAnnotations;

namespace DinoEnvases.Models.Requests
{
    public class TicketRequest
    {
        [Required(ErrorMessage = "The field TipoTkFiscal is required")]
        public string TipoTkFiscal { get; set; } = "";

        [Required(ErrorMessage = "The field NroTkFiscal is required")]
        public string NroTkFiscal { get; set; } = "";

        [Required(ErrorMessage = "The field PVFiscal is required")]
        public string PVFiscal { get; set; } = "";

        [Required(ErrorMessage = "The field NroTransaccion is required")]
        public string NroTransaccion { get; set; } = "";
    }
}