using Application.Core;


namespace Presenters.Reclamos.IViews
{
    public interface IReadingReclamoServicioView : IView
    {
        // Admin Reclamo
        string IdReclamo { get; }
        string Categoria { get; set; }
        string Area { get; set; }
    }
}
