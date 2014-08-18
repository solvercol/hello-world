using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Application.Core;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Modules.Reclamos.UI;
using Presenters.Reclamos.IViews;
using Presenters.Reclamos.Presenters;

namespace Modules.Reclamos.UserControls
{
    public partial class WUCAdminSolucionesReclamo : ViewUserControl<AdminSolucionesReclamoPresenter, IAdminSolucionesReclamoView>, IAdminSolucionesReclamoView, IReclamoWebUserControl
    {
        #region Page Events

        #region Load

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        #endregion

        #region Buttons

        protected void BtnAddSolucion_Click(object sender, EventArgs e)
        {
            InitAdminSolucion();
            ShowAdminSolucionWindow(true);
        }

        protected void BtnSaveSolucion_Click(object sender, EventArgs e)
        {
            if (IsNewSolucion)
                Presenter.AddSolucionReclamo();
            else
                Presenter.UpdateSolucionReclamo();
        }

        protected void BtnRemoveSolucion_Click(object sender, EventArgs e)
        {
            var btn = (ImageButton)sender;

            IdSelectedSolucion = btn.CommandArgument;

            Presenter.RemoveSolucionReclamo();
        }

        protected void BtnSelectSolucion_Click(object sender, EventArgs e)
        {
            var btn = (ImageButton)sender;

            IdSelectedSolucion = btn.CommandArgument;

            Presenter.LoadSolucionReclamo();
        }

        #endregion

        #region Repeaters

        protected void RptSolucionList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var item = (TBL_ModuloReclamos_Soluciones)(e.Item.DataItem);
                // Bindind data

                var hddIdSolucion = e.Item.FindControl("hddIdSolucion") as HiddenField;
                if (hddIdSolucion != null) hddIdSolucion.Value = string.Format("{0}", item.IdSolucion);

                var lblFechaSolucion = e.Item.FindControl("lblFechaSolucion") as Label;
                if (lblFechaSolucion != null) lblFechaSolucion.Text = string.Format("{0:dd/MM/yyyy hh:mm:ss tt}", item.CreateOn);

                var lblReferenciaSolucion = e.Item.FindControl("lblReferenciaSolucion") as Label;
                if (lblReferenciaSolucion != null) lblReferenciaSolucion.Text = string.Format("{0}", item.Referencia);

                var lblDepartamentoSolucion = e.Item.FindControl("lblDepartamentoSolucion") as Label;
                if (lblDepartamentoSolucion != null) lblDepartamentoSolucion.Text = string.Format("{0}", item.Departamento);

                var lblCreateBy = e.Item.FindControl("lblCreateBy") as Label;
                if (lblCreateBy != null) lblCreateBy.Text = string.Format("{0}", item.TBL_Admin_Usuarios.Nombres);

                var imgDeleteSolucion = e.Item.FindControl("imgDeleteSolucion") as ImageButton;
                if (imgDeleteSolucion != null) imgDeleteSolucion.CommandArgument = string.Format("{0}", item.IdSolucion);

                var imgSelectSolucion = e.Item.FindControl("imgSelectSolucion") as ImageButton;
                if (imgSelectSolucion != null) imgSelectSolucion.CommandArgument = string.Format("{0}", item.IdSolucion);
            }
        }

        #endregion

        #endregion

        #region Methods

        void InitAdminSolucion()
        {
            wddDepartamento.SelectedItemIndex = 0;
            Referencia = string.Empty;
            Observaciones = string.Empty;
            IsNewSolucion = true;
            IdSelectedSolucion = string.Empty;
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

        public void ShowAdminSolucionWindow(bool visible)
        {
            if (visible)
                mpeAdminSolucion.Show();
            else
                mpeAdminSolucion.Hide();
        }

        public void LoadSolucionesReclamo(List<TBL_ModuloReclamos_Soluciones> items)
        {
            rptSolucionList.DataSource = items;
            rptSolucionList.DataBind();
        }

        public void LoadDepartamentos(List<DTO_ValueKey> items)
        {
            if (items.Any())
            {
                items = items.OrderBy(x => x.Value).ToList();
            }

            wddDepartamento.DataSource = items;
            wddDepartamento.TextField = "Value";
            wddDepartamento.ValueField = "Id";
            wddDepartamento.DataBind();

            wddDepartamento.SelectedItemIndex = 0;
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

        public string Departamento
        {
            get
            {
                return wddDepartamento.SelectedValue;
            }
            set
            {
                wddDepartamento.SelectedValue = value;
            }
        }

        public string Referencia
        {
            get
            {
                return txtReferencia.Text;
            }
            set
            {
                txtReferencia.Text = value;
            }
        }

        public string Observaciones
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

        public string IdSelectedSolucion
        {
            get
            {
                return ViewState["AdminSolucion_IdSolucionSelect"] as string;
            }
            set
            {
                ViewState["AdminSolucion_IdSolucionSelect"] = value;
            }
        }

        public bool IsNewSolucion
        {
            get
            {
                if (ViewState["AdminSolucion_IsNewSolucion"] == null)
                    ViewState["AdminSolucion_IsNewSolucion"] = false;

                return Convert.ToBoolean(ViewState["AdminSolucion_IsNewSolucion"]);
            }
            set
            {
                ViewState["AdminSolucion_IsNewSolucion"] = value;
            }
        }

        #endregion        

        #endregion
    }
}