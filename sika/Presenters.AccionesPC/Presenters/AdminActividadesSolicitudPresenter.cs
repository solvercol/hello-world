using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Application.Core;
using Application.MainModule.AccionesPC.IServices;
using Application.MainModule.SqlServices.IServices;
using Applications.MainModule.Admin.IServices;
using Domain.MainModule.AccionesPC.Enum;
using Domain.MainModules.Entities;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.AccionesPC.IViews;

namespace Presenters.AccionesPC.Presenters
{
    public class AdminActividadesSolicitudPresenter : Presenter<IAdminActividadesSolicitudView>
    {
        readonly ISfTBL_ModuloAPC_ActividadesManagementServices _actividadesService;
        readonly ISfTBL_ModuloAPC_SolicitudManagementServices _solicitudService;
        readonly ISfTBL_Admin_UsuariosManagementServices _usuariosService;
        readonly ISolicitudAdoService _solicitudAdoService;
        readonly ISfTBL_ModuloAPC_AnexosActividadesManagementServices _anexosService;

        public AdminActividadesSolicitudPresenter(ISfTBL_ModuloAPC_ActividadesManagementServices actividadesService,
                                                ISfTBL_ModuloAPC_SolicitudManagementServices solicitudService,
                                                ISfTBL_Admin_UsuariosManagementServices usuariosService,
                                                ISolicitudAdoService solicitudAdoService,
                                                ISfTBL_ModuloAPC_AnexosActividadesManagementServices anexosService)
        {
            _actividadesService = actividadesService;
            _solicitudService = solicitudService;
            _usuariosService = usuariosService;
            _solicitudAdoService = solicitudAdoService;
            _usuariosService = usuariosService;
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
            LoadSolicitud();
            LoadUsuarioSeguimiento();
            LoadUsuarioEjecucion();
            LoadActividadesSolicitud();
        }

        void LoadSolicitud()
        {
            if (string.IsNullOrEmpty(View.IdSolicitud)) return;

            try
            {
                var solicitud = _solicitudService.GetById(Convert.ToDecimal(View.IdSolicitud));

                if (solicitud != null)
                {
                    View.CanAddActividades = ((solicitud.IdEstado == 14) && solicitud.IdResponsableActual == View.UserSession.IdUser);

                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }


        void LoadActividadesSolicitud()
        {
            if (string.IsNullOrEmpty(View.IdSolicitud)) return;

            try
            {
                var items = _actividadesService.GetByIdSolicitud(Convert.ToDecimal(View.IdSolicitud));

                View.LoadActividadesSolicitud(items);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        void LoadUsuarioSeguimiento()
        {
            try
            {
                var items = _usuariosService.FindBySpec(true);
                View.LoadUsuarioSeguimiento(items);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        void LoadUsuarioEjecucion()
        {
            try
            {
                var items = _usuariosService.FindBySpec(true);
                View.LoadUsuarioEjecucion(items);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void AddActividadSolicitud()
        {
            if (string.IsNullOrEmpty(View.IdSolicitud)) return;

            try
            {
                var model = GetModel();

                _actividadesService.Add(model);

                if (View.ArchivosAdjuntos.Any())
                {
                    foreach (var archivo in View.ArchivosAdjuntos)
                    {
                        var anexo = new TBL_ModuloAPC_AnexosActividades();
                        anexo.IdActividad = model.IdActividad;
                        anexo.NombreArchivo = archivo.Value;
                        anexo.Archivo = (byte[])archivo.ComplexValue;
                        anexo.IsActive = true;
                        anexo.CreateBy = View.UserSession.IdUser;
                        anexo.CreateOn = DateTime.Now;
                        anexo.ModifiedBy = View.UserSession.IdUser;
                        anexo.ModifiedOn = DateTime.Now;

                        _anexosService.Add(anexo);
                    }
                }
                LoadActividadesSolicitud();

                try
                {
                   // _senMailServices.EnviarCorreoelectronicoActividades(model.IdActividad, View.UserSession); Pendiente Envio de Correo
                }
                catch (Exception ex)
                {
                    CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void UpdateActividadSolicitud()
        {
            if (string.IsNullOrEmpty(View.IdSolicitud) || string.IsNullOrEmpty(View.IdSelectedActividad)) return;

            try
            {
                var model = _actividadesService.GetById(Convert.ToDecimal(View.IdSelectedActividad));

                if (model != null)
                {
                    model.EstadoActividad = View.Estado;
                    model.ModifiedBy = View.UserSession.IdUser;
                    model.ModifiedOn = DateTime.Now;
                    _actividadesService.Modify(model);

                    LoadActividadesSolicitud();
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void LoadActividadSolicitud()
        {
            if (string.IsNullOrEmpty(View.IdSelectedActividad)) return;

            try
            {
                var model = _actividadesService.GetById(Convert.ToDecimal(View.IdSelectedActividad));

                if (model != null)
                {
                    View.Descripcion = model.Descripcion;
                    View.FechaActividad = model.FechaActividad;
                    View.IdUsuarioEjecucion = model.IdResponsableEjecucion.ToString();
                    View.IdUsuarioSeguimiento = model.IdResponsableSeguimiento.ToString();

                    View.ArchivosAdjuntos = new List<DTO_ValueKey>();
                    if (model.TBL_ModuloAPC_AnexosActividades.Any())
                    {
                        foreach (var anexo in model.TBL_ModuloAPC_AnexosActividades)
                        {
                            var archivo = new DTO_ValueKey();
                            archivo.Id = string.Format("{0}", anexo.IdAnexoActividad);
                            archivo.Value = anexo.NombreArchivo;
                            archivo.ComplexValue = anexo.Archivo;
                            View.ArchivosAdjuntos.Add(archivo);
                        }
                    }
                    View.LoadArchivosAdjuntos(View.ArchivosAdjuntos);
                    View.IsNewActividad = false;
                    View.ShowAdminActividadWindow(true);
                    View.EnableEdit(false);
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void RemoveActividadSolicitud()
        {
            if (string.IsNullOrEmpty(View.IdSelectedActividad)) return;

            try
            {
                var model = _actividadesService.GetById(Convert.ToDecimal(View.IdSelectedActividad));

                if (model != null)
                {
                    _actividadesService.Remove(model);

                    LoadActividadesSolicitud();
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        TBL_ModuloAPC_Actividades GetModel()
        {
            var model = new TBL_ModuloAPC_Actividades {  IdSolicitudAPC= Convert.ToInt32(View.IdSolicitud) };

          
            model.Descripcion = View.Descripcion;
            model.FechaActividad = View.FechaActividad;
            if (!string.IsNullOrEmpty(View.IdUsuarioEjecucion))
                model.IdResponsableEjecucion = Convert.ToInt32(View.IdUsuarioEjecucion);
            if (!string.IsNullOrEmpty(View.IdUsuarioSeguimiento))
                model.IdResponsableSeguimiento = Convert.ToInt32(View.IdUsuarioSeguimiento);
            model.EstadoActividad = "Programada";
            model.IsActive = true;
            model.CreateBy = View.UserSession.IdUser;
            model.CreateOn = DateTime.Now;
            model.ModifiedBy = View.UserSession.IdUser;
            model.ModifiedOn = DateTime.Now;

            return model;
        }

    }
}
