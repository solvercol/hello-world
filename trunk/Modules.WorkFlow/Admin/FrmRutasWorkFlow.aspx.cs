using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Core;
using ASP.NETCLIENTE.UI;
using Domain.MainModules.Entities;
using Presenters.WorkFlow.IViews;
using Presenters.WorkFlow.Presenters;

namespace Modules.WorkFlow.Admin
{
    public partial class FrmRutasWorkFlow : ViewPage<EditWorkFlowPresenter, IAdminWorkFlowView>, IAdminWorkFlowView
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadUserControl();
            ImprimirTituloVentana("Configuración WorkFlow");
           
        }

        protected void MnuItemClick(object sender, MenuEventArgs e)
        {
            LastLoadedControl = e.Item.Value;
            LoadUserControl();
         
        }


        private string LastLoadedControl
        {
            get
            {
                return ViewState["LastLoaded"] as string;
            }
            set
            {
                ViewState["LastLoaded"] = value;
            }
        }

        private void LoadUserControl()
        {
            var controlPath = LastLoadedControl;

            if (string.IsNullOrEmpty(controlPath))
            {
                controlPath = "WucRutasWorkFlow.ascx";
                if (mnuSecciones.Items.Count > 0)
                    mnuSecciones.Items[0].Selected = true;
            }
            if (string.IsNullOrEmpty(controlPath)) return;
            phlContent.Controls.Clear();
            var uc = LoadControl(controlPath);
            uc.ID = controlPath.Split('.')[0];
            ConfigurarUserControl(uc);
            phlContent.Controls.Add(uc);

            switch (controlPath.Split('.')[0])
            {
                case "WucRutasWorkFlow":
                    {
                        var bl = this.GetUserControl<WucRutasWorkFlow>("WucRutasWorkFlow", "phlContent");
                        if (bl != null)
                        {
                            bl.ActualizarEvent += BlActualizarEvent;
                            bl.ViewResult += BlViewResult;
                        }
                    }
                    break;

                case "WucCamposValidacion":
                    {
                        var bl = this.GetUserControl<WucCamposValidacion>("WucCamposValidacion", "phlContent");
                        if (bl != null)
                        {
                            bl.ActualizarEvent += BlActualizarEvent;
                            bl.ViewResult += BlViewResult;
                        }
                    }
                    break;
            }

        }

        void BlViewResult(object sender, MessageBoxEventArgs e)
        {
            PresenterMessageBox(null,new MessageBoxEventArgs(e.Message, e.Tipo));
        }

        void BlActualizarEvent(object sender, ViewResulteventArgs e)
        {
            //btnEliminar.Visible = true;
            //btnGuardar.Visible = true;
        }

        private void ConfigurarUserControl(Control oControl)
        {
            var uc = (BaseUserControl)oControl;
            if (uc == null) return;
            if (Modulo == null) return;
            var tbm = Modulo.TBL_Admin_TypeByModules.Where(x => x.TBL_Admin_ModuleType.NombreEnsamblado == "Modules.WorkFlow").SingleOrDefault();
            if (tbm != null)
            {
                var mt = tbm.TBL_Admin_ModuleType;
                uc.Module = LoaderModule.GetModuleFromType(mt);
            }
        }

        public TBL_Admin_Usuarios UserSession
        {
            get { return AuthenticatedUser; }
        }

        public string IdModule
        {
            get { return ModuleId; }
        }

        public void ListadoSecciones(IEnumerable<TBL_Admin_Secciones> secciones)
        {

            mnuSecciones.Items.Clear();
            foreach (var seccione in from tab in secciones select tab)
            {
                var opcion = new MenuItem
                {
                    Text = seccione.Titulo,
                    Value =
                        (string.IsNullOrEmpty(IsEdit))
                            ? seccione.PathEdit
                            : seccione.PathPreview
                };
                mnuSecciones.Items.Add(opcion);
            }
            mnuSecciones.Items[0].Selected = true;
        }


        //protected void BtnGuardarClick(object sender, EventArgs e)
        //{
        //    try
        //    {

        //        var result = false;
        //        var bl = this.GetUserControl<WucRutasWorkFlow>("WucRutasWorkFlow");
        //        if (bl != null)
        //        {
        //            result = bl.GuardarEvent();

        //        }
        //        else
        //        {
        //            var cv = this.GetUserControl<WucCamposValidacion>("WucCamposValidacion");
        //            if (cv != null)
        //                result = cv.GuardarEvent();
        //        }


        //        PresenterMessageBox(null,
        //                            result
        //                                ? new MessageBoxEventArgs("Proceso Realizado satisfactoriamente!!", TypeError.Ok)
        //                                : new MessageBoxEventArgs("Error al insertar el registro en la Base de Datos.",
        //                                                          TypeError.Error));
        //        btnEliminar.Visible = false;
        //        btnGuardar.Visible = false;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogError(MethodBase.GetCurrentMethod().Name, AuthenticatedUser.Nombres, new Uri(GetUrl, UriKind.RelativeOrAbsolute), ex);
        //        PresenterMessageBox(null, new MessageBoxEventArgs(ex.Message, TypeError.Error));
        //    }
        //}

        //protected void BtnNuevoClick(object sender, EventArgs e)
        //{
        //    var rw = this.GetUserControl<WucRutasWorkFlow>("WucRutasWorkFlow");
        //    if (rw != null)
        //    {
        //        rw.LimpiarVista();
        //    }
        //    else
        //    {
        //        var cv = this.GetUserControl<WucCamposValidacion>("WucCamposValidacion");
        //        if (cv != null)
        //            cv.LimpiarVista();
        //    }
          
        //    btnEliminar.Visible = false;
        //    btnGuardar.Visible = true;
        //}

        //protected void BtnEliminarClick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        var bl = this.GetUserControl<WucRutasWorkFlow>("WucRutasWorkFlow");
        //        if (bl != null)
        //        {
        //            bl.EliminarEvent();
        //        }
        //        else
        //        {
        //            var cv = this.GetUserControl<WucCamposValidacion>("WucCamposValidacion");
        //            if (cv != null)
        //                cv.EliminarEvent();
        //        }
        //        btnEliminar.Visible = false;
        //        btnGuardar.Visible = false;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogError(MethodBase.GetCurrentMethod().Name, AuthenticatedUser.Nombres, new Uri(GetUrl), ex);
        //        PresenterMessageBox(null, new MessageBoxEventArgs(ex.Message, TypeError.Error));
        //    }
        //}
    }
}