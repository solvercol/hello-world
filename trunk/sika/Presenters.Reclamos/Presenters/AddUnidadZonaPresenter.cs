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
    public class AddUnidadZonaPresenter : Presenter<IAddUnidadZonaView>
    {
        private readonly ISfTBL_ModuloReclamos_UnidadesZonasManagementServices _unidadesZona;

        private readonly ISfTBL_Admin_UsuariosManagementServices _gerente;

        private readonly ISfTBL_ModuloReclamos_UnidadManagementServices _unidad;

        private readonly ISfTBL_ModuloReclamos_ZonaManagementServices _zona;

        public AddUnidadZonaPresenter(ISfTBL_ModuloReclamos_UnidadesZonasManagementServices unidadesZona,
            ISfTBL_Admin_UsuariosManagementServices gerente,
            ISfTBL_ModuloReclamos_UnidadManagementServices unidad,
            ISfTBL_ModuloReclamos_ZonaManagementServices zona)
        {
            _unidadesZona = unidadesZona;
            _gerente = gerente;
            _unidad = unidad;
            _zona = zona;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += ViewLoad;
            View.SaveEvent += ViewSaveEvent;
        }

        void ViewLoad(object sender, EventArgs e)
        {
            GetUnidades();
            GetZonas();
            if (View.IsPostBack) return;
            GetGerentes();
            Load();
        }

        void ViewSaveEvent(object sender, EventArgs e)
        {
            GuardarUnidadZona();
        }

        private void Load()
        {
            View.Descripcion = string.Empty;
            //View.TarifasFletes = 0;
            View.Activo = false;
            View.CreateBy = View.UserSession.UserName;
            View.CreateOn = DateTime.Now.ToShortDateString();
        }

        /// <summary>
        /// Guarda una Unidad Zona en base de datos.
        /// </summary>
        private void GuardarUnidadZona()
        {

            try
            {
                var uz = _unidadesZona.NewEntity();

                uz.IdUnidad = View.IdUnidad;
                uz.IdZona = View.IdZona;
                uz.Descripcion = View.Descripcion;
                uz.IdGerente = View.IdGerente;
                uz.TarifaFletes = View.TarifasFletes;
                uz.IsActive = View.Activo;
                uz.CreateBy = View.UserSession.IdUser;
                uz.CreateOn = DateTime.Now;
                uz.ModifiedBy = View.UserSession.IdUser;
                uz.ModifiedOn = DateTime.Now;
                _unidadesZona.Add(uz);
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
            try
            {
            var listado = _gerente.FindBySpec(true);
            View.GetGerentes(listado);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.SaveError), TypeError.Error));
            }
        }

        private void GetUnidades()
        {
            try
            {
            var listado = _unidad.FindBySpec(true);
            View.GetUnidades(listado);
            }
             
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.SaveError), TypeError.Error));
            }
        }

        private void GetZonas()
        {
            try
            {
                var listado = _zona.FindBySpec(true);
                View.GetZonas(listado);
            }

            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.SaveError), TypeError.Error));
            }
        }
    }
}
