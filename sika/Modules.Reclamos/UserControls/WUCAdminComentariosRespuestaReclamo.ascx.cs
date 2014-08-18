using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Modules.Reclamos.UI;
using Presenters.Reclamos.IViews;
using Presenters.Reclamos.Presenters;

namespace Modules.Reclamos.UserControls
{
    public partial class WUCAdminComentariosRespuestaReclamo : ViewUserControl<AdminComentariosRespuestaReclamoPresenter, IAdminComentariosRespuestaReclamoView>, IAdminComentariosRespuestaReclamoView, IReclamoWebUserControl
    {
        #region Page Events

        #region Load

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        #endregion

        #region Buttons

        protected void BtnAddComentario_Click(object sender, EventArgs e)
        {
            InitAdminComentario();
            ShowAdminComentarioWindow(true);
        }

        protected void BtnSaveComentario_Click(object sender, EventArgs e)
        {
            if (IsNewComentario)
                Presenter.AddComentarioReclamo();
            else
                Presenter.UpdateComentarioReclamo();
        }

        protected void BtnRemoveActividad_Click(object sender, EventArgs e)
        {
            var btn = (ImageButton)sender;

            IdSelectedComentario = btn.CommandArgument;

            Presenter.RemoveComentarioReclamo();
        }

        protected void BtnSelectComentario_Click(object sender, EventArgs e)
        {
            var btn = (ImageButton)sender;

            IdSelectedComentario = btn.CommandArgument;

            Presenter.LoadComentarioReclamo();
        }

        #endregion

        #region Repeaters

        protected void RptComentariosList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var item = (TBL_ModuloReclamos_ComentariosRespuesta)(e.Item.DataItem);
                // Bindind data

                var hddIdComentario = e.Item.FindControl("hddIdComentario") as HiddenField;
                if (hddIdComentario != null) hddIdComentario.Value = string.Format("{0}", item.IdComentario);

                var lblDescripcion = e.Item.FindControl("lblDescripcion") as Label;
                if (lblDescripcion != null) lblDescripcion.Text = string.Format("{0}", item.Comentario);

                var lblFechaComentario = e.Item.FindControl("lblFechaComentario") as Label;
                if (lblFechaComentario != null) lblFechaComentario.Text = string.Format("{0:dd/MM/yyyy hh:mm:ss tt}", item.CreateOn);

                var lblAutor = e.Item.FindControl("lblAutor") as Label;
                if (lblAutor != null) lblAutor.Text = string.Format("{0}", item.TBL_Admin_Usuarios.Nombres);

                var imgSelectComentario = e.Item.FindControl("imgSelectComentario") as ImageButton;
                if (imgSelectComentario != null) imgSelectComentario.CommandArgument = string.Format("{0}", item.IdComentario);
            }
        }

        #endregion

        #endregion

        #region Methods

        void InitAdminComentario()
        {
            Asunto = string.Empty;
            Comentario = string.Empty;
            IdUsuarioDestino = UserSession.IdUser.ToString();
            IsNewComentario = true;
        }

        #endregion

        #region IReclamoWebUserControl

        public void LoadControlData()
        {
            Presenter.LoadInitData();
        }

        #endregion

        #region View Members

        #region Methods

        public void ShowAdminComentarioWindow(bool visible)
        {
            if (visible)
                mpeAdminSolucion.Show();
            else
                mpeAdminSolucion.Hide();
        }

        public void LoadComentariosReclamo(List<TBL_ModuloReclamos_ComentariosRespuesta> items)
        {
            rptComentariosList.DataSource = items;
            rptComentariosList.DataBind();
        }

        public void LoadDestinatarios(List<TBL_Admin_Usuarios> items)
        {
            if (items.Any())
            {
                items = items.OrderBy(x => x.Nombres).ToList();
            }

            wddDestinatarios.DataSource = items;
            wddDestinatarios.TextField = "Nombres";
            wddDestinatarios.ValueField = "IdUser";
            wddDestinatarios.DataBind();
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

        public string IdReclamo
        {
            get { return Request.QueryString.Get("IdReclamo"); }
        }
        
        public string Asunto
        {
            get
            {
                return txtAsunto.Text;
            }
            set
            {
                txtAsunto.Text = value;
            }
        }

        public string Comentario
        {
            get
            {
                return txtObservaciones.Text;
            }
            set
            {
                txtObservaciones.Text = value;
            }
        }

        public string IdUsuarioDestino
        {
            get
            {
                return wddDestinatarios.SelectedValue;
            }
            set
            {
                wddDestinatarios.SelectedValue = value;
            }
        }

        public string IdSelectedComentario
        {
            get
            {
                return ViewState["AdminComentario_IdComentarioSelect"] as string;
            }
            set
            {
                ViewState["AdminComentario_IdComentarioSelect"] = value;
            }
        }

        public bool IsNewComentario
        {
            get
            {
                if (ViewState["AdminComentario_IsNewComentario"] == null)
                    ViewState["AdminComentario_IsNewComentario"] = false;

                return Convert.ToBoolean(ViewState["AdminComentario_IsNewComentario"]);
            }
            set
            {
                ViewState["AdminComentario_IsNewComentario"] = value;
            }
        }        

        #endregion

        #endregion
    }
}