using System;
using System.Collections.Generic;
using Application.Core;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.DocumentLibrary.IViews;
using Presenters.DocumentLibrary.Presenters;

namespace Modules.DocumentLibrary.UserControls
{
    public partial class WucNewFolder : ViewUserControl<NewFolderPresenter, INewFolderView>, INewFolderView
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtNombreCarpeta.Focus();
        }

        public TBL_Admin_Usuarios UserSession
        {
            get { return AuthenticatedUser; }
        }

        public event EventHandler SaveEvent;

        public string IdParent
        {
            get { return FolderId; }
        }

        public string Idcategoria
        {
            get { return ddlcategoria.SelectedValue; }
        }

        public string NombreFolder
        {
            get { return txtNombreCarpeta.Text; }
        }

        public string IdContrato
        {
            get { return Request.QueryString["IdReclamo"]; }
        }

        public void Listadocategorias(List<TBL_ModuloDocumentosAnexos_Categorias> items)
        {
            ddlcategoria.DataSource = items;
            ddlcategoria.DataValueField = "IdCategoria";
            ddlcategoria.DataTextField = "Nombre";
            ddlcategoria.DataBind();
        }

       
        protected void OkButtonClick(object sender, EventArgs e)
        {
            if (SaveEvent != null)
                SaveEvent(null, EventArgs.Empty);

            InvokeActualizarEvent(new ViewResulteventArgs(null));
        }
    }
}