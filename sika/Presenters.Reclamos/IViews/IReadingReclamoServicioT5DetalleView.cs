using System;
using Application.Core;

namespace Presenters.Reclamos.IViews
{
    public interface IReadingReclamoServicioT5DetalleView : IView
    {
        string IdReclamo { get; }

        // Informacion Generral
        string CategoriaReclamo { get; set; }
        string Area { get; set; }
        string Planta { get; set; }
        string AtendidoPor { get; set; }
        string QuienReclama { get; set; }
        int NoRecordatorios { get; set; }
        string UnidadZona { get; set; }
        string ProcedimientoInternoAfectado { get; set; }
        string AreaIncumpleProcedimiento { get; set; }

        // Descripcion de Problema
        string DescripcionProblema { get; set; }
    }
}