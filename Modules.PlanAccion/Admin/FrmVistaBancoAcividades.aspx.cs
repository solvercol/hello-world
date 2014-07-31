﻿using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.PlanAccion.IViews;
using Presenters.PlanAccion.Presenters;
using ServerControls;

namespace Modules.PlanAccion.Admin
{
    public partial class FrmVistaBancoAcividades : ViewPage<VistaBancoActividadesPresenter, IVistaBancoActividadesView>, IVistaBancoActividadesView
    {
        public event EventHandler FilterEvent;
        public event EventHandler DeleteEvent;

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana("Listado de Actividades");
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            rptPartes.ItemCommand += RptPartesItemCommand;
        }

        void RptPartesItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("Editar"))
            {
                Response.Redirect(string.Format("FrmEditarBancoActividades.aspx{0}&IdActividad={1}", GetBaseQueryString(), e.CommandArgument));
            }
            else if (e.CommandName.Equals("Config"))
            {
                const string sourceForm = "FrmVistaBancoAcividades.aspx";
                Response.Redirect(string.Format("FrmConfiguracionActividades.aspx{0}&IdActividad={1}&Form={2}", GetBaseQueryString(), e.CommandArgument, sourceForm));
            }
            else
            {
                if (DeleteEvent != null)
                    DeleteEvent(e.CommandArgument, EventArgs.Empty);
            }
        }

        protected void BtnNuevaClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("FrmEditarBancoActividades.aspx{0}", GetBaseQueryString()));
        }

        protected void PgrListadoPageChanged(object sender, PageChanged e)
        {
            if (FilterEvent != null)
                FilterEvent(e.CurrentPage, EventArgs.Empty);
        }

        public TBL_Admin_Usuarios UserSession
        {
            get { return AuthenticatedUser; }
        }

       
        public void ListaBancoActividades(List<TBL_ModuloPlanAccion_BancoActividades> items)
        {
            rptPartes.DataSource = items;
            rptPartes.DataBind();
        }

        public int TotalRegistrosPaginador
        {
            set { pgrListado.RowCount = value; }
        }

        public int PageZise
        {
            get { return pgrListado.PageSize; }
        }

    }
}