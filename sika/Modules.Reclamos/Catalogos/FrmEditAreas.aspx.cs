using System;
using System.Collections.Generic;
using Application.Core;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.Reclamos.IViews;
using Presenters.Reclamos.Presenters;
using System.Linq;

namespace Modules.Reclamos.Catalogos
{
    public partial class FrmEditAreas : ViewPage<EditAreasPresenter, IEditAreasViewn>, IEditAreasViewn
    {
        #region Delegates

        public event EventHandler SaveEvent;
        public event EventHandler DeleteEvent;

        #endregion

        #region Load

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana("Editar Área");

            // btnEliminar.Visible = !string.IsNullOrEmpty(IdArea);
            btnSave.Visible = !string.IsNullOrEmpty(IdArea);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            HideControlsevent += FrmEditUserControlsevent;
        }

        void FrmEditUserControlsevent(object sender, EventArgs e)
        {
            btnSave.Visible = false;
            btnEliminar.Visible = false;
        }

        #endregion

        #region events

        protected void BtnSaveClick(object sender, EventArgs e)
        {
            if (SaveEvent != null)
            {
                SaveEvent(null, EventArgs.Empty);
                Response.Redirect(string.Format("FrmViewAreas.aspx{0}&CategoriaReclamoId={1}", GetBaseQueryString(), this.IdArea));
            }
        }

        protected void BtnDeleteClick(object sender, EventArgs e)
        {
            if (DeleteEvent != null)
                DeleteEvent(null, EventArgs.Empty);
        }

        protected void BtnBackClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("FrmViewAreas.aspx{0}", GetBaseQueryString()));
        }

        #endregion

        public TBL_Admin_Usuarios UserSession
        {
            get { return AuthenticatedUser; }
        }

        public string IdModule
        {
            get { return ModuleId; }
        }


        public void GetGerentes(IList<TBL_Admin_Usuarios> items)
        {
            if (items.Any())
            {
                items = items.OrderBy(x => x.Nombres).ToList();
            }

            ddlGerentes.DataSource = items;
            ddlGerentes.DataTextField = "Nombres";
            ddlGerentes.DataValueField = "IdUser";
            ddlGerentes.DataBind();
        }

        public string IdArea
        {
            get { return Request.QueryString["CategoriaReclamoId"]; }
        }

        public string Nombre
        {
            get { return txtNombre.Text; }
            set { txtNombre.Text = value; }
        }

        public string Procesos
        {
            get { return txtProceso.Text; }
            set { txtProceso.Text = value; }
        }

        public string IdGerente
        {
            get { return ddlGerentes.SelectedValue; }
            set { ddlGerentes.SelectedValue = value; }
        }

        public bool Activo
        {
            get { return chkActive.Checked; }
            set { chkActive.Checked = value; }
        }

        public string CreateBy
        {
            set { lblCreateBy.Text = value; }
        }

        public string CreateOn
        {
            set { lblCreateOn.Text = value; }
        }

        public string ModifiedBy
        {
            set { lblModifiedBy.Text = value; }
        }

        public string ModifiedOn
        {
            set { lblModifiedOn.Text = value; }
        }

        string IView.IdModule
        {
            get { return IdModule; }
        }
    }
}