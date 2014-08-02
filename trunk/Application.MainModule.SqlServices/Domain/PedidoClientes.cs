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

        public DataTable GetPedidoWorkFlowByIdPedido(string idPedido)
        {
            try
            {
                return _sql.ExecuteDataTable("GetPedidoWorkFlowByIdPedido", CommandType.StoredProcedure,
                                       new SqlParameter("@IdPedido", idPedido));
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("CargarPedidoCompletoPorIdPedido", ex);
            }
        }

        public string EstadoPedido(string idPedido)
        {
            try
            {
                const string strSql = "Select IdEstado from TBL_ModuloPedidos_Pedidos where IdPedido = @IdPedido";
                var result =  _sql.ExecuteScalar(strSql, CommandType.Text,
                                       new SqlParameter("@IdPedido", idPedido));
                
                return result==null ? string.Empty:  result.ToString();
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("EstadoPedido", ex);
            }
        }

        public bool VerificarSuministros(string sp)
        {
            try
            {
                var result = _sql.ExecuteScalar(sp, CommandType.StoredProcedure);

                return result == null ? false : Convert.ToBoolean(result);
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("VerificarSuministros", ex);
            }
        }

        public DataTable GetResumenPedido(string idPedido)
        {
            try
            {
                var result = _sql.ExecuteDataTable("GetResumenPedido", CommandType.StoredProcedure, new SqlParameter("@IdPedido",idPedido));

                return result;
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("GetResumenPedido", ex);
            }
        }

        public string RetornarNombreClienteByIdPedido(string idPedido)
        {
            try
            {
                const string strSql = " SELECT Cli.Cliente  FROM PEDIDOS_EMPACOR.dbo.COR_Clientes Cli "+
                                      " INNER JOIN TBL_ModuloPedidos_Pedidos Ped ON Cli.Codigo_Cliente = Ped.IdCliente "+
                                      " WHERE Ped.IdPedido = @IdPedido";

                var result = _sql.ExecuteScalar(strSql, CommandType.Text, new SqlParameter("@IdPedido", idPedido));
                if (result != null)
                return result.ToString();

            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("RetornarNombreClienteByIdPedido", ex);
            }
            return string.Empty;
        }
    }
}