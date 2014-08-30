using System;
using System.Linq;
using System.Reflection;
using Application.Core;
using Application.MainModule.Reclamos.IServices;
using Applications.MainModule.Admin.IServices;
using Domain.MainModules.Entities;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Reclamos.IViews;
using Application.MainModule.SqlServices.IServices;
using System.Collections.Generic;

namespace Presenters.Reclamos.Presenters
{
    public class AdminActividadesReclamoPresenter : Presenter<IAdminActividadesReclamoView>
    {
        readonly ISfTBL_ModuloReclamos_ActividadesManagementServices _actividadesService;
        readonly ISfTBL_ModuloReclamos_ActividadesReclamoManagementServices _actividadesReclamoAdmService;
        readonly ISfTBL_ModuloReclamos_ReclamoManagementServices _reclamoService;
        readonly ISfTBL_Admin_UsuariosManagementServices _usuariosService;
        readonly IReclamosAdoService _reclamoAdoService;
        readonly ISfTBL_ModuloReclamos_AnexosActividadManagementServices _anexosService;

        public AdminActividadesReclamoPresenter(ISfTBL_ModuloReclamos_ActividadesManagementServices actividadesService,
                                                ISfTBL_ModuloReclamos_ActividadesReclamoManagementServices actividadesReclamoAdmService,
                                                ISfTBL_ModuloReclamos_ReclamoManagementServices reclamoService,
                                                ISfTBL_Admin_UsuariosManagementServices usuariosService,
                                                IReclamosAdoService reclamoAdoService,
                                                ISfTBL_ModuloReclamos_AnexosActividadManagementServices anexosService)
        {
            _actividadesService = actividadesService;
            _actividadesReclamoAdmService = actividadesReclamoAdmService;
            _reclamoService = reclamoService;
            _usuariosService = usuariosService;
            _reclamoAdoService = reclamoAdoService;
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
            LoadActividadesReclamo();
            LoadActividadesReclamoAdmin();
            LoadUsuarioAsignacion();
            LoadUsuarioCopia();
        }

        void LoadActividadesReclamo()
        {
            if (string.IsNullOrEmpty(View.IdReclamo)) return;

            try
            {
                var items = _actividadesService.GetByIdReclamo(Convert.ToDecimal(View.IdReclamo));

                View.LoadActividadesReclamo(items);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        void LoadActividadesReclamoAdmin()
        {
            if (string.IsNullOrEmpty(View.IdReclamo)) return;

            try
            {
                var reclamo = _reclamoService.GetReclamoById(Convert.ToDecimal(View.IdReclamo));
                if (reclamo != null)
                {
                    var items = _actividadesReclamoAdmService.GetByTypoReclamo(reclamo.IdTipoReclamo);

                    View.LoadActividadesAdmin(items);
                }                
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        void LoadUsuarioAsignacion()
        {
            try
            {
                var items = _usuariosService.FindBySpec(true);
                View.LoadUsuarioAsignacion(items);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        void LoadUsuarioCopia()
        {
            try
            {
                var items = _usuariosService.FindBySpec(true);
                View.LoadUsuarioCopia(items);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void AddActividadReclamo()
        {
            if (string.IsNullOrEmpty(View.IdReclamo)) return;

            try
            {
                var model = GetModel();

                _actividadesService.Add(model);

                if (View.UsuariosCopia.Any())
                {
                    foreach (var item in View.UsuariosCopia)
                    {
                        _reclamoAdoService.InsertUsuarioCopiaActividades(item.Id, string.Format("{0}", model.IdActividad));
                    }
                }

                if (View.ArchivosAdjuntos.Any())
                {
                    foreach (var archivo in View.ArchivosAdjuntos)
                    {
                        var anexo = new TBL_ModuloReclamos_AnexosActividad();
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

                LoadActividadesReclamo();
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void UpdateActividadReclamo()
        {
            if (string.IsNullOrEmpty(View.IdReclamo) || string.IsNullOrEmpty(View.IdSelectedActividad)) return;

            try
            {
                var model = _actividadesService.GetById(Convert.ToDecimal(View.IdSelectedActividad));

                if (model != null)
                {
                    model.IdActividadReclamo = Convert.ToInt32(View.IdActividadReclamo);
                    model.Descripcion = View.Descripcion;
                    model.Fecha = View.FechaActividad;
                    model.IdUsuarioAsignacion = Convert.ToInt32(View.IdUsuarioAsignacion);
                    model.ObservacionesCierre = View.Observaciones;
                    model.ModifiedBy = View.UserSession.IdUser;
                    model.ModifiedOn = DateTime.Now;

                    _actividadesService.Modify(model);

                    LoadActividadesReclamo();
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void LoadActividadReclamo()
        {
            if (string.IsNullOrEmpty(View.IdSelectedActividad)) return;

            try
            {
                var model = _actividadesService.GetById(Convert.ToDecimal(View.IdSelectedActividad));

                if (model != null)
                {
                    View.IdActividadReclamo = model.IdActividadReclamo.ToString();
                    View.Descripcion = model.Descripcion;
                    View.FechaActividad = model.Fecha;
                    View.IdUsuarioAsignacion = model.IdUsuarioAsignacion.ToString();
                    View.Observaciones = model.ObservacionesCierre;
                    View.UsuariosCopia = new List<DTO_ValueKey>();
                    if (model.TBL_Admin_Usuarios3.Any())
                    {
                        foreach (var itm in model.TBL_Admin_Usuarios3)
                        {
                            var usuarioCopia = new DTO_ValueKey() { Id = itm.IdUser.ToString(), Value = itm.Nombres };
                            View.UsuariosCopia.Add(usuarioCopia);
                        }
                    }
                    View.LoadUsuariosCopia(View.UsuariosCopia);
                    View.ArchivosAdjuntos = new List<DTO_ValueKey>();
                    if (model.TBL_ModuloReclamos_AnexosActividad.Any())
                    {
                        foreach (var anexo in model.TBL_ModuloReclamos_AnexosActividad)
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

        public void RemoveActividadReclamo()
        {
            if (string.IsNullOrEmpty(View.IdSelectedActividad)) return;

            try
            {
                var model = _actividadesService.GetById(Convert.ToDecimal(View.IdSelectedActividad));

                if (model != null)
                {
                    _actividadesService.Remove(model);

                    LoadActividadesReclamo();
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        TBL_ModuloReclamos_Actividades GetModel()
        {
            var model = new TBL_ModuloReclamos_Actividades();

            model.IdReclamo = Convert.ToInt32(View.IdReclamo);
            model.IdActividadReclamo = Convert.ToInt32(View.IdActividadReclamo);
            model.Descripcion = View.Descripcion;
            model.Fecha = View.FechaActividad;
            model.IdUsuarioAsignacion = Convert.ToInt32(View.IdUsuarioAsignacion);
            model.ObservacionesCierre = View.Observaciones;
            model.Estado = "Registrada";
            model.IsActive = true;
            model.CreateBy = View.UserSession.IdUser;
            model.CreateOn = DateTime.Now;
            model.ModifiedBy = View.UserSession.IdUser;
            model.ModifiedOn = DateTime.Now;

            return model;
        }
    }
}