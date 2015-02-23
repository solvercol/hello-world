namespace Application.MainModule.Documentos.IServices
{
    public interface IReclamoMailService
    {
        void SendDocumentoPublicacionMailNotification(object parameters);
    }
}