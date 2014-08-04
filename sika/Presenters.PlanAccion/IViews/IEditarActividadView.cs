using System;
using System.Collections.Generic;
using Application.Core;

namespace Presenters.PlanAccion.IViews
{
    public interface IEditarActividadView : IView
    {
        #region eventos

        event EventHandler GuardarEvent;
        event EventHandler EliminarEvent;

        #endregion

        #region Miembros

        string IdActividad { get; }
        void TiposRespuesta(IEnumerable<string> tipos);
        string Codigo { get; set; }
        string Descripcion { get; set; }
        bool Tienepregunta { get; set; }
        string Pregunta { get; set; }
        string TipoRespuesta { get; set; }
        string[] ValorRespuestas { get; set; }
        string RespuestaObligatoria { get; set; }
        bool RequiereAnexo { get; set; }
        bool RequiereComentarios { get; set; }
        bool Activa { get; set; }


        #endregion
    }
}