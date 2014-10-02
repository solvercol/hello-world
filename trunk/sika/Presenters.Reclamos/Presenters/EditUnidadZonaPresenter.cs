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
    public class EditUnidadZonaPresenter: Presenter<IEditUnidadZonaView>
    {
        private readonly ISfTBL_ModuloReclamos_UnidadesZonasManagementServices _unidadesZona;


        public EditUnidadZonaPresenter(ISfTBL_ModuloReclamos_UnidadesZonasManagementServices unidadesZona)
        {
            _unidadesZona = unidadesZona;
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
            Load();
        }


        void ViewSaveEvent(object sender, EventArgs e)
        {
                GuardarUnidadZona();
        }

        void ViewDeleteEvent(object sender, EventArgs e)
        {
            EliminarUnidadZona();
        }
        private void Load()
        {
            if ((string.IsNullOrEmpty(View.IdUnidad)) && (string.IsNullOrEmpty(View.IdZona)) && (string.IsNullOrEmpty(View.IdGerente)))
                return;

            var uz = _unidadesZona.FindById(Convert.ToInt32(View.IdUnidad), Convert.ToInt32(View.IdZona), Convert.ToInt32(View.IdGerente));

            if (uz == null) return;
            View.Descripcion = uz.Descripcion;
            View.Unidad = uz.TBL_ModuloReclamos_Unidad.Nombre;
            View.Zona = uz.TBL_ModuloReclamos_Zona.Descripcion;
            View.Gerente = uz.TBL_Admin_Usuarios1.Nombres;
            View.TarifasFletes = uz.TarifaFletes;
            View.Activo = uz.IsActive;
            View.CreateBy = uz.TBL_Admin_Usuarios.Nombres;
            View.CreateOn = uz.CreateOn != null ? uz.CreateOn.ToShortDateString() : "";
            View.ModifiedBy = uz.TBL_Admin_Usuarios2.Nombres;
            View.ModifiedOn = uz.ModifiedOn != null ? uz.ModifiedOn.ToShortDateString() : "";

        }

        /// <summary>
        /// Guarda una Unidad Zona en base de datos.
        /// </summary>
        private void GuardarUnidadZona()
        {

            try
            {
                if ((string.IsNullOrEmpty(View.IdUnidad)) && (string.IsNullOrEmpty(View.IdZona)) && (string.IsNullOrEmpty(View.IdGerente)))
                    return;

                var uz = _unidadesZona.FindById(Convert.ToInt32(View.IdUnidad), Convert.ToInt32(View.IdZona), Convert.ToInt32(View.IdGerente));


                uz.Descripcion = View.Descripcion;
                uz.TarifaFletes = View.TarifasFletes;
                uz.IsActive = View.Activo;
                uz.ModifiedBy = View.UserSession.IdUser;
                uz.ModifiedOn = DateTime.Now;
                _unidadesZona.Modify(uz);
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ProcessOk), TypeError.Ok));
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.SaveError), TypeError.Error));
            }

        }

        private void EliminarUnidadZona()
        {
            try
            {
                if ((string.IsNullOrEmpty(View.IdUnidad)) && (string.IsNullOrEmpty(View.IdZona)) && (string.IsNullOrEmpty(View.IdGerente)))
                    return;

                var uz = _unidadesZona.FindById(Convert.ToInt32(View.IdUnidad), Convert.ToInt32(View.IdZona), Convert.ToInt32(View.IdGerente));
                if (uz == null) return;
                _unidadesZona.Remove(uz);
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ProcessOk), TypeError.Ok));
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.DeleteError), TypeError.Error));
            }
        }
    }
}
