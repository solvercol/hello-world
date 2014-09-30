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
    public class AddActividadReclamosPresenter : Presenter<IAddActividadReclamosView>
    {
        private readonly ISfTBL_ModuloReclamos_ActividadesReclamoManagementServices _actReclamos;

        private readonly ISfTBL_ModuloReclamos_TipoReclamoManagementServices _tipoReclamo;

        public AddActividadReclamosPresenter(ISfTBL_ModuloReclamos_ActividadesReclamoManagementServices actReclamos, ISfTBL_ModuloReclamos_TipoReclamoManagementServices tipoReclamo)
        {
            _tipoReclamo = tipoReclamo;
            _actReclamos = actReclamos;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += ViewLoad;
            View.SaveEvent += ViewSaveEvent;
        }

        void ViewLoad(object sender, EventArgs e)
        {
            if (View.IsPostBack) return;
            GetTipoReclamos();
            Load();
        }

        void ViewSaveEvent(object sender, EventArgs e)
        {
            GuardarActividadReclamo();
        }

        private void Load()
        {
            View.Nombre = string.Empty;
            View.Descripcion = string.Empty;
            View.Activo = false;
            View.CreateBy = View.UserSession.UserName;
            View.CreateOn = DateTime.Now.ToShortDateString();
        }

        /// <summary>
        /// Guarda una actividad de Reclamos en Base de datos.
        /// </summary>
        private void GuardarActividadReclamo()
        {

            try
            {
                var ac = _actReclamos.NewEntity();
                ac.Nombre = View.Nombre;
                ac.Descripcion = View.Descripcion;
                ac.IdTipoReclamo = View.IdTipoReclamo;
                ac.IsActive = View.Activo;
                ac.CreateBy = View.UserSession.IdUser;
                ac.CreateOn = DateTime.Now;
                ac.ModifiedBy = View.UserSession.IdUser;
                ac.ModifiedOn = DateTime.Now;
                _actReclamos.Add(ac);
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ProcessOk), TypeError.Ok));
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.SaveError), TypeError.Error));
            }

        }

        private void GetTipoReclamos()
        {
            var listado = _tipoReclamo.FindBySpec(true);
            View.GetTipoReclamos(listado);
        }
    }
}
