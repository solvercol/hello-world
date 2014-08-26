using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.MainModules.Entities
{
    public partial class TBL_ModuloReclamos_Reclamo
    {
        public TBL_Admin_Usuarios ResponsableActual
        {
            get { return this.TBL_Admin_Usuarios; }
        }


        public TBL_Admin_Usuarios AsesoradoPor
        {
            get { return this.TBL_Admin_Usuarios1; }
        }

        public TBL_Admin_Usuarios AtentidoPor
        {
            get { return this.TBL_Admin_Usuarios2; }
        }

        public TBL_Admin_Usuarios CreadoPor
        {
            get { return this.TBL_Admin_Usuarios3; }
        }

        public TBL_Admin_Usuarios IngenieroResponsable
        {
            get { return this.TBL_Admin_Usuarios4; }
        }

        public TBL_Admin_Usuarios ModificadoPor
        {
            get { return this.TBL_Admin_Usuarios5; }
        }

        public TBL_Admin_Usuarios Solicitante
        {
            get { return this.TBL_Admin_Usuarios6; }
        }

        public TBL_Admin_Usuarios UsuarioCierre
        {
            get { return this.TBL_Admin_Usuarios7; }
        }

        public object DtoProducto { get; set; }
        public object DtoCliente { get; set; }
    }
}