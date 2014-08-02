namespace Domain.MainModules.Entities
{
    public partial class TBL_ModuloWorkFlow_Rutas
    {
        public string EstadoInicial
        {
            get { return TBL_Admin_EstadosProceso == null ? string.Empty : TBL_Admin_EstadosProceso.Descripcion; }
        }

        public string EstadoFinal
        {
            get { return TBL_Admin_EstadosProceso1 == null ? string.Empty : TBL_Admin_EstadosProceso1.Descripcion; }
        }
    }
}