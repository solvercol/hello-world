using System.Collections.Generic;
using System.Data;
using Application.Core;
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


        public ReclamosModule()
        {
            _sqlServices = IoC.Resolve<IReclamosAdoService>();
            _categoriasService = IoC.Resolve<ISfTBL_ModuloReclamos_CategoriasReclamoManagementServices>();
            _optionServices = IoC.Resolve<ISfTBL_Admin_OptionListManagementServices>();
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
    }
}