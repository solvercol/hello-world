﻿using System;
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
using Domain.MainModule.Reclamos.DTO;
using Domain.MainModule.Reclamos.Enum;
using Application.Core;

namespace Modules.Reclamos.UserControls
{
    public partial class WUCAdminRecServicioT5 : ViewUserControl<AdminRecServicioT5Presenter, IAdminRecServicioT5View>, IAdminRecServicioT5View
    {
        #region Members

        public string FromPage
        {
            get
            {
                return Request.QueryString["from"];
            }
        }

        #endregion

        #region Page Events

        #region Load

        protected void Page_Load(object sender, EventArgs e)
        {
            IdTipoReclamo = TipoReclamo.Servicio;
        }

        #endregion

        #region Buttons

        protected void BtnRegresar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(IdReclamo))
            {
                switch (FromPage)
                {
                    case "pendientes":
                        Response.Redirect(string.Format("../Views/FrmMisPendientes.aspx?ModuleId={0}", ModuleId));
                        break;
                    case "misfecha":
                        Response.Redirect(string.Format("../Views/FrmMisReclamosPorFecha.aspx?ModuleId={0}", ModuleId));
                        break;
                    case "misestado":
                        Response.Redirect(string.Format("../Views/FrmMisReclamosPorEstado.aspx?ModuleId={0}", ModuleId));
                        break;
                    case "rectipo":
                        Response.Redirect(string.Format("../Views/FrmReclamosPorTipo.aspx?ModuleId={0}", ModuleId));
                        break;
                    case "recestado":
                        Response.Redirect(string.Format("../Views/FrmReclamosPorEstado.aspx?ModuleId={0}", ModuleId));
                        break;
                    case "recnumero":
                        Response.Redirect(string.Format("../Views/FrmReclamosPorNumero.aspx?ModuleId={0}", ModuleId));
                        break;
                    case "rectargetmarket":
                        Response.Redirect(string.Format("../Views/FrmReclamosPorTargetMarket.aspx?ModuleId={0}", ModuleId));
                        break;
                }
            }
            else
                Response.Redirect(string.Format("../Admin/FrmReclamo.aspx?ModuleId={0}&IdReclamo={1}", ModuleId, IdReclamo));
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(IdReclamo))
                Presenter.SaveReclamo();
            else
                Presenter.UpdateReclamo();
        }

        #endregion

        #endregion

        #region Methods

        void WucPostBackEvent()
        {
            Secciones.SelectedIndex = 1;
        }

        void WucClientSelectEvent(Dto_Cliente cliente)
        {
            UnidadZona = cliente.Unidad;
        }

        #endregion

        #region View Members

        #region Methods

        public void GoToReclamoView(string idReclamo)
        {
            Response.Redirect(string.Format("../Admin/FrmReclamo.aspx?ModuleId={0}&IdReclamo={1}", IdModule, idReclamo));
        }

        public void LoadQuienReclama(List<TBL_Admin_Usuarios> items)
        {
            if (items.Any())
            {
                items = items.OrderBy(x => x.Nombres).ToList();
            }
            wddQuienReclama.DataSource = items;
            wddQuienReclama.TextField = "Nombres";
            wddQuienReclama.ValueField = "IdUser";
            wddQuienReclama.DataBind();
        }

        public void LoadAtendidoPor(List<TBL_Admin_Usuarios> items)
        {
            if (items.Any())
            {
                items = items.OrderBy(x => x.Nombres).ToList();
            }
            wddReclamoAtentidoPor.DataSource = items;
            wddReclamoAtentidoPor.TextField = "Nombres";
            wddReclamoAtentidoPor.ValueField = "IdUser";
            wddReclamoAtentidoPor.DataBind();
        }

        public void LoadPlantas(List<DTO_ValueKey> items)
        {
            if (items.Any())
            {
                items = items.OrderBy(x => x.Value).ToList();
            }
            wddPlanta.DataSource = items;
            wddPlanta.TextField = "Id";
            wddPlanta.ValueField = "Value";
            wddPlanta.DataBind();

            wddPlanta.SelectedItemIndex = 0;
        }

        public void LoadAreaIncumpleProcedimiento(List<DTO_ValueKey> items)
        {
            if (items.Any())
            {
                items = items.OrderBy(x => x.Value).ToList();
            }
            wddAreaIncumpleProcedimiento.DataSource = items;
            wddAreaIncumpleProcedimiento.TextField = "Id";
            wddAreaIncumpleProcedimiento.ValueField = "Value";
            wddAreaIncumpleProcedimiento.DataBind();

            wddAreaIncumpleProcedimiento.SelectedItemIndex = 0;
        }

        public void LoadProcedimientoInternoAfectado(List<DTO_ValueKey> items)
        {
            if (items.Any())
            {
                items = items.OrderBy(x => x.Value).ToList();
            }
            wddProcedimientoInternoAfectado.DataSource = items;
            wddProcedimientoInternoAfectado.TextField = "Id";
            wddProcedimientoInternoAfectado.ValueField = "Value";
            wddProcedimientoInternoAfectado.DataBind();

            wddProcedimientoInternoAfectado.SelectedItemIndex = 0;
        }

        #endregion

        #region Members

        public TBL_Admin_Usuarios UserSession
        {
            get { return AuthenticatedUser; }
        }

        public string IdModule
        {
            get { return ModuleId; }
        }

        public string IdAtendidoPor
        {
            get
            {
                return wddReclamoAtentidoPor.SelectedValue;
            }
            set
            {
                wddReclamoAtentidoPor.SelectedValue = value;
            }
        }

        public int NoRecordatorios
        {
            get
            {
                return txtNoRecordatorios.ValueInt;
            }
            set
            {
                txtNoRecordatorios.ValueInt = value;
            }
        }

        public string UnidadZona
        {
            get
            {
                return lblUnidadZona.Text;
            }
            set
            {
                lblUnidadZona.Text = value;
            }
        }

        public string DescripcionProblema
        {
            get
            {
                return txtDescripcionProblema.Text;
            }
            set
            {
                txtDescripcionProblema.Text = value;
            }
        }        

        public string MensajeDescripcionProblema
        {
            get
            {
                return lblMensajeDescripcionProblema.Text;
            }
            set
            {
                lblMensajeDescripcionProblema.Text = value;
            }
        }

        public string ConsecutivoReclamo
        {
            get
            {
                if (ViewState["AdminRecServicio_ConsecutivoReclamo"] == null)
                    ViewState["AdminRecServicio_ConsecutivoReclamo"] = string.Empty;

                return ViewState["AdminRecServicio_ConsecutivoReclamo"].ToString();
            }
            set
            {
                ViewState["AdminRecServicio_ConsecutivoReclamo"] = value;
            }
        }

        public int IdTipoReclamo
        {
            get
            {
                if (ViewState["AdminRecServicio_IdTipoReclamo"] == null)
                    ViewState["AdminRecServicio_IdTipoReclamo"] = TipoReclamo.Servicio;

                return Convert.ToInt32(ViewState["AdminRecServicio_IdTipoReclamo"]);
            }
            set
            {
                ViewState["AdminRecServicio_IdTipoReclamo"] = value;
            }
        }

        public string IdCategoriaReclamo
        {
            get
            {
                if (ViewState["AdminRecServicio_IdCategoriaReclamo"] == null)
                    ViewState["AdminRecServicio_IdCategoriaReclamo"] = Request.QueryString.Get("cat");

                return ViewState["AdminRecServicio_IdCategoriaReclamo"].ToString();
            }
            set
            {
                ViewState["AdminRecServicio_IdCategoriaReclamo"] = value;
            }
        }

        public string CategoriaReclamo
        {
            get
            {
                return lblCategoriaReclamo.Text;
            }
            set
            {
                lblCategoriaReclamo.Text = value;
            }
        }

        public string Area
        {
            get
            {
                return lblArea.Text;
            }
            set
            {
                lblArea.Text = value;
            }
        }

        public string Planta
        {
            get
            {
                return wddPlanta.SelectedValue;
            }
            set
            {
                wddPlanta.SelectedValue = value;
            }
        }

        public string QuienReclama
        {
            get
            {
                return wddQuienReclama.SelectedValue;
            }
            set
            {
                wddQuienReclama.SelectedValue = value;
            }
        }

        public string NombreQuienReclama
        {
            get
            {
                return wddQuienReclama.SelectedItem.Text;
            }
        }

        public string ProcedimientoInternoAfectado
        {
            get
            {
                return wddProcedimientoInternoAfectado.SelectedValue;
            }
            set
            {
                wddProcedimientoInternoAfectado.SelectedValue = value;
            }
        }

        public string AreaIncumpleProcedimiento
        {
            get
            {
                return wddAreaIncumpleProcedimiento.SelectedValue;
            }
            set
            {
                wddAreaIncumpleProcedimiento.SelectedValue = value;
            }
        }

        public string IdReclamo
        {
            get { return Request.QueryString.Get("IdReclamo"); }
        }

        #endregion

        #endregion
    }
}