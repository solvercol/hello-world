using System;
using Application.Core;
using Presenters.Reclamos.IViews;
using Application.MainModule.Reclamos.IServices;
using System.Reflection;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Domain.MainModule.Reclamos.Enum;
using Applications.MainModule.Admin.IServices;
using System.Collections.Generic;

namespace Presenters.Reclamos.Presenters
{
    public class ListaGeneralReclamosPresenter : Presenter<IListaGeneralReclamosView>
    {
        readonly ISfTBL_ModuloReclamos_CategoriasReclamoManagementServices _categoriasReclamoService;        

        public ListaGeneralReclamosPresenter(ISfTBL_ModuloReclamos_CategoriasReclamoManagementServices categoriasReclamoService)
        {
            _categoriasReclamoService = categoriasReclamoService;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += View_Load;
        }

        void View_Load(object sender, EventArgs e)
        {
            if (View.IsPostBack) return;
            LoadCategoriasReclamo();
        }

        #region Methods

        public void LoadCategoriasReclamo()
        {
            try
            {
                var categories = _categoriasReclamoService.GetByTipoReclamo(TipoReclamo.Servicio);
                View.LoadCategoriasReclamo(categories);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
            }
        }

        #endregion
    }
}