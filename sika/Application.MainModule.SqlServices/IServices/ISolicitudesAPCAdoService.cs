using System;
using System.Data;

namespace Application.MainModule.SqlServices.IServices
{
    public interface ISolicitudesAPCAdoService
    {
        #region Vistas Y Reportes

        DataTable GetVistaSolicitudesMisPendientes(DateTime from, DateTime end, string serverHost, string moduleId,
                                                  string noSolicitud, string tipo, int area, string proceso,int idResponsable);


        #endregion
    }
}