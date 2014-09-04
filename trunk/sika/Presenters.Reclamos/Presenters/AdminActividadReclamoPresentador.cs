using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Application.Core;
using Application.MainModule.Reclamos.IServices;
using Domain.MainModule.Reclamos.DTO;
using Domain.MainModules.Entities;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Reclamos.IViews;

namespace Presenters.Reclamos.Presenters
{
    public class AdminActividadReclamoPresentador : Presenter<IAdminActividadReclamoView>
    {
        readonly ISfTBL_ModuloReclamos_ReclamoManagementServices _reclamoService;
        readonly ISfTBL_ModuloReclamos_ActividadesManagementServices _actividadesService;
        readonly ISfTBL_ModuloReclamos_AnexosActividadManagementServices _anexosService;

        public AdminActividadReclamoPresentador(ISfTBL_ModuloReclamos_ReclamoManagementServices reclamoService,
                                                ISfTBL_ModuloReclamos_ActividadesManagementServices actividadesService,
                                                ISfTBL_ModuloReclamos_AnexosActividadManagementServices anexosService)
        {
            _reclamoService = reclamoService;
            _actividadesService = actividadesService;
            _anexosService = anexosService;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += View_Load;
        }

        void View_Load(object sender, EventArgs e)
        {
            if (View.IsPostBack) return;
            LoadInitData();
        }

        public void LoadInitData()
        {
            LoadActividadReclamo();
        }

        public void LoadActividadReclamo()
        {
            if (string.IsNullOrEmpty(View.IdActividad)) return;

            try
            {
                var model = _actividadesService.GetById(Convert.ToDecimal(View.IdActividad));

                if (model != null)
                {
                    View.Descripcion = model.Descripcion;
                    View.Actividad = model.TBL_ModuloReclamos_ActividadesReclamo.Nombre;
                    View.FechaActividad = model.Fecha;
                    View.UsuarioAsignacion = model.TBL_Admin_Usuarios2.Nombres;
                    View.Estado = model.Estado;
                    View.LogCierre = string.Format("{0}", model.LogCierre);
                    if ((model.Estado != "Registrada" && model.Estado != "Programada"))
                    {
                        View.Observaciones = model.Estado == "Cancelada" ? model.ObservacionesCancelacion : model.ObservacionesCierre;
                        View.ShoeObservaciones(true);
                    }
                    else
                        View.ShoeObservaciones(false);
                    
                    var usuariosCopia = new List<DTO_ValueKey>();
                    if (model.TBL_Admin_Usuarios3.Any())
                    {
                        foreach (var itm in model.TBL_Admin_Usuarios3)
                        {
                            var usuarioCopia = new DTO_ValueKey() { Id = itm.IdUser.ToString(), Value = itm.Nombres };
                            usuariosCopia.Add(usuarioCopia);
                        }
                    }
                    View.LoadUsuariosCopia(usuariosCopia);
                    View.EnableEdit(false);
                    View.CanRegister = View.UserSession.IdUser == model.IdUsuarioAsignacion && (model.Estado == "Registrada" || model.Estado == "Programada");
                    LoadArhchivosAdjuntos();
                    LoadReclamo(model.IdReclamo);
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
                    View.IdReclamo = string.Format("{0}", idReclamo);

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
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void UpdateActividadReclamo()
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

                    LoadActividadReclamo();
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void MarcarRealizadaActividadReclamo()
        {
            if (string.IsNullOrEmpty(View.IdActividad)) return;

            try
            {
                var model = _actividadesService.GetById(Convert.ToDecimal(View.IdActividad));

                if (model != null)
                {
                    model.Estado = "Realizada";
                    model.ObservacionesCierre = View.ObservacionesCierre;
                    model.ModifiedBy = View.UserSession.IdUser;
                    model.ModifiedOn = DateTime.Now;
                    model.FechaCierre = DateTime.Now;
                    model.LogCierre = string.Format("La actividad ha sido realizada por {0}, en {1:dd/MM/yyyy}", View.UserSession.Nombres, DateTime.Now);

                    _actividadesService.Modify(model);

                    LoadActividadReclamo();
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
                    model.Estado = "Cancelada";
                    model.ObservacionesCancelacion = View.ObservacionesCancelacion;
                    model.ModifiedBy = View.UserSession.IdUser;
                    model.ModifiedOn = DateTime.Now;
                    model.FechaCierre = DateTime.Now;
                    model.LogCierre = string.Format("La actividad ha sido cancelada por {0}, en {1:dd/MM/yyyy}", View.UserSession.Nombres, DateTime.Now);

                    _actividadesService.Modify(model);

                    LoadActividadReclamo();
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
                var archivo = new TBL_ModuloReclamos_AnexosActividad();
                archivo.IdActividad = Convert.ToDecimal(View.IdActividad);
                archivo.NombreArchivo = View.NombreArchivoAdjunto;
                archivo.Archivo = View.ArchivoAdjunto;
                archivo.CreateBy = View.UserSession.IdUser;
                archivo.CreateOn = DateTime.Now;
                archivo.ModifiedBy = View.UserSession.IdUser;
                archivo.ModifiedOn = DateTime.Now;

                _anexosService.Add(archivo);

                LoadArhchivosAdjuntos();
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

                    LoadArhchivosAdjuntos();
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

        public void LoadArhchivosAdjuntos()
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