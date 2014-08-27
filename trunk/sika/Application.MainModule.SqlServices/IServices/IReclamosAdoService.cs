using System;
using System.Collections.Generic;
using System.Data;
using Domain.MainModule.Reclamos.DTO;

namespace Application.MainModule.SqlServices.IServices
{
    public interface IReclamosAdoService
    {
        List<Dto_Asesor> GetAllAsesores();

        Dto_Asesor GetByIdAsesor(int idAsesor);

        void InsertUsuarioCopiaActividades(string idUsuario, string idActividad);

        string EstadoReclamo(string idreclamo);

        DataTable GetReclamoWorkFlowById(string idReclamo);

        string EjecutarSpToBool(string spName, Dictionary<string, string> parametros);

        DataTable ResumenReclamosPanelWorkFlow(string idReclamo);

        #region Vistas Y Reportes

        DataTable GetVistaGeneralReclamos(DateTime from, DateTime end, string serverHost, string moduleId);

        DataTable GetVistaReclamosMisPendientes(DateTime from, DateTime end, string serverHost, string moduleId,
                                                int idResponsable, string noReclamo, string cliente, string producto, string servicio);

        DataTable GetVistaMisReclamosPorFecha(DateTime from, DateTime end, string serverHost, string moduleId,
                                                int idCreador, string noReclamo, string cliente, string producto, string servicio);

        DataTable GetVistaMisReclamosPorEstado(DateTime from, DateTime end, string serverHost, string moduleId,
                                                int idCreador, string noReclamo, string cliente, string producto, string servicio);

        DataTable GetVistaReclamosPorTipo(DateTime from, DateTime end, string serverHost, string moduleId,
                                          string noReclamo, string cliente, string producto, string servicio);

        #endregion
    }
}