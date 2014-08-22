using System;
using System.Reflection;
using Application.Core;
using Application.MainModule.Reclamos.IServices;
using Domain.MainModule.Reclamos.DTO;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Reclamos.IViews;

namespace Presenters.Reclamos.Presenters
{
    public class ReadingReclamoServicioPresenter : Presenter<IReadingReclamoServicioView>
    {
        readonly ISfTBL_ModuloReclamos_ReclamoManagementServices _reclamoService;

        public ReadingReclamoServicioPresenter(ISfTBL_ModuloReclamos_ReclamoManagementServices reclamoService)
        {
            _reclamoService = reclamoService;
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
            LoadReclamo();
        }

        void LoadReclamo()
        {
            if (string.IsNullOrEmpty(View.IdReclamo)) return;

            try
            {
                var reclamo = _reclamoService.GetReclamoWithNavById(Convert.ToDecimal(View.IdReclamo));

                if (reclamo != null)
                {
                    var producto = (Dto_Producto)reclamo.DtoProducto;
                    View.Categoria = reclamo.TBL_ModuloReclamos_CategoriasReclamo.Nombre;
                    View.Area = reclamo.Area;
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }
    }
}