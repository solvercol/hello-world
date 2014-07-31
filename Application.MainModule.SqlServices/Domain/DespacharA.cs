using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Management;
using Infraestructure.Data.Core;

namespace Application.MainModule.SqlServices.Domain
{
    public class DespacharA
    {
         private readonly ISqlHelper _sql;

        public DespacharA(ISqlHelper sql)
        {
            _sql = sql;
        }

        public DataTable ListadoDespacharA(int pagina, int registrosPorPagina, string codigoCliente, string empresaDespacho)
        {
            try
            {
                return _sql.ExecuteDataTable("ListadoDespacharA", CommandType.StoredProcedure,
                                      new SqlParameter("@Pagina", pagina),
                                      new SqlParameter("@RegistrosporPagina", registrosPorPagina),
                                      new SqlParameter("@Codigo", string.IsNullOrEmpty(codigoCliente) ? null : codigoCliente),
                                      new SqlParameter("@EmpresaDespacho", string.IsNullOrEmpty(empresaDespacho) ? null : empresaDespacho));
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("ListadoDespacharA", ex);
            }
        }

        public int CountDespacharA(string codigoCliente, string empresaDespacho)
        {
            try
            {
                var result = _sql.ExecuteScalar("CountDespacharA", CommandType.StoredProcedure,
                    new SqlParameter("@Codigo", string.IsNullOrEmpty(codigoCliente) ? null : codigoCliente),
                    new SqlParameter("@EmpresaDespacho", string.IsNullOrEmpty(empresaDespacho) ? null : empresaDespacho));

                if (result != null)
                    return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("CountDespacharA", ex);
            }

            return 0;
        }

        public DataTable ListadoSitiosDespacho(string codigoCliente)
        {
            try
            {
                return _sql.ExecuteDataTable("SitiosDespacho", CommandType.StoredProcedure,
                                      new SqlParameter("@Codigo", string.IsNullOrEmpty(codigoCliente) ? null : codigoCliente));
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("ListadoSitiosDespacho", ex);
            }
        }
    }
}