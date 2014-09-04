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
        string PesoNetoProducto { get; set; }
        string PrecioListaProducto { get; set; }
        string IdReclamo { get; }
        int UnidadesProducto { get; set; }
        int UnidadesDisponerProducto { get; set; }
        decimal CostoProducto { get; set; }
        decimal KilosProducto { get; set; }
        decimal CostoDisponibleProducto { get; set; }

        void CheckCostosProductoSelect();
        void CheckCostosDisponibilidadProductoSelect();

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

        // Buscar producto
        string FilterText { get; set; }
        string NombreProducto { get; set; }
        void LoadProructos(List<Dto_Producto> items);
        void ShowSelectProductWindow(bool visible);
        int TotalRegistrosPaginador { set; }
        int PageZise { get; }
        event EventHandler Filterevent;

        // Fletes Reclamo
        decimal TarifaFetes { get; set; }
    }
}