using System;
using Application.Core;
using Application.MainModule.SqlServices.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Reclamos.IViews;

namespace Presenters.Reclamos.Presenters
{
    public class PanelEstadosPresenter : Presenter<IPanelEstadosView>
    {
        private readonly IReclamosAdoService _sqlReclamosServices;

        public PanelEstadosPresenter(IReclamosAdoService sqlReclamosServices)
        {
            _sqlReclamosServices = sqlReclamosServices;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += ViewLoad;
            View.UpdateEvent += ViewUpdateEvent;
        }


        void ViewUpdateEvent(object sender, EventArgs e)
        {
            GetResumenProceso();
        }

        void ViewLoad(object sender, EventArgs e)
        {
            if (View.IsPostBack) return;
            GetResumenProceso();
        }


        private void GetResumenProceso()
        {
            if (string.IsNullOrEmpty(View.IdReclamo)) return;
            try
            {
                var dt = _sqlReclamosServices.ResumenReclamosPanelWorkFlow(View.IdReclamo);
                if (dt.Rows.Count == 0) return;

                View.Estado = dt.Rows[0]["Descripcion"].ToString();
                View.SolicitadoPor = dt.Rows[0]["Solicitante"].ToString();
                View.FechaSolicitud = dt.Rows[0]["CreateOn"].ToString();
                View.Responsable = dt.Rows[0]["Responsable"].ToString();
                View.AsignadoEn = dt.Rows[0]["ModifiOn"].ToString();
                View.NumeroDias = dt.Rows[0]["Diferencia"].ToString();

            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }
    }
}