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
    public class DetailAsesorPresenter : Presenter<IDetailAsesorView>
    {
        private readonly ISfTBL_ModuloReclamos_AsesoresManagementServices _asesores;

        private readonly ISfTBL_Admin_UsuariosManagementServices _usuarios;


        public DetailAsesorPresenter(ISfTBL_ModuloReclamos_AsesoresManagementServices asesores, ISfTBL_Admin_UsuariosManagementServices usuarios)
        {
            _asesores = asesores;
            _usuarios = usuarios;
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
            if (String.IsNullOrEmpty(View.IdUser)) return;
            var asesor = _asesores.FindById(int.Parse(View.IdUser));
            if (asesor == null) return;
            View.IdUnidad = asesor.TBL_ModuloReclamos_Unidad.Nombre;
            View.IdZona = asesor.TBL_ModuloReclamos_Zona.Descripcion;
            View.AsesorName = _usuarios.FindById(asesor.IdUsuario).Nombres;
            View.Activo = asesor.IsActive;
            View.CreateBy = asesor.TBL_Admin_Usuarios.Nombres;
            View.CreateOn = asesor.CreateOn != null ? asesor.CreateOn.ToShortDateString() + " " + asesor.CreateOn.ToShortTimeString() : string.Empty;
            View.ModifiedBy = asesor.TBL_Admin_Usuarios1.Nombres;
            View.ModifiedOn = asesor.ModifiedOn != null ? asesor.ModifiedOn.ToShortDateString() + " " + asesor.ModifiedOn.ToShortTimeString() : string.Empty;
            foreach (var User in asesor.TBL_Admin_Usuarios2)
            {
                if (!string.IsNullOrEmpty(View.JefesInmediatos))
                {
                    View.JefesInmediatos += "</br>";
                }
                View.JefesInmediatos = View.JefesInmediatos + User.Nombres;
            }

        }


    }
}
