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
    public class EditActividadReclamosPresenter : Presenter<IEditActividadReclamosView>
    {
        private readonly ISfTBL_ModuloReclamos_ActividadesReclamoManagementServices _actReclamos;

        private readonly ISfTBL_ModuloReclamos_TipoReclamoManagementServices _tipoReclamo;

        public EditActividadReclamosPresenter(ISfTBL_ModuloReclamos_ActividadesReclamoManagementServices actReclamos, ISfTBL_ModuloReclamos_TipoReclamoManagementServices tipoReclamo)
        {
            _tipoReclamo = tipoReclamo;
            _actReclamos = actReclamos;
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
            GetTipoReclamos();
            Load();
        }

        void ViewSaveEvent(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(View.IdActividadReclamo))
                GuardarActividadReclamo();
        }

        void ViewDeleteEvent(object sender, EventArgs e)
        {
            EliminarActividadReclamo();
        }

        private void Load()
        {
            if (string.IsNullOrEmpty(View.IdActividadReclamo)) return;

            var cr = _actReclamos.FindById(Convert.ToInt32(View.IdActividadReclamo));

            if (cr == null) return;


            View.Nombre = cr.Nombre;
            View.Descripcion = cr.Descripcion;
            View.IdTipoReclamo = cr.IdTipoReclamo;
            View.Activo = cr.IsActive;
            View.CreateBy = cr.TBL_Admin_Usuarios.Nombres;
            View.CreateOn = cr.CreateOn != null ? cr.CreateOn.ToShortDateString() + " " + cr.CreateOn.ToShortTimeString() : "";
            View.ModifiedBy = cr.TBL_Admin_Usuarios1.Nombres;
            View.ModifiedOn = cr.ModifiedOn != null ? cr.ModifiedOn.ToShortDateString()+" "+cr.ModifiedOn.ToShortTimeString(): "";

        }

        /// <summary>
        /// Actualiza una actividad de Reclamos en Base de datos.
        /// </summary>
        private void GuardarActividadReclamo()
        {

            try
            {
                if (string.IsNullOrEmpty(View.IdActividadReclamo)) return;

                var cr = _actReclamos.FindById(Convert.ToInt32(View.IdActividadReclamo));

                if (cr == null) return;
                cr.Nombre = View.Nombre;
                cr.Descripcion = View.Descripcion;
                cr.IdTipoReclamo = View.IdTipoReclamo;
                cr.IsActive = View.Activo;
                cr.ModifiedBy = View.UserSession.IdUser;
                cr.ModifiedOn = DateTime.Now;
                _actReclamos.Modify(cr);
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ProcessOk), TypeError.Ok));
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.SaveError), TypeError.Error));
            }

        }

        /// <summary>
        /// Elimina una actividad de Reclamos seleccionada en Base de Datos
        /// </summary>
        private void EliminarActividadReclamo()
        {
            try
            {
                if (View.IdActividadReclamo == "") return;
                var usuario = _actReclamos.FindById(Convert.ToInt32(View.IdActividadReclamo));
                if (usuario == null) return;
                _actReclamos.Remove(usuario);
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ProcessOk), TypeError.Ok));
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.DeleteError), TypeError.Error));
            }
        }

        private void GetTipoReclamos()
        {
            var listado = _tipoReclamo.FindBySpec(true);
            View.GetTipoReclamos(listado);
        }

    }
}
