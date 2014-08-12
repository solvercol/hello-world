namespace Domain.MainModules.Entities
{
    public partial class TBL_ModuloReclamos_CategoriasReclamo
    {
        public string IdCategoriaGrupoInformacion
        {
            get 
            {
                return string.Format("{0}-{1}", IdCategoriaReclamo, GrupoInformacion);
            }
        }
    }
}