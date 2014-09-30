using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.Reclamos.IViews;
using Presenters.Reclamos.Presenters;
using ServerControls;

namespace Modules.Reclamos.Catalogos
{
    public partial class FrmAdminUnidadesZonas : ViewPage<UnidadesZonasListPresenter, IUnidadesZonasListView>, IUnidadesZonasListView
    {
        #region Delegates

        public event EventHandler FilterEvent;
        public event EventHandler PagerEvent;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana("Administrador de Unidades Zona");
        }

        protected void BtnNewClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("FrmAddUnidadZona.aspx{0}", GetBaseQueryString()));
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

        public void GetUnidadesZonas(List<TBL_ModuloReclamos_UnidadesZonas> items)
        {
            rptListado.DataSource = items;
            rptListado.DataBind();
        }


        public int TotalRegistrosPaginador
        {
            set { pgrListado.RowCount = value; }
        }

        public int PageZise
        {
            get { return pgrListado.PageSize; }
        }

        public string ModuleSetupId
        {
            get { return ViewState["ModuleSetupId"] == null ? string.Empty : ViewState["ModuleSetupId"].ToString(); }
            set { ViewState["ModuleSetupId"] = value; }
        }

        protected void RptListadoItemCommand(object source, RepeaterCommandEventArgs e)
        {
            var ArrayId= e.CommandArgument.ToString().Split('|');
            Response.Redirect(string.Format("FrmViewUnidadZona.aspx{0}&UnidadId={1}&ZonaId={2}&GerenteId={3}", GetBaseQueryString(), ArrayId[0],ArrayId[1], ArrayId[2]));
        }

        protected void RptListadoItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var UnidadZona = e.Item.DataItem as TBL_ModuloReclamos_UnidadesZonas;

            if (UnidadZona == null) return;

            var cmdEditar = e.Item.FindControl("CmdEditar") as LinkButton;

            if (cmdEditar != null)
            {
                cmdEditar.CommandArgument = UnidadZona.IdUnidad + "|" + UnidadZona.IdZona + "|" + UnidadZona.IdGerente;
            }

            var chkActivo = e.Item.FindControl("chkActivo") as CheckBox;

            if (chkActivo != null)
            {
                chkActivo.Checked = UnidadZona.IsActive;
            }

            var litUnidad = e.Item.FindControl("litUnidad") as Literal;
            if (litUnidad != null)
            {
                litUnidad.Text = UnidadZona == null ? string.Empty : UnidadZona.TBL_ModuloReclamos_Unidad.Nombre;
            }

            var litZona = e.Item.FindControl("litZona") as Literal;
            if (litZona != null)
            {
                litZona.Text = UnidadZona == null ? string.Empty : UnidadZona.TBL_ModuloReclamos_Zona.Descripcion;
            }

            var litDescripcion = e.Item.FindControl("litDescripcion") as Literal;
            if (litDescripcion != null)
            {
                litDescripcion.Text = UnidadZona == null ? string.Empty : UnidadZona.Descripcion;
            }

            var litGerente = e.Item.FindControl("litGerente") as Literal;
            if (litGerente != null)
            {
                litGerente.Text = UnidadZona == null ? string.Empty : UnidadZona.TBL_Admin_Usuarios1.Nombres;
            }

            var litTarifa = e.Item.FindControl("litTarifa") as Literal;
            if (litTarifa != null)
            {
                litTarifa.Text = UnidadZona == null ? string.Empty : string.Format("{0:C}", UnidadZona.TarifaFletes);
            }

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
    }
}