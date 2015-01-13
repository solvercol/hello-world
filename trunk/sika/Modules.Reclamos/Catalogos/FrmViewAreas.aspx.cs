using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Application.Core;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.Reclamos.IViews;
using Presenters.Reclamos.Presenters;
using ServerControls;

namespace Modules.Reclamos.Catalogos
{
    public partial class FrmViewAreas : ViewPage<FrmViewAreasPresenter, IFrmViewAreasView>, IFrmViewAreasView
    {
        #region Delegates

        public event EventHandler FilterEvent;
        public event EventHandler PagerEvent;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana("Administrador de Áreas");
        }

        protected void BtnNewClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("FrmAddAreas.aspx{0}", GetBaseQueryString()));
        }

        protected void BtnFilterReclamos_Click(object sender, EventArgs e)
        {
            if (FilterEvent != null)
                FilterEvent(sender, EventArgs.Empty);
        }

        protected void PgrChanged(object sender, PageChanged e)
        {
            if (PagerEvent != null)
                PagerEvent(e.CurrentPage, EventArgs.Empty);
        }

        public void GetAreas(List<TBL_ModuloAPC_Areas> items)
        {
            rptListado.DataSource = items;
            rptListado.DataBind();
        }

        public TBL_Admin_Usuarios UserSession
        {
            get { return AuthenticatedUser; }
        }

        public string Search
        {
            get { return txtSearch.Text; }
        }

        public string IdModule
        {
            get { return ModuleId; }
        }

        public int TotalRegistrosPaginador
        {
            set { pgrListado.RowCount = value; }
        }

        public int PageZise
        {
            get { return pgrListado.PageSize; }
        }

        string IView.IdModule
        {
            get { return IdModule; }
        }

        protected void RptListadoItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Response.Redirect(string.Format("FrmEditAreas.aspx{0}&CategoriaReclamoId={1}", GetBaseQueryString(), e.CommandArgument));
        }


        protected void RptListadoItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var categoria = e.Item.DataItem as TBL_ModuloAPC_Areas;

            if (categoria == null) return;

            var cmdEditar = e.Item.FindControl("CmdEditar") as LinkButton;

            if (cmdEditar != null)
            {
                cmdEditar.CommandArgument = categoria.IdArea.ToString();
            }

            var chkActivo = e.Item.FindControl("chkActivo") as CheckBox;

            if (chkActivo != null)
            {
                chkActivo.Checked = categoria.IsActive;
            }

        }
    }
}