using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.MainModule.SqlServices.IServices;
using Infraestructure.Data.Core;
using System.Data;
using System.Web.Management;
using System.Data.SqlClient;

namespace Application.MainModule.SqlServices.Services
{
    public class SolicitudesAPCAdoService : ISolicitudesAPCAdoService
    {
        readonly ISqlHelper _sql;

        public SolicitudesAPCAdoService(ISqlHelper sql)
        {
            _sql = sql;
        }

        public DataTable  GetVistaSolicitudesMisPendientes(DateTime from, DateTime end, string serverHost, string moduleId,
                                                            string noSolicitud, string tipo, int area, string proceso, int idResponsable)
        {
            try
            {
                string strSql = "SolicitudesAPC_Vistas_MisPendientes";
                var result = _sql.ExecuteDataTable(strSql, CommandType.StoredProcedure
                                       , new SqlParameter("@dateFrom", from)
                                       , new SqlParameter("@dateEnd", end)
                                       , new SqlParameter("@ServerHostPath", serverHost)
                                       , new SqlParameter("@ModuleId", moduleId)
                                       , new SqlParameter("@NoAccion", noSolicitud)
                                       , new SqlParameter("@Tipo", tipo)
                                       , new SqlParameter("@Area", area)
                                       , new SqlParameter("@Proceso", proceso)
                                       , new SqlParameter("@IdResponsable", idResponsable));

                return result;
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("SolicitudesAPC_Vistas_MisPendientes", ex);
            }
        }
    }
}