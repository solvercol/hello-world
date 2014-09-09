using System;
using Application.Core;

namespace Presenters.Reclamos.IViews
{
    public interface IReadingReclamoServicioT2DetalleView : IView
    {
        string IdReclamo { get; }

        // Informacion Generral
        string CategoriaReclamo { get; set; }
        string Area { get; set; }
        string Planta { get; set; }
        string Asesor { get; set; }
        string AtendidoPor { get; set; }
        string PedidoRemisionFactura { get; set; }
        int DiarioInventario { get; set; }
        string TipoContacto { get; set; }

        // Informacion de Cliente
        string NombreCliente { get; set; }
        string NombreContacto { get; set; }
        string UnidadZona { get; set; }
        string EmailContacto { get; set; }
        DateTime FechaPedido { get; set; }
        DateTime FechaCompromiso { get; set; }
        DateTime FechaRealEntrega { get; set; }
        int DiasIncumplimiento { get; set; }

        // Descripcion de Problema
        string DescripcionProblema { get; set; }
    }
}