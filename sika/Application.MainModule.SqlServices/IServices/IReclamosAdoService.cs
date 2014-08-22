using System.Collections.Generic;
using Domain.MainModule.Reclamos.DTO;

namespace Application.MainModule.SqlServices.IServices
{
    public interface IReclamosAdoService
    {
        List<Dto_Asesor> GetAllAsesores();
        Dto_Asesor GetByIdAsesor(int idAsesor);

        void InsertUsuarioCopiaActividades(string idUsuario, string idActividad);
    }
}