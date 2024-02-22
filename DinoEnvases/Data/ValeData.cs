using DinoEmpleoAPI.Data;
using DinoEnvases.Models;
using DinoEnvases.Models.DTO;
using DinoEnvases.Models.Requests;

namespace DinoEnvases.Data
{
    public class ValeData
    {
        private readonly DatabaseSingleton singleton = DatabaseSingleton.Instance;

        public ValeRequest? ValeExist(string nroVale)
        {
            string query = $"SELECT * FROM Vale WHERE Id LIKE '-{nroVale}'";

            ValeRequest? data = singleton.ExecuteQuery<ValeRequest?>(query, new { valeNro = nroVale }).FirstOrDefault();

            return data;
        }

        public async Task<bool> InsertarVale(Vale vale)
        {
            string query =  "INSERT INTO Vale " +
                            "(Id, IdUsuario, NombreUsuario, IdSucursal, NroSucursal, NombreSucursal, TipoTkFiscal, NrtoTkFiscal, PVFiscal, NroTransaccion, IdEstadoVale, FechaHora) " +
                            "VALUES (@ValeId, @IdUs, @NombreUs, @IdSuc, @NroSuc, @NombreSuc, @TipoTK, @NroTK, @PV, @Transaccion, @IdEstadoVale, GETDATE())";

            bool data = await singleton.ExecuteQueryTransacction(query, new
            {
                ValeId = vale.Id,
                IdUs = vale.IdUsuario,
                IdSuc = vale.IdSucursal,
                NroSuc = vale.NroSucursal != null ? vale.NroSucursal : null,
                NombreUs = vale.NombreUsuario,
                NombreSuc = vale.NombreSucursal,
                TipoTK = vale.TipoTkFiscal,
                NroTK = vale.NroTkFiscal,
                PV = vale.PVFiscal,
                Transaccion = vale.NroTransaccion,
                IdEstadoVale = 1,
            }); ;

            return data;
        }

        public Vale? ValeInfo(string sucursal)
        {
            string query = "SELECT u.Id AS 'IdUsuario', u.Usuario AS 'NombreUsuario', suc.Id AS 'IdSucursal', " +
                            "suc.SucTipre AS 'NroSucursal', suc.DescripcionCorta AS 'NombreSucursal' FROM Usuario u " +
                            "INNER JOIN Sucursal suc WITH(NOLOCK) ON suc.DescripcionCorta = u.Usuario " +
                            "WHERE u.Usuario = @nombreSuc";

            Vale? data = singleton.ExecuteQuery<Vale>(query, new { nombreSuc = sucursal }).FirstOrDefault();

            return data;
        }

        public string? LastValeId()
        {
            string query = "SELECT MAX(CAST(LEFT(Id, CHARINDEX('-', Id) - 1) AS INT)) AS MaxNumberBeforeDash FROM Vale WHERE CHARINDEX('-', Id) > 0;";

            string? data = singleton.ExecuteQuery<string>(query, null).FirstOrDefault();

            if (data == null)
                return null;
            
            string valeId = (int.Parse(data) + 1).ToString();


            return valeId;
        }

        public async Task<bool> InsertEnvase(Envase envase, string valeId)
        {
            string query = "INSERT INTO DetalleVale (IdVale, IdEnvase, Codigo, Descripcion, Cantidad) " +
                           "VALUES (@ValeId, @EnvaseId, @CodigoEnvase, @DescEnvase, @CantEnvase)";

            bool data = await singleton.ExecuteQueryTransacction(query, new 
            { 
                ValeId = valeId, 
                EnvaseId = envase.Id, 
                CodigoEnvase = int.Parse(envase.Codigo!), 
                DescEnvase = envase.Descripcion, 
                CantEnvase = envase.Cantidades 
            });

            return data;
        }

        public int? UltimoEnvaseId()
        {
            string query = "SELECT TOP 1 Id FROM DetalleVale ORDER BY Id DESC";

            string? data = singleton.ExecuteQuery<string>(query, null).FirstOrDefault();

            if (data == null)
            {
                return null;
            }

            return int.Parse(data);
        }

        public async Task<bool> AnularVale(string valeNro, string nombreUsuario)
        {
            string query = $"UPDATE Vale SET IdEstadoVale = 2 WHERE IdEstadoVale = 1 AND Id LIKE '%-{valeNro}' AND NombreUsuario = '{nombreUsuario}'";

            bool data = await singleton.ExecuteQueryTransacction(query, null);

            return data;
        }

    }
}
