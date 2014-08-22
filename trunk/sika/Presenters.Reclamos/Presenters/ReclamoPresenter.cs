using System;
using System.Reflection;
using Application.Core;
using Application.MainModule.Reclamos.IServices;
using Applications.MainModule.Admin.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Reclamos.IViews;

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
                    View.NumeroReclamo = reclamo.NumeroReclamo;

                    // Load Nav
                    View.IdCategoriaReclamo = reclamo.IdCategoriaReclamo.ToString();
                    if (reclamo.TBL_ModuloReclamos_CategoriasReclamo != null)
                        View.IdGrupoInformacion = reclamo.TBL_ModuloReclamos_CategoriasReclamo.GrupoInformacion.ToString();

                    View.LoadInitReclamoControl();
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