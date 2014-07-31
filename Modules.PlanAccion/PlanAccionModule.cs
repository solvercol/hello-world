using Application.Core;

namespace Modules.PlanAccion
{
    public class PlanAccionModule : ModuleBase
    {
        public override string DefaultViewControlPath
        {
            get
            {
                return "Pages/Modules/Actividades/Actividades.ascx";
            }
        }
    }
}