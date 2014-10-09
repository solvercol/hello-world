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
    public class AddUnidadPresenter : Presenter<IAddUnidadView>
    {
        private readonly ISfTBL_ModuloReclamos_UnidadManagementServices _unidad;

        public AddUnidadPresenter(ISfTBL_ModuloReclamos_UnidadManagementServices unidad)
        {
            _unidad = unidad;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += ViewLoad;
            View.SaveEvent += ViewSaveEvent;
        }

        void ViewLoad(object sender, EventArgs e)
        {
            if (View.IsPostBack) return;
            Load();
        }

        void ViewSaveEvent(object sender, EventArgs e)
        {
            GuardarUnidad();
        }

        private void Load()
        {
            initDataUnidad();
        }

        private void initDataUnidad()
        {
            View.Nombre = string.Empty;
            View.Activo = false;
            View.CreateBy = View.UserSession.Nombres;
            View.CreateOn = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
       }

        private void GuardarUnidad()
        {
            try
            {
                var un = _unidad.NewEntity();

                un.Nombre = View.Nombre;
                un.IsActive = View.Activo;
                un.CreateBy = View.UserSession.IdUser;
                un.CreateOn = DateTime.Now;
                un.ModifiedBy = View.UserSession.IdUser;
                un.ModifiedOn = DateTime.Now;
                _unidad.Add(un);
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ProcessOk), TypeError.Ok));
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.SaveError), TypeError.Error));
            }
        }
    }
}
