﻿using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;
using System.Collections;
namespace Presenters.Reclamos.IViews
{
    public interface IDetailAsesorView : IView
    {
        #region Members
        string IdUser { get; }
        string IdUnidad { set; }
        string IdZona { set; }
        string AsesorName { set; }
        string JefesInmediatos { get; set; }

        #endregion
    }
}
