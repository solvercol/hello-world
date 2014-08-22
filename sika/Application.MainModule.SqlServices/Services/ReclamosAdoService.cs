using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Management;
using Application.MainModule.SqlServices.IServices;
using Domain.MainModule.Reclamos.DTO;
using Infraestructure.Data.Core;

namespace Application.MainModule.SqlServices.Services
{
    public class ReclamosAdoService : IReclamosAdoService
    {
         private readonly ISqlHelper _sql;

        public ReclamosAdoService(ISqlHelper sql)
        {
            _sql = sql;
        }

        public List<Dto_Asesor> GetAllAsesores()
        {
            var items = new List<Dto_Asesor>();
            var dt = new DataTable();

            try
            {
                dt = _sql.ExecuteDataTable("Reclamos_GetAllAsesores", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("Reclamos_GetAllAsesores", ex);
            }

            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var item = new Dto_Asesor();

                    item.IdUser = string.IsNullOrEmpty(string.Format("{0}", dt.Rows[i]["IdUser"])) ? 0 : Convert.ToInt32(string.Format("{0}", dt.Rows[i]["IdUser"]));
                    item.Asesor = string.Format("{0}", dt.Rows[i]["Nombres"]);
                    item.IdUnidad = string.IsNullOrEmpty(string.Format("{0}", dt.Rows[i]["IdUnidad"])) ? 0 : Convert.ToInt32(string.Format("{0}", dt.Rows[i]["IdUnidad"]));
                    item.IdZona = string.IsNullOrEmpty(string.Format("{0}", dt.Rows[i]["IdZona"])) ? 0 : Convert.ToInt32(string.Format("{0}", dt.Rows[i]["IdZona"]));
                    item.Unidad = string.Format("{0}", dt.Rows[i]["Unidad"]);
                    item.Zona = string.Format("{0}", dt.Rows[i]["Zona"]);

                    items.Add(item);
                }
            }

            return items;
        }

        public Dto_Asesor GetByIdAsesor(int idAsesor)
        {
            var item = new Dto_Asesor();
            var dt = new DataTable();

            try
            {
                dt = _sql.ExecuteDataTable("Reclamos_GetAsesorByIdUsuario", CommandType.StoredProcedure, new SqlParameter("@IdAsesor", idAsesor));
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("Reclamos_GetAsesorByIdUsuario", ex);
            }

            if (dt != null && dt.Rows.Count > 0)
            {
                int i = 0;

                item.IdUser = string.IsNullOrEmpty(string.Format("{0}", dt.Rows[i]["IdUser"])) ? 0 : Convert.ToInt32(string.Format("{0}", dt.Rows[i]["IdUser"]));
                item.Asesor = string.Format("{0}", dt.Rows[i]["Nombres"]);
                item.IdUnidad = string.IsNullOrEmpty(string.Format("{0}", dt.Rows[i]["IdUnidad"])) ? 0 : Convert.ToInt32(string.Format("{0}", dt.Rows[i]["IdUnidad"]));
                item.IdZona = string.IsNullOrEmpty(string.Format("{0}", dt.Rows[i]["IdZona"])) ? 0 : Convert.ToInt32(string.Format("{0}", dt.Rows[i]["IdZona"]));
                item.Unidad = string.Format("{0}", dt.Rows[i]["Unidad"]);
                item.Zona = string.Format("{0}", dt.Rows[i]["Zona"]);
            }

            return item;
        }


        public void InsertUsuarioCopiaActividades(string idUsuario, string idActividad)
        {
            var sql = string.Format("insert into TBL_ModuloReclamos_UsuariosCopiaActividades(IdUsuario,IdActividad) values({0},{1})", idUsuario, idActividad);
            try
            {
                _sql.ExecuteNonquery(sql, CommandType.Text);
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("InsertUsuarioCopiaActividades", ex);
            }
        }
    }
}
