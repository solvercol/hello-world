using System;
using Application.Core;
using Application.MainModule.AccionesPC.IServices;
using Applications.MainModule.Admin.IServices;
using Application.MainModule.Reclamos.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.Reclamos.IViews;

namespace Presenters.Reclamos.Presenters
{
    public class AddAreaPresenter : Presenter<IAddAreasView>
    {
        private readonly ISfTBL_ModuloAPC_AreasManagementServices _areas;
        private readonly ISfTBL_Admin_UsuariosManagementServices _gerentes;
        private readonly ISfTBL_Admin_OptionListManagementServices _optionList;

        public AddAreaPresenter(ISfTBL_Admin_UsuariosManagementServices gerentes,
            ISfTBL_ModuloAPC_AreasManagementServices areas,
            ISfTBL_Admin_OptionListManagementServices optionList)
        {
            _gerentes = gerentes;
            _areas = areas;
            _optionList = optionList;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += ViewLoad;
            View.SaveEvent += ViewSaveEvent;
        }

        void ViewLoad(object sender, EventArgs e)
        {
            if (View.IsPostBack) return;
            GetGerentes();
            Load();
        }

        void ViewSaveEvent(object sender, EventArgs e)
        {
            GuardarArea();
        }


        private void Load()
        {
            View.Nombre = string.Empty;
            View.Proceso = string.Empty;
            View.Activo = false;
            View.CreateBy = View.UserSession.Nombres;
            View.CreateOn = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();

            var mensajeMultivalor = _optionList.ObtenerOpcionBykey("MensajeCamposMultivalor");
            if (mensajeMultivalor == null) return;
            View.MensajeMultivalor = mensajeMultivalor.Value;
        }

        private void GuardarArea()
        {

            try
            {
                var cr = _areas.NewEntity();
                cr.Nombre = View.Nombre;
                cr.Procesos = View.Proceso;
                cr.IsActive = View.Activo;
                cr.IdGerente = Convert.ToInt32(View.IdGerente);
                cr.CreateBy = View.UserSession.IdUser;
                cr.CreateOn = DateTime.Now;
                cr.ModifiedBy = View.UserSession.IdUser;
                cr.ModifiedOn = DateTime.Now;
                _areas.Add(cr);
    
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ProcessOk), TypeError.Ok));
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.SaveError), TypeError.Error));
            }

        }

        private void GetGerentes()
        {
            var listado = _gerentes.FindBySpec(true);
            View.GetGerentes(listado);
        }
    }
}