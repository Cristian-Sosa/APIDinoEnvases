using DinoEmpleoAPI.Data;
using DinoEnvases.Data;
using DinoEnvases.Models;
using DinoEnvases.Models.DTO;

namespace DinoEnvases.Rules
{
    public class EnvaseRule
    {
        public List<Envase>? ActiveEnvases()
        {
            List<Envase>? data = new EnvaseData().ActiveEnvases();

            return data;
        }
    }
}
