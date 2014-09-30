using System;
using System.Collections.Generic;
using Application.Core;
using Domain.MainModules.Entities;
using System.Collections;


namespace Presenters.Reclamos.IViews
{
    public interface IDetailCategoriaReclamosView : IView
    {
        #region Members
        string IdCategoriaReclamo { get;}
        string Nombre {set;}
        string SubCategoria {set;}
        string Descripcion {set;}
        string Area {set;}
        int GrupoInformacion {set;}
        string IdResponsable { set; }
        string IdTipoReclamo { get; set; }
        bool Activo {set;}
        string CreateBy {set;}
        string CreateOn {set;}
        string ModifiedBy {set;}
        string ModifiedOn {set;}
        string IdModule { get;}
        #endregion
    }
}
