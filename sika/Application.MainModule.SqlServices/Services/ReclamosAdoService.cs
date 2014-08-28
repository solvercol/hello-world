using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Management;
using Application.MainModule.SqlServices.Domain;
using Application.MainModule.SqlServices.IServices;
using Domain.MainModule.Reclamos.DTO;
using Infraestructure.Data.Core;
using SqlHelper = Application.MainModule.SqlServices.Domain.SqlHelper;

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

        public string EstadoReclamo(string idreclamo)
        {
            var oreclamo = new Reclamos(_sql);
            return oreclamo.EstadoReclamo(idreclamo);
        }

        public DataTable GetReclamoWorkFlowById(string idReclamo)
        {
            var oreclamo = new Reclamos(_sql);
            return oreclamo.GetReclamoWorkFlowById(idReclamo);
        }

        public string EjecutarSpToBool(string spName, Dictionary<string ,string > parametros)
        {
            var help = new SqlHelper(_sql);
            return help.EjecutarScalarSp(spName, parametros);
        }

        public  DataTable ResumenReclamosPanelWorkFlow(string idReclamo)
        {
            var oreclamo = new Reclamos(_sql);
            return oreclamo.ResumenReclamosPanelWorkFlow(idReclamo);
        }

        #region Vistas Y Reportes

        public DataTable GetVistaGeneralReclamos(DateTime from, DateTime end, string serverHost, string moduleId)
        {
            try
            {
                string strSql = "Vistas_VistaGeneralReclamos";
                var result = _sql.ExecuteDataTable(strSql, CommandType.StoredProcedure
                                       , new SqlParameter("@dateFrom", from)
                                       , new SqlParameter("@dateEnd", end)
                                       , new SqlParameter("@ServerHostPath", serverHost)
                                       , new SqlParameter("@ModuleId", moduleId));

                return result;
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("Vistas_VistaGeneralReclamos", ex);
            }
        }

        public DataTable GetVistaReclamosMisPendientes(DateTime from, DateTime end, string serverHost, string moduleId, int idResponsable, string noReclamo, string cliente, string producto, string servicio)
        {
            try
            {
                string strSql = "Vistas_MisPendientes";
                var result = _sql.ExecuteDataTable(strSql, CommandType.StoredProcedure
                                       , new SqlParameter("@dateFrom", from)
                                       , new SqlParameter("@dateEnd", end)
                                       , new SqlParameter("@ServerHostPath", serverHost)
                                       , new SqlParameter("@ModuleId", moduleId)
                                       , new SqlParameter("@IdResponsable", idResponsable)
                                       , new SqlParameter("@NoReclamo", noReclamo)
                                       , new SqlParameter("@Cliente", cliente)
                                       , new SqlParameter("@Producto", producto)
                                       , new SqlParameter("@Servicio", servicio));

                return result;
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("Vistas_MisPendientes", ex);
            }
        }

        public DataTable GetVistaMisReclamosPorFecha(DateTime from, DateTime end, string serverHost, string moduleId, int idCreador, string noReclamo, string cliente, string producto, string servicio)
        {
            try
            {
                string strSql = "Vistas_MisReclamosPorFecha";
                var result = _sql.ExecuteDataTable(strSql, CommandType.StoredProcedure
                                       , new SqlParameter("@dateFrom", from)
                                       , new SqlParameter("@dateEnd", end)
                                       , new SqlParameter("@ServerHostPath", serverHost)
                                       , new SqlParameter("@ModuleId", moduleId)
                                       , new SqlParameter("@IdCreador", idCreador)
                                       , new SqlParameter("@NoReclamo", noReclamo)
                                       , new SqlParameter("@Cliente", cliente)
                                       , new SqlParameter("@Producto", producto)
                                       , new SqlParameter("@Servicio", servicio));

                return result;
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("Vistas_MisReclamosPorFecha", ex);
            }
        }

        public DataTable GetVistaMisReclamosPorEstado(DateTime from, DateTime end, string serverHost, string moduleId, int idCreador, string noReclamo, string cliente, string producto, string servicio)
        {
            try
            {
                string strSql = "Vistas_MisReclamosPorEstado";
                var result = _sql.ExecuteDataTable(strSql, CommandType.StoredProcedure
                                       , new SqlParameter("@dateFrom", from)
                                       , new SqlParameter("@dateEnd", end)
                                       , new SqlParameter("@ServerHostPath", serverHost)
                                       , new SqlParameter("@ModuleId", moduleId)
                                       , new SqlParameter("@IdCreador", idCreador)
                                       , new SqlParameter("@NoReclamo", noReclamo)
                                       , new SqlParameter("@Cliente", cliente)
                                       , new SqlParameter("@Producto", producto)
                                       , new SqlParameter("@Servicio", servicio));

                return result;
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("Vistas_MisReclamosPorEstado", ex);
            }
        }

        public DataTable GetVistaReclamosPorTipo(DateTime from, DateTime end, string serverHost, string moduleId, string noReclamo, string cliente, string producto, string servicio)
        {
            try
            {
                string strSql = "Vistas_ReclamosPorTipo";
                var result = _sql.ExecuteDataTable(strSql, CommandType.StoredProcedure
                                       , new SqlParameter("@dateFrom", from)
                                       , new SqlParameter("@dateEnd", end)
                                       , new SqlParameter("@ServerHostPath", serverHost)
                                       , new SqlParameter("@ModuleId", moduleId)
                                       , new SqlParameter("@NoReclamo", noReclamo)
                                       , new SqlParameter("@Cliente", cliente)
                                       , new SqlParameter("@Producto", producto)
                                       , new SqlParameter("@Servicio", servicio));

                return result;
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("Vistas_ReclamosPorTipo", ex);
            }
        }

        public DataTable GetVistaReclamosPorEstado(DateTime from, DateTime end, string serverHost, string moduleId, string noReclamo, string cliente, string producto, string servicio)
        {
            try
            {
                string strSql = "Vistas_ReclamosPorEstado";
                var result = _sql.ExecuteDataTable(strSql, CommandType.StoredProcedure
                                       , new SqlParameter("@dateFrom", from)
                                       , new SqlParameter("@dateEnd", end)
                                       , new SqlParameter("@ServerHostPath", serverHost)
                                       , new SqlParameter("@ModuleId", moduleId)
                                       , new SqlParameter("@NoReclamo", noReclamo)
                                       , new SqlParameter("@Cliente", cliente)
                                       , new SqlParameter("@Producto", producto)
                                       , new SqlParameter("@Servicio", servicio));

                return result;
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("Vistas_ReclamosPorEstado", ex);
            }
        }

        public DataTable GetVistaReclamosPorNumero(DateTime from, DateTime end, string serverHost, string moduleId, string noReclamo, string cliente, string producto, string servicio)
        {
            try
            {
                string strSql = "Vistas_ReclamosPorNumero";
                var result = _sql.ExecuteDataTable(strSql, CommandType.StoredProcedure
                                       , new SqlParameter("@dateFrom", from)
                                       , new SqlParameter("@dateEnd", end)
                                       , new SqlParameter("@ServerHostPath", serverHost)
                                       , new SqlParameter("@ModuleId", moduleId)
                                       , new SqlParameter("@NoReclamo", noReclamo)
                                       , new SqlParameter("@Cliente", cliente)
                                       , new SqlParameter("@Producto", producto)
                                       , new SqlParameter("@Servicio", servicio));

                return result;
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("Vistas_ReclamosPorNumero", ex);
            }
        }

        public DataTable GetVistaReclamosPorTargetMarket(DateTime from, DateTime end, string serverHost, string moduleId, string noReclamo, string cliente, string producto)
        {
            try
            {
                string strSql = "Vistas_ReclamosPorTargetMarket";
                var result = _sql.ExecuteDataTable(strSql, CommandType.StoredProcedure
                                       , new SqlParameter("@dateFrom", from)
                                       , new SqlParameter("@dateEnd", end)
                                       , new SqlParameter("@ServerHostPath", serverHost)
                                       , new SqlParameter("@ModuleId", moduleId)
                                       , new SqlParameter("@NoReclamo", noReclamo)
                                       , new SqlParameter("@Cliente", cliente)
                                       , new SqlParameter("@Producto", producto));

                return result;
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("Vistas_ReclamosPorTargetMarket", ex);
            }
        }

        #endregion
    }
}
