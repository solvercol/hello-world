using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Management;
using Application.MainModule.SqlServices.Domain;
using Application.MainModule.SqlServices.IServices;
using Infraestructure.Data.Core;
using SqlHelper = Application.MainModule.SqlServices.Domain.SqlHelper;

namespace Application.MainModule.SqlServices.Services
{
    public class DocumentosAdoService : IDocumentosAdoService
    {
        private readonly ISqlHelper _sql;

        public DocumentosAdoService(ISqlHelper sql)
        {
            _sql = sql;
        }

        public DataTable GetVistaDocumentos(int idUser, int idEstado, string searchText, string fromview, string serverHost, string moduleId)
        {
            try
            {
                string strSql = "Vistas_Documentos_MisDocumentos";
                var result = _sql.ExecuteDataTable(strSql, CommandType.StoredProcedure
                                       , new SqlParameter("@IdUser", idUser)
                                       , new SqlParameter("@IdEstado", idEstado)
                                       , new SqlParameter("@SearchText", searchText)
                                       , new SqlParameter("@FromView", fromview)
                                       , new SqlParameter("@ServerHostPath", serverHost)
                                       , new SqlParameter("@ModuleId", moduleId));

                return result;
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("GetVistaDocumentos", ex);
            }
        }

        public DataTable GetUsuariosResponsables()
        {
            try
            {
                string strSql = "Documentos_GetUsuariosResponables";
                var result = _sql.ExecuteDataTable(strSql, CommandType.StoredProcedure);

                return result;
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("GetUsuariosResponsables", ex);
            }
        }
    }
}