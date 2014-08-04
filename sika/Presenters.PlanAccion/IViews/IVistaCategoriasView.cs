using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.PlanAccion.IViews
{
    public interface IVistaCategoriasView : IView
    {

        #region Eventos

        event EventHandler FilterEvent;

        event EventHandler DeleteEvent;

        #endregion

        #region Members

        void ListaCategorias(List<TBL_ModuloPlanAccion_Categorias> items);

        int TotalRegistrosPaginador { set; }

        int PageZise { get; }

        #endregion
    }
}