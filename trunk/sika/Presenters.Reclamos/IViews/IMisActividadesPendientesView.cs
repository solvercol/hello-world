using System.Data;
using Application.Core;

namespace Presenters.Reclamos.IViews
{
    public interface IMisActividadesPendientesView : IView
    {
        string ServerHostPath { get; }
        void LoadViewReclamos(DataTable dt);
    }
}