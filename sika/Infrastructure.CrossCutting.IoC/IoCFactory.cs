
using System;
using System.Collections.Generic;
using System.Linq;
using Application.MainModule.ExportExcel.ExportServices;
using Application.MainModule.ExportExcel.IExportServices;
using Application.MainModule.SqlServices.IServices;
using Application.MainModule.SqlServices.Services;
using Applications.MainModule.Admin.IServices;
using Applications.MainModule.Admin.Services;
using Domain.MainModule.AccionesPC.Services;
using Domain.MainModule.Contracts;
using Domain.MainModule.Reclamos.Services;
using Domain.MainModule.WorkFlow.Services.FieldsValidatos;
using Domain.MainModule.WorkFlow.Services.WorkFlow;
using Infraestructure.CrossCutting.Security.IServices;
using Infraestructure.CrossCutting.Security.Security;
using Infraestructure.CrossCutting.Security.Services;
using Infraestructure.Data.Core;
using Infrastructure.CrossCutting.Logging;
using Infrastructure.CrossCutting.NetFramework.Logging;
using Infrastructure.CrossCutting.NetFramework.Services.Email;
using Infrastructure.CrossCutting.NetFramework.Services.Files;
using Infrastructure.CrossCutting.NetFramework.Services.RunAssemblies;
using Infrastructure.Data.MainModule.Repositories;
using Infrastructure.Data.MainModule.UnitOfWork;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Infraestructure.CrossCutting.NetCommunication;
using Application.MainModule.Documentos.IServices;
using Application.MainModule.Documentos.Services;

namespace Infrastructure.CrossCutting.IoC
{
    public static class IoCFactory
    {

        #region Members

        static IDictionary<string, IUnityContainer> _containersDictionary;

        /// <summary>
        /// Initialize Container
        /// </summary>
        /// 
        public static void InitializeContainer()
        {
            try
            {
                _containersDictionary = new Dictionary<string, IUnityContainer>();

                // Initialize Windsor
                IUnityContainer rootContainer = new UnityContainer();
                rootContainer.AddNewExtension<Interception>();

                _containersDictionary.Add("RootContext", rootContainer);

                ConfigureRootContainer(rootContainer);

                // Inititialize the static Windsor helper class. 
                IoC.Initialize(rootContainer);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static void RegisterModuleLoader()
        {
            var ensamblados = new Dictionary<string, string>
                                  {
                                      {
                                          "Modules.Loader.ModuleLoader, Modules.Loader"
                                          ,
                                          "Modules.Loader.ModuleLoader, Modules.Loader"
                                       },
                                  };

            foreach (var classType in
                ensamblados
                .Select(ensamblado => Type.GetType(ensamblado.Value))
                .Where(classType => classType != null))
            {
                IoC.RegisterType(classType);
            }
        }

        static void ConfigureRootContainer(IUnityContainer container)
        {
           
            #region Repositorios para el modulo Administrativos

            container.RegisterType<ITBL_Admin_ModuleServiceRepository, TBL_Admin_ModuleServiceRepository>(new TransientLifetimeManager());
            container.RegisterType<ITBL_Admin_ModuleRepositoryRepository, TBL_Admin_ModuleRepositoryRepository>(new TransientLifetimeManager());
            container.RegisterType<ITBL_Admin_ModuleTypeRepository, TBL_Admin_ModuleTypeRepository>(new TransientLifetimeManager());
            container.RegisterType<ITBL_Admin_ModulosRepository, TBL_Admin_ModulosRepository>(new TransientLifetimeManager());
            container.RegisterType<ITBL_Admin_RolesRepository, TBL_Admin_RolesRepository>(new TransientLifetimeManager());
            container.RegisterType<ITBL_Admin_UsuariosRepository, TBL_Admin_UsuariosRepository>(new TransientLifetimeManager());
            container.RegisterType<ITBL_Admin_TypeByModulesRepository, TBL_Admin_TypeByModulesRepository>(new TransientLifetimeManager());
            container.RegisterType<ITBL_Admin_SistemaNotificacionesRepository, TBL_Admin_SistemaNotificacionesRepository>(new TransientLifetimeManager());

            #endregion

            #region Servicios Genericos de la capa de aplicacion

            container.RegisterType<ISfTBL_Admin_UsuariosManagementServices, SfTBL_Admin_UsuariosManagementServices>(new TransientLifetimeManager());
            container.RegisterType<ISfTBL_Admin_TypeByModulesManagementServices, SfTBL_Admin_TypeByModulesManagementServices>(new TransientLifetimeManager());
            container.RegisterType<ISfTBL_Admin_RolesManagementServices, SfTBL_Admin_RolesManagementServices>(new TransientLifetimeManager());
            container.RegisterType<ISfTBL_Admin_ModulosManagementServices, SfTBL_Admin_ModulosManagementServices>(new TransientLifetimeManager());
            container.RegisterType<ISfTBL_Admin_ModuleTypeManagementServices, SfTBL_Admin_ModuleTypeManagementServices>(new TransientLifetimeManager());
            container.RegisterType<ISfTBL_Admin_ModuleServiceManagementServices, SfTBL_Admin_ModuleServiceManagementServices>(new TransientLifetimeManager());
            container.RegisterType<ILogServices, LogServices>(new TransientLifetimeManager());
            container.RegisterType<ISectionServices, SectionServices>(new TransientLifetimeManager());
            container.RegisterType<ISfTBL_Admin_SistemaNotificacionesManagementServices, SfTBL_Admin_SistemaNotificacionesManagementServices>(new TransientLifetimeManager());
            
            #endregion 

            #region Servicio de Consultas SQl
            
            container.RegisterType<IReclamosExternalInterfacesService, ReclamosExternalInterfacesService>(new TransientLifetimeManager());
            container.RegisterType<IReclamosAdoService, ReclamosAdoService>(new TransientLifetimeManager());
            container.RegisterType<ISolicitudAdoService, SolicitudAdoService>(new TransientLifetimeManager());
            container.RegisterType<IDocumentosAdoService, DocumentosAdoService>(new TransientLifetimeManager());
            container.RegisterType<ISolicitudesAPCAdoService, SolicitudesAPCAdoService>(new TransientLifetimeManager());

            #endregion

            #region Register crosscuting mappings
            container.RegisterType<ITraceManager, TraceManager>(new TransientLifetimeManager());
            container.RegisterType<ISqlHelper, SqlHelper>(new TransientLifetimeManager());
            container.RegisterType<IMainModuleUnitOfWork, MainModuleContext>(new PerResolveLifetimeManager(), new InjectionConstructor());
            container.RegisterType<IMailHelper, MailHelper>(new TransientLifetimeManager());
            container.RegisterType<IFileService, TransactionalFileService>(new TransientLifetimeManager());
            #endregion

            #region Servicios de Exportación
            container.RegisterType<IExportToExcel, ExportToExcel>(new TransientLifetimeManager());
            //container.RegisterType<IExportDataTableToHtml, ExportDataTableToHtml>(new TransientLifetimeManager());
            #endregion
          
            #region Servicios Transversales
            container.RegisterType<IReclamoMailService, ReclamoMailService>(new TransientLifetimeManager());
            container.RegisterType<IAutentication, AutenticationServices>(new TransientLifetimeManager());
            container.RegisterType<IEmailService, DefaultEmailService>(new TransientLifetimeManager());
            container.RegisterType<IEmailTemplateEngine, SimpleEmailTemplateEngine>(new TransientLifetimeManager());
            container.RegisterType<IEmailSender, SmtpNet2EmailSender>(new TransientLifetimeManager());
            container.RegisterType<ISystemActionsManagementServices, SystemActionsManagementServices>(new TransientLifetimeManager());
            #endregion

            #region Servicios Capa de Dominio
            //Reclamos
            container.RegisterType<ITBL_Moduloreclamos_ReclamoDomainServices, TBL_Moduloreclamos_ReclamoDomainServices>(new TransientLifetimeManager());
            //Solicitudes APC
            container.RegisterType<ISolicitudesDomainServices, SolicitudesDomainServices>(new TransientLifetimeManager());
            //WorkFlow
            container.RegisterType<ITblModuloWorkFlowRutasFieldsValidatorDomainServices, TblModuloWorkFlowRutasFieldsValidatorDomainServices>(new TransientLifetimeManager());
            container.RegisterType<ITblModuloWorkFlowRutaDomainServices, TblModuloWorkFlowRutaDomainServices>(new TransientLifetimeManager());
            
            #endregion
        }

        #endregion
    }
}