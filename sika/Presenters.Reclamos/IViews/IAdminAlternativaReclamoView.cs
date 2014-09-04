using System;
using Application.Core;
using System.Collections.Generic;

namespace Presenters.Reclamos.IViews
{
    public interface IAdminAlternativaReclamoView : IView
    {
        // Admin Alternativa
        string IdAlternativa { get; }
        string Alternativa { get; set; }
        string Causas { get; set; }
        string Factores { get; set; }
        string Responsable { get; set; }
        DateTime FechaAlternativa { get; set; }
        string Seguimiento { get; set; }
        string Estado { get; set; }
        string LogCierre { get; set; }

        // Seccion Info Reclamo
        string IdReclamo { get; set; }
        string TipoReclamo { get; set; }
        string NumeroReclamo { get; set; }
        string MonedaLocal { get; set; }
        string TitleReclamo { get; set; }
        string TitleReclamoFrom { get; set; }
        string Unidad { get; set; }
        string FechaReclamo { get; set; }
        string Asesor { get; set; }

        // Methods
        void EnableEdit(bool enabled);

        // Archivos Adjuntos        
        byte[] ArchivoAdjunto { get; }
        string NombreArchivoAdjunto { get; }
        void LoadArchivosAdjuntos(List<DTO_ValueKey> items);
        void DescargarArchivo(DTO_ValueKey archivo);

        // Register
        bool CanRegister { get; set; }
    }
}