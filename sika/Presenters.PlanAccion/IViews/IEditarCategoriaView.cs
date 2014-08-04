using System;
using Application.Core;

namespace Presenters.PlanAccion.IViews
{
    public interface IEditarCategoriaView : IView
    {
        event EventHandler GuardarEvent;
        event EventHandler EliminarEvent;

        string IdCategoria { get; }
        int Secuencia { get; set; }
        string Descripcion { get; set; }
        int Numeroactividades { get; set; }
        bool Activo { get; set; }

    }
}