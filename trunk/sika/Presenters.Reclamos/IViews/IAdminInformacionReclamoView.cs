using Application.Core;

namespace Presenters.Reclamos.IViews
{
    public interface IAdminInformacionReclamoView : IView
    {
        // Properties
        string IdReclamo { get; }
        string TipoReclamo { get; set; }
        string IdCategoriaReclamo { get; set; }
        string IdGrupoInformacion { get; set; }

        void LoadInitReclamoControl();
    }
}