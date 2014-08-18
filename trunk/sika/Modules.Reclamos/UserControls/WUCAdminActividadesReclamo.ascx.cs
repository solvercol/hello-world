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
    public partial class WUCAdminActividadesReclamo : ViewUserControl<AdminActividadesReclamoPresenter, IAdminActividadesReclamoView>, IAdminActividadesReclamoView, IReclamoWebUserControl
    {
        #region Page Events

        #region Load

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        #endregion

        #region Buttons

        protected void BtnAddActividad_Click(object sender, EventArgs e)
        {
            InitAdminActividad();
            ShowAdminActividadWindow(true);
        }

        protected void BtnSaveActividad_Click(object sender, EventArgs e)
        {
            if (IsNewActividad)
                Presenter.AddActividadReclamo();
            else
                Presenter.UpdateActividadReclamo();
        }

        protected void BtnRemoveActividad_Click(object sender, EventArgs e)
        {
            var btn = (ImageButton)sender;

            IdSelectedActividad = btn.CommandArgument;

            Presenter.RemoveActividadReclamo();
        }

        protected void BtnSelectActividad_Click(object sender, EventArgs e)
        {
            var btn = (ImageButton)sender;

            IdSelectedActividad = btn.CommandArgument;

            Presenter.LoadActividadReclamo();
        }

        #endregion

        #region Repeaters

        protected void RptActividadesList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var item = (TBL_ModuloReclamos_Actividades)(e.Item.DataItem);
                // Bindind data

                var hddIdActividad = e.Item.FindControl("hddIdActividad") as HiddenField;
                if (hddIdActividad != null) hddIdActividad.Value = string.Format("{0}", item.IdActividad);

                var lblDescripcion = e.Item.FindControl("lblDescripcion") as Label;
                if (lblDescripcion != null) lblDescripcion.Text = string.Format("{0}", item.Descripcion);

                var lblFechaActividad = e.Item.FindControl("lblFechaActividad") as Label;
                if (lblFechaActividad != null) lblFechaActividad.Text = string.Format("{0:dd/MM/yyyy hh:mm:ss tt}", item.Fecha);

                var lblAsignado = e.Item.FindControl("lblAsignado") as Label;
                if (lblAsignado != null) lblAsignado.Text = string.Format("{0}", item.TBL_Admin_Usuarios2.Nombres);

                var lblAutor = e.Item.FindControl("lblAutor") as Label;
                if (lblAutor != null) lblAutor.Text = string.Format("{0}", item.TBL_Admin_Usuarios.Nombres);

                var imgSelectActividad = e.Item.FindControl("imgSelectActividad") as ImageButton;
                if (imgSelectActividad != null) imgSelectActividad.CommandArgument = string.Format("{0}", item.IdActividad);

                var imgDeleteActividad = e.Item.FindControl("imgDeleteActividad") as ImageButton;
                if (imgDeleteActividad != null) imgDeleteActividad.CommandArgument = string.Format("{0}", item.IdActividad);
            }
        }

        #endregion

        #endregion

        #region Methods

        void InitAdminActividad()
        {
            wddActividadesReclamo.SelectedItemIndex = 0;
            IdUsuarioAsignacion = UserSession.IdUser.ToString();
            Descripcion = string.Empty;
            FechaActividad = DateTime.Now;
            Observaciones = string.Empty;
            IsNewActividad = true;
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

        public void ShowAdminActividadWindow(bool visible)
        {
            if (visible)
                mpeAdminActividad.Show();
            else
                mpeAdminActividad.Hide();
        }

        public void LoadActividadesReclamo(List<TBL_ModuloReclamos_Actividades> items)
        {
            rptActividadesList.DataSource = items;
            rptActividadesList.DataBind();
        }

        public void LoadUsuarioAsignacion(List<TBL_Admin_Usuarios> items)
        {
            if (items.Any())
            {
                items = items.OrderBy(x => x.Nombres).ToList();
            }

            wddUsuarioAsignacion.DataSource = items;
            wddUsuarioAsignacion.TextField = "Nombres";
            wddUsuarioAsignacion.ValueField = "IdUser";
            wddUsuarioAsignacion.DataBind();
        }

        public void LoadActividadesAdmin(List<TBL_ModuloReclamos_ActividadesReclamo> items)
        {
            if (items.Any())
            {
                items = items.OrderBy(x => x.Nombre).ToList();
            }

            wddActividadesReclamo.DataSource = items;
            wddActividadesReclamo.TextField = "Nombre";
            wddActividadesReclamo.ValueField = "IdActividad";
            wddActividadesReclamo.DataBind();
        }

        #endregion

        #region Methods        

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

        public string IdActividadReclamo
        {
            get
            {
                return wddActividadesReclamo.SelectedValue;
            }
            set
            {
                wddActividadesReclamo.SelectedValue = value;
            }
        }

        public string Estado
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
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

        public DateTime FechaActividad
        {
            get
            {
                return Convert.ToDateTime(txtFechaActividad.Text);
            }
            set
            {
                txtFechaActividad.Text = string.Format("{0:dd/MM/yyyy}", value);
            }
        }

        public string IdUsuarioAsignacion
        {
            get
            {
                return wddUsuarioAsignacion.SelectedValue;
            }
            set
            {
                wddUsuarioAsignacion.SelectedValue = value;
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

        public string IdSelectedActividad
        {
            get
            {
                return ViewState["AdminActividad_IdActividadSelect"] as string;
            }
            set
            {
                ViewState["AdminActividad_IdActividadSelect"] = value;
            }
        }

        public bool IsNewActividad
        {
            get
            {
                if (ViewState["AdminActividad_IsNewActividad"] == null)
                    ViewState["AdminActividad_IsNewActividad"] = false;

                return Convert.ToBoolean(ViewState["AdminActividad_IsNewActividad"]);
            }
            set
            {
                ViewState["AdminActividad_IsNewActividad"] = value;
            }
        }

        #endregion

        #endregion        
    }
}