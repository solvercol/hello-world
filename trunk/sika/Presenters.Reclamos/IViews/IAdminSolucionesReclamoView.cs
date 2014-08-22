using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.Reclamos.IViews
{
    public interface IAdminSolucionesReclamoView : IView
    {
        // Admin Solucion
        string IdReclamo { get; }
        string Departamento { get; set; }
        string Referencia { get; set; }
        string Observaciones { get; set; }

        string IdSelectedSolucion { get; set; }
        bool IsNewSolucion { get; set; }

        // Methods
        void ShowAdminSolucionWindow(bool visible);
        void LoadSolucionesReclamo(List<TBL_ModuloReclamos_Soluciones> items);
        void LoadDepartamentos(List<DTO_ValueKey> items);

        // Archivos Adjuntos
        List<DTO_ValueKey> ArchivosAdjuntos { get; set; }
        void LoadArchivosAdjuntos(List<DTO_ValueKey> items);
    }
}