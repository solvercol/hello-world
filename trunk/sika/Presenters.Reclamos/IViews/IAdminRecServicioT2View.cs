using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModule.Reclamos.DTO;
using Domain.MainModules.Entities;

namespace Presenters.Reclamos.IViews
{
    public interface IAdminRecServicioT2View : IView
    {
        // Global
        string ConsecutivoReclamo { get; set; }

        // Informacion Generral
        int IdTipoReclamo { get; set; }
        string IdCategoriaReclamo { get; set; }
        string CategoriaReclamo { get; set; }
        string Area { get; set; }
        string Planta { get; set; }
        string IdAsesor { get; set; }
        string IdAtendidoPor { get; set; }
        string PedidoRemisionFactura { get; set; }
        int DiarioInventario { get; set; }
        int NoRecordatorios { get; set; }
        string TipoContacto { get; set; }
        bool RespuestaInmediata { get; set; }

        // Informacion de Cliente
        string NombreContacto { get; set; }
        string UnidadZona { get; set; }
        string EmailContacto { get; set; }
        Dto_Cliente SelectedCliente { get; }
        void SetSelectedClient(Dto_Cliente cliente);
        DateTime FechaPedido { get; set; }
        DateTime FechaCompromiso { get; set; }
        DateTime FechaRealEntrega { get; set; }
        int DiasIncumplimiento { get; set; }

        // Descripcion de Problema
        string DescripcionProblema { get; set; }
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