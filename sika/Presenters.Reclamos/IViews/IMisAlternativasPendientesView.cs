using System.Data;
using Application.Core;

namespace Presenters.Reclamos.IViews
{
    public interface IMisAlternativasPendientesView : IView
    {
        string ServerHostPath { get; }
        void LoadViewReclamos(DataTable dt);
    }
}