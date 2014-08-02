using System;
using System.Collections.Generic;
using System.Reflection;
using Application.Core;
using Applications.MainModule.Admin.IServices;
using Applications.MainModule.PlanAccion.IServices;
using Domain.MainModules.Entities;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.PlanAccion.IViews;

namespace Presenters.PlanAccion.Presenters
{
    public class ConfigurarActividadesPresenter : Presenter<IConfigurarActividadesView>
    {
        private readonly ISfTBL_ModuloPlanAccion_BancoActividadesManagementServices _bancoActividadesServices;
        private readonly ISfTBL_ModuloPlanAccion_ConfiguracionActividadesManagementServices _configuracionServices;
        private readonly ISfTBL_ModuloPlanAccion_CategoriasManagementServices _categoriasServices;
        private readonly ISfTBL_Admin_RolesManagementServices _rolesServices;

        public ConfigurarActividadesPresenter(
            ISfTBL_ModuloPlanAccion_BancoActividadesManagementServices bancoActividadesServices, 
            ISfTBL_ModuloPlanAccion_ConfiguracionActividadesManagementServices configuracionServices, 
            ISfTBL_ModuloPlanAccion_CategoriasManagementServices categoriasServices, 
            ISfTBL_Admin_RolesManagementServices rolesServices)
        {
            _bancoActividadesServices = bancoActividadesServices;
            _rolesServices = rolesServices;
            _categoriasServices = categoriasServices;
            _configuracionServices = configuracionServices;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += ViewLoad;
            View.GuardarEvent += ViewGuardarEvent;
            View.CargarEvent += ViewCargarEvent;
            View.EliminarEvent += ViewEliminarEvent;
        }

        void ViewEliminarEvent(object sender, EventArgs e)
        {
            Eliminar();
        }

        void ViewCargarEvent(object sender, EventArgs e)
        {
            Cargar();
        }

        void ViewGuardarEvent(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(View.IdConfiguracion))
                Guardar();
            else
                Actualizar();
        }

        void ViewLoad(object sender, EventArgs e)
        {
            if(View.IsPostBack)return;
            ListadoCategorias();
            ListadoActividades();
            ListadoRoles();
        }

        private void ListadoCategorias()
        {
            try
            {
                var list = _categoriasServices.FindBySpec(true);
                View.Categorias(list);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.GetObjectError, "Listado Categorías"), TypeError.Error));
            }
        }

        private void ListadoActividades()
        {
            try
            {
                var list = _bancoActividadesServices.FindBySpec(true);
                View.Actividades(list);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.GetObjectError, "Listado Actividades"), TypeError.Error));
            }
        }

        private void ListadoRoles()
        {
            try
            {
                var list = _rolesServices.FindBySpec(true);
                View.Roles(list);
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.GetObjectError, "Listado Roles"), TypeError.Error));
            }
        }

        public IEnumerable<TBL_ModuloPlanAccion_ConfiguracionActividades> ListadoActividadesConfiguracion(string idcategoria)
        {
            try
            {
                if (string.IsNullOrEmpty(idcategoria)) return null;
                return _configuracionServices.ActividadesPorCategoria(Convert.ToInt32(idcategoria));
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.GetObjectError, "Listado ActividadesConfiguracion"), TypeError.Error));
            }

            return null;
        }

        private void Guardar()
        {
            try
            {
                if(string.IsNullOrEmpty(View.IdActividad))
                {
                    InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ErrorLecturaID, " de la Actividad"), TypeError.Error));
                    return;
                }
                if (string.IsNullOrEmpty(View.Idcategoria))
                {
                    InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ErrorLecturaID, " de la categoría"), TypeError.Error));
                    return;
                }

                var result = _configuracionServices.ExisteActividadEnCategoria(Convert.ToInt32(View.IdActividad),
                                                                               Convert.ToInt32(View.Idcategoria));
                if(result)
                {
                    InvokeMessageBox(new MessageBoxEventArgs(string.Format("La Actividad ya se encuentra registrada dentro de la categoría seleccionada."), TypeError.Error));
                    return;
                }

                var config = _configuracionServices.NewEntity();
                config.IdActividad = Convert.ToInt32(View.IdActividad);
                config.IdCategoria = Convert.ToInt32(View.Idcategoria);
                config.CreateBy = View.UserSession.IdUser.ToString();
                config.CreateOn = DateTime.Now;
                config.EsFinal = View.Final;
                config.Exclusivo = View.Exclusiva;
                if(View.Exclusiva)
                {
                    config.RolExclusivo = View.RolExclusiva;
                }
                config.IsActive = true;
                config.ModifiedBy = View.UserSession.IdUser.ToString();
                config.ModifiedOn = DateTime.Now;
                config.PreProgramar = View.ProgramarActividad;
                if(View.ProgramarActividad)
                {
                    config.NumeroDiasHabiles = View.DiasHabiles;
                }
                config.Oblogatorio = View.Obligatoria;
                config.Secuencia = View.Secuencia;
                _configuracionServices.Add(config);
                InvokeMessageBox(new MessageBoxEventArgs(Message.ProcessOk, TypeError.Ok));
                ListadoCategorias();
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(Message.SaveError, TypeError.Error));
            }
        }

        private  void Actualizar()
        {
            try
            {
                if (string.IsNullOrEmpty(View.IdActividad))
                {
                    InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ErrorLecturaID, " de la Actividad"), TypeError.Error));
                    return;
                }

                var config = _configuracionServices.FindById(Convert.ToInt32(View.IdActividad));
                if (config == null)
                {
                    InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.GetObjectError, "Actividad"), TypeError.Error));
                    return;
                }

                config.EsFinal = View.Final;
                config.Exclusivo = View.Exclusiva;
                if (View.Exclusiva)
                {
                    config.RolExclusivo = View.RolExclusiva;
                }
                config.IsActive = true;
                config.ModifiedBy = View.UserSession.IdUser.ToString();
                config.ModifiedOn = DateTime.Now;
                config.PreProgramar = View.ProgramarActividad;
                if (View.ProgramarActividad)
                {
                    config.NumeroDiasHabiles = View.DiasHabiles;
                }
                config.Oblogatorio = View.Obligatoria;
                config.Secuencia = View.Secuencia;
                _configuracionServices.Add(config);
                InvokeMessageBox(new MessageBoxEventArgs(Message.ProcessOk, TypeError.Ok));
                ListadoCategorias();
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(Message.EditError, TypeError.Error));
            }
        }

        private void Cargar()
        {
            try
            {
                if (string.IsNullOrEmpty(View.IdConfiguracion))
                {
                    InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ErrorLecturaID, " de la Actividad"), TypeError.Error));
                    return;
                }

                var config = _configuracionServices.FindById(Convert.ToInt32(View.IdConfiguracion));
                if (config == null)
                {
                    InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.GetObjectError, "Actividad"), TypeError.Error));
                    return;
                }
                View.IdActividad = config.IdActividad.ToString();
                View.Idcategoria = config.IdCategoria.ToString();
                View.Final = config.EsFinal;
                View.Exclusiva = config.Exclusivo.GetValueOrDefault();
                if (config.Exclusivo.GetValueOrDefault())
                {
                    View.RolExclusiva = config.RolExclusivo;
                }
                
                View.ProgramarActividad = config.PreProgramar;
                if (config.PreProgramar)
                {
                    View.DiasHabiles = config.NumeroDiasHabiles.GetValueOrDefault();
                }
                View.Obligatoria = config.Oblogatorio;
                View.Secuencia = config.Secuencia;
              
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.GetObjectError, "Actividad-Categoria"), TypeError.Error));
            }
        }

        private void Eliminar()
        {
            try
            {
                if (string.IsNullOrEmpty(View.IdConfiguracion))
                {
                    InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ErrorLecturaID, " de la Actividad"), TypeError.Error));
                    return;
                }

                var config = _configuracionServices.FindById(Convert.ToInt32(View.IdConfiguracion));
                if (config == null)
                {
                    InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.GetObjectError, "Actividad"), TypeError.Error));
                    return;
                }

                _configuracionServices.Remove(config);
                InvokeMessageBox(new MessageBoxEventArgs(Message.ProcessOk, TypeError.Ok));
                ListadoCategorias();
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.DeleteError, "Actividad"), TypeError.Error));
            }
        }
    }
}