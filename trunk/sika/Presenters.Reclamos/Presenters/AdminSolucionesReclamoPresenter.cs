using System;
using Application.Core;
using Presenters.Reclamos.IViews;

namespace Presenters.Reclamos.Presenters
{
    public class AdminSolucionesReclamoPresenter : Presenter<IAdminSolucionesReclamoView>
    {
        public AdminSolucionesReclamoPresenter()
        {
        }

        public override void SubscribeViewToEvents()
        {
            View.Load += View_Load;
        }

        void View_Load(object sender, EventArgs e)
        {
            if (View.IsPostBack) return;
        }
    }
}