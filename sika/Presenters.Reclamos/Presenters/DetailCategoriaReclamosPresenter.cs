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
    public class DetailCategoriaReclamosPresenter : Presenter<IDetailCategoriaReclamosView>
    {
        private readonly ISfTBL_ModuloReclamos_CategoriasReclamoManagementServices _catReclamos;

        private readonly ISfTBL_Admin_UsuariosManagementServices _responsable;

        private readonly ISfTBL_ModuloReclamos_TipoReclamoManagementServices _tipoReclamo;

        public DetailCategoriaReclamosPresenter(ISfTBL_ModuloReclamos_CategoriasReclamoManagementServices catReclamos, ISfTBL_Admin_UsuariosManagementServices responsable, ISfTBL_ModuloReclamos_TipoReclamoManagementServices tipoReclamo)
        {
            _responsable = responsable;
            _tipoReclamo = tipoReclamo;
            _catReclamos = catReclamos;
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
            if (string.IsNullOrEmpty(View.IdCategoriaReclamo)) return;

            var cr = _catReclamos.FindById(Convert.ToInt32(View.IdCategoriaReclamo));

            if (cr == null) return;
            View.Nombre = cr.Nombre;
            View.SubCategoria = cr.SubCategoria;
            View.Descripcion = cr.Descripcion;
            View.Area = cr.Area;
            View.IdResponsable = cr.TBL_Admin_Usuarios.Nombres;
            View.GrupoInformacion = cr.GrupoInformacion;
            View.IdTipoReclamo = cr.TBL_ModuloReclamos_TipoReclamo.Nombre;
            View.Activo = cr.IsActive;
            View.CreateBy = cr.TBL_Admin_Usuarios1.Nombres;
            View.CreateOn = cr.CreateOn != null ? cr.CreateOn.ToShortDateString() : "";
            View.ModifiedBy = cr.TBL_Admin_Usuarios2.Nombres;
            View.ModifiedOn = cr.ModifiedOn != null ? cr.ModifiedOn.ToShortDateString() : "";

        }



    }
}
