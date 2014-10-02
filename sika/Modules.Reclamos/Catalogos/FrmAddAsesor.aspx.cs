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
    public partial class FrmAddAsesor : ViewPage<AddAsesorPresenter, IAddAsesorView>, IAddAsesorView
    {
        #region Delegates

        public event EventHandler SaveEvent;

        #endregion

        #region Load

        protected void Page_Load(object sender, EventArgs e)
        {
            ImprimirTituloVentana("Adicionar asesor");
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
            {
                SaveEvent(null, EventArgs.Empty);
                if (this.InsertFlag)
                {
                    Response.Redirect(string.Format("FrmViewAsesor.aspx{0}&UserId={1}", GetBaseQueryString(), this.IdUser));
                }
                else
                {
                    ShowError("El usuario ya se encuentra registrado como asesor");
                }
            }
        }

        protected void BtnBackClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("FrmAdminAsesores.aspx{0}", GetBaseQueryString()));
        }

        protected void BtnAddUsuarioCopia_Click(object sender, EventArgs e)
        {
            var usuarioCopia = new DTO_ValueKey() { Id = IdUsuarioCopia, Value = wddJefes.SelectedItem.Text };
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

        public void GetJefes(IList<TBL_Admin_Usuarios> items)
        {
            if (items.Any())
            {
                items = items.OrderBy(x => x.Nombres).ToList();
            }
            wddJefes.DataSource = items;
            wddJefes.TextField = "Nombres";
            wddJefes.ValueField = "IdUser";
            wddJefes.DataBind();
        }

        public void GetAsesores(IList<TBL_Admin_Usuarios> items)
        {
            if (items.Any())
            {
                items = items.OrderBy(x => x.Nombres).ToList();
            }
            wddAsesor.DataSource = items;
            wddAsesor.TextField = "Nombres";
            wddAsesor.ValueField = "IdUser";
            wddAsesor.DataBind();
        }

        public void GetUnidades(IList<TBL_ModuloReclamos_Unidad> items)
        {
            if (items.Any())
            {
                items = items.OrderBy(x => x.Nombre).ToList();
            }

            wddUnidad.DataSource = items;
            wddUnidad.TextField = "Nombre";
            wddUnidad.ValueField = "IdUnidad";
            wddUnidad.DataBind();
        }

        public void GetZonas(IList<TBL_ModuloReclamos_Zona> items)
        {
            if (items.Any())
            {
                items = items.OrderBy(x => x.Descripcion).ToList();
            }

            wddZona.DataSource = items;
            wddZona.TextField = "Descripcion";
            wddZona.ValueField = "IdZona";
            wddZona.DataBind();
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
                return wddJefes.SelectedValue;
            }
            set
            {
                wddJefes.SelectedValue = value;
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


        public string IdUser
        {
            get { return wddAsesor.SelectedValue; }
            set{}
        }

        public string IdUnidad
        {
            get { return wddUnidad.SelectedValue; }
        }

        public string IdZona
        {
            get { return wddZona.SelectedValue; }
        }

        public bool InsertFlag
        {
            get;
            set;
        }



        #endregion

    }
}