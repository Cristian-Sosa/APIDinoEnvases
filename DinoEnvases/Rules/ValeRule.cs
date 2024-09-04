using DinoEnvases.Data;
using DinoEnvases.Models;
using DinoEnvases.Models.Requests;
using DinoEnvases.Models.Responses;
using Microsoft.AspNetCore.Mvc;
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
            if (nroSucursal == null || id == null)
            {
                return null;
            }

            // Generar el código base
            string cod1 = "9" + nroSucursal.Value.ToString("D3") + id.PadLeft(8, '0');

            // Calcular el dígito de control
            int sum = 0;
            for (int i = 0; i < cod1.Length; i++)
            {
                int digit = int.Parse(cod1[i].ToString());
                sum += (i % 2 == 0) ? digit : digit * 3;
            }

            int checkDigit = (10 - (sum % 10)) % 10;

            // Devolver el código completo
            return cod1 + checkDigit.ToString();
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

        public async Task<bool> ConsumirTicket(TicketRequest data)
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    await new ValeData().ConsumirTicket(data);

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

        public List<EnvaseFacturable>? ObtenerDetalleVale(string nroTransaccion)
        {
            return new ValeData().DetalleVale(nroTransaccion);
        }

        public string ValidarValeRendido(string nroTransaccion)
        {
            Vale? isValeRendido = new ValeData().ValidarValeRendido(nroTransaccion);

            if (isValeRendido != null && isValeRendido.IdEstadoVale == 3)
            {
                return "El vale ya se encuentra facturado";
            } else if(isValeRendido == null)
            {
                return "El vale no existe";
            }

            return "";
        }

        public async Task<bool> AddEnvase(List<Envase> envases, string valeId, string ean)
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
