using System;
using System.Collections.Generic;
using Domain.MainModule.AccionesPC.Contracts;
using Domain.MainModule.Reclamos.Contracts;
using Infrastructure.CrossCutting.IDtoService;
using Infrastructure.CrossCutting.IoC;

namespace Applications.MainModule.AccionesSistema.Validaciones
{
    public class ValidarPlanDeAccionActividadesProgramadas
    {
        private readonly List<string> _errores = new List<string>();
        private readonly ITBL_ModuloReclamos_ActividadesRepository _actividadesRepository;
        private readonly ITBL_ModuloAPC_ActividadesRepository _actividadesApcrepository;


        public ValidarPlanDeAccionActividadesProgramadas()
        {
            _actividadesRepository = IoC.Resolve<ITBL_ModuloReclamos_ActividadesRepository>();
            _actividadesApcrepository = IoC.Resolve<ITBL_ModuloAPC_ActividadesRepository>();
        }

        public List<string> GetListErrors
        {
            get { return _errores; }
        }

        public bool ValidarplanAccion(IDocumentDto oDocument)
        {
            if (oDocument == null)
            {
                _errores.Add("Error de lectura del objeto oDocument pasado como parámetro al método..");
                return false;
            }

            if (oDocument.IdDocument == null)
            {
                _errores.Add("Error de lectura del identificador de pedido. el valor es nulo.");
                return false;
            }

            var result = _actividadesRepository.VerificarActividadesPorEstadoPorreclamo("Programada", Convert.ToDecimal(oDocument.IdDocument));

            var resultApc = _actividadesApcrepository.VerificarActividadesPorEstadoPorReclamo("Programada",Convert.ToDecimal(oDocument.IdDocument));

            if (result || resultApc)
            {
                _errores.Add("Existen actividades pendientes asociadas al reclamo y/o plan de acción que no han sido confirmadas..");
                return false;
            }

            return true;
        }
    }
}