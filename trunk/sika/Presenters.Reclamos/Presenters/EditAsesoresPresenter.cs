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
using System.Collections.Generic;
namespace Presenters.Reclamos.Presenters
{
    public class EditAsesoresPresenter: Presenter<IEditAsesoresView>
    {
        private readonly ISfTBL_ModuloReclamos_AsesoresManagementServices _asesores;

        private readonly ISfTBL_Admin_UsuariosManagementServices _usuarios;

        private readonly ISfTBL_ModuloReclamos_UnidadManagementServices _unidad;

        private readonly ISfTBL_ModuloReclamos_ZonaManagementServices _zona;

        private readonly ISfTBL_Admin_OptionListManagementServices _optionList;

        private readonly ISfTBL_Admin_RolesManagementServices _rol;


        public EditAsesoresPresenter(ISfTBL_ModuloReclamos_AsesoresManagementServices asesores,
            ISfTBL_Admin_UsuariosManagementServices usuarios,
            ISfTBL_ModuloReclamos_UnidadManagementServices unidad,
            ISfTBL_ModuloReclamos_ZonaManagementServices zona,
            ISfTBL_Admin_OptionListManagementServices optionList,
            ISfTBL_Admin_RolesManagementServices rol)
        {
            _asesores = asesores;
            _usuarios = usuarios;
            _unidad = unidad;
            _zona = zona;
            _optionList = optionList;
            _rol = rol;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += ViewLoad;
            View.SaveEvent += ViewSaveEvent;
        }

        void ViewLoad(object sender, EventArgs e)
        {
            if (View.IsPostBack) return;
            GetJefes();
            GetUnidades();
            GetZonas();
            Load();
        }

        void ViewSaveEvent(object sender, EventArgs e)
        {
            GuardarUnidadZona();
        }
        private void Load()
        {
            GetAsesorInfo();
        }

        private void GuardarUnidadZona()
        {

            try
            {
                if (String.IsNullOrEmpty(View.IdUser)) return;
                var asesor = _asesores.FindById(int.Parse(View.IdUser));
                if (asesor == null) return;
                asesor.IdUnidad = int.Parse(View.IdUnidad);
                asesor.IdZona = int.Parse(View.IdZona);
                asesor.TBL_Admin_Usuarios.Clear();

                foreach (var item in View.UsuariosCopia)
                {
                    var obj = _usuarios.FindById(Convert.ToInt32(item.Id));
                    asesor.TBL_Admin_Usuarios.Add(obj);
                }

                _asesores.Modify(asesor);
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ProcessOk), TypeError.Ok));
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.SaveError), TypeError.Error));
            }

        }

        private void GetAsesorInfo()
        {
            try
            {
                if (String.IsNullOrEmpty(View.IdUser)) return;
                var asesor = _asesores.FindById(int.Parse(View.IdUser));
                if (asesor == null) return;
                View.IdUnidad = asesor.IdUnidad.ToString();
                View.IdZona = asesor.IdZona.ToString();
                View.AsesorName = _usuarios.FindById(asesor.IdUsuario).Nombres;
             
                List<DTO_ValueKey> Users = new List<DTO_ValueKey>();
                foreach (var User in asesor.TBL_Admin_Usuarios)
                {
                    Users.Add(new DTO_ValueKey() { Id = User.IdUser.ToString(), Value = User.Nombres });

                }
                View.UsuariosCopia = Users;
                View.LoadUsuariosCopia(Users);

            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.SaveError), TypeError.Error));
            }


        }

        private void GetJefes()
        {
            try
            {
                var idrolresponsable = _optionList.ObtenerOpcionBykey("IdRolJefeInmediato");
                if (idrolresponsable.Value != "0")
                {
                    var rol = _rol.FindById(int.Parse(idrolresponsable.Value));
                    var listado = _usuarios.FindBySpecWithRols(true);
                    var listadoResponsables = listado.Where(u => u.TBL_Admin_Roles1.Contains(rol)).ToList();
                    View.GetJefes(listadoResponsables);
                }
                else
                {
                    var listado = _usuarios.FindBySpec(true);
                    View.GetJefes(listado);
                }
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

        //private void GetGerentes()
        //{
        //    try
        //    {
        //        var listado = _usuarios.FindBySpec(true);
        //        View.GetUsuarios(listado);
        //    }
        //    catch (Exception ex)
        //    {
        //        CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
        //        InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.SaveError), TypeError.Error));
        //    }
        //}

    }
}
