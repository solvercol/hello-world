using System;
using Application.Core;

namespace Presenters.Reclamos.IViews
{
    public interface IReadingReclamoProductoDetalleView : IView
    {
        string IdReclamo { get; }
        string Asesor { get; set; }
        string AtendidoPor { get; set; }
        int CantidadVendidaUnidad { get; set; }
        int CantidadReclamadaUnidad { get; set; }
        bool Aplicado { get; set; }
        DateTime FechaVenta { get; set; }
        string TipoContacto { get; set; }
        string Planta { get; set; }

        // Informacion de Cliente
        string NombreCliente { get; set; }
        string NombreContacto { get; set; }
        string UnidadZona { get; set; }
        string EmailContacto { get; set; }
        string NombreObra { get; set; }
        string AplicadoPor { get; set; }
        string PropietarioObra { get; set; }
        string EmailQuienAplica { get; set; }
        string EmailPropietario { get; set; }

        // Informacion de Producto
        string AspectoExteriorEnvase { get; set; }
        string AspectoProducto { get; set; }
        string DescripcionAspectoEnvase { get; set; }
        string DescripcionAspectoProducto { get; set; }
        string NumeroLote { get; set; }
        string NumeroLote2 { get; set; }
        string NumeroLote3 { get; set; }
        bool MuestraDisponible { get; set; }

        // Descripcion de Problema
        string DescripcionProblema { get; set; }
        string Diagnostico { get; set; }
        string ConclusionesPrevias { get; set; }
        string Solucion { get; set; }
    }
}