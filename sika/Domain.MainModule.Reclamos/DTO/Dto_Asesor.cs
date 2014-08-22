namespace Domain.MainModule.Reclamos.DTO
{
    public class Dto_Asesor
    {
        public Dto_Asesor()
        {
        }

        public int IdUser { get; set; }
        public string Asesor { get; set; }
        public int IdUnidad { get; set; }
        public int IdZona { get; set; }
        public string Unidad { get; set; }
        public string Zona { get; set; }
    }
}