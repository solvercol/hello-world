using System;
using System.Reflection;
using Application.Core;
using Application.MainModule.Reclamos.IServices;
using Domain.MainModule.Reclamos.DTO;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Reclamos.IViews;

namespace Presenters.Reclamos.Presenters
{
    public class ReadingReclamoServicioT1DetallePresenter : Presenter<IReadingReclamoServicioT1DetalleView>
    {
        readonly ISfTBL_ModuloReclamos_ReclamoManagementServices _reclamoService;

        public ReadingReclamoServicioT1DetallePresenter(ISfTBL_ModuloReclamos_ReclamoManagementServices reclamoService)
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
                var model = _reclamoService.GetReclamoWithNavById(Convert.ToDecimal(View.IdReclamo));

                if (model != null)
                {
                    var cliente = (Dto_Cliente)model.DtoCliente;

                    View.CategoriaReclamo = model.TBL_ModuloReclamos_CategoriasReclamo.Nombre;
                    View.Area = model.Area;
                    View.Asesor = model.AsesoradoPor.Nombres;
                    View.PedidoRemisionFactura = model.NumPFR;
                    View.NoRecordatorios = model.NumeroDeVeces;
                    View.DiarioInventario = Convert.ToInt32(model.NumDiarioInventario);
                    View.AtendidoPor = model.AtentidoPor.Nombres;
                    View.TipoContacto = model.TipoContrato;
                    View.RespuestaInmediata = model.RespuestaInmediata;
                    View.NombreCliente = cliente.NombreCliente;
                    View.UnidadZona = model.UnidadZona;
                    View.NombreContacto = model.Contacto;
                    View.EmailContacto = model.EmailContacto;
                    View.DescripcionProblema = model.DescripcionProblema;
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }
    }
}