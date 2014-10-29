using System;
using System.Collections.Generic;
using System.Data;
using Domain.MainModule.Reclamos.DTO;
using Infrastructure.CrossCutting.NetFramework.Enums;

namespace Application.MainModule.SqlServices.IServices
{
    public interface IReclamosAdoService
    {
        List<Dto_Asesor> GetAllAsesores();

        Dto_Asesor GetByIdAsesor(int idAsesor);

        void InsertUsuarioCopiaActividades(string idUsuario, string idActividad);
        void InsertUsuarioCopiaComentario(string idUsuario, string idComentario);

        string EstadoDocumento(string id, ModulosAplicacion module);

        DataTable GetDocumentWorkFlowById(string id, ModulosAplicacion module);

        string EjecutarSpToBool(string spName, Dictionary<string, string> parametros);

        DataTable ResumenReclamosPanelWorkFlow(string idReclamo);

        DataTable ListadoIngenierosResponsablesPorcategoría(string idCategoria);

        DataTable Search_Unidad(string strPrefijo);

        DataTable Search_Zona(string strPrefijo);

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

        DataTable GetVistaReclamosPorEstado(DateTime from, DateTime end, string serverHost, string moduleId,
                                            string noReclamo, string cliente, string producto, string servicio);

        DataTable GetVistaReclamosPorNumero(DateTime from, DateTime end, string serverHost, string moduleId,
                                            string noReclamo, string cliente, string producto, string servicio);

        DataTable GetVistaReclamosPorTargetMarket(DateTime from, DateTime end, string serverHost, string moduleId,
                                                  string noReclamo, string cliente, string producto);

        DataTable GetVistaMisAlternativasPendientes(string serverHost, string moduleId,int idResponsable);

        DataTable GetVistaMisActividadesPendientes(string serverHost, string moduleId, int idResponsable);

        DataTable GetVistaMisAlternativas(string serverHost, string moduleId, int idUser,
                                            DateTime from, DateTime end, string noReclamo,
                                            string cliente, string producto, string servicio);

        DataTable GetVistaMisActividades(string serverHost, string moduleId, int idUser,
                                            DateTime from, DateTime end, string noReclamo,
                                            string cliente, string producto, string servicio);

        DataTable GetVistaGestionActividades(string serverHost, string moduleId, int idUser,
                                            DateTime from, DateTime end, string noReclamo,
                                            string cliente, string producto, string servicio, string fromView);

        DataTable GetVistaGestionAlternativas(string serverHost, string moduleId, int idUser,
                                              DateTime from, DateTime end, string noReclamo,
                                              string cliente, string producto, string servicio, string fromView);

        DataTable GetVistaGestionReclamos(string serverHost, string moduleId, int idUser,
                                              DateTime from, DateTime end, string noReclamo,
                                              string cliente, string producto, string servicio, string fromView);

        #endregion
    }
}