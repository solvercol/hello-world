﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Management;
using Application.MainModule.SqlServices.IServices;
using Infraestructure.Data.Core;

namespace Application.MainModule.SqlServices.Services
{
    public class SolicitudAdoService : ISolicitudAdoService
    {
        private readonly ISqlHelper _sql;

        public SolicitudAdoService(ISqlHelper sql)
        {
            _sql = sql;
        }
        public void InsertUsuarioCopiaComentario(string idUsuario, string idComentario)
        {
            var sql = string.Format("insert into TBL_ModuloAPC_UsuarioCopiaComentariosRespuesta(IdComentario,IdUsuario) values({0},{1})", idComentario,idUsuario);
            try
            {
                _sql.ExecuteNonquery(sql, CommandType.Text);
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("InsertUsuarioCopiaComentario", ex);
            }
        }


       

        public DataTable ListadoActividadesProgramadasPorSolicitudApc(string idSolicitud, string estado)
        {
            try
            {
                var srtSql = "ListadoActividadesprogramadasApc";
                return _sql.ExecuteDataTable(srtSql, CommandType.StoredProcedure, new SqlParameter("@IdSolicitud", idSolicitud), new SqlParameter("@Estado", estado));
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("ListadoActividadesProgramadasPorSolicitudApc", ex);
            }
        }

    }
}
