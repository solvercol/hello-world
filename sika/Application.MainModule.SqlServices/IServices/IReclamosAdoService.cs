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

        #endregion
    }
}