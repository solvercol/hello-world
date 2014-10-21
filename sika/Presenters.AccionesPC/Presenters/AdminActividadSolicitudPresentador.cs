using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Application.Core;
using Application.MainModule.AccionesPC.IServices;
using Domain.MainModules.Entities;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.AccionesPC.IViews;
using Application.MainModule.Reclamos.IServices;
using Domain.MainModule.Reclamos.DTO;

namespace Presenters.AccionesPC.Presenters
{
    public class AdminActividadSolicitudPresentador : Presenter<IAdminActividadSolicitudView>
    {
        readonly ISfTBL_ModuloAPC_SolicitudManagementServices _solicitudService; 
        readonly ISfTBL_ModuloAPC_ActividadesManagementServices _actividadesService;
        readonly ISfTBL_ModuloReclamos_ReclamoManagementServices _reclamoService;
        readonly ISfTBL_ModuloAPC_AnexosActividadesManagementServices _anexosService;

        public AdminActividadSolicitudPresentador(ISfTBL_ModuloAPC_SolicitudManagementServices solicitudService,
                                                ISfTBL_ModuloAPC_ActividadesManagementServices actividadesService,
                                                ISfTBL_ModuloReclamos_ReclamoManagementServices reclamoService,
                                                ISfTBL_ModuloAPC_AnexosActividadesManagementServices anexosService)
        {
            _solicitudService = solicitudService;
            _actividadesService = actividadesService;
            _reclamoService = reclamoService;
            _anexosService = anexosService;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += View_Load;
            View.CloseEvent += ViewCloseEvent;
            View.CancelEvent += ViewCancelEvent;
        }

        void View_Load(object sender, EventArgs e)
        {
            if (View.IsPostBack) return;
            LoadInitData();
        }

        public void LoadInitData()
        {
            LoadActividadSolicitud();
            LoadSolicitud();
        }

        void ViewCloseEvent(object sender, EventArgs e)
        {
            MarcarCerradaActividadReclamo();
        }

        void ViewCancelEvent(object sender, EventArgs e)
        {
            CancelarActividadReclamo();
        }

        public void LoadActividadSolicitud()
        {
            if (string.IsNullOrEmpty(View.IdSolicitud)) return;

            try
            {
                var model = _actividadesService.GetById(Convert.ToDecimal(View.IdActividad));

                if (model != null)
                {
                    View.Descripcion = model.Descripcion;
                    View.FechaActividad = model.FechaActividad;
                    View.UsuarioSeguimiento = model.TBL_Admin_Usuarios1.Nombres;
                    View.UsuarioEjecucion= model.TBL_Admin_Usuarios.Nombres;
                    View.Estado = model.EstadoActividad;

                    View.LogInfoMessage = string.Format("Creado por {0} en {1:dd/MM/yyyy hh:mm ss tt}. Modificado por {2} en {3:dd/MM/yyyy hh:mm ss tt}.",
                                                       model.TBL_Admin_Usuarios2.Nombres, model.CreateOn,
                                                       model.TBL_Admin_Usuarios3.Nombres, model.ModifiedOn);
                    if ((model.EstadoActividad != "Registrada" && model.EstadoActividad != "Programada"))
                    {
                        View.Observaciones = model.EstadoActividad == "Cancelada" ? model.ObservacionesCancelacion : model.ObservacionesCierre;
                        View.ShowObservaciones(true);
                    }
                    else
                        View.ShowObservaciones(false);
                    View.EnableEdit(false);
                    View.CanRegister = View.UserSession.IdUser == model.CreateBy && (model.EstadoActividad == "Registrada" || model.EstadoActividad == "Programada");
                    LoadArchivosAdjuntos();
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void LoadSolicitud()
        {
            if (string.IsNullOrEmpty(View.IdSolicitud)) return;

            try
            {
                var item = _solicitudService.GetWithNavById(Convert.ToDecimal(View.IdSolicitud));

                if (item != null)
                {
                    View.TipoAccion = item.TipoAccion;
                    View.CodSolicitud = item.Codigo;
                    View.Area = item.TBL_ModuloAPC_Areas.Nombre;
                    View.GerenteArea = item.TBL_Admin_Usuarios4.Nombres;
                    View.ResponsableAccion = item.TBL_Admin_Usuarios8.Nombres;
                    View.FechaInicio = string.Format("{0:dd/MM/yyyy}", item.FechaDesde);
                    View.FechaFinal = string.Format("{0:dd/MM/yyyy}", item.FechaHasta);


                    View.ShowInfoReclamo = false;

                    if (item.IdReclamoCreacion.HasValue)
                        LoadReclamo(item.IdReclamoCreacion.GetValueOrDefault());
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void UpdateActividadSolicitud()
        {
            if (string.IsNullOrEmpty(View.IdActividad)) return;

            try
            {
                var model = _actividadesService.GetById(Convert.ToDecimal(View.IdActividad));

                if (model != null)
                {
                    model.Descripcion = View.Descripcion;
                    model.ObservacionesCierre = View.Observaciones;
                    model.ModifiedBy = View.UserSession.IdUser;
                    model.ModifiedOn = DateTime.Now;

                    _actividadesService.Modify(model);

                    LoadActividadSolicitud();
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void MarcarCerradaActividadReclamo()
        {
            if (string.IsNullOrEmpty(View.IdActividad)) return;

            try
            {
                var model = _actividadesService.GetById(Convert.ToDecimal(View.IdActividad));

                if (model != null)
                {
                    model.EstadoActividad = "Realizada";
                    model.ObservacionesCierre = View.ObservacionesCierre;
                    model.ModifiedBy = View.UserSession.IdUser;
                    model.ModifiedOn = DateTime.Now;
                    _actividadesService.Modify(model);

                    LoadActividadSolicitud();
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void CancelarActividadReclamo()
        {
            if (string.IsNullOrEmpty(View.IdActividad)) return;

            try
            {
                var model = _actividadesService.GetById(Convert.ToDecimal(View.IdActividad));

                if (model != null)
                {
                    model.EstadoActividad = "Cancelada";
                    model.ObservacionesCancelacion = View.ObservacionesCancelacion;
                    model.ModifiedBy = View.UserSession.IdUser;
                    model.ModifiedOn = DateTime.Now;
                    _actividadesService.Modify(model);

                    LoadActividadSolicitud();
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        void LoadReclamo(decimal idReclamo)
        {
            try
            {
                var reclamo = _reclamoService.GetReclamoWithNavById(idReclamo);

                if (reclamo != null)
                {
                    var dtoProducto = (Dto_Producto)reclamo.DtoProducto;
                    var dtoCliente = (Dto_Cliente)reclamo.DtoCliente;

                    View.TipoReclamo = reclamo.TBL_ModuloReclamos_TipoReclamo.Nombre;
                    View.NumeroReclamo = reclamo.NumeroReclamo;

                    if (View.TipoReclamo == "Producto")
                    {
                        View.TitleReclamo = dtoProducto.NombreProducto;
                        View.TitleReclamoFrom = dtoCliente.NombreCliente;
                        View.Unidad = reclamo.UnidadZona;
                        View.FechaReclamo = string.Format("{0:dd/MM/yyyy}", reclamo.CreateOn);
                        View.Asesor = reclamo.AsesoradoPor.Nombres;
                    }
                    else
                    {
                        View.TitleReclamo = reclamo.TBL_ModuloReclamos_CategoriasReclamo.Nombre;
                        if (dtoCliente != null && !string.IsNullOrEmpty(dtoCliente.NombreCliente))
                        {
                            View.TitleReclamoFrom = dtoCliente.NombreCliente;
                        }
                        else
                        {
                            View.TitleReclamoFrom = string.Format("{0} / {1}", reclamo.NombreReclama, reclamo.ProcedimientoInternoAfectado);
                        }
                        View.Unidad = reclamo.UnidadZona;
                        View.FechaReclamo = string.Format("{0:dd/MM/yyyy}", reclamo.CreateOn);
                        if (reclamo.AsesoradoPor != null)
                            View.Asesor = reclamo.AsesoradoPor.Nombres;
                    }

                    View.ShowInfoReclamo = true;
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void AddArchivoAdjunto()
        {
            try
            {
                var archivo = new TBL_ModuloAPC_AnexosActividades();
                archivo.IdActividad = Convert.ToDecimal(View.IdActividad);
                archivo.NombreArchivo = View.NombreArchivoAdjunto;
                archivo.Archivo = View.ArchivoAdjunto;
                archivo.CreateBy = View.UserSession.IdUser;
                archivo.CreateOn = DateTime.Now;
                archivo.ModifiedBy = View.UserSession.IdUser;
                archivo.ModifiedOn = DateTime.Now;

                _anexosService.Add(archivo);

                LoadArchivosAdjuntos();
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void RemoveArchivoAdjunto(decimal idArchivo)
        {
            try
            {
                var model = _anexosService.GetById(idArchivo);

                if (model != null)
                {
                    _anexosService.Remove(model);
                    LoadArchivosAdjuntos();
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void DownloadArchivoAdjunto(decimal idArchivo)
        {
            try
            {
                var model = _anexosService.GetById(idArchivo);

                if (model != null)
                {
                    var archivo = new DTO_ValueKey();
                    archivo.ComplexValue = model.Archivo;
                    archivo.Id = string.Format("{0}", model.IdAnexoActividad);
                    archivo.Value = model.NombreArchivo;

                    View.DescargarArchivo(archivo);
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void LoadArchivosAdjuntos()
        {
            if (string.IsNullOrEmpty(View.IdActividad)) return;

            try
            {
                var anexos = _anexosService.GetByIdActividad(Convert.ToDecimal(View.IdActividad));
                var archivosAdjuntos = new List<DTO_ValueKey>();
                if (anexos.Any())
                {
                    foreach (var anexo in anexos)
                    {
                        var archivo = new DTO_ValueKey();
                        archivo.Id = string.Format("{0}", anexo.IdAnexoActividad);
                        archivo.Value = anexo.NombreArchivo;
                        archivo.ComplexValue = anexo.Archivo;

                        archivosAdjuntos.Add(archivo);
                    }
                }
                View.LoadArchivosAdjuntos(archivosAdjuntos);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }
    }
}
