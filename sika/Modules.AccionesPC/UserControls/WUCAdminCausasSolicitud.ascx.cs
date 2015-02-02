using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Core;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Modules.AccionesPC.UI;
using Presenters.AccionesPC.IViews;
using Presenters.AccionesPC.Presenters;

namespace Modules.AccionesPC.UserControls
{
    public partial class WUCAdminCausasSolicitud : ViewUserControl<AdminCausasSolicitudPresenter, IAdminCausasSolicitudView>, IAdminCausasSolicitudView, ISolicitudWebUserControl
    {
        #region Members

        public string FromPage
        {
            get
            {
                return Request.QueryString["from"];
            }
        }

        public string IdFrom
        {
            get
            {
                return Request.QueryString["idfrom"];
            }
        }

        #endregion

        #region Page Events

        #region Load

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        #endregion

        #region Buttons

        protected void BtnAddCausa_Click(object sender, EventArgs e)
        {
            InitAdminCausa();
            IsNew = true;
            ShowAdminCausaWindow(true);
        }

        protected void BtnSaveActividad_Click(object sender, EventArgs e)
        {
            var messages = new List<string>();

            if (string.IsNullOrEmpty(Descripcion))
                messages.Add("Es necesario ingresar una descripción.");

            if (messages.Any())
            {
                AddErrorMessages(messages);
                ShowAdminCausaWindow(true);
                return;
            }

            if (IsNew)
                Presenter.AddCausaSolicitud();
            else
                Presenter.UpdateCausa();
        }

        protected void BtnSelectCausa_Click(object sender, EventArgs e)
        {
            var btn = (ImageButton)sender;

            var IdSelectedCausa = btn.CommandArgument;
            SelectedId = Convert.ToDecimal(IdSelectedCausa);

            Presenter.LoadCausa();
        }

        #endregion

        #region Repeaters

        protected void RptCausasList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var item = (TBL_ModuloAPC_Causas)(e.Item.DataItem);
                // Bindind data

                var hddIdCausa = e.Item.FindControl("hddIdCausa") as HiddenField;
                if (hddIdCausa != null) hddIdCausa.Value = string.Format("{0}", item.IdCausa);

                var lblDescripcion = e.Item.FindControl("lblDescripcion") as Label;
                if (lblDescripcion != null) lblDescripcion.Text = item.Descripcion;

                var lblComentarios = e.Item.FindControl("lblComentarios") as Label;
                if (lblComentarios != null) lblComentarios.Text = item.Comentarios;

                var lblAutor = e.Item.FindControl("lblAutor") as Label;
                if (lblAutor != null) lblAutor.Text = string.Format("{0}", item.TBL_Admin_Usuarios.Nombres);

                var imgSelectCausa = e.Item.FindControl("imgSelectCausa") as ImageButton;
                if (imgSelectCausa != null) imgSelectCausa.CommandArgument = string.Format("{0}", item.IdCausa);
            }
        }

        #endregion

        #endregion

        #region Methods

        void AddErrorMessages(List<string> messages)
        {
            if (messages.Any())
            {
                foreach (var msg in messages)
                {
                    var custVal = new CustomValidator();
                    custVal.IsValid = false;
                    custVal.ErrorMessage = msg;
                    custVal.EnableClientScript = false;
                    custVal.Display = ValidatorDisplay.None;
                    custVal.ValidationGroup = "vsCausas";
                    this.Page.Form.Controls.Add(custVal);
                }
            }
        }

        void InitAdminCausa()
        {
            Descripcion = string.Empty;
            Comentarios = string.Empty;
        }



        #endregion

        #region ISolicitudWebUserControl

        public void LoadControlData()
        {
            Presenter.LoadInitData();
        }

        #endregion

        #region View Members

        #region Methods

        public void ShowAdminCausaWindow(bool visible)
        {
            if (visible)
                mpeAdminCausa.Show();
            else
                mpeAdminCausa.Hide();
        }

        public void LoadCausasSolicitud(List<TBL_ModuloAPC_Causas> items)
        {
            rptCausasList.DataSource = items;
            rptCausasList.DataBind();
        }

        public void ReloadWuc()
        {
            if (FilterEvent != null)
                FilterEvent(null, EventArgs.Empty);
        }

        #endregion

        #region Properties

        public event Action RiseFatherPostback;

        public TBL_Admin_Usuarios UserSession
        {
            get { return AuthenticatedUser; }
        }

        public string IdModule
        {
            get { return ModuleId; }
        }

        public event EventHandler FilterEvent;

        public string IdSolicitud
        {
            get { return Request.QueryString.Get("IdSolicitud"); }
        }

        public string Descripcion
        {
            get
            {
                return txtDescripcion.Text;
            }
            set
            {
                txtDescripcion.Text = value;
            }
        }

        public string Comentarios
        {
            get
            {
                return txtComentarios.Text;
            }
            set
            {
                txtComentarios.Text = value;
            }
        }

        public bool IsNew
        {
            get
            {
                return Convert.ToBoolean(ViewState["AdminCausasSolicitud_IsNew"]);
            }
            set
            {
                ViewState["AdminCausasSolicitud_IsNew"] = value;
            }
        }

        public decimal SelectedId
        {
            get
            {
                return Convert.ToDecimal(ViewState["AdminCausasSolicitud_SelectedId"]);
            }
            set
            {
                ViewState["AdminCausasSolicitud_SelectedId"] = value;
            }
        }

        public bool CanAddCausas
        {
            get
            {
                return btnAddCausa.Visible;
            }
            set
            {
                btnAddCausa.Visible = value;
            }
        }

        #endregion

        #endregion
    }
}