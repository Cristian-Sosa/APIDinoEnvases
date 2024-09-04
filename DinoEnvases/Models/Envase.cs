using System.Text.Json.Serialization;

namespace DinoEnvases.Models
{
    public class Envase
    {
        public int Id { get; set; }
        public string? Codigo { get; set; }
        public string? Descripcion { get; set; }
        public string? Ean { get; set; }
        public string? Precio { get; set; }
        public int TipoEnvaseId { get; set; }
        public int? Cantidades { get; set; }
    }

    public class TipoEnvase
    {
        public int Id { get; set;}
        public string? Nombre { get; set; }
        public string? Habilitado { get; set; }
    }
}
