using System;
using System.Reflection;
using Application.Core;
using Application.MainModule.Reclamos.IServices;
using Domain.MainModule.Reclamos.DTO;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Reclamos.IViews;

namespace Presenters.Reclamos.Presenters
{
    public class ReadingReclamoProductoPresenter : Presenter<IReadingReclamoProductoView>
    {
        readonly ISfTBL_ModuloReclamos_ReclamoManagementServices _reclamoService;

        public ReadingReclamoProductoPresenter(ISfTBL_ModuloReclamos_ReclamoManagementServices reclamoService)
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
                var reclamo = _reclamoService.GetReclamoById(Convert.ToDecimal(View.IdReclamo));

                if (reclamo != null)
                {
                    var producto = (Dto_Producto)reclamo.DtoProducto;
                    View.NombreProducto = producto.NombreProducto;
                    View.TargetMarket = producto.GrupoCompradores;
                    View.CampoAplicacion = producto.CampoApl;
                    View.SubCampoAplicacion = producto.Categoria;
                    View.Presentacion = producto.Unidad;
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }
    }
}