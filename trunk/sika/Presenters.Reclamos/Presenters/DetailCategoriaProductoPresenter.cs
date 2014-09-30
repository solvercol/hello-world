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
    public class DetailCategoriaProductoPresenter: Presenter<IDetailCategoriaProductoView>
    {
        private readonly ISfTBL_ModuloReclamos_CategoriaProductoManagementServices _catProducto;

        public DetailCategoriaProductoPresenter(ISfTBL_ModuloReclamos_CategoriaProductoManagementServices catProducto, ISfTBL_Admin_UsuariosManagementServices ingenieroResponsable, ISfTBL_Admin_OptionListManagementServices optionList, ISfTBL_Admin_RolesManagementServices rol)
        {
            _catProducto = catProducto;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += ViewLoad;
        }

        void ViewLoad(object sender, EventArgs e)
        {
            if (View.IsPostBack) return;
            Load();
        }

        private void Load()
        {

            GetCategoriaProducto();
        }

        private void GetCategoriaProducto()
        {
            try
            {
                if (string.IsNullOrEmpty(View.IdCategoriaProducto)) return;
                var cp = _catProducto.FindById(Convert.ToInt32(View.IdCategoriaProducto));
                if (cp == null) return;
                View.Nombre = cp.Nombre;
                View.Descripcion = cp.Descripcion;
                View.Activo = cp.IsActive;
                View.CreateBy = cp.TBL_Admin_Usuarios.Nombres;
                View.CreateOn = cp.CreateOn != null ? cp.CreateOn.ToShortDateString() : string.Empty;
                View.ModifiedBy = cp.TBL_Admin_Usuarios1.Nombres;
                View.ModifiedOn = cp.ModifiedOn != null ? cp.ModifiedOn.ToShortDateString() : string.Empty;
                foreach (var User in cp.TBL_Admin_Usuarios2)
                {
                    if (!string.IsNullOrEmpty(View.IngResponsables))
                    {
                        View.IngResponsables += "</br>";
                    }
                    View.IngResponsables = View.IngResponsables + User.Nombres;
                }
            }
            catch (Exception ex)
            {
                CrearEntradaLogProcesamiento(new LogProcesamientoEventArgs(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, Logtype.Archivo));
                InvokeMessageBox(new MessageBoxEventArgs(string.Format(Message.SaveError), TypeError.Error));
            }


        }
    }
}
