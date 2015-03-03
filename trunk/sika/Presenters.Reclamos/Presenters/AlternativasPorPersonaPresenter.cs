using System;
using System.Reflection;
using Application.Core;
using Application.MainModule.SqlServices.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Reclamos.IViews;
using Applications.MainModule.Admin.IServices;

namespace Presenters.Reclamos.Presenters
{
    public class AlternativasPorPersonaPresenter : Presenter<IAlternativasPorPersonaView>
    {
        readonly IReclamosAdoService _recladoAdoService;
        readonly ISfTBL_Admin_OptionListManagementServices _optionListService;

        public AlternativasPorPersonaPresenter(IReclamosAdoService recladoAdoService, ISfTBL_Admin_OptionListManagementServices optionListService)
        {
            _recladoAdoService = recladoAdoService;
            _optionListService = optionListService;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += View_Load;
        }

        void View_Load(object sender, EventArgs e)
        {
            if (View.IsPostBack) return;
            LoadInitView();
            LoadInitDate();
            LoadReport();
        }

        #region Methods

        void LoadInitView()
        {            
            View.FechaFilterTo = new DateTime(DateTime.Now.Year, 12, 31);
        }

        void LoadInitDate()
        {
            try
            {
                var op = _optionListService.ObtenerOpcionBykeyModuleId("FechaInitVistas", Convert.ToInt32(View.IdModule));

                if (op != null)
                {
                    View.FechaFilterFrom = Convert.ToDateTime(op.Value);
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void LoadReport()
        {
            try
            {
                var dt = _recladoAdoService.GetVistaGestionAlternativas(View.ServerHostPath, View.IdModule, View.UserSession.IdUser
                                                                       , View.FechaFilterFrom, View.FechaFilterTo, View.FilterNoReclamo
                                                                       , View.FilterCliente, View.FilterProducto, View.FilterServicio,"altpersona");
                View.LoadView(dt);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        #endregion
    }
}