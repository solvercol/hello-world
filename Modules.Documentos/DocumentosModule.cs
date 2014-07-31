using Application.Core;

namespace Modules.Documentos
{
    public class DocumentosModule : ModuleBase
    {
        public override string DefaultViewControlPath
        {
            get
            {
                return "Pages/Modules/Documentos/Documentos.ascx";
            }
        }
    }
}