using System;
using Application.Core;
using Presenters.Reclamos.IViews;

namespace Presenters.Reclamos.Presenters
{
    public class AdminRecServicioT1Presenter : Presenter<IAdminRecServicioT1View>
    {
        public AdminRecServicioT1Presenter()
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