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
    public class AdminAlternativasReclamoPresenter : Presenter<IAdminAlternativasReclamoView>
    {
        readonly ISfTBL_ModuloReclamos_AlternativasManagementServices _alternativaReclamoService;
        readonly ISfTBL_Admin_UsuariosManagementServices _usuariosService;

        public AdminAlternativasReclamoPresenter(ISfTBL_ModuloReclamos_AlternativasManagementServices alternativaReclamoService,
                                                 ISfTBL_Admin_UsuariosManagementServices usuariosService)
        {
            _alternativaReclamoService = alternativaReclamoService;
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
            LoadAlternativasReclamo();
            LoadResponsables();
        }

        void LoadAlternativasReclamo()
        {
            if (string.IsNullOrEmpty(View.IdReclamo)) return;

            try
            {
                var items = _alternativaReclamoService.GetByIdReclamo(Convert.ToDecimal(View.IdReclamo));

                View.LoadAlternativasReclamo(items);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        void LoadResponsables()
        {
            try
            {
                var items = _usuariosService.FindBySpec(true);
                View.LoadResponsables(items);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void AddAlternativaReclamo()
        {
            if (string.IsNullOrEmpty(View.IdReclamo)) return;

            try
            {
                var model = GetModel();

                _alternativaReclamoService.Add(model);

                LoadAlternativasReclamo();
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void UpdateAlternativaReclamo()
        {
            if (string.IsNullOrEmpty(View.IdReclamo) || string.IsNullOrEmpty(View.IdSelectedAlternativa)) return;

            try
            {
                var model = _alternativaReclamoService.GetById(Convert.ToDecimal(View.IdSelectedAlternativa));

                if (model != null)
                {
                    model.Causas = View.Causas;
                    model.Factores = View.Factores;
                    model.Alternativa = View.Alternativa;
                    model.IdResponsable = Convert.ToInt32(View.IdResponsable);
                    model.FechaAlternativa = View.FechaAlternativa;
                    model.Seguimiento = View.Seguimiento;
                    model.ModifiedBy = View.UserSession.IdUser;
                    model.ModifiedOn = DateTime.Now;

                    _alternativaReclamoService.Modify(model);

                    LoadAlternativasReclamo();
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void LoadAlternativaReclamo()
        {
            if (string.IsNullOrEmpty(View.IdSelectedAlternativa)) return;

            try
            {
                var model = _alternativaReclamoService.GetById(Convert.ToDecimal(View.IdSelectedAlternativa));

                if (model != null)
                {
                    View.Causas = model.Causas;
                    View.Factores = model.Factores;
                    View.Alternativa = model.Alternativa;
                    View.IdResponsable = model.IdResponsable.ToString();
                    View.FechaAlternativa = model.FechaAlternativa;
                    View.Seguimiento = model.Seguimiento;
                    View.IsNewAlternativa = false;
                    View.ShowAdminAlternativaWindow(true);
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void RemoveAlternativaReclamo()
        {
            if (string.IsNullOrEmpty(View.IdSelectedAlternativa)) return;

            try
            {
                var model = _alternativaReclamoService.GetById(Convert.ToDecimal(View.IdSelectedAlternativa));

                if (model != null)
                {
                    _alternativaReclamoService.Remove(model);

                    LoadAlternativasReclamo();
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        TBL_ModuloReclamos_Alternativas GetModel()
        {
            var model = new TBL_ModuloReclamos_Alternativas();

            model.IdReclamo = Convert.ToDecimal(View.IdReclamo);            
            model.Causas = View.Causas;
            model.Factores = View.Factores;
            model.Alternativa = View.Alternativa;
            model.IdResponsable = Convert.ToInt32(View.IdResponsable);
            model.FechaAlternativa = View.FechaAlternativa;
            model.Seguimiento = View.Seguimiento;
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