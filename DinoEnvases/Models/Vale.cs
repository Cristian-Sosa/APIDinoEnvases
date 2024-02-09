namespace DinoEnvases.Models
{
    public class Vale
    {
        public string? Id { get; set; }
        public int? IdUsuario { get; set; }
        public string? NombreUsuario { get; set; }
        public int? IdSucursal { get; set; }
        public int? NroSucursal { get; set; }
        public string? NombreSucursal { get; set; }
        public string? TipoTkFiscal { get; set; } = null;
        public int? NroTkFiscal { get; set; } = null;
        public int? PVFiscal { get; set; } = null;
        public string? NroTransaccion { get; set; } = null;
        public int? IdEstadoVale { get; set; } = 1;
        public DateTime? FechaHora { get; set; } = DateTime.Now;
    }
}
