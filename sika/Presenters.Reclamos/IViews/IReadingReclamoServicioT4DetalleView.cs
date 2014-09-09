using System;
using Application.Core;

namespace Presenters.Reclamos.IViews
{
    public interface IReadingReclamoServicioT4DetalleView : IView
    {
        string IdReclamo { get; }
        // Informacion Generral
        string CategoriaReclamo { get; set; }
        string SubCategoriaReclamo { get; set; }
        string Area { get; set; }
        string Planta { get; set; }
        string Asesor { get; set; }
        string AtendidoPor { get; set; }
        string TipoContacto { get; set; }

        // Informacion de Cliente
        string NombreCliente { get; set; }
        string NombreContacto { get; set; }
        string UnidadZona { get; set; }
        string EmailContacto { get; set; }
        string NombreObra { get; set; }
        string PropietarioObra { get; set; }
        string AplicadoPor { get; set; }
        string EmailPropietario { get; set; }
        string EmailQuienAplica { get; set; }

        // Descripcion de Problema
        string DescripcionProblema { get; set; }        
    }
}