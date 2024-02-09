using System.ComponentModel.DataAnnotations;

namespace DinoEnvases.Models.Requests
{
    public class ValeRequest
    {
        [Required(ErrorMessage = "The branch office is required")]
        public string? Sucursal { get; set; }

        [Required(ErrorMessage = "The ID value is required")]
        public string? ValeNro { get; set; }

        [Required(ErrorMessage = "The vale content is required")]
        public List<Envase>? Items { get; set; }
    }
}
