using Application.Core;

namespace Modules.WorkFlow
{
    public class WorkFlowModule : ModuleBase
    {
        public override string DefaultViewControlPath
        {
            get
            {
                return "Pages/Modules/WorkFlow/WorkFlowPanel.ascx";
            }
        }
    }
}