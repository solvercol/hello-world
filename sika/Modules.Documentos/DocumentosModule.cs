using Application.Core;

namespace Modules.Documentos
{
    public class DocumentosModule : ModuleBase
    {
        public string DefaultViewControlPath
        {
            get
            {
                return "Pages/Modules/Documentos/Documentos.ascx";
            }
        }
    }
}