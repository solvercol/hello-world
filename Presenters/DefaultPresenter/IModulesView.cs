using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.DefaultPresenter
{
    public interface IModulesView :IView
    {
        #region Members

        void GetModulesList(List<TBL_Maestra_Modulos> items);

        #endregion
    }
}