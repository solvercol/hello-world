using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Management;
using Infraestructure.Data.Core;

namespace Application.MainModule.SqlServices.Domain
{
    public class PedidoClientes
    {
        private readonly ISqlHelper _sql;

        public PedidoClientes(ISqlHelper sql)
        {
            _sql = sql;
        }

        public DataTable ListadoClientesPedido(int pagina, int registrosPorPagina, string codigoCliente, string nombreCliente)
        {
            try
            {
               return  _sql.ExecuteDataTable("ListadoClientesYDespachos", CommandType.StoredProcedure,
                                      new SqlParameter("@Pagina", pagina),
                                      new SqlParameter("@RegistrosporPagina", registrosPorPagina),
                                      new SqlParameter("@Codigo", string.IsNullOrEmpty(codigoCliente) ? null : codigoCliente),
                                      new SqlParameter("@NombreCliente", string.IsNullOrEmpty(nombreCliente) ? null : nombreCliente));
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("ListadoClientesPedido", ex);
            }
        }

        public int CountClientesPedido(string codigoCliente, string nombreCliente)
        {
            try
            {
                var result = _sql.ExecuteScalar("CountClientesYDespachos", CommandType.StoredProcedure,
                    new SqlParameter("@Codigo", string.IsNullOrEmpty(codigoCliente) ? null : codigoCliente),
                    new SqlParameter("@NombreCliente", string.IsNullOrEmpty(nombreCliente) ? null : nombreCliente));

                if (result != null)
                    return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("ListadoClientesPedido", ex);
            }

            return 0;
        }


        public DataTable RetornarPedidoPorIdPedido(string idPedido)
        {
            try
            {
                return _sql.ExecuteDataTable("GetPedidoByIdPedido", CommandType.StoredProcedure,
                                       new SqlParameter("@IdPedido", idPedido));
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("RetornarPedidoPorIdPedido", ex);
            }
        }

        public DataTable CargarPedidoCompetoPorIdPedido(string idPedido)
        {
            try
            {
                return _sql.ExecuteDataTable("CargarPedidoCompletoPorIdPedido", CommandType.StoredProcedure,
                                       new SqlParameter("@IdPedido", idPedido));
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("CargarPedidoCompletoPorIdPedido", ex);
            }
        }
    }
}