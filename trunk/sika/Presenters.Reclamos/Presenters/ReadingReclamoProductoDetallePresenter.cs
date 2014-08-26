﻿using System;
using System.Reflection;
using Application.Core;
using Application.MainModule.Reclamos.IServices;
using Domain.MainModule.Reclamos.DTO;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Reclamos.IViews;

namespace Presenters.Reclamos.Presenters
{
    public class ReadingReclamoProductoDetallePresenter : Presenter<IReadingReclamoProductoDetalleView>
    {
        readonly ISfTBL_ModuloReclamos_ReclamoManagementServices _reclamoService;

        public ReadingReclamoProductoDetallePresenter(ISfTBL_ModuloReclamos_ReclamoManagementServices reclamoService)
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
                    var producto = (Dto_Producto)model.DtoProducto;
                    var cliente = (Dto_Cliente)model.DtoCliente;

                    View.Asesor = model.AsesoradoPor.Nombres;
                    View.Planta = model.Planta;
                    View.CantidadVendidaUnidad = model.CantidadVendida.GetValueOrDefault();
                    View.CantidadReclamadaUnidad = model.CantidadReclamada.GetValueOrDefault();
                    View.Aplicado = model.Aplicado.GetValueOrDefault();
                    View.FechaVenta = model.FechaVenta.GetValueOrDefault();
                    View.NoRecordatorios = model.NumeroDeVeces;
                    View.AtendidoPor = model.AtentidoPor.Nombres;
                    View.TipoContacto = model.TipoContrato;
                    View.RespuestaInmediata = model.RespuestaInmediata;
                    View.NombreCliente = cliente.NombreCliente;
                    View.UnidadZona = model.UnidadZona;
                    View.NombreContacto = model.Contacto;
                    View.EmailContacto = model.EmailContacto;
                    View.NombreObra = model.NombreObra;
                    View.AplicadoPor = model.AplicadoPor;
                    View.PropietarioObra = model.PropietarioObra;
                    View.EmailPropietario = model.EmailPropietarioObra;
                    View.EmailQuienAplica = model.EmailQuienAplica;
                    View.AspectoExteriorEnvase = model.AspectoEnvase;
                    View.AspectoProducto = model.AspectoProducto;
                    View.DescripcionProducto = model.UsoDescripcion;
                    View.NumeroLote = model.Lote;
                    View.MuestraDisponible = model.MuestraDisponible.GetValueOrDefault();
                    View.DescripcionProblema = model.DescripcionProblema;
                    View.Diagnostico = model.DiagnosticoPrevio;
                    View.ConclusionesPrevias = model.ConclusionesPrevias;
                    View.Solucion = model.ObservacionesSolucion;

                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }
    }
}