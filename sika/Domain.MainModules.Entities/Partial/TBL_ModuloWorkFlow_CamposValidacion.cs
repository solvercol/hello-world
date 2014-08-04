namespace Domain.MainModules.Entities
{
    public partial class TBL_ModuloWorkFlow_CamposValidacion
    {
        public string Descripcion
        {
            get { return TBL_Admin_EstadosProceso == null ? string.Empty : TBL_Admin_EstadosProceso.Descripcion; }
        }
    }
}