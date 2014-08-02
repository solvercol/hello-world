using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.Admin.IViews;
using Presenters.Admin.Presenters;
using ServerControls;

namespace Modules.Admin.Catalogos
{
    public partial class FrmAdminEmailTemplate : ViewPage<PlantillasCorreoListPresenter, IPlantillasCorreoListView>, IPlantillasCorreoListView
    {
        public event EventHandler FilterEvent;

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana("Administration Plantillas");
        }

        protected void BtnNewClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("FrmEditEmailTemplate.aspx{0}", GetBaseQueryString()));
        }


        public void GetTemplates(List<TBL_Admin_Plantillas> items)
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
            Response.Redirect(string.Format("FrmEditEmailTemplate.aspx{0}&TemplateId={1}", GetBaseQueryString(), e.CommandArgument));
        }

        protected void RptListadoItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var template = e.Item.DataItem as TBL_Admin_Plantillas;

            if (template == null) return;

            var cmdEditar = e.Item.FindControl("CmdEditar") as LinkButton;

            if (cmdEditar != null)
            {
                cmdEditar.CommandArgument = template.IdPlantilla.ToString();
            }

            var chkActivo = e.Item.FindControl("chkActivo") as CheckBox;

            if (chkActivo != null)
            {
                chkActivo.Checked = template.IsActive;
            }

            var litPais = e.Item.FindControl("litPais") as Literal;
            if (litPais != null)
            {
                litPais.Text = template.TBL_Admin_Paises == null ? string.Empty : template.TBL_Admin_Paises.Nombre;
            }

            var litCodeTemplate = e.Item.FindControl("litCodeTemplate") as Literal;
            if (litCodeTemplate != null)
            {
                litCodeTemplate.Text = template.Codigo;
            }
        }

        protected void PgrChanged(object sender, PageChanged e)
        {
            if (FilterEvent != null)
                FilterEvent(e.CurrentPage, EventArgs.Empty);
        }

        public TBL_Admin_Usuarios UserSession
        {
            get { return AuthenticatedUser; }
        }

        public string IdModule
        {
            get { return ModuleId; }
        }
    }
}