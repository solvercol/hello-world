﻿using System;
using System.Reflection;
using Application.Core;
using Application.MainModule.AccionesPC.IServices;
using Applications.MainModule.Admin.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.AccionesPC.IViews;
using Application.MainModule.Reclamos.IServices;
using Domain.MainModule.Reclamos.DTO;

namespace Presenters.AccionesPC.Presenters
{
    public class SolicitudAPCPresenter : Presenter<ISolicitudAPCView>
    {
        readonly ISfTBL_ModuloAPC_SolicitudManagementServices _solicitudService;
        readonly ISfTBL_Admin_SeccionesManagementServices _seccionesServices;
        readonly ISfTBL_Admin_OptionListManagementServices _optionListService;
        readonly ISfTBL_ModuloReclamos_ReclamoManagementServices _reclamoService;

        public SolicitudAPCPresenter(ISfTBL_ModuloAPC_SolicitudManagementServices solicitudService,
                                ISfTBL_Admin_SeccionesManagementServices seccionesServices,
                                ISfTBL_Admin_OptionListManagementServices optionListService,
                                ISfTBL_ModuloReclamos_ReclamoManagementServices reclamoService)
        {
            _solicitudService = solicitudService;
            _seccionesServices = seccionesServices;
            _optionListService = optionListService;
            _reclamoService = reclamoService;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += ViewLoad;
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
            CargarSecciones();
        }

        /// <summary>
        /// 
        /// </summary>
        public void LoadSolicitud()
        {
            if (string.IsNullOrEmpty(View.IdSolicitud)) return;

            try
            {
                var item = _solicitudService.GetWithNavById(Convert.ToDecimal(View.IdSolicitud));

                if (item != null)
                {
                    View.TipoAccion = item.TipoAccion;
                    View.Area = item.TBL_ModuloAPC_Areas.Nombre;
                    View.GerenteArea = item.TBL_Admin_Usuarios4.Nombres;
                    View.ResponsableAccion = item.TBL_Admin_Usuarios8.Nombres;
                    View.FechaInicio = string.Format("{0:dd/MM/yyyy}", item.FechaDesde);
                    View.FechaFinal = string.Format("{0:dd/MM/yyyy}", item.FechaHasta);

                    View.LogInfoMessage = string.Format("Creado por {0} en {1:dd/MM/yyyy hh:mm ss tt}. Modificado por {2} en {3:dd/MM/yyyy hh:mm ss tt}.",
                                                       item.TBL_Admin_Usuarios3.Nombres, item.CreateOn,
                                                       item.TBL_Admin_Usuarios5.Nombres, item.ModifiedOn);

                    View.ShowInfoReclamo = false;

                    if (item.IdReclamoCreacion.HasValue)
                        LoadReclamo(item.IdReclamoCreacion.GetValueOrDefault());
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        void LoadReclamo(decimal idReclamo)
        {
            try
            {
                var reclamo = _reclamoService.GetReclamoWithNavById(idReclamo);

                if (reclamo != null)
                {
                    var dtoProducto = (Dto_Producto)reclamo.DtoProducto;
                    var dtoCliente = (Dto_Cliente)reclamo.DtoCliente;

                    View.TipoReclamo = reclamo.TBL_ModuloReclamos_TipoReclamo.Nombre;
                    View.NumeroReclamo = reclamo.NumeroReclamo;

                    if (View.TipoReclamo == "Producto")
                    {
                        View.TitleReclamo = dtoProducto.NombreProducto;
                        View.TitleReclamoFrom = dtoCliente.NombreCliente;
                        View.Unidad = reclamo.UnidadZona;
                        View.FechaReclamo = string.Format("{0:dd/MM/yyyy}", reclamo.CreateOn);
                        View.Asesor = reclamo.AsesoradoPor.Nombres;
                    }
                    else
                    {
                        View.TitleReclamo = reclamo.TBL_ModuloReclamos_CategoriasReclamo.Nombre;
                        if (dtoCliente != null && !string.IsNullOrEmpty(dtoCliente.NombreCliente))
                        {
                            View.TitleReclamoFrom = dtoCliente.NombreCliente;
                        }
                        else
                        {
                            View.TitleReclamoFrom = string.Format("{0} / {1}", reclamo.NombreReclama, reclamo.ProcedimientoInternoAfectado);
                        }
                        View.Unidad = reclamo.UnidadZona;
                        View.FechaReclamo = string.Format("{0:dd/MM/yyyy}", reclamo.CreateOn);
                        if (reclamo.AsesoradoPor != null)
                            View.Asesor = reclamo.AsesoradoPor.Nombres;
                    }

                    View.ShowInfoReclamo = true;
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        /// <summary>
        /// 
        /// </summary>
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