using System;
using System.Reflection;
using Application.Core;
using Applications.MainModule.PlanAccion.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.PlanAccion.IViews;

namespace Presenters.PlanAccion.Presenters
{
    public class EditarCategoriaPresenter : Presenter<IEditarCategoriaView>
    {

        private readonly ISfTBL_ModuloPlanAccion_CategoriasManagementServices _categoryServices;

        public EditarCategoriaPresenter(ISfTBL_ModuloPlanAccion_CategoriasManagementServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += ViewLoad;
            View.EliminarEvent += ViewEliminarEvent;
            View.GuardarEvent += ViewGuardarEvent;
        }

        void ViewGuardarEvent(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty( View.IdCategoria) )
                Guardar();
            else
            {
                Actualizar();
            }
        }

        void ViewEliminarEvent(object sender, EventArgs e)
        {
            Eliminar();
        }

        void ViewLoad(object sender, EventArgs e)
        {
            if(View.IsPostBack)return;
            Cargar();
        }

        private void Cargar()
        {
            try
            {
                if (string.IsNullOrEmpty(View.IdCategoria))
                {
                    return;
                }

                var oCategoria = _categoryServices.FindById(Convert.ToInt32(View.IdCategoria));
                if (oCategoria == null)
                {
                    InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.GetObjectError, "Categoría"), TypeError.Error));
                    return;
                }

                View.Descripcion = oCategoria.Descripcion;
                View.Activo = oCategoria.IsActive;
                View.Numeroactividades = oCategoria.NumeroMinimoAct.GetValueOrDefault();
                View.Secuencia = oCategoria.Secuencia;
                
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs( string.Format(Message.GetObjectError,"Categoría"), TypeError.Error));
            }
        }


        private void Guardar()
        {
            try
            {
                var oCategoria = _categoryServices.NewEntity();
                oCategoria.CreateBy = View.UserSession.IdUser.ToString();
                oCategoria.CreateOn = DateTime.Now;
                oCategoria.Descripcion = View.Descripcion;
                oCategoria.IsActive = View.Activo;
                oCategoria.ModifiedBy = View.UserSession.IdUser.ToString();
                oCategoria.ModifiedOn = DateTime.Now;
                oCategoria.NumeroMinimoAct = View.Numeroactividades;
                oCategoria.Secuencia = View.Secuencia;
                _categoryServices.Add(oCategoria);
                InvokeMessageBox(new MessageBoxEventArgs(Message.ProcessOk,TypeError.Ok));
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(Message.SaveError, TypeError.Error));
            }
        }

        private void Actualizar()
        {
            try
            {
                if(string.IsNullOrEmpty(View.IdCategoria))
                {
                    InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ErrorLecturaID," registro Categoría"), TypeError.Error));
                    return;
                }

                var oCategoria = _categoryServices.FindById(Convert.ToInt32(View.IdCategoria));
                if (oCategoria == null)
                {
                    InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.GetObjectError, "Categoría"), TypeError.Error));
                    return;
                }

                oCategoria.Descripcion = View.Descripcion;
                oCategoria.IsActive = View.Activo;
                oCategoria.ModifiedBy = View.UserSession.IdUser.ToString();
                oCategoria.ModifiedOn = DateTime.Now;
                oCategoria.NumeroMinimoAct = View.Numeroactividades;
                oCategoria.Secuencia = View.Secuencia;
                _categoryServices.Modify(oCategoria);
                InvokeMessageBox(new MessageBoxEventArgs(Message.ProcessOk, TypeError.Ok));

            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(Message.EditError, TypeError.Error));
            }
        }

        private void Eliminar()
        {
            try
            {
                if (string.IsNullOrEmpty(View.IdCategoria))
                {
                    InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ErrorLecturaID, "la Categoría"), TypeError.Error));
                    return;
                }


                var result = _categoryServices.Delete(Convert.ToInt32(View.IdCategoria));
                InvokeMessageBox(result
                                    ? new MessageBoxEventArgs(Message.ProcessOk, TypeError.Ok)
                                    : new MessageBoxEventArgs(string.Format(Message.DeleteError, "Categoría"),
                                                              TypeError.Error));

            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.DeleteError,"Categoría"), TypeError.Error));
            }
        }


    }
}