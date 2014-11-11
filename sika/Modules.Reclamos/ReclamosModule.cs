using System.Collections.Generic;
using System.Data;
using Application.Core;
using Application.MainModule.AccionesPC.IServices;
using Application.MainModule.Reclamos.IServices;
using Application.MainModule.SqlServices.IServices;
using Applications.MainModule.Admin.IServices;
using Domain.MainModules.Entities;
using Infrastructure.CrossCutting.IoC;

namespace Modules.Reclamos
{
    public class ReclamosModule : ModuleBase
    {
        private readonly IReclamosAdoService _sqlServices;
        private readonly ISfTBL_ModuloReclamos_CategoriasReclamoManagementServices _categoriasService;
        private readonly ISfTBL_Admin_OptionListManagementServices _optionServices;
        private readonly ISfTBL_ModuloAPC_SolicitudManagementServices _solicitudesApcServices;
        private readonly ISfTBL_ModuloReclamos_ReclamoManagementServices _reclamosServies;
        public ReclamosModule()
        {
            _sqlServices = IoC.Resolve<IReclamosAdoService>();
            _categoriasService = IoC.Resolve<ISfTBL_ModuloReclamos_CategoriasReclamoManagementServices>();
            _optionServices = IoC.Resolve<ISfTBL_Admin_OptionListManagementServices>();
            _solicitudesApcServices = IoC.Resolve<ISfTBL_ModuloAPC_SolicitudManagementServices>();
            _reclamosServies = IoC.Resolve<ISfTBL_ModuloReclamos_ReclamoManagementServices>();
        }


        public DataTable ListadoIngenieros(string idCategoria)
        {
            return _sqlServices.ListadoIngenierosResponsablesPorcategoría(idCategoria);
        }


        public List<TBL_ModuloReclamos_CategoriasReclamo> ListadoCategorias()
        {
            return _categoriasService.GetByTipoReclamo(1);
        }

        public IEnumerable<string> ListadoAreas()
        {
            var opcion = _optionServices.ObtenerOpcionBykey("Areas");
            return opcion == null ? null : opcion.Value.Split('|');
        }

        public List<TBL_ModuloAPC_Solicitud> ListadoSolicitudesApc()
        {
            return _solicitudesApcServices.FindBySpec(true);
        }

        public bool AsociarReclamoConSolicitudApc(decimal idSolicitud, decimal idreclamo, int idUserSession)
        {
            return _reclamosServies.AsociarReclamoConSolicitudApc(idSolicitud, idreclamo, idUserSession);
        }
    }
}