using Application.Core;
using Applications.MainModule.PlanAccion.IServices;

namespace Modules.PlanAccion
{
    public class PlanAccionModule : ModuleBase
    {
        private readonly ISfTBL_ModuloPlanAccion_ConfiguracionActividadesManagementServices _configServices;

        public PlanAccionModule(ISfTBL_ModuloPlanAccion_ConfiguracionActividadesManagementServices configServices)
        {
            
            _configServices = configServices;
        }

        public ISfTBL_ModuloPlanAccion_ConfiguracionActividadesManagementServices ConfigServices
        {
            get { return _configServices; }
        }

    }
}