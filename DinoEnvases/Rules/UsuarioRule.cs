using DinoEmpleoAPI.Data;
using DinoEnvases.Models.DTO;
using DinoEnvases.Models.Requests;

namespace DinoEnvases.Rules
{
    public class UsuarioRule
    {
        public UsuarioDTO? Login(LoginRequest modelo)
        {
            UsuarioDTO usuario = new()
            {
                Usuario = modelo.Username,
                Password = modelo.Password,
            };

            UsuarioDTO? dataUsuario = new UsuarioData().Login(usuario);

            if(dataUsuario != null)
            {
                usuario = dataUsuario;
                return usuario;
            
            } else
            {
                return null;
            }
        }

        public List<UsuarioDTO>? ActiveUsers()
        {
            List<UsuarioDTO>? data = new UsuarioData().ActiveUsers();

            return data;
        }
    };
};
