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
    public class EditCategoriaReclamosPresenter : Presenter<IEditCategoriaReclamosView>
    {
        private readonly ISfTBL_ModuloReclamos_CategoriasReclamoManagementServices _catReclamos;
        private readonly ISfTBL_Admin_UsuariosManagementServices _responsable;
        private readonly ISfTBL_ModuloReclamos_TipoReclamoManagementServices _tipoReclamo;

        public EditCategoriaReclamosPresenter(ISfTBL_ModuloReclamos_CategoriasReclamoManagementServices catReclamos, ISfTBL_Admin_UsuariosManagementServices responsable, ISfTBL_ModuloReclamos_TipoReclamoManagementServices tipoReclamo)
        {
            _catReclamos = catReclamos;
            _responsable = responsable;
            _tipoReclamo = tipoReclamo;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += ViewLoad;
            View.SaveEvent += ViewSaveEvent;
            View.DeleteEvent += ViewDeleteEvent;
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
            if (!string.IsNullOrEmpty(View.IdCategoriaReclamo))
                GuardarCategoriaReclamo();
        }

        void ViewDeleteEvent(object sender, EventArgs e)
        {
            EliminarCategoriaReclamo();
        }

        private void Load()
        {
            if (string.IsNullOrEmpty(View.IdCategoriaReclamo)) return;

            var cr = _catReclamos.FindById(Convert.ToInt32(View.IdCategoriaReclamo));

            if (cr == null) return;


            View.Nombre = cr.Nombre;
            View.SubCategoria = cr.SubCategoria;
            View.Descripcion = cr.Descripcion;
            View.Area = cr.Area;
            View.IdResponsable = cr.IdResponsable;
            View.GrupoInformacion = cr.GrupoInformacion;
            View.IdTipoReclamo = cr.IdTipoReclamo;
            View.Activo = cr.IsActive;
            View.ModifiedBy = cr.TBL_Admin_Usuarios2.Nombres;
            View.ModifiedOn = cr.ModifiedOn != null ? cr.ModifiedOn.ToShortDateString() : string.Empty;

        }

        /// <summary>
        /// Actualiza una categoria de Reclamos en Base de datos.
        /// </summary>
        private void GuardarCategoriaReclamo()
        {

            try
            {
                if (string.IsNullOrEmpty(View.IdCategoriaReclamo)) return;

                var cr = _catReclamos.FindById(Convert.ToInt32(View.IdCategoriaReclamo));

                if (cr == null) return;
                cr.Nombre = View.Nombre;
                cr.SubCategoria = View.SubCategoria;
                cr.Descripcion = View.Descripcion;
                cr.Area = View.Area;
                cr.IdResponsable = View.IdResponsable;
                cr.GrupoInformacion = View.GrupoInformacion;
                cr.IdTipoReclamo = View.IdTipoReclamo;
                cr.IsActive = View.Activo;
                cr.ModifiedBy = View.UserSession.IdUser;
                cr.ModifiedOn = DateTime.Now;
                _catReclamos.Modify(cr);
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ProcessOk), TypeError.Ok));
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.SaveError), TypeError.Error));
            }

        }

        /// <summary>
        /// Elimina una categoria de Reclamos seleccionada en Base de Datos
        /// </summary>
        private void EliminarCategoriaReclamo()
        {
            try
            {
                if (View.IdCategoriaReclamo == "") return;
                var usuario = _catReclamos.FindById(Convert.ToInt32(View.IdCategoriaReclamo));
                if (usuario == null) return;
                _catReclamos.Remove(usuario);
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ProcessOk), TypeError.Ok));
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.DeleteError), TypeError.Error));
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
