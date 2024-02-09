using DinoEnvases.Data;
using DinoEnvases.Models;
using DinoEnvases.Models.Requests;
using System.Transactions;

namespace DinoEnvases.Rules
{
    public class ValeRule
    {        
        public Vale? ObtenerDatosVale(ValeRequest modelo)
        {
            Vale? datosVale = new ValeData().ValeInfo(modelo.Sucursal!);

            datosVale!.Id = string.Concat(new ValeData().LastValeId(), "-", modelo.ValeNro);

            return datosVale;
        }

        public async Task<bool> AddVale(Vale vale)
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    await new ValeData().InsertarVale(vale);

                    // Realizar commit de la transacción
                    transaction.Complete();
                    return true;
                }
                catch (Exception ex)
                {
                    // Manejar el error
                    Console.WriteLine("Error al insertar envases: " + ex.Message);
                    return false;
                }
            }
        }

        public async Task<bool> AddEnvase(List<Envase> envases, string valeId)
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    int? lastId = new ValeData().UltimoEnvaseId();

                    foreach (var envase in envases)
                    {
                        lastId++;

                        await new ValeData().InsertEnvase(envase, valeId);
                    }

                    // Realizar commit de la transacción
                    transaction.Complete();
                    return true;
                }
                catch (Exception ex)
                {
                    // Manejar el error
                    Console.WriteLine("Error al insertar envases: " + ex.Message);
                    // Deshacer la transacción en caso de error
                    return false;
                }
            }
        }
    }
}
