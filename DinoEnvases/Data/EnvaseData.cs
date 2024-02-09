using DinoEmpleoAPI.Data;
using DinoEnvases.Models;

namespace DinoEnvases.Data
{
    public class EnvaseData
    {
        private readonly DatabaseSingleton singleton = DatabaseSingleton.Instance;

        public List<Envase>? ActiveEnvases()
        {
            string query = "SELECT * FROM Envase WHERE Habilitado = 1";

            List<Envase>? data = singleton.ExecuteQuery<Envase>(query, null).ToList();

            return data;
        }

        public List<TipoEnvase>? TipoEnvases()
        {
            string query = "SELECT * FROM TipoEnvases";

            List<TipoEnvase>? data = singleton.ExecuteQuery<TipoEnvase>(query, null).ToList();

            return data;
        }
    }
}
