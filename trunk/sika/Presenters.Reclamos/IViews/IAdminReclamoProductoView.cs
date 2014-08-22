using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModule.Reclamos.DTO;
using Domain.MainModules.Entities;

namespace Presenters.Reclamos.IViews
{
    public interface IAdminReclamoProductoView : IView
    {
        // Global
        string ConsecutivoReclamo { get; set; }

        // Informacion Generral
        int IdTipoReclamo { get; set; }
        string IdAsesor { get; set; }
        string IdAtendidoPor { get; set; }
        int CantidadVendidaUnidad { get; set; }
        int CantidadReclamadaUnidad { get; set; }
        bool Aplicado { get; set; }
        DateTime FechaVenta { get; set; }
        int NoRecordatorios { get; set; }
        string TipoContacto { get; set; }
        bool RespuestaInmediata { get; set; }
        string Planta { get; set; }
        Dto_Producto SelectedProduct { get; }
        void SetSelectedProduct(Dto_Producto producto);

        // Informacion de Cliente
        string NombreContacto { get; set; }
        string UnidadZona { get; set; }
        string EmailContacto { get; set; }
        string NombreObra { get; set; }
        string AplicadoPor { get; set; }
        string PropietarioObra { get; set; }
        string EmailQuienAplica { get; set; }
        string EmailPropietario { get; set; }
        Dto_Cliente SelectedCliente { get; }
        void SetSelectedClient(Dto_Cliente cliente);

        // Informacion de Producto
        string AspectoExteriorEnvase { get; set; }
        string AspectoProducto { get; set; }
        string DescripcionProducto { get; set; }
        string NumeroLote { get; set; }
        bool MuestraDisponible { get; set; }

        // Descripcion de Problema
        string DescripcionProblema { get; set; }
        string Diagnostico { get; set; }
        string ConclusionesPrevias { get; set; }
        string Solucion { get; set; }
        string MensajeDescripcionProblema { get; set; }

        // Carga
        void LoadAsesores(List<Dto_Asesor> items);
        void LoadAtendidoPor(List<TBL_Admin_Usuarios> items);
        void LoadPlantas(List<DTO_ValueKey> items);

        // View 
        void GoToReclamoView(string idReclamo);

        // Edit Params
        string IdReclamo { get; }
    }
}