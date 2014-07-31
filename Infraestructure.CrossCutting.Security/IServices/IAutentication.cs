namespace Infraestructure.CrossCutting.Security.IServices
{
    public interface IAutentication
    {
        //TBL_Maestra_Usuarios AuthenticatedUser(string userName, string password, bool persistLogin);

        //TBL_Maestra_Usuarios AuthenticatedUser(string userName, bool persistLogin);

        //TBL_Maestra_Usuarios AuthenticatedByUserId(int userId, bool persistLogin);

        bool ValidarAutorizacion(string className);

        int GetIdUserFromTicket();
    }
}