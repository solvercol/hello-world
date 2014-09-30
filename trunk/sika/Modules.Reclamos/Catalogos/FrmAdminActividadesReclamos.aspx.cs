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
    public partial class FrmAdminActividadesReclamos : ViewPage<ActividadesReclamosListPresenter, IActividadesReclamosListView>, IActividadesReclamosListView
    {
        #region Delegates

        public event EventHandler FilterEvent;
        public event EventHandler PagerEvent;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana("Administrador de Actividades de Reclamos");
        }

        protected void BtnNewClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("FrmAddActividadReclamo.aspx{0}", GetBaseQueryString()));
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

        public void GetActividadesReclamos(List<TBL_ModuloReclamos_ActividadesReclamo> items)
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
            Response.Redirect(string.Format("FrmViewActividadReclamo.aspx{0}&ActividadReclamoId={1}", GetBaseQueryString(), e.CommandArgument));
        }

        protected void RptListadoItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var Actividad = e.Item.DataItem as TBL_ModuloReclamos_ActividadesReclamo;

            if (Actividad == null) return;

            var cmdEditar = e.Item.FindControl("CmdEditar") as LinkButton;

            if (cmdEditar != null)
            {
                cmdEditar.CommandArgument = Actividad.IdActividad.ToString();
            }

            var chkActivo = e.Item.FindControl("chkActivo") as CheckBox;

            if (chkActivo != null)
            {
                chkActivo.Checked = Actividad.IsActive;
            }

            var litTipoReclamo = e.Item.FindControl("litTipoReclamo") as Literal;
            if (litTipoReclamo != null)
            {
                litTipoReclamo.Text = Actividad == null ? string.Empty : Actividad.TBL_ModuloReclamos_TipoReclamo.Nombre;
            }

            var litNombre = e.Item.FindControl("litNombre") as Literal;
            if (litNombre != null)
            {
                litNombre.Text = Actividad == null ? string.Empty : Actividad.Nombre;
            }

            var litDescripcion = e.Item.FindControl("litActividadDescripcion") as Literal;
            if (litDescripcion != null)
            {
                litDescripcion.Text = Actividad == null ? string.Empty : Actividad.Descripcion;
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