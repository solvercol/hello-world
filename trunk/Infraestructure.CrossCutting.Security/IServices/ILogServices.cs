using Infrastructure.CrossCutting.NetFramework.Enums;

namespace Infraestructure.CrossCutting.Security.IServices
{
    public interface ILogServices
    {
        void CrearEntradaLogPedidos(int idPedido, int idUser, string userName, Acciones accion);
    }
}