using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Management;
using Application.MainModule.SqlServices.Domain;
using Application.MainModule.SqlServices.IServices;
using Domain.MainModule.Reclamos.DTO;
using Infraestructure.Data.Core;
using Infrastructure.CrossCutting.NetFramework.Enums;
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

        public void InsertUsuarioCopiaComentario(string idUsuario, string idComentario)
        {
            var sql = string.Format("insert into TBL_ModuloReclamos_UsuarioCopiaComentariosRespuesta(IdUsuario,IdComentario) values({0},{1})", idUsuario, idComentario);
            try
            {
                _sql.ExecuteNonquery(sql, CommandType.Text);
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("InsertUsuarioCopiaComentario", ex);
            }
        }

        public string EstadoDocumento(string id, ModulosAplicacion module)
        {
            var oreclamo = new Reclamos(_sql);
            return module == ModulosAplicacion.Reclamos ? oreclamo.EstadoReclamo(id) : oreclamo.EstadoAccionesPc(id);
        }

        public DataTable GetDocumentWorkFlowById(string id, ModulosAplicacion module)
        {
            var oreclamo = new Reclamos(_sql);

            return module == ModulosAplicacion.Reclamos ? oreclamo.GetReclamoWorkFlowById(id) : oreclamo.GetAccionesWorkFlowById(id);
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

        public DataTable Search_Unidad(string strPrefijo)
        {
            var dt = new DataTable();

            try
            {
                dt = _sql.ExecuteDataTable("Reclamos_GetUnidadesByPrefix", CommandType.StoredProcedure, new SqlParameter("@prefijo", strPrefijo));
                return dt;
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("Reclamos_GetUnidadesByPrefix", ex);
            }
        }

        public DataTable Search_Zona(string strPrefijo)
        {
            var dt = new DataTable();

            try
            {
                dt = _sql.ExecuteDataTable("Reclamos_GetZonasByPrefix", CommandType.StoredProcedure, new SqlParameter("@prefijo", strPrefijo));
                return dt;
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("Reclamos_GetZonasByPrefix", ex);
            }
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

        public DataTable GetVistaMisAlternativasPendientes(string serverHost, string moduleId, int idResponsable)
        {
            try
            {
                string strSql = "Vistas_MisAlternativasPendientes";
                var result = _sql.ExecuteDataTable(strSql, CommandType.StoredProcedure
                                       , new SqlParameter("@ServerHostPath", serverHost)
                                       , new SqlParameter("@ModuleId", moduleId)
                                       , new SqlParameter("@IdResponsable", idResponsable));

                return result;
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("Vistas_MisAlternativasPendientes", ex);
            }
        }

        public DataTable GetVistaMisActividadesPendientes(string serverHost, string moduleId, int idResponsable)
        {
            try
            {
                string strSql = "Vistas_MisActividadesPendientes";
                var result = _sql.ExecuteDataTable(strSql, CommandType.StoredProcedure
                                       , new SqlParameter("@ServerHostPath", serverHost)
                                       , new SqlParameter("@ModuleId", moduleId)
                                       , new SqlParameter("@IdResponsable", idResponsable));

                return result;
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("Vistas_MisActividadesPendientes", ex);
            }
        }

        public DataTable GetVistaMisAlternativas(string serverHost, string moduleId, int idUser, DateTime from, DateTime end, string noReclamo, string cliente, string producto, string servicio)
        {
            try
            {
                string strSql = "Vistas_MisAlternativas";
                var result = _sql.ExecuteDataTable(strSql, CommandType.StoredProcedure
                                       , new SqlParameter("@ServerHostPath", serverHost)
                                       , new SqlParameter("@ModuleId", moduleId)
                                       , new SqlParameter("@UsuarioCreacion", idUser)
                                       , new SqlParameter("@dateFrom", from)
                                       , new SqlParameter("@dateEnd", end)
                                       , new SqlParameter("@NoReclamo", noReclamo)
                                       , new SqlParameter("@Cliente", cliente)
                                       , new SqlParameter("@Producto", producto)
                                       , new SqlParameter("@Servicio", servicio));

                return result;
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("Vistas_MisAlternativas", ex);
            }
        }

        public DataTable GetVistaMisActividades(string serverHost, string moduleId, int idUser, DateTime from, DateTime end, string noReclamo, string cliente, string producto, string servicio)
        {
            try
            {
                string strSql = "Vistas_MisActividades";
                var result = _sql.ExecuteDataTable(strSql, CommandType.StoredProcedure
                                       , new SqlParameter("@ServerHostPath", serverHost)
                                       , new SqlParameter("@ModuleId", moduleId)
                                       , new SqlParameter("@UsuarioCreacion", idUser)
                                       , new SqlParameter("@dateFrom", from)
                                       , new SqlParameter("@dateEnd", end)
                                       , new SqlParameter("@NoReclamo", noReclamo)
                                       , new SqlParameter("@Cliente", cliente)
                                       , new SqlParameter("@Producto", producto)
                                       , new SqlParameter("@Servicio", servicio));

                return result;
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("Vistas_MisActividades", ex);
            }
        }

        public DataTable GetVistaGestionActividades(string serverHost, string moduleId, int idUser, DateTime from, DateTime end, string noReclamo, string cliente, string producto, string servicio, string fromView)
        {
            try
            {
                string strSql = "Vistas_GestionActividades";
                var result = _sql.ExecuteDataTable(strSql, CommandType.StoredProcedure
                                       , new SqlParameter("@ServerHostPath", serverHost)
                                       , new SqlParameter("@ModuleId", moduleId)
                                       , new SqlParameter("@UsuarioCreacion", idUser)
                                       , new SqlParameter("@dateFrom", from)
                                       , new SqlParameter("@dateEnd", end)
                                       , new SqlParameter("@NoReclamo", noReclamo)
                                       , new SqlParameter("@Cliente", cliente)
                                       , new SqlParameter("@Producto", producto)
                                       , new SqlParameter("@Servicio", servicio)
                                       , new SqlParameter("@FromView", fromView));

                return result;
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("Vistas_GestionActividades", ex);
            }
        }

        public DataTable GetVistaGestionAlternativas(string serverHost, string moduleId, int idUser, DateTime from, DateTime end, string noReclamo, string cliente, string producto, string servicio, string fromView)
        {
            try
            {
                string strSql = "Vistas_GestionAlternativas";
                var result = _sql.ExecuteDataTable(strSql, CommandType.StoredProcedure
                                       , new SqlParameter("@ServerHostPath", serverHost)
                                       , new SqlParameter("@ModuleId", moduleId)
                                       , new SqlParameter("@UsuarioCreacion", idUser)
                                       , new SqlParameter("@dateFrom", from)
                                       , new SqlParameter("@dateEnd", end)
                                       , new SqlParameter("@NoReclamo", noReclamo)
                                       , new SqlParameter("@Cliente", cliente)
                                       , new SqlParameter("@Producto", producto)
                                       , new SqlParameter("@Servicio", servicio)
                                       , new SqlParameter("@FromView", fromView));

                return result;
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("Vistas_GestionAlternativas", ex);
            }
        }

        public DataTable GetVistaGestionReclamos(string serverHost, string moduleId, int idUser, DateTime from, DateTime end, string noReclamo, string cliente, string producto, string servicio, string fromView)
        {
            try
            {
                string strSql = "Vistas_GestionReclamo";
                var result = _sql.ExecuteDataTable(strSql, CommandType.StoredProcedure
                                       , new SqlParameter("@ServerHostPath", serverHost)
                                       , new SqlParameter("@ModuleId", moduleId)
                                       , new SqlParameter("@UsuarioCreacion", idUser)
                                       , new SqlParameter("@dateFrom", from)
                                       , new SqlParameter("@dateEnd", end)
                                       , new SqlParameter("@NoReclamo", noReclamo)
                                       , new SqlParameter("@Cliente", cliente)
                                       , new SqlParameter("@Producto", producto)
                                       , new SqlParameter("@Servicio", servicio)
                                       , new SqlParameter("@FromView", fromView));

                return result;
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("Vistas_GestionReclamo", ex);
            }
        }

        public DataTable GetVistaTotalReclamos(string serverHost, string moduleId, int idUser, DateTime from, DateTime end, string noReclamo, string noRelacion, string cliente, string producto, string servicio, string fromView)
        {
            try
            {
                string strSql = "Vistas_TotalReclamos";
                var result = _sql.ExecuteDataTable(strSql, CommandType.StoredProcedure
                                       , new SqlParameter("@ServerHostPath", serverHost)
                                       , new SqlParameter("@ModuleId", moduleId)
                                       , new SqlParameter("@UsuarioCreacion", idUser)
                                       , new SqlParameter("@dateFrom", from)
                                       , new SqlParameter("@dateEnd", end)
                                       , new SqlParameter("@NoReclamo", noReclamo)
                                       , new SqlParameter("@NoRelacion", noRelacion)
                                       , new SqlParameter("@Cliente", cliente)
                                       , new SqlParameter("@Producto", producto)
                                       , new SqlParameter("@Servicio", servicio)
                                       , new SqlParameter("@FromView", fromView));

                return result;
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("Vistas_TotalReclamos", ex);
            }
        }


        public DataTable ListadoIngenierosResponsablesPorcategoría(string  idCategoria)
        {
            try
            {
                var strSQl = " SELECT "+
                             " Usu.IdUser "+
                             ",Usu.Nombres "+
                             " FROM "+
                             " TBL_Admin_Usuarios Usu  "+
                             " INNER JOIN TBL_ModuloReclamos_IngenierosResponsablesByCategoriaProducto Ing On Usu.IdUser = Ing.IdIngeniero "+
                             " WHERE Ing.IdCategoria = @IdCategoria";

                return _sql.ExecuteDataTable(strSQl, CommandType.Text, new SqlParameter("@IdCategoria", idCategoria));
            }
              catch (Exception ex)
            {
                throw new SqlExecutionException("ListadoIngenierosResponsablesPorcategoría", ex);
            }
        }

        #endregion


        
    }
}