using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.Reclamos.IViews;
using Presenters.Reclamos.Presenters;
using System.Collections;
using System.Linq;
using Application.Core;


namespace Modules.Reclamos.Catalogos
{
    public partial class FrmAddCategoriaProducto : ViewPage<AddCategoriaProductoPresenter, IAddCategoriaProductoView>, IAddCategoriaProductoView
    {
        #region Delegates

        public event EventHandler SaveEvent;

        #endregion

        #region Load

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana("Adicionar categoría producto");
            btnSave.Visible = true;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            HideControlsevent += FrmEditUserControlsevent;
        }

        void FrmEditUserControlsevent(object sender, EventArgs e)
        {
            btnSave.Visible = false;
        }

        #endregion

        #region events

        protected void BtnSaveClick(object sender, EventArgs e)
        {
            if (SaveEvent != null)
                SaveEvent(null, EventArgs.Empty);
        }

        protected void BtnBackClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("FrmAdminCategoriasProductos.aspx{0}", GetBaseQueryString()));
        }

        protected void BtnAddUsuarioCopia_Click(object sender, EventArgs e)
        {
            var usuarioCopia = new DTO_ValueKey() { Id = IdUsuarioCopia, Value = wddResponsables.SelectedItem.Text };
            if (!ExistsInCopia(usuarioCopia))
                UsuariosCopia.Add(usuarioCopia);

            LoadUsuariosCopia(UsuariosCopia);
        }

        protected void BtnRemoveUsuarioCopia_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(IdUsuarioCopiaSelected)) return;

            var usuarioCopia = UsuariosCopia.Where(x => x.Id == IdUsuarioCopiaSelected).SingleOrDefault();

            if (usuarioCopia != null)
                UsuariosCopia.Remove(usuarioCopia);

            LoadUsuariosCopia(UsuariosCopia);
        }

        #endregion

        #region Members

        public void GetIngenieros(IList<TBL_Admin_Usuarios> items)
        {
            if (items.Any())
            {
                items = items.OrderBy(x => x.Nombres).ToList();
            }

            wddResponsables.DataSource = items;
            wddResponsables.TextField = "Nombres";
            wddResponsables.ValueField = "IdUser";
            wddResponsables.DataBind();
        }

        public string Nombre
        {
            get { return txtNombre.Text; }
            set { txtNombre.Text = value; }
        }

        public string Descripcion
        {
            get { return txtDescripcion.Text; }
            set { txtDescripcion.Text = value; }
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

        public TBL_Admin_Usuarios UserSession
        {
            get { return AuthenticatedUser; }
        }

        public string IdModule
        {
            get { return ModuleId; }
        }

        public void LoadUsuariosCopia(List<DTO_ValueKey> items)
        {
            lstUsuariosCopia.DataSource = items;
            lstUsuariosCopia.DataValueField = "Id";
            lstUsuariosCopia.DataTextField = "Value";
            lstUsuariosCopia.DataBind();
        }

        public string IdUsuarioCopiaSelected
        {
            get
            {
                return lstUsuariosCopia.SelectedValue;
            }
            set
            {
                lstUsuariosCopia.SelectedValue = value;
            }
        }

        public List<DTO_ValueKey> UsuariosCopia
        {
            get
            {
                if (Session["AdminComentarios_UsuarioCopia"] == null)
                    Session["AdminComentarios_UsuarioCopia"] = new List<DTO_ValueKey>();

                return Session["AdminComentarios_UsuarioCopia"] as List<DTO_ValueKey>;
            }
            set
            {
                Session["AdminComentarios_UsuarioCopia"] = value;
            }
        }

        public string IdUsuarioCopia
        {
            get
            {
                return wddResponsables.SelectedValue;
            }
            set
            {
                wddResponsables.SelectedValue = value;
            }
        }

        bool ExistsInCopia(DTO_ValueKey item)
        {
            foreach (var itm in UsuariosCopia)
            {
                if (itm.Id == item.Id)
                    return true;
            }

            return false;
        }

        #endregion
    }
}