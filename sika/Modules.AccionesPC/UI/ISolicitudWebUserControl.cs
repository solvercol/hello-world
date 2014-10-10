using System;

namespace Modules.AccionesPC.UI
{
    public interface ISolicitudWebUserControl
    {
        void LoadControlData();
        event Action RiseFatherPostback;
    }
}