using System;
using Application.Core;
using Application.MainModule.AccionesPC.IServices;
using Applications.MainModule.Admin.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Reclamos.IViews;

namespace Presenters.Reclamos.Presenters
{
    public class EditAreasPresenter : Presenter<IEditAreasViewn>
    {
        private readonly ISfTBL_ModuloAPC_AreasManagementServices _areas;
        private readonly ISfTBL_Admin_UsuariosManagementServices _gerentes;

        public EditAreasPresenter(ISfTBL_Admin_UsuariosManagementServices gerentes,
            ISfTBL_ModuloAPC_AreasManagementServices areas)
        {
            _gerentes = gerentes;
            _areas = areas;
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
            GetGerentes();
            Load();
        }

        void ViewSaveEvent(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(View.IdArea))
                GuardarArea();
        }

        void ViewDeleteEvent(object sender, EventArgs e)
        {
            EliminarArea();
        }

        private void Load()
        {
            if (string.IsNullOrEmpty(View.IdArea)) return;

            var cr = _areas.FindById(Convert.ToInt32(View.IdArea));

            if (cr == null) return;


            View.Nombre = cr.Nombre;
            View.Procesos = cr.Procesos;
            View.IdGerente = cr.IdGerente.ToString();
            View.Activo = cr.IsActive;
            View.CreateBy = cr.TBL_Admin_Usuarios1.Nombres;
            View.CreateOn = cr.CreateOn.ToShortDateString() + " " + cr.CreateOn.ToShortTimeString();
            View.ModifiedBy = cr.TBL_Admin_Usuarios2.Nombres;
            View.ModifiedOn = cr.ModifiedOn.ToShortDateString() + " " + cr.ModifiedOn.ToShortTimeString();
        }

        private void GuardarArea()
        {

            try
            {
                if (string.IsNullOrEmpty(View.IdArea)) return;

                var cr = _areas.FindById(Convert.ToInt32(View.IdArea));

                if (cr == null) return;
                cr.Nombre = View.Nombre;
                cr.Procesos = View.Procesos;
                cr.IdGerente = Convert.ToInt32(View.IdGerente);
                cr.IsActive = View.Activo;
                cr.ModifiedBy = View.UserSession.IdUser;
                cr.ModifiedOn = DateTime.Now;
                _areas.Modify(cr);
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ProcessOk), TypeError.Ok));
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.SaveError), TypeError.Error));
            }

        }

        private void EliminarArea()
        {
            try
            {
                if (View.IdArea == "") return;
                var usuario = _areas.FindById(Convert.ToInt32(View.IdArea));
                if (usuario == null) return;
                _areas.Remove(usuario);
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ProcessOk), TypeError.Ok));
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.DeleteError), TypeError.Error));
            }
        }

        private void GetGerentes()
        {
            var listado = _gerentes.FindBySpec(true);
            View.GetGerentes(listado);
        }

    }
}