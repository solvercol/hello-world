using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.Admin.IViews
{
    public interface IEditEmailTemplateView : IView
    {
        #region Events

        event EventHandler SaveEvent;

        event EventHandler DeleteEvent;

        #endregion

        #region Members

        void ListadoPaises(List<TBL_Admin_Paises> items);
        string NombrePlantilla { get; set; }
        string Encabezado { get; set; }
        string Cuerpo { get; set; }
        bool Activo { get; set; }
        string IdPlantilla { get; }
        string IdPais { get; set; }
        string CodeTemplate { get; set; }
        #endregion
    }
}