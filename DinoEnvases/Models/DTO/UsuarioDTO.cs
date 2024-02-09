using System.ComponentModel.DataAnnotations;

namespace DinoEnvases.Models.DTO
{
    public class UsuarioDTO
    {
        [Required(ErrorMessage = "User's id is required")]
        public int?  Id { get; set; }

        [Required(ErrorMessage = "Guard's name is required")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "Lastname's guard is required")]
        public string? Apellido { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string? Usuario { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password {  get; set; }
                
    }
}
