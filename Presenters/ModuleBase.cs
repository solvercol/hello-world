using System;
using Domain.MainModules.Entities;

namespace Presenters
{
    public abstract class ModuleBase
    {
        public TBL_Admin_TypeByModules Seccion { get; set; }

        public virtual void ReadSectionSettings()
        {
            if (Seccion == null)
            {
                throw new NullReferenceException("Can't access the section for settings.");
            }
        }

        public string SectionUrl { get; set; }

        public virtual string DefaultViewControlPath
        {
            get { return ""; }
        }
    }
}