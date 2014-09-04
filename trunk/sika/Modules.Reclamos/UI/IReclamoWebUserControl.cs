using System;

namespace Modules.Reclamos.UI
{
    public interface IReclamoWebUserControl
    {
        void LoadControlData();
        event Action RiseFatherPostback;
    }
}