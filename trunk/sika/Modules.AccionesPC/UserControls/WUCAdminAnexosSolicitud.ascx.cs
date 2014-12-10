using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Core;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.AccionesPC.IViews;
using Presenters.AccionesPC.Presenters;
using Modules.AccionesPC.UI;

namespace Modules.AccionesPC.UserControls
{
    public partial class WUCAdminAnexosSolicitud : ViewUserControl<AdminAnexosSolicitudPresenter, IAdminAnexosSolicitudView>, IAdminAnexosSolicitudView, ISolicitudWebUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string IdSolicitud
        {
            get { return Request.QueryString.Get("IdSolicitud"); }
        }

        public TBL_Admin_Usuarios UserSession
        {
            get { return AuthenticatedUser; }
        }

        public string IdModule
        {
            get { return ModuleId; }
        }

        public void LoadControlData()
        {
            Presenter.LoadInitData();
        }

        public event Action RiseFatherPostback;

        public byte[] ArchivoAdjunto
        {
            get { return fupAnexoArchivoSolicitud.FileBytes; }
        }

        public string NombreArchivoAdjunto
        {
            get { return fupAnexoArchivoSolicitud.FileName; }
        }

        public void LoadArchivosAdjuntos(List<DTO_ValueKey> items)
        {
            rptArchivosAdjuntosSolicitud.DataSource = items;
            rptArchivosAdjuntosSolicitud.DataBind();
        }

        public void DescargarArchivo(DTO_ValueKey archivo)
        {
            DownloadDocument((byte[])archivo.ComplexValue, archivo.Value, "application/octet-stream");
        }

        #region Buttons

        protected void BtnAddArchivoAdjunto_Click(object sender, EventArgs e)
        {
            if (!fupAnexoArchivoSolicitud.HasFile)
            {
                return;
            }

            Presenter.AddArchivoAdjunto();
        }

        protected void BtnRemoveArchivoAdjunto_Click(object sender, EventArgs e)
        {
            var btn = (ImageButton)sender;

            var IdArchivo = Convert.ToDecimal(btn.CommandArgument);

            Presenter.RemoveArchivoAdjunto(IdArchivo);
        }

        protected void BtnDownloadArchivoAdjunto_Click(object sender, EventArgs e)
        {
            var btn = (LinkButton)sender;

            var IdArchivo = Convert.ToDecimal(btn.CommandArgument);

            Presenter.DownloadArchivoAdjunto(IdArchivo);
        }

        #endregion

        #region Repeaters

        protected void RptArchivosAdjuntos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var item = (DTO_ValueKey)(e.Item.DataItem);
                // Bindind data

                var hddIdArchivo = e.Item.FindControl("hddIdArchivoSolicitud") as HiddenField;
                if (hddIdArchivo != null) hddIdArchivo.Value = string.Format("{0}", item.Id);

                var lnkNombreArchivo = e.Item.FindControl("lnkNombreArchivoSolicitud") as LinkButton;
                if (lnkNombreArchivo != null)
                {
                    lnkNombreArchivo.Text = string.Format("{0}", item.Value);
                    lnkNombreArchivo.CommandArgument = string.Format("{0}", item.Id);

                    ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                    scriptManager.RegisterPostBackControl(lnkNombreArchivo);
                }

                var imgDeleteAnexo = e.Item.FindControl("imgDeleteAnexoSolicitud") as ImageButton;
                if (imgDeleteAnexo != null)
                {
                    imgDeleteAnexo.CommandArgument = string.Format("{0}", item.Id);
                }
            }
        }

        public bool CanAddAnexos
        {
            get
            {
                return trAddAnexos.Visible;
            }
            set
            {
                trAddAnexos.Visible = value;

                //if (value)
                //{
                //    ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                //    scriptManager.RegisterPostBackControl(btnAddArchivoAdjuntoSolicitud);
                //}
            }
        }

        #endregion


        
    }
}