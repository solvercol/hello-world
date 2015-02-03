using System;
using System.Collections.Generic;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.Reclamos.IViews;
using Presenters.Reclamos.Presenters;

namespace Modules.Reclamos.Admin
{
    public partial class FrmNewReclamo : ViewPage<NewReclamoPresenter, INewReclamoView>, INewReclamoView
    {
        #region Page Events

        #region Load

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana("Nuevo Reclamo");
        }

        #endregion

        #region Buttons

        protected void BtnConfirmNuevoReclamo_Click(object sender, EventArgs e)
        {
            if (TipoReclamo == "Producto")
            {
                Response.Redirect(string.Format("FrmAddReclamo.aspx?ModuleId={0}&tr={1}&from=pendientes", ModuleId, TipoReclamo));
            }
            else
            {
                Response.Redirect(string.Format("FrmAddReclamo.aspx?ModuleId={0}&tr={1}&cat={2}&gruinf={3}&from=pendientes",
                                                ModuleId, TipoReclamo, IdCategoriaReclamo, IdGrupoInformacion
                                                ));
            }
        }


        #endregion

        #region Radio Button

        protected void RblReclamoType_Changed(Object sender, EventArgs e)
        {
            ShowCategoriaReclamoList(rblReclamoType.SelectedValue != "Producto");
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