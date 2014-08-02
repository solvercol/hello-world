using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Management;
using Infraestructure.Data.Core;

namespace Application.MainModule.SqlServices.Domain
{
    public class Partes
    {
        private readonly ISqlHelper _sql;

        public Partes(ISqlHelper sql)
        {
            _sql = sql;
        }

        public DataTable ListadoPartesPorNumeroCotizacion(string numeroCotizacion)
        {
            try
            {
                return _sql.ExecuteDataTable("ListadoPartesPorNumeroCotizacion", CommandType.StoredProcedure,
                                      new SqlParameter("@No_Cotizacion", numeroCotizacion));
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("ListadoPartesPorNumeroCotizacion", ex);
            }
        }
    }
}