using System;
using System.Collections.Generic;
using System.Data;
using Application.Core;
using Applications.MainModule.Admin.IServices;
using Domain.MainModules.Entities;
using Infrastructure.CrossCutting.IoC;

namespace Modules.AccionesPC
{
    public class AccionesPCModule : ModuleBase
    {

        private readonly ISfTBL_Admin_UsuariosManagementServices _usuariosServices;

        public AccionesPCModule()
        {
            _usuariosServices = IoC.Resolve<ISfTBL_Admin_UsuariosManagementServices>();
        }


        public List<TBL_Admin_Usuarios> ListadoUsuarios()
        {
            var listado = _usuariosServices.FindBySpec(true);
            return listado;
        }

    }
}