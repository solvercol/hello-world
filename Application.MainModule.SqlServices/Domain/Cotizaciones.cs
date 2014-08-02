using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Management;
using Infraestructure.Data.Core;

namespace Application.MainModule.SqlServices.Domain
{
    public class Cotizaciones
    {
        private readonly ISqlHelper _sql;

        public Cotizaciones(ISqlHelper sql)
        {
            _sql = sql;
        }

        public DataTable ListadoCotizaciones(int pagina, int registrosPorPagina, string codigoCliente, string descripcion)
        {
            try
            {
                return _sql.ExecuteDataTable("ListadoCotizaciones", CommandType.StoredProcedure,
                                      new SqlParameter("@Pagina", pagina),
                                      new SqlParameter("@RegistrosporPagina", registrosPorPagina),
                                      new SqlParameter("@Codigo", string.IsNullOrEmpty(codigoCliente) ? null : codigoCliente),
                                      new SqlParameter("@Descripcion", string.IsNullOrEmpty(descripcion) ? null : descripcion));
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("ListadoCotizaciones", ex);
            }
        }

        public int CountCotizaciones(string codigoCliente, string descripcion)
        {
            try
            {
                var result = _sql.ExecuteScalar("CountCotizaciones", CommandType.StoredProcedure,
                    new SqlParameter("@Codigo", string.IsNullOrEmpty(codigoCliente) ? null : codigoCliente),
                    new SqlParameter("@Descripcion", string.IsNullOrEmpty(descripcion) ? null : descripcion));

                if (result != null)
                    return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("CountCotizaciones", ex);
            }

            return 0;
        }


        public DataTable ObtenerCotizacionByNoCotizacion(string numeroCotizacion)
        {
            try
            {
                return _sql.ExecuteDataTable("GetCotizacionbyNumeroCotizacion", CommandType.StoredProcedure,
                                      new SqlParameter("@No_Cotizacion", string.IsNullOrEmpty(numeroCotizacion) ? null : numeroCotizacion));
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("ObtenerCotizacionByNoCotizacion", ex);
            }
        }

        public DataTable ObtenerListadoMensajesPornumeroCotizacion(string numeroCotizacion)
        {
            try
            {
                return _sql.ExecuteDataTable("ListadoMensajesporNumeroCotizacion", CommandType.StoredProcedure,
                                      new SqlParameter("@No_Cotizacion", numeroCotizacion));
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("ObtenerCotizacionByNoCotizacion", ex);
            }
        }


    }
}