using System;
using System.Reflection;
using Application.Core;
using Application.MainModule.Reclamos.IServices;
using Applications.MainModule.Admin.IServices;
using Domain.MainModules.Entities;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Reclamos.IViews;

namespace Presenters.Reclamos.Presenters
{
    public class AdminActividadesReclamoPresenter : Presenter<IAdminActividadesReclamoView>
    {
        readonly ISfTBL_ModuloReclamos_ActividadesManagementServices _actividadesService;
        readonly ISfTBL_ModuloReclamos_ActividadesReclamoManagementServices _actividadesReclamoAdmService;
        readonly ISfTBL_ModuloReclamos_ReclamoManagementServices _reclamoService;
        readonly ISfTBL_Admin_UsuariosManagementServices _usuariosService;

        public AdminActividadesReclamoPresenter(ISfTBL_ModuloReclamos_ActividadesManagementServices actividadesService,
                                                ISfTBL_ModuloReclamos_ActividadesReclamoManagementServices actividadesReclamoAdmService,
                                                ISfTBL_ModuloReclamos_ReclamoManagementServices reclamoService,
                                                ISfTBL_Admin_UsuariosManagementServices usuariosService)
        {
            _actividadesService = actividadesService;
            _actividadesReclamoAdmService = actividadesReclamoAdmService;
            _reclamoService = reclamoService;
            _usuariosService = usuariosService;
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

        public void AddActividadReclamo()
        {
            if (string.IsNullOrEmpty(View.IdReclamo)) return;

            try
            {
                var model = GetModel();

                _actividadesService.Add(model);

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
                    View.IsNewActividad = false;
                    View.ShowAdminActividadWindow(true);
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

            model.IdReclamo = Convert.ToDecimal(View.IdReclamo);
            model.IdActividadReclamo = Convert.ToInt32(View.IdActividadReclamo);
            model.Descripcion = View.Descripcion;
            model.Fecha = View.FechaActividad;
            model.IdUsuarioAsignacion = Convert.ToInt32(View.IdUsuarioAsignacion);
            model.ObservacionesCierre = View.Observaciones;
            model.Estado = "Creado";
            model.IsActive = true;
            model.CreateBy = View.UserSession.IdUser;
            model.CreateOn = DateTime.Now;
            model.ModifiedBy = View.UserSession.IdUser;
            model.ModifiedOn = DateTime.Now;

            return model;
        }
    }
}