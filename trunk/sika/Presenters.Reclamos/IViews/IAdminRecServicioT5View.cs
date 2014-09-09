using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModule.Reclamos.DTO;
using Domain.MainModules.Entities;

namespace Presenters.Reclamos.IViews
{
    public interface IAdminRecServicioT5View : IView
    {
        // Global
        string ConsecutivoReclamo { get; set; }

        // Informacion Generral
        int IdTipoReclamo { get; set; }
        string IdCategoriaReclamo { get; set; }
        string CategoriaReclamo { get; set; }
        int IdResponsableCategoriaReclamo { get; set; }
        string Area { get; set; }
        string Planta { get; set; }
        string IdAtendidoPor { get; set; }
        string QuienReclama { get; set; }
        string NombreQuienReclama { get; }        
        string UnidadZona { get; set; }
        string ProcedimientoInternoAfectado { get; set; }
        string AreaIncumpleProcedimiento { get; set; }

        // Descripcion de Problema
        string DescripcionProblema { get; set; }
        string MensajeDescripcionProblema { get; set; }

        // Carga
        void LoadAtendidoPor(List<TBL_Admin_Usuarios> items);
        void LoadQuienReclama(List<TBL_Admin_Usuarios> items);
        void LoadPlantas(List<DTO_ValueKey> items);
        void LoadAreaIncumpleProcedimiento(List<DTO_ValueKey> items);
        void LoadProcedimientoInternoAfectado(List<DTO_ValueKey> items);

        // View 
        void GoToReclamoView(string idReclamo);

        // Edit Params
        string IdReclamo { get; }
    }
}