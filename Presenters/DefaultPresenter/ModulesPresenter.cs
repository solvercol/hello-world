using System;
using Application.Core;
using Application.MainModule.IApplicationServices;

namespace Presenters.DefaultPresenter
{
    public class ModulesPresenter : Presenter<IModulesView>
    {
        private ISfTBL_Maestra_ModulosManagementServices _modules;

        public ModulesPresenter(ISfTBL_Maestra_ModulosManagementServices modules)
        {
            _modules = modules;
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += ViewLoad;
        }

        void ViewLoad(object sender, EventArgs e)
        {
            if(View.IsPostBack)return;
            GetAll();
        }

        private void GetAll()
        {
            var list = _modules.FindAllModulesBySpec();
            View.GetModulesList(list);
        }
    }
}