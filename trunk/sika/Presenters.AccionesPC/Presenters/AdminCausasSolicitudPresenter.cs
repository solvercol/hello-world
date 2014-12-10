using System;
using System.Reflection;
using Application.Core;
using Application.MainModule.AccionesPC.IServices;
using Application.MainModule.SqlServices.IServices;
using Applications.MainModule.Admin.IServices;
using Domain.MainModules.Entities;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.AccionesPC.IViews;

namespace Presenters.AccionesPC.Presenters
{
    public class AdminCausasSolicitudPresenter : Presenter<IAdminCausasSolicitudView>
    {
        readonly ISfTBL_ModuloAPC_CausasManagementServices _causasService;
        readonly ISfTBL_ModuloAPC_SolicitudManagementServices _solicitudService;

        public AdminCausasSolicitudPresenter(ISfTBL_ModuloAPC_CausasManagementServices causasService,
                                                ISfTBL_ModuloAPC_SolicitudManagementServices solicitudService,
                                                ISfTBL_Admin_UsuariosManagementServices usuariosService,
                                                ISolicitudAdoService solicitudAdoService,
                                                ISfTBL_ModuloAPC_AnexosActividadesManagementServices anexosService)
        {
            _causasService = causasService;
            _solicitudService = solicitudService;
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
            LoadCausasSolicitud();
        }

        void LoadSolicitud()
        {
            if (string.IsNullOrEmpty(View.IdSolicitud)) return;

            try
            {
                var solicitud = _solicitudService.GetById(Convert.ToDecimal(View.IdSolicitud));

                if (solicitud != null)
                {
                    View.CanAddCausas = ((solicitud.IdEstado == 14) && solicitud.IdResponsableActual == View.UserSession.IdUser);

                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void LoadCausa()
        {
            try
            {
                var causa = _causasService.GetById(View.SelectedId);

                View.Descripcion = causa.Descripcion;
                View.Comentarios = causa.Comentarios;

                View.IsNew = false;

                View.ShowAdminCausaWindow(true);

            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        void LoadCausasSolicitud()
        {
            if (string.IsNullOrEmpty(View.IdSolicitud)) return;

            try
            {
                var items = _causasService.GetByIdSolicitud(Convert.ToDecimal(View.IdSolicitud));

                View.LoadCausasSolicitud(items);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void AddCausaSolicitud()
        {
            if (string.IsNullOrEmpty(View.IdSolicitud)) return;

            try
            {
                var model = GetModel();

                _causasService.Add(model);
              
                LoadCausasSolicitud();
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void UpdateCausa()
        {
            try
            {
                var causa = _causasService.GetById(View.SelectedId);

                causa.Descripcion = View.Descripcion;
                causa.Comentarios = View.Comentarios;
                causa.ModifiedBy = View.UserSession.IdUser;
                causa.ModifiedOn = DateTime.Now;

                _causasService.Modify(causa);

                LoadCausasSolicitud();
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }
        
        TBL_ModuloAPC_Causas GetModel()
        {
            var model = new TBL_ModuloAPC_Causas {  IdSolicitudAPC= Convert.ToInt32(View.IdSolicitud) };
          
            model.Descripcion = View.Descripcion;
            model.Comentarios = View.Comentarios;
            model.IsActive = true;
            model.CreateBy = View.UserSession.IdUser;
            model.CreateOn = DateTime.Now;
            model.ModifiedBy = View.UserSession.IdUser;
            model.ModifiedOn = DateTime.Now;

            return model;
        }

    }
}