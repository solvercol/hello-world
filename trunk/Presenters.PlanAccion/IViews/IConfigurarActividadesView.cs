using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.PlanAccion.IViews
{
    public interface IConfigurarActividadesView : IView
    {
        #region Eventos

        event EventHandler GuardarEvent;
        event EventHandler EliminarEvent;
        event EventHandler CargarEvent;

        #endregion
        #region Miembros

        void Categorias(IEnumerable<TBL_ModuloPlanAccion_Categorias> list);
        void Actividades(List<TBL_ModuloPlanAccion_BancoActividades> list);
        void Roles(List<TBL_Admin_Roles> list);

        string IdConfiguracion { get;  }
        string IdActividad { get; set; }
        string Idcategoria { get; set; }
        bool Obligatoria { get; set; }
        bool Final { get; set; }
        bool Exclusiva { get; set; }
        string RolExclusiva { get; set; }
        bool ProgramarActividad { get; set; }
        int DiasHabiles { get; set; }
        int Secuencia { get; set; }


        #endregion

    }
}