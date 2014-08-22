using System;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.Reclamos.IViews;
using Presenters.Reclamos.Presenters;
using Modules.Reclamos.UI;

namespace Modules.Reclamos.UserControls
{
    public partial class WUCReadingReclamoServicioT5Detalle : ViewUserControl<ReadingReclamoServicioT5DetallePresenter, IReadingReclamoServicioT5DetalleView>, IReadingReclamoServicioT5DetalleView, IReclamoWebUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region IReclamoWebUserControl

        public void LoadControlData()
        {
            Presenter.LoadInitData();
        }

        #endregion

        public string IdReclamo
        {
            get { return Request.QueryString.Get("IdReclamo"); }
        }

        public TBL_Admin_Usuarios UserSession
        {
            get { return AuthenticatedUser; }
        }

        public string IdModule
        {
            get { return ModuleId; }
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
                return lblPlanta.Text;
            }
            set
            {
                lblPlanta.Text = value;
            }
        }

        public string AtendidoPor
        {
            get
            {
                return lblAtendidoPor.Text;
            }
            set
            {
                lblAtendidoPor.Text = value;
            }
        }

        public string QuienReclama
        {
            get
            {
                return lblQuienReclama.Text;
            }
            set
            {
                lblQuienReclama.Text = value;
            }
        }

        public int NoRecordatorios
        {
            get
            {
                return 0;
            }
            set
            {
                lblNoRecordatorios.Text = string.Format("{0}", value);
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

        public string ProcedimientoInternoAfectado
        {
            get
            {
                return lblProcedimientoAfectado.Text;
            }
            set
            {
                lblProcedimientoAfectado.Text = value;
            }
        }

        public string AreaIncumpleProcedimiento
        {
            get
            {
                return lblAreaIncumple.Text;
            }
            set
            {
                lblAreaIncumple.Text = value;
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
    }
}