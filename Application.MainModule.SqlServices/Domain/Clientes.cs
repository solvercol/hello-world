using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Management;
using Infraestructure.Data.Core;

namespace Application.MainModule.SqlServices.Domain
{
    public class Clientes
    {
        private readonly ISqlHelper _sql;

        public Clientes(ISqlHelper sql)
        {
            _sql = sql;
        }

        public DataTable ListadoClientes(int pagina, int registrosPorPagina, string codigoCliente, string nombre)
        {
            try
            {
                return _sql.ExecuteDataTable("ListadoClientes", CommandType.StoredProcedure,
                                      new SqlParameter("@Pagina", pagina),
                                      new SqlParameter("@RegistrosporPagina", registrosPorPagina),
                                      new SqlParameter("@Codigo", string.IsNullOrEmpty(codigoCliente) ? null : codigoCliente),
                                      new SqlParameter("@Nombre", string.IsNullOrEmpty(nombre) ? null : nombre));
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("ListadoClientes", ex);
            }
        }

        public int CountClientes( string codigoCliente, string nombre)
        {
            try
            {
                var res = _sql.ExecuteScalar("CountClientes", CommandType.StoredProcedure,
                                      new SqlParameter("@Codigo", string.IsNullOrEmpty(codigoCliente) ? null : codigoCliente),
                                      new SqlParameter("@Nombre", string.IsNullOrEmpty(nombre) ? null : nombre));

                if (res != null)
                    return Convert.ToInt32(res);
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("CountClientes", ex);
            }

            return 0;
        }
    }
}