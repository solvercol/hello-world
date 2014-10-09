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
    public class DetailUnidadZonaPresenter : Presenter<IDetailUnidadZonaView>
    {
        private readonly ISfTBL_ModuloReclamos_UnidadesZonasManagementServices _unidadesZona;


        public DetailUnidadZonaPresenter(ISfTBL_ModuloReclamos_UnidadesZonasManagementServices unidadesZona)
        {
            _unidadesZona = unidadesZona;
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
            if ((string.IsNullOrEmpty(View.IdUnidad)) &&(string.IsNullOrEmpty(View.IdZona))&&(string.IsNullOrEmpty(View.IdGerente)))
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
            View.CreateOn = uz.CreateOn != null ? uz.CreateOn.ToShortDateString() + " " + uz.CreateOn.ToShortTimeString() : "";
            View.ModifiedBy = uz.TBL_Admin_Usuarios2.Nombres;
            View.ModifiedOn = uz.ModifiedOn != null ? uz.ModifiedOn.ToShortDateString() + " " + uz.ModifiedOn.ToShortTimeString() : "";

        }

    }
}
