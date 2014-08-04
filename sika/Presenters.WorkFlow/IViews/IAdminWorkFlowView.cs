using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;

namespace Presenters.WorkFlow.IViews
{
    public interface IAdminWorkFlowView : IView
    {
        void ListadoSecciones(IEnumerable<TBL_Admin_Secciones> secciones);
    }
}