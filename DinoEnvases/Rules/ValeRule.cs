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
            string? lastVale = new ValeData().LastValeId();
            Vale? datosVale = new ValeData().ValeInfo(modelo.Sucursal!);

            string? ean = EANGenerator(datosVale!.NroSucursal!, lastVale!);

            datosVale.EAN = ean;

            datosVale!.Id = string.Concat(lastVale, "-", modelo.ValeNro);

            return datosVale;
        }

        private static string? EANGenerator(int? nroSucursal, string? id)
        {
            string cod1 = "";

            if (nroSucursal != null && id != null)
            {
                cod1 = "9" + nroSucursal.ToString()!.PadLeft(3, '0') + id!.PadLeft(8, '0');
            }

            return cod1!;
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

        public async Task<bool> AnularVale(string NroVale, string Username)
        {
            await new ValeData().AnularVale(NroVale, Username);

            return true;
        }


        public async Task<bool> AnularVale2(string nroVale, string nombreUsuario)
        {
            await new ValeData().AnularVale(nroVale, nombreUsuario);

            return true;
        }
    }
}
