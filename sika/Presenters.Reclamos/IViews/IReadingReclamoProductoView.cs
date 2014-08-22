using Application.Core;

namespace Presenters.Reclamos.IViews
{
    public interface IReadingReclamoProductoView : IView
    {
        // Admin Reclamo
        string IdReclamo { get; }
        string NombreProducto { get; set; }
        string TargetMarket { get; set; }
        string CampoAplicacion { get; set; }
        string SubCampoAplicacion { get; set; }
        string Presentacion { get; set; }
    }
}