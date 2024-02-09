namespace DinoEnvases.Models.DTO
{
    public class ValeDTO
    {
        public int? ID { get; set; }
        public int? IdUsuario { get; set; }
        public string? NombreUsuario { get; set; }
        public string? IdSucursal { get; set; }
        public string? NroSucursal { get; set; }
        public string? NombreSucursal { get; set; }
        public string? TipoTkFiscal { get; set; } = null;
        public string? NroTkFiscal { get; set; } = null;
        public string? PVFiscal { get; set; } = null;
        public string? NroTransaccion { get; set; } = null;
        public int? IdEstadoVale { get; set; } = 1;
        public DateTime FechaHora { get; set; } = DateTime.Now;
    }
}
