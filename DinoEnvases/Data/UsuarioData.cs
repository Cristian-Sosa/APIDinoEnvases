using DinoEnvases.Models;
using DinoEnvases.Models.DTO;

namespace DinoEmpleoAPI.Data
{
    public class UsuarioData
    {
        private readonly DatabaseSingleton singleton = DatabaseSingleton.Instance;

        public UsuarioDTO? Login(UsuarioDTO usuario)
        {
            string query = "SELECT * FROM dbo.Usuario WHERE Usuario = @username AND Password = @password";

            UsuarioDTO? data = singleton.ExecuteQuery<UsuarioDTO?>(query, new { username = usuario.Usuario, password = usuario.Password }).FirstOrDefault();

            return data;
        }

        public List<UsuarioDTO>? ActiveUsers()
        {
            string query = "SELECT * FROM dbo.Usuario WHERE Habilitado = 1";

            List<UsuarioDTO>? data = singleton.ExecuteQuery<UsuarioDTO>(query, null).ToList();

            return data;
        }
    }
}
