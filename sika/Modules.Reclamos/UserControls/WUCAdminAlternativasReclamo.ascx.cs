﻿using System;
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
    public partial class WUCAdminAlternativasReclamo : ViewUserControl<AdminAlternativasReclamoPresenter, IAdminAlternativasReclamoView>, IAdminAlternativasReclamoView, IReclamoWebUserControl
    {
        #region Page Events

        #region Load

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        #endregion

        #region Buttons

        protected void BtnAddAlternativa_Click(object sender, EventArgs e)
        {
            InitAdminAlternativa();
            ShowAdminAlternativaWindow(true);
        }

        protected void BtnSaveAlternativa_Click(object sender, EventArgs e)
        {
            if (IsNewAlternativa)
                Presenter.AddAlternativaReclamo();
            else
                Presenter.UpdateAlternativaReclamo();
        }

        protected void BtnRemoveAlternativa_Click(object sender, EventArgs e)
        {
            var btn = (ImageButton)sender;

            IdSelectedAlternativa = btn.CommandArgument;

            Presenter.RemoveAlternativaReclamo();
        }

        protected void BtnSelectAlternativa_Click(object sender, EventArgs e)
        {
            var btn = (ImageButton)sender;

            IdSelectedAlternativa = btn.CommandArgument;

            Presenter.LoadAlternativaReclamo();
        }

        #endregion

        #region Repeaters

        protected void RptAlternativasList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var item = (TBL_ModuloReclamos_Alternativas)(e.Item.DataItem);
                // Bindind data

                var hddIdAlternativa = e.Item.FindControl("hddIdAlternativa") as HiddenField;
                if (hddIdAlternativa != null) hddIdAlternativa.Value = string.Format("{0}", item.IdAlternativa);

                var lblAlternativa = e.Item.FindControl("lblAlternativa") as Label;
                if (lblAlternativa != null) lblAlternativa.Text = string.Format("{0}", item.Alternativa);

                var lblFechaAlternativa = e.Item.FindControl("lblFechaAlternativa") as Label;
                if (lblFechaAlternativa != null) lblFechaAlternativa.Text = string.Format("{0:dd/MM/yyyy hh:mm:ss tt}", item.FechaAlternativa);

                var lblResponable = e.Item.FindControl("lblResponable") as Label;
                if (lblResponable != null) lblResponable.Text = string.Format("{0}", item.TBL_Admin_Usuarios2.Nombres);

                var lblEstado = e.Item.FindControl("lblEstado") as Label;
                if (lblEstado != null) lblEstado.Text = string.Format("{0}", item.Estado);

                var lblSeguimiento = e.Item.FindControl("lblSeguimiento") as Label;
                if (lblSeguimiento != null) lblSeguimiento.Text = string.Format("{0}", item.Seguimiento);

                var imgDeleteAlternativa = e.Item.FindControl("imgDeleteAlternativa") as ImageButton;
                if (imgDeleteAlternativa != null) imgDeleteAlternativa.CommandArgument = string.Format("{0}", item.IdAlternativa);

                var imgSelectAlternativa = e.Item.FindControl("imgSelectAlternativa") as ImageButton;
                if (imgSelectAlternativa != null) imgSelectAlternativa.CommandArgument = string.Format("{0}", item.IdAlternativa);
            }
        }

        #endregion

        #endregion

        #region Methods

        void InitAdminAlternativa()
        {
            Causas = string.Empty;
            Factores = string.Empty;
            Alternativa = string.Empty;
            IdResponsable = UserSession.IdUser.ToString();
            FechaAlternativa = DateTime.Now;
            Seguimiento = string.Empty;
            IsNewAlternativa = true;
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

        public void ShowAdminAlternativaWindow(bool visible)
        {
            if (visible)
                mpeAdminAlternativa.Show();
            else
                mpeAdminAlternativa.Hide();
        }

        public void LoadAlternativasReclamo(List<TBL_ModuloReclamos_Alternativas> items)
        {
            rptAlternativasList.DataSource = items;
            rptAlternativasList.DataBind();
        }

        public void LoadResponsables(List<TBL_Admin_Usuarios> items)
        {
            if (items.Any())
            {
                items = items.OrderBy(x => x.Nombres).ToList();
            }

            wddResponsable.DataSource = items;
            wddResponsable.TextField = "Nombres";
            wddResponsable.ValueField = "IdUser";
            wddResponsable.DataBind();
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

        public string Alternativa
        {
            get
            {
                return txtAlternativa.Text;
            }
            set
            {
                txtAlternativa.Text = value;
            }
        }

        public string Causas
        {
            get
            {
                return txtCausas.Text;
            }
            set
            {
                txtCausas.Text = value;
            }
        }

        public string Factores
        {
            get
            {
                return txtFactores.Text;
            }
            set
            {
                txtFactores.Text = value;
            }
        }

        public string IdResponsable
        {
            get
            {
                return wddResponsable.SelectedValue;
            }
            set
            {
                wddResponsable.SelectedValue = value;
            }
        }

        public DateTime FechaAlternativa
        {
            get
            {
                return Convert.ToDateTime(txtFechaAlternativa.Text);
            }
            set
            {
                txtFechaAlternativa.Text = string.Format("{0:dd/MM/yyy}", value);
            }
        }

        public string Seguimiento
        {
            get
            {
                return txtSeguimiento.Text;
            }
            set
            {
                txtSeguimiento.Text = value;
            }
        }

        public string Estado
        {
            get
            {
                return "Creado";
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string IdSelectedAlternativa
        {
            get
            {
                return ViewState["AdminAlternativa_IdAlternativaSelect"] as string;
            }
            set
            {
                ViewState["AdminAlternativa_IdAlternativaSelect"] = value;
            }
        }

        public bool IsNewAlternativa
        {
            get
            {
                if (ViewState["AdminAlternativa_IsNewAlternativa"] == null)
                    ViewState["AdminAlternativa_IsNewAlternativa"] = false;

                return Convert.ToBoolean(ViewState["AdminAlternativa_IsNewAlternativa"]);
            }
            set
            {
                ViewState["AdminAlternativa_IsNewAlternativa"] = value;
            }
        }

        #endregion        

        #endregion
    }
}