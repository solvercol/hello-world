using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Application.Core;
using Applications.MainModule.Admin.IServices;
using Application.MainModule.Reclamos.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Reclamos.IViews;
using System.Collections;
using System.Linq;

namespace Presenters.Reclamos.Presenters
{
    public class AddCategoriaReclamosPresenter : Presenter<IAddCategoriasReclamosView>
    {
        private readonly ISfTBL_ModuloReclamos_CategoriasReclamoManagementServices _catReclamos;

        private readonly ISfTBL_Admin_UsuariosManagementServices _responsable;

        private readonly ISfTBL_ModuloReclamos_TipoReclamoManagementServices _tipoReclamo;

        public AddCategoriaReclamosPresenter(ISfTBL_ModuloReclamos_CategoriasReclamoManagementServices catReclamos, ISfTBL_Admin_UsuariosManagementServices responsable, ISfTBL_ModuloReclamos_TipoReclamoManagementServices tipoReclamo)
        {
            _responsable = responsable;
            _tipoReclamo = tipoReclamo;
            _catReclamos = catReclamos;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += ViewLoad;
            View.SaveEvent += ViewSaveEvent;
        }

        void ViewLoad(object sender, EventArgs e)
        {
            if (View.IsPostBack) return;
            GetResponsables();
            GetTipoReclamos();
            Load();
        }

        void ViewSaveEvent(object sender, EventArgs e)
        {
                GuardarCategoriaReclamo();
        }

        private void Load()
        {
            View.Nombre = string.Empty;
            View.SubCategoria = string.Empty;
            View.Descripcion = string.Empty;
            View.Area = string.Empty;
            View.GrupoInformacion = 0;
            View.Activo = false;
            View.CreateBy = View.UserSession.Nombres;
            View.CreateOn = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
        }

        /// <summary>
        /// Guarda una categoria de Reclamos en Base de datos.
        /// </summary>
        private void GuardarCategoriaReclamo()
        {

            try
            {
                var cr = _catReclamos.NewEntity();
                cr.Nombre = View.Nombre;
                cr.SubCategoria = View.SubCategoria;
                cr.Descripcion = View.Descripcion;
                cr.Area = View.Area;
                cr.IdResponsable = View.IdResponsable;
                cr.GrupoInformacion = View.GrupoInformacion;
                cr.IdTipoReclamo = View.IdTipoReclamo;
                cr.IsActive = View.Activo;
                cr.CreateBy = View.UserSession.IdUser;
                cr.CreateOn = DateTime.Now;
                cr.ModifiedBy = View.UserSession.IdUser;
                cr.ModifiedOn = DateTime.Now;
                _catReclamos.Add(cr);
                View.IdCategoriaReclamo = cr.IdCategoriaReclamo.ToString();
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ProcessOk), TypeError.Ok));
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.SaveError), TypeError.Error));
            }

        }

        private void GetResponsables()
        {
            var listado = _responsable.FindBySpec(true);
            View.GetResponsables(listado);
        }

        private void GetTipoReclamos()
        {
            var listado = _tipoReclamo.FindBySpec(true);
            View.GetTipoReclamos(listado);
        }
    }
}
