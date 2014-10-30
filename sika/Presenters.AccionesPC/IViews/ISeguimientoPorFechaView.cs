using System;
using System.Data;
using Application.Core;
using System.Collections.Generic;
using Domain.MainModules.Entities;

namespace Presenters.AccionesPC.IViews
{
    public interface ISeguimientoPorFechaView : IView
    {
        // Filtros
        string ServerHostPath { get; }
        string FilterNoSolicitud { get; set; }
        string FilterTipo { get; set; }
        int FilterArea { get; set; }
        string FilterProceso { get; set; }
        DateTime FechaFilterFrom { get; set; }
        DateTime FechaFilterTo { get; set; }

        void LoadView(DataTable dt);

        // Carga
        void LoadAreaAcion(List<TBL_ModuloAPC_Areas> items);
        void LoadProcesos(List<DTO_ValueKey> items);
    }
}