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
    public class DetailActividadReclamosPresenter : Presenter<IDetailActividadReclamosView>
    {
        private readonly ISfTBL_ModuloReclamos_ActividadesReclamoManagementServices _actReclamos;

        private readonly ISfTBL_ModuloReclamos_TipoReclamoManagementServices _tipoReclamo;

        public DetailActividadReclamosPresenter(ISfTBL_ModuloReclamos_ActividadesReclamoManagementServices actReclamos, ISfTBL_ModuloReclamos_TipoReclamoManagementServices tipoReclamo)
        {
            _tipoReclamo = tipoReclamo;
            _actReclamos = actReclamos;
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
            if (string.IsNullOrEmpty(View.IdActividadReclamo)) return;

            var cr = _actReclamos.FindById(Convert.ToInt32(View.IdActividadReclamo));

            if (cr == null) return;
            View.Nombre = cr.Nombre;
            View.Descripcion = cr.Descripcion;
            View.IdTipoReclamo = cr.TBL_ModuloReclamos_TipoReclamo.Nombre;
            View.Activo = cr.IsActive;
            View.CreateBy = cr.TBL_Admin_Usuarios.Nombres;
            View.CreateOn = cr.CreateOn != null ? cr.CreateOn.ToShortDateString() : "";
            View.ModifiedBy = cr.TBL_Admin_Usuarios1.Nombres;
            View.ModifiedOn = cr.ModifiedOn != null ? cr.ModifiedOn.ToShortDateString() : "";

        }


    }
}
