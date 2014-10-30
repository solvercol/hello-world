﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Application.Core;
using Application.MainModule.AccionesPC.IServices;
using Application.MainModule.SqlServices.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.AccionesPC.IViews;

namespace Presenters.AccionesPC.Presenters
{
    public class SeguimientoTotalSolicitudesPresenter : Presenter<ISeguimientoTotalSolicitudesView>
    {
        readonly ISfTBL_ModuloAPC_AreasManagementServices _areasService;
        readonly ISolicitudesAPCAdoService _solicitudesAdoService;

        public SeguimientoTotalSolicitudesPresenter(ISfTBL_ModuloAPC_AreasManagementServices areasService, ISolicitudesAPCAdoService solicitudesAdoService)
        {
            _areasService = areasService;
            _solicitudesAdoService = solicitudesAdoService;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += View_Load;
        }

        void View_Load(object sender, EventArgs e)
        {
            if (View.IsPostBack) return;
            InitViewValues();
            LoadAreasAccion();
            LoadView();
        }

        void InitViewValues()
        {
            View.FechaFilterFrom = DateTime.Now.AddMonths(-6);
            View.FechaFilterTo = DateTime.Now.AddMonths(6);
            View.FilterNoSolicitud = string.Empty;
        }

        void LoadAreasAccion()
        {
            try
            {
                var items = _areasService.GetEntitiesWithGerente();

                if (items.Any())
                    items = items.OrderBy(x => x.Nombre).ToList();

                View.LoadAreaAcion(items);
                LoadProcesos();
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        public void LoadProcesos()
        {
            var procesos = new List<DTO_ValueKey>();

            procesos.Add(new DTO_ValueKey() { Id = "Seleccionar", Value = "" });

            try
            {
                if (View.FilterArea != 0)
                {
                    var area = _areasService.GetById(View.FilterArea);

                    if (area != null)
                    {
                        var spltProc = area.Procesos.Split('|');

                        foreach (var proc in spltProc)
                        {
                            procesos.Add(new DTO_ValueKey() { Id = proc, Value = proc });
                        }
                    }
                }                
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }

            View.LoadProcesos(procesos);
        }

        public void LoadView()
        {
            try
            {
                var dt = _solicitudesAdoService.GetVistaSolicitudesMisPendientes(View.FechaFilterFrom, View.FechaFilterTo, View.ServerHostPath, View.IdModule, "misolestados"
                                                                         , View.FilterNoSolicitud, View.FilterTipo, View.FilterArea, View.FilterProceso, View.UserSession.IdUser);
                View.LoadView(dt);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }
    }
}