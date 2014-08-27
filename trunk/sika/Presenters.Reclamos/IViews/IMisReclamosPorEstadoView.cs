using System;
using System.Collections.Generic;
using System.Data;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.Reclamos.IViews
{
    public interface IMisReclamosPorEstadoView : IView
    {
        void ShowNewReclamoWindow(bool visible);

        void LoadCategoriasReclamo(List<TBL_ModuloReclamos_CategoriasReclamo> list);

        string TipoReclamo { get; set; }
        string IdCategoriaReclamo { get; }
        string IdGrupoInformacion { get; }

        // Filtros
        string ServerHostPath { get; }
        string FilterNoReclamo { get; set; }
        string FilterCliente { get; set; }
        string FilterProducto { get; set; }
        string FilterServicio { get; set; }
        DateTime FechaFilterFrom { get; set; }
        DateTime FechaFilterTo { get; set; }

        void LoadViewReclamos(DataTable dt);
    }
}