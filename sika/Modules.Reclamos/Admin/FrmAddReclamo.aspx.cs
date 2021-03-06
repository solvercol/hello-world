﻿using System;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.Reclamos.IViews;
using Presenters.Reclamos.Presenters;

namespace Modules.Reclamos.Admin
{
    public partial class FrmAddReclamo : ViewPage<AddReclamoPresenter, IAddReclamoView>, IAddReclamoView
    {
        #region Members

        public const string ROOTUC = "../UserControls/";

        #endregion

        #region Page Events

        #region Load

        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(IdReclamo))
                ImprimirTituloVentana("Registrar Nuevo Reclamo");
            else
                ImprimirTituloVentana("Editar Reclamo");

            LoadInitReclamoControl();
        }

        void LoadInitReclamoControl()
        {
            var controlPath = "";
            var idUc = "";

            if (TipoReclamo == "Producto")
            {
                controlPath = string.Format("{0}WUCAdminReclamoProducto.ascx", ROOTUC);
                idUc = "WUCAdminReclamoProducto";
            }
            else
            {
                controlPath = string.Format("{0}WUCAdminRecServicioT{1}.ascx", ROOTUC, GrupoInformacion);

                idUc = "WUCAdminRecServicioT";
            }

            phlCreateReclamo.Controls.Clear();
            var uc = LoadControl(controlPath);
            uc.ID = idUc;
            phlCreateReclamo.Controls.Add(uc);
        }

        #endregion

        #endregion

        #region Methods
        #endregion

        #region View Members

        #region Methods
        #endregion

        #region Memebers        

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
                return Request.QueryString.Get("tr");
            }
        }

        public string IdCategoria
        {
            get
            {
                return Request.QueryString.Get("cat");
            }
        }

        public string GrupoInformacion
        {
            get
            {
                return Request.QueryString.Get("gruinf");
            }
        }

        public string IdReclamo
        {
            get
            {
                return Request.QueryString.Get("IdReclamo");
            }
        }

        #endregion

        #endregion
    }
}