namespace DinoEnvases.Models
{
    public class Usuario
    {
        public int? Id { get; set; }
        public string? Dni { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public bool? Habilitado { get; set; }
    }
}
