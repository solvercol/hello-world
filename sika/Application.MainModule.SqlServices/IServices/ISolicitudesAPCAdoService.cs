using System;
using System.Data;

namespace Application.MainModule.SqlServices.IServices
{
    public interface ISolicitudesAPCAdoService
    {
        #region Vistas Y Reportes

        DataTable GetVistaSolicitudesMisPendientes(DateTime from, DateTime end, string serverHost, string moduleId, string fromview,
                                                  string noSolicitud, string tipo, int area, string proceso, int idResponsable);

        DataTable GetVistaMisSolicitudes(DateTime from, DateTime end, string serverHost, string moduleId, string fromview,
                                                  string noSolicitud, string tipo, int area, string proceso, int idUsuario);

        DataTable GetVistaSeguimiento(DateTime from, DateTime end, string serverHost, string moduleId, string fromview,
                                                  string noSolicitud, string tipo, int area, string proceso, int idUsuario);

        DataTable GetVistaActividades(DateTime from, DateTime end, string serverHost, string moduleId, string fromview,
                                                  string noSolicitud, string tipo, int area, string proceso, string estado, int idUsuario);

        DataTable ResumenSolicitudesApcPanelWorkFlow(string idSolicitud);

        #endregion
    }
}