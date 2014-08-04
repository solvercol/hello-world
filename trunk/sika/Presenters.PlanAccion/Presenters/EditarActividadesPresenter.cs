using System;
using System.Collections.Generic;
using System.Reflection;
using Application.Core;
using Applications.MainModule.Admin.IServices;
using Applications.MainModule.PlanAccion.IServices;
using Infrastructure.CrossCutting.NetFramework.Enums;
using Presenters.PlanAccion.IViews;

namespace Presenters.PlanAccion.Presenters
{
    public class EditarActividadesPresenter : Presenter<IEditarActividadView>
    {
        private readonly ISfTBL_Admin_OptionListManagementServices _optionServices;
        private readonly ISfTBL_ModuloPlanAccion_BancoActividadesManagementServices _actividadesServices;
        public EditarActividadesPresenter(
            ISfTBL_Admin_OptionListManagementServices optionServices, 
            ISfTBL_ModuloPlanAccion_BancoActividadesManagementServices actividadesServices)
        {
            _optionServices = optionServices;
            _actividadesServices = actividadesServices;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += ViewLoad;
            View.GuardarEvent += ViewGuardarEvent;
            View.EliminarEvent += ViewEliminarEvent;
        }

        void ViewEliminarEvent(object sender, EventArgs e)
        {
            Eliminar();
        }

        void ViewGuardarEvent(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(View.IdActividad))
            {
                Guardar();
            }
            else
            {
                Actualizar();
            }
        }

        void ViewLoad(object sender, EventArgs e)
        {
            if(View.IsPostBack)return;
            TiposRespuesta();
            CargarObjeto();
        }

        private void TiposRespuesta()
        {
            try
            {
                var tipos = _optionServices.ObtenerOpcionBykey("TipoRespuesta");
                if(tipos == null)return;
                View.TiposRespuesta(tipos.Value.Split('|'));
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.GetObjectError, "TiposRespuesta"), TypeError.Error));
            }
        }

        private void CargarObjeto()
        {
            try
            {
                if(string.IsNullOrEmpty(View.IdActividad))
                {
                    return;
                }

                var oActividad = _actividadesServices.FindById(Convert.ToInt32(View.IdActividad));
                if(oActividad == null)
                {
                    InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.GetObjectError, "Actividad"), TypeError.Error));
                    return;
                }

                View.Activa = oActividad.IsActive;
                View.Codigo = oActividad.Codigo;
                View.Descripcion = oActividad.Descripcion;
                View.Pregunta = oActividad.Pregunta;
                View.RequiereAnexo = oActividad.TieneAnexo;
                View.RequiereComentarios = oActividad.ComentariosObligatorios.GetValueOrDefault();
                View.RespuestaObligatoria = oActividad.RespuestaObligatoria;
                View.Tienepregunta = oActividad.TienePregunta;
                View.TipoRespuesta = oActividad.TipoRespuesta;
                if (!string.IsNullOrEmpty(oActividad.ValoresRespuesta))
                    View.ValorRespuestas = oActividad.ValoresRespuesta.Split('|');
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.GetObjectError, "Actividad"), TypeError.Error));
            }
        }

        private void Guardar()
        {
            try
            {
                var oActividad = _actividadesServices.NewEntity();
                oActividad.IsActive = View.Activa;
                oActividad.Codigo = View.Codigo.ToUpper();
                oActividad.Descripcion = View.Descripcion;
                oActividad.TieneAnexo = View.RequiereAnexo;
                oActividad.ComentariosObligatorios = View.RequiereComentarios;
                oActividad.TienePregunta = View.Tienepregunta;

                if (View.Tienepregunta)
                {
                    oActividad.Pregunta = View.Pregunta.ToUpper();
                    oActividad.TipoRespuesta = View.TipoRespuesta;
                    if(View.TipoRespuesta.Equals("S/N"))
                    {
                        oActividad.RespuestaObligatoria = View.RespuestaObligatoria;
                    }
                    else
                    {
                        oActividad.ValoresRespuesta = RetornarRespuestasConcatenadas(View.ValorRespuestas).ToUpper();
                    }
                }
                oActividad.CreateBy = View.UserSession.IdUser.ToString();
                oActividad.CreateOn = DateTime.Now;
                oActividad.ModifiedBy = View.UserSession.IdUser.ToString();
                oActividad.ModifiedOn = DateTime.Now;
                _actividadesServices.Add(oActividad);
                InvokeMessageBox(new MessageBoxEventArgs(Message.ProcessOk, TypeError.Ok));
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


                if (string.IsNullOrEmpty(View.IdActividad))
                {
                    InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ErrorLecturaID, " registro Actividad"), TypeError.Error));
                    return;
                }

                var oActividad = _actividadesServices.FindById(Convert.ToInt32(View.IdActividad));
                if (oActividad == null)
                {
                    InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.GetObjectError, "Actividad"), TypeError.Error));
                    return;
                }

                oActividad.IsActive = View.Activa;
                oActividad.Codigo = View.Codigo.ToUpper();
                oActividad.Descripcion = View.Descripcion;
                oActividad.TieneAnexo = View.RequiereAnexo;
                oActividad.ComentariosObligatorios = View.RequiereComentarios;
                oActividad.TienePregunta = View.Tienepregunta;

                if (View.Tienepregunta)
                {
                    oActividad.Pregunta = View.Pregunta.ToUpper();
                    oActividad.TipoRespuesta = View.TipoRespuesta;
                    if (View.TipoRespuesta.Equals("S/N"))
                    {
                        oActividad.RespuestaObligatoria = View.RespuestaObligatoria;
                    }
                    else
                    {
                        oActividad.ValoresRespuesta = RetornarRespuestasConcatenadas(View.ValorRespuestas);
                    }
                }

                oActividad.ModifiedBy = View.UserSession.IdUser.ToString();
                oActividad.ModifiedOn = DateTime.Now;
                _actividadesServices.Modify(oActividad);
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
                if (string.IsNullOrEmpty(View.IdActividad))
                {
                    InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.ErrorLecturaID, " registro Actividad"), TypeError.Error));
                    return;
                }
                _actividadesServices.Delete(Convert.ToInt32(View.IdActividad));
                LimpiarVista();
                InvokeMessageBox(new MessageBoxEventArgs(Message.ProcessOk, TypeError.Ok));
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.DeleteError, "Actividad"), TypeError.Error));
            }
        }

        private void LimpiarVista()
        {
            View.Activa = true;
            View.Codigo = string.Empty;
            View.Descripcion = string.Empty;
            View.Pregunta = string.Empty;
            View.RequiereAnexo = false;
            View.RequiereComentarios = false;
            View.Tienepregunta = false;
            View.TipoRespuesta = string.Empty;
            View.ValorRespuestas = null;
        }

        private static string RetornarRespuestasConcatenadas(ICollection<string> respuestas)
        {
            var strRespuestas = string.Empty;
            var index = 0;
            foreach (var respuesta in respuestas)
            {
                if (index < respuestas.Count - 1)
                    strRespuestas += string.Format("{0}|", respuesta);
                else
                    strRespuestas += string.Format("{0}", respuesta);
                index++;
            }
            return strRespuestas;
        }
    }
}