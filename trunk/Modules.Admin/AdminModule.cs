using Application.Core;

namespace Modules.Admin
{
    public class AdminModule : ModuleBase
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