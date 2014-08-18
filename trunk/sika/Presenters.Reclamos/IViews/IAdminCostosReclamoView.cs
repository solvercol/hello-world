using System.Collections.Generic;
using Application.Core;
using Domain.MainModule.Reclamos.DTO;
using System;
using Domain.MainModules.Entities;

namespace Presenters.Reclamos.IViews
{
    public interface IAdminCostosReclamoView : IView
    {
        // Admin Producto
        Dto_Producto SelectedProduct { get; set; }
        string IdReclamo { get; }
        int UnidadesProducto { get; set; }
        int UnidadesDisponerProducto { get; set; }
        decimal CostoProducto { get; set; }
        decimal KilosProducto { get; set; }
        decimal CostoDisponibleProducto { get; set; }

        // Resumen Costos
        decimal CostoProductoReclamo { get; set; }
        decimal CostoTransporte { get; set; }
        decimal CostoDisposicion { get; set; }
        decimal CostoPruebasCampo { get; set; }
        decimal CostoManoObra { get; set; }
        decimal OtrosCostos { get; set; }
        decimal CostosAsistenciaTecnica { get; set; }
        decimal CostosAsistenciaRegional { get; set; }
        decimal CostoViajePersonas { get; set; }
        decimal CostoEquiposHerramientas { get; set; }
        decimal TotalCostosReclamo { get; set; }

        // Valores De Parametros
        decimal PorcentajeDescuento { get; set; }
        decimal ValorDisposicion { get; set; }

        // Methods
        void ShowAdminProductoWindow(bool visible);

        void LoadCostoProductos(List<TBL_ModuloReclamos_CostosProducto> items);
    }
}