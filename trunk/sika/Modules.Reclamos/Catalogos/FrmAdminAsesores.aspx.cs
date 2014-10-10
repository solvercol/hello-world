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
    public partial class FrmAdminAsesores : ViewPage<AsesoresListPresenter, IAsesoresListView>, IAsesoresListView
    {
        #region Delegates

        public event EventHandler FilterEvent;
        public event EventHandler PagerEvent;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana("Administrador de Asesores");
        }

        protected void BtnNewClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("FrmAddAsesor.aspx{0}", GetBaseQueryString()));
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

        public void GetAsesores(List<TBL_ModuloReclamos_Asesores> items)
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
            
            Response.Redirect(string.Format("FrmViewAsesor.aspx{0}&UserId={1}", GetBaseQueryString(),e.CommandArgument));
        }

        protected void RptListadoItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var Asesor = e.Item.DataItem as TBL_ModuloReclamos_Asesores;

            if (Asesor == null) return; 

            var cmdEditar = e.Item.FindControl("CmdEditar") as LinkButton;

            if (cmdEditar != null)
            {
                cmdEditar.CommandArgument = Asesor.IdUsuario.ToString();
            }

            var litAsesor = e.Item.FindControl("litAsesor") as Literal;
            if (litAsesor != null)
            {
                litAsesor.Text = Asesor == null ? string.Empty : Presenter.GetNameAsesor(Asesor.IdUsuario);//no existe propiedad de navigacion con tabla usuarios para esta propiedad
            }

            var litUnidad = e.Item.FindControl("litUnidad") as Literal;
            if (litUnidad != null)
            {
                litUnidad.Text = Asesor == null ? string.Empty : Asesor.TBL_ModuloReclamos_Unidad.Nombre;
            }


            var litZona = e.Item.FindControl("litZona") as Literal;
            if (litZona != null)
            {
                litZona.Text = Asesor == null ? string.Empty : Asesor.TBL_ModuloReclamos_Zona.Descripcion;
            }


            if (Asesor.TBL_Admin_Usuarios != null)
            {
                var litJefe = e.Item.FindControl("litJefe") as Literal;
                if (litJefe != null)
                {
                    foreach (var ing in Asesor.TBL_Admin_Usuarios2)
                    {
                        if (ing.Nombres != string.Empty)
                        {
                            if (litJefe.Text != string.Empty)
                            {
                                litJefe.Text = litJefe.Text + "<br/>";
                            }
                            litJefe.Text = litJefe.Text + ing.Nombres;
                        }
                    }

                }

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