using Infrastructure.CrossCutting.NetFramework.Enums;

namespace Infraestructure.CrossCutting.Security.IServices
{
    public interface ILogServices
    {
        void CrearEntradaLogPedidos(int idDocumento, int idHostorial, string userName, Acciones accion);
    }
}