using System;
using Application.Core;
using Presenters.Reclamos.IViews;
using System.Reflection;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Application.MainModule.Reclamos.IServices;
using Applications.MainModule.Admin.IServices;

namespace Presenters.Reclamos.Presenters
{
    public class ReclamoPresenter : Presenter<IReclamoView>
    {
        readonly ISfTBL_ModuloReclamos_ReclamoManagementServices _reclamoService;
        private readonly ISfTBL_Admin_SeccionesManagementServices _seccionesServices;

        public ReclamoPresenter(ISfTBL_ModuloReclamos_ReclamoManagementServices reclamoService,
                                ISfTBL_Admin_SeccionesManagementServices seccionesServices)
        {
            _reclamoService = reclamoService;
            _seccionesServices = seccionesServices;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += View_Load;
        }

        void View_Load(object sender, EventArgs e)
        {
            if (View.IsPostBack) return;
            LoadReclamo();
            CargarSecciones();
        }

        void LoadReclamo()
        {
            try
            {
                if (string.IsNullOrEmpty(View.IdReclamo)) return;

                var reclamo = _reclamoService.GetReclamoWithNavById(Convert.ToDecimal(View.IdReclamo));

                if (reclamo != null)
                {
                    View.TipoReclamo = reclamo.TBL_ModuloReclamos_TipoReclamo.Nombre;
                    View.Solicitante = reclamo.Solicitante.Nombres;
                    View.NumeroReclamo = reclamo.NumeroReclamo;
                    View.DescripcionProblema = reclamo.DescripcionProblema;
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        private void CargarSecciones()
        {
            try
            {
                var secciones = _seccionesServices.ListadoSeccionesPorModulo(Convert.ToInt32(View.IdModule));
                View.LoadSecciones(secciones);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }
    }
}