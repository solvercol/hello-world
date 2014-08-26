using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Management;
using Infraestructure.Data.Core;

namespace Application.MainModule.SqlServices.Domain
{
    public class Reclamos
    {
        private readonly ISqlHelper _sql;

        public Reclamos(ISqlHelper sql)
        {
            _sql = sql;
        }

        public string EstadoReclamo(string idReclamo)
        {
            try
            {
                const string strSql = " Select Est.Estado from TBL_ModuloReclamos_Reclamo Rec INNER JOIN TBL_Admin_EstadosProceso Est On Rec.IdEstado = Est.IdEstado where Rec.IdReclamo = @IdReclamo";
                var result = _sql.ExecuteScalar(strSql, CommandType.Text,
                                       new SqlParameter("@IdReclamo", idReclamo));

                return result == null ? string.Empty : result.ToString();
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("EstadoPedido", ex);
            }
        }

        public DataTable GetReclamoWorkFlowById(string idReclamo)
        {
            try
            {
                return _sql.ExecuteDataTable("GetReclamoWorkFlowById", CommandType.StoredProcedure,
                                       new SqlParameter("@idReclamo", idReclamo));
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("CargarPedidoCompletoPorIdPedido", ex);
            }
        }

        public DataTable ResumenReclamosPanelWorkFlow(string idReclamo)
        {
            try
            {
                return _sql.ExecuteDataTable("GetResumenReclamosPanelWf", CommandType.StoredProcedure,
                                       new SqlParameter("@idReclamo", idReclamo));
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("ResumenReclamosPanelWorkFlow", ex);
            }
        }
    }
}