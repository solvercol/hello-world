using Applications.MainModule.WorkFlow.DTO;

namespace Applications.MainModule.WorkFlow.Util
{
    public interface ISendMailNotification
    {
        bool EnviarCorreoElectronicoNotificacion(RenderTypeControlButtonDto oDocument);

        byte[] GetMergeTemplate(RenderTypeControlButtonDto oDocument);
    }
}