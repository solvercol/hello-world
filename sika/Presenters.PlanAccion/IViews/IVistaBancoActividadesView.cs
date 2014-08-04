using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.PlanAccion.IViews
{
    public interface IVistaBancoActividadesView : IView
    {
        #region Eventos

        event EventHandler FilterEvent;

        event EventHandler DeleteEvent;

        #endregion

        #region Members

        void ListaBancoActividades(List<TBL_ModuloPlanAccion_BancoActividades> items);

        int TotalRegistrosPaginador { set; }

        int PageZise { get; }

        #endregion
    }
}