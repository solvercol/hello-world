using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Infrastructure.CrossCutting.IoC;
using Presenters.Reclamos.IViews;
using Presenters.Reclamos.Presenters;

namespace Modules.Reclamos.Admin
{
    public partial class FrmListaGeneralReclamos : ViewPage<ListaGeneralReclamosPresenter, IListaGeneralReclamosView>, IListaGeneralReclamosView
    {
        #region Page Events

        #region Load

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana("Vista General de Reclamos");
        }

        #endregion

        #region Buttons

        protected void BtnNuevoReclamo_Click(object sender, EventArgs e)
        {
            InitNewReclamo();
            ShowNewReclamoWindow(true);
        }

        protected void BtnConfirmNuevoReclamo_Click(object sender, EventArgs e)
        {
            if (TipoReclamo == "Producto")
            {
                Response.Redirect(string.Format("FrmAddReclamo.aspx?ModuleId={0}&tr={1}", ModuleId, TipoReclamo));
            }
            else
            {
                Response.Redirect(string.Format("FrmAddReclamo.aspx?ModuleId={0}&tr={1}&cat={2}&gruinf={3}",
                                                ModuleId,TipoReclamo,IdCategoriaReclamo,IdGrupoInformacion
                                                ));
            }
        }

        #endregion

        #region Radio Button

        protected void RblReclamoType_Changed(Object sender, EventArgs e)
        {
            ShowCategoriaReclamoList(rblReclamoType.SelectedValue != "Producto");
            ShowNewReclamoWindow(true);
        }

        #endregion

        #endregion

        #region Methods
        #endregion

        #region View Members        

        #region Members

        public void ShowCategoriaReclamoList(bool visible)
        {
            trCategoriaReclamo.Visible = visible;
        }

        public void ShowNewReclamoWindow(bool visible)
        {
            if (visible)
                mpeNewReclamo.Show();
            else
                mpeNewReclamo.Hide();
        }

        public void LoadCategoriasReclamo(List<TBL_ModuloReclamos_CategoriasReclamo> list)
        {
            ddlCategoriaReclamo.DataSource = list;
            ddlCategoriaReclamo.DataTextField = "Nombre";
            ddlCategoriaReclamo.DataValueField = "IdCategoriaGrupoInformacion";
            ddlCategoriaReclamo.DataBind();
        }

        public void InitNewReclamo()
        {
            rblReclamoType.SelectedValue = "Producto";
            ShowCategoriaReclamoList(false);
        }

        #endregion

        #region Properties

        public TBL_Admin_Usuarios UserSession
        {
            get { return AuthenticatedUser; }
        }

        public string IdModule
        {
            get { return ModuleId; }
        }

        public string TipoReclamo
        {
            get
            {
                return rblReclamoType.SelectedValue;
            }
            set
            {
                rblReclamoType.SelectedValue = value;
            }
        }

        public string IdCategoriaReclamo
        {
            get
            {
                return ddlCategoriaReclamo.SelectedValue.Split('-')[0];
            }
        }

        public string IdGrupoInformacion
        {
            get
            {
                return ddlCategoriaReclamo.SelectedValue.Split('-')[1];
            }
        }

        #endregion
        
        #endregion
    }
}