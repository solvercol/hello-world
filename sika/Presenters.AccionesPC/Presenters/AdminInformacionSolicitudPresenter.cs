using System;
using System.Linq;
using System.Reflection;
using Application.Core;
using Application.MainModule.AccionesPC.IServices;
using Applications.MainModule.Admin.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.AccionesPC.IViews;

namespace Presenters.AccionesPC.Presenters
{
    public class AdminInformacionSolicitudPresenter : Presenter<IAdminInformacionSolicitudView>
    {
        readonly ISfTBL_ModuloAPC_SolicitudManagementServices _solicitudService;

        public AdminInformacionSolicitudPresenter(ISfTBL_ModuloAPC_SolicitudManagementServices solicitudService)
        {
            _solicitudService = solicitudService;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += ViewLoad;
            View.FilterEvent += ViewFilterEvent;
        }

        void ViewFilterEvent(object sender, EventArgs e)
        {
            LoadSolicitud();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ViewLoad(object sender, EventArgs e)
        {
            if (View.IsPostBack) return;
            LoadSolicitud();
        }

        public void LoadInitData()
        {
            LoadSolicitud();
        }

        /// <summary>
        /// 
        /// </summary>
        public void LoadSolicitud()
        {
            if (string.IsNullOrEmpty(View.IdSolicitud)) return;

            try
            {
                var item = _solicitudService.GetById(Convert.ToDecimal(View.IdSolicitud));

                if (item != null)
                {
                    View.ProcesoAsociado = item.Proceso;
                    View.DescripcionAccion = item.DescripcionAccion;
                    View.Observaciones = item.Observaciones;
                    View.ResultadoCierre = item.Resultado;
                    View.ObservacionesCierre = item.ObservacionesCierre;
                    View.ConformidadEliminada = item.Cerrada;

                    // Verificando info de cierre
                    var status = _solicitudService.ReturnStatusBySolicitudId(Convert.ToDecimal(View.IdSolicitud));
                    View.ShowInfoCierre = status == 16;
                    var listadoReclamos = item.TBL_ModuloReclamos_Reclamo1.Aggregate(string.Empty, (current, reclamo) => current + string.Format("{0} - ", reclamo.NumeroReclamo));
                    if (!string.IsNullOrEmpty(listadoReclamos))
                        View.ReclamosRelacionados = listadoReclamos.Substring(0, listadoReclamos.TrimEnd().Length - 1);
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }
    }
}