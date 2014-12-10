﻿using System;
using System.Reflection;
using Application.Core;
using Application.MainModule.SqlServices.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Reclamos.IViews;

namespace Presenters.Reclamos.Presenters
{
    public class AlternativasPorPersonaPresenter : Presenter<IAlternativasPorPersonaView>
    {
        readonly IReclamosAdoService _recladoAdoService;

        public AlternativasPorPersonaPresenter(IReclamosAdoService recladoAdoService)
        {
            _recladoAdoService = recladoAdoService;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += View_Load;
        }

        void View_Load(object sender, EventArgs e)
        {
            if (View.IsPostBack) return;
            LoadInitView();
            LoadReport();
        }

        #region Methods

        void LoadInitView()
        {
            View.FechaFilterFrom = new DateTime(DateTime.Now.Year, 1, 1);
            View.FechaFilterTo = new DateTime(DateTime.Now.Year, 12, 31);
        }

        public void LoadReport()
        {
            try
            {
                var dt = _recladoAdoService.GetVistaGestionAlternativas(View.ServerHostPath, View.IdModule, View.UserSession.IdUser
                                                                       , View.FechaFilterFrom, View.FechaFilterTo, View.FilterNoReclamo
                                                                       , View.FilterCliente, View.FilterProducto, View.FilterServicio,"altpersona");
                View.LoadView(dt);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        #endregion
    }
}