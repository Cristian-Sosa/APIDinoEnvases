using System.ComponentModel.DataAnnotations;

namespace DinoEnvases.Models.Requests
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Username field is required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password field is required")]
        public string? Password { get; set; }
    }
}
