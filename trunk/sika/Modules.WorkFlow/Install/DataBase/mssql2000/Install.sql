DECLARE @moduletypeid int
DECLARE @moduleid int
DECLARE @Menuid int

INSERT INTO [TBL_Admin_ModuleType]
           ([Nombre]
           ,[NombreEnsamblado]
           ,[NombreClase]
           ,[path]
           ,[PathEdicion]
           ,[AutoActivar]
           ,[IsActive]
           ,[CreateBy]
           ,[CreateOn]
           ,[ModifiedBy]
           ,[ModifiedOn])
     VALUES
           ('WorkFlow'
           ,'Modules.WorkFlow'
           ,'Modules.WorkFlow.WorkFlowModule'
           ,'~/Pages/Modules/WorkFlow/Default.aspx'
           ,'~/Pages/Modules/WorkFlow/Default.aspx'
           ,1
           ,1
           ,'1'
           ,GETDATE()
           ,'1'
           ,GETDATE())

SELECT @moduletypeid = Scope_Identity()


INSERT INTO [TBL_Admin_Modulos]
           ([NombreModulo]
           ,[Descripcion]
           ,[ShowInMenu]
           ,[IsActive]
           ,[CreateBy]
           ,[CreateOn]
           ,[ModifiedBy]
           ,[ModifiedOn]
           )
     VALUES
           ('WorkFlow'
           ,'WorkFlow'
           ,1
           ,1
           ,'1'
           ,GETDATE()
           ,'1'
           ,GETDATE()
           )


SELECT @moduleid  = Scope_Identity()

INSERT INTO [TBL_Admin_TypeByModules]
           ([IdModulo]
           ,[IdModuleType]
           ,[Titulo]
           ,[MostrarTitulo]
           ,[position]
           ,[IsActive]
           ,[CreateBy]
           ,[CreateOn]
           ,[ModifiedBy]
           ,[ModifiedOn])
     VALUES
           (@moduleid
           ,@moduletypeid
           ,'WorkFlow'
           ,1
           ,4
           ,1
           ,'1'
           ,GETDATE()
           ,'1'
           ,GETDATE())


INSERT INTO [TBL_Admin_OpcionesMenu]
           (
            [AplicationId]
           ,[TituloOpcion]
           ,[Posicion]
           ,[ShowSecondMenu]
           ,[ShowMainMenu]
           ,[Activo]
           ,[LinkUrl]
           ,[CreateBy]
           ,[CreateOn]
           ,[ModifiedBy]
           ,[ModifiedOn])
     VALUES
           (
            @moduleid
           ,'Home'
           ,1
           ,1
           ,0
           ,1
           ,'~/Default.aspx'          
           ,'1'
           ,GETDATE()
           ,'1'
           ,GETDATE())

INSERT INTO [TBL_Admin_OpcionesMenu]
           (
            [AplicationId]
           ,[TituloOpcion]
           ,[Posicion]
           ,[ShowSecondMenu]
           ,[ShowMainMenu]
           ,[Activo]
           ,[CreateBy]
           ,[CreateOn]
           ,[ModifiedBy]
           ,[ModifiedOn]
           )
     VALUES
           (
            @moduleid
           ,'Work Flow'
           ,2
           ,1
           ,1
           ,1
           ,'1'
           ,GETDATE()
           ,'1'
           ,GETDATE()
            )

SELECT @Menuid = Scope_Identity()

INSERT INTO [TBL_Admin_OpcionesMenu]
           ([IdopcionPadre]
           ,[AplicationId]
           ,[TituloOpcion]
           ,[Posicion]
           ,[ShowSecondMenu]
           ,[ShowMainMenu]
           ,[Activo]
           ,[LinkUrl]
           ,[CreateBy]
           ,[CreateOn]
           ,[ModifiedBy]
           ,[ModifiedOn])
     VALUES
           (@Menuid 
           ,@moduleid
           ,'Configuración WF'
           ,1
           ,1
           ,1
           ,1
           ,'~/Pages/Modules/WorkFlow/Admin/FrmRutasWorkFlow.aspx'          
           ,'1'
           ,GETDATE()
           ,'1'
           ,GETDATE())


INSERT INTO [TBL_Admin_Secciones]
           ([IdModule]
           ,[Posicion]
           ,[Titulo]
           ,[TituloEdit]
           ,[PathPreview]
           ,[PathEdit]
           ,[ShowPreview]
           ,[ShowInEdit]
           ,[IsActive]
           ,[CreateBy]
           ,[CreateOn]
           ,[ModifiedBy]
           ,[ModifiedOn])
     VALUES
           (@moduleid
           ,1
           ,'Rutas WorkFlow'
           ,'Rutas WorkFlow'
           ,'WucRutasWorkFlow.ascx'
           ,'WucRutasWorkFlow.ascx'
           ,1
           ,1
           ,1
           ,'1'
           ,GETDATE()
           ,'1'
           ,GETDATE())
           
           INSERT INTO [TBL_Admin_Secciones]
           ([IdModule]
           ,[Posicion]
           ,[Titulo]
           ,[TituloEdit]
           ,[PathPreview]
           ,[PathEdit]
           ,[ShowPreview]
           ,[ShowInEdit]
           ,[IsActive]
           ,[CreateBy]
           ,[CreateOn]
           ,[ModifiedBy]
           ,[ModifiedOn])
     VALUES
           (@moduleid
           ,2
           ,'Validación Campos'
           ,'Validación Campos'
           ,'WucCamposValidacion.ascx'
           ,'WucCamposValidacion.ascx'
           ,1
           ,1
           ,1
           ,'1'
           ,GETDATE()
           ,'1'
           ,GETDATE())

INSERT INTO [TBL_Admin_Secciones]
           ([IdModule]
           ,[Posicion]
           ,[Titulo]
           ,[TituloEdit]
           ,[PathPreview]
           ,[PathEdit]
           ,[ShowPreview]
           ,[ShowInEdit]
           ,[IsActive]
           ,[CreateBy]
           ,[CreateOn]
           ,[ModifiedBy]
           ,[ModifiedOn])
     VALUES
           (@moduleid
           ,3
           ,'Configuración'
           ,'Configuración'
           ,'WucEditSections.ascx'
           ,'WucEditSections.ascx'
           ,1
           ,1
           ,1
           ,'1'
           ,GETDATE()
           ,'1'
           ,GETDATE())

insert into TBL_Admin_ModuleRepository(IdModuleType,Repositorykey,RepositoryType,classtype,IsActive)
values(@moduletypeid,'WorkFlow.Rutas','Domain.MainModule.WorkFlow.Contracts.ITBL_ModuloWorkFlow_RutasRepository,Domain.MainModule.WorkFlow','Infrastructure.Data.MainModule.Repositories.TBL_ModuloWorkFlow_RutasRepository,Infrastructure.Data.MainModule',1)

insert into TBL_Admin_ModuleRepository(IdModuleType,Repositorykey,RepositoryType,classtype,IsActive)
values(@moduletypeid,'WorkFlow.Campos','Domain.MainModule.WorkFlow.Contracts.ITBL_ModuloWorkFlow_CamposValidacionRepository,Domain.MainModule.WorkFlow','Infrastructure.Data.MainModule.Repositories.TBL_ModuloWorkFlow_CamposValidacionRepository,Infrastructure.Data.MainModule',1)

insert into TBL_Admin_ModuleRepository(IdModuleType,Repositorykey,RepositoryType,classtype,IsActive)
values(@moduletypeid,'WorkFlow.ValidacionesSalida','Domain.MainModule.WorkFlow.Contracts.ITBL_ModuloWorkFlow_ValidacionesSalidaRepository,Domain.MainModule.WorkFlow','Infrastructure.Data.MainModule.Repositories.TBL_ModuloWorkFlow_ValidacionesSalidaRepository,Infrastructure.Data.MainModule',1)






insert into TBL_Admin_ModuleService(IdModuleType,servicekey,servicetype,classtype,IsActive)
values(@moduletypeid,'WorkFlow.Rutas','Applications.MainModule.WorkFlow.IServices.ISfTBL_ModuloWorkFlow_RutasManagementServices,Applications.MainModule.WorkFlow','Applications.MainModule.WorkFlow.Services.SfTBL_ModuloWorkFlow_RutasManagementServices,Applications.MainModule.WorkFlow',1)

insert into TBL_Admin_ModuleService(IdModuleType,servicekey,servicetype,classtype,IsActive)
values(@moduletypeid,'WorkFlow.Campos','Applications.MainModule.WorkFlow.IServices.ISfTBL_ModuloWorkFlow_CamposValidacionManagementServices,Applications.MainModule.WorkFlow','Applications.MainModule.WorkFlow.Services.SfTBL_ModuloWorkFlow_CamposValidacionManagementServices,Applications.MainModule.WorkFlow',1)

insert into TBL_Admin_ModuleService(IdModuleType,servicekey,servicetype,classtype,IsActive)
values(@moduletypeid,'WorkFlow.ValidacionesSalida','Applications.MainModule.WorkFlow.IServices.ISfTBL_ModuloWorkFlow_ValidacionesSalidaManagementServices,Applications.MainModule.WorkFlow','Applications.MainModule.WorkFlow.Services.SfTBL_ModuloWorkFlow_ValidacionesSalidaManagementServices,Applications.MainModule.WorkFlow',1)


insert into TBL_Admin_ModuleService(IdModuleType,servicekey,servicetype,classtype,IsActive)
values(@moduletypeid,'WorkFlow.SystemActions','Applications.MainModule.SystemActions.Service.ISystemActionsManagementServices,Applications.MainModule.SystemActions','Applications.MainModule.SystemActions.Service.SystemActionsManagementServices,Applications.MainModule.SystemActions',1)


insert into TBL_Admin_ModuleService(IdModuleType,servicekey,servicetype,classtype,IsActive)
values(@moduletypeid,'WorkFlow.SendmailNotify','Applications.MainModule.WorkFlow.Util.ISendMailNotification,Applications.MainModule.WorkFlow','Applications.MainModule.WorkFlow.Util.SendMailNotification,Applications.MainModule.WorkFlow',1)



go

insert into TBL_Admin_ModuleVersion(assembly,major,minor,patch)
values('Modules.WorkFlow',1,0,0)
go

