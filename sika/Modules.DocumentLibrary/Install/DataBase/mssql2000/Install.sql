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
           ('DocumentLibrary'
           ,'Modules.DocumentLibrary'
           ,'Modules.DocumentLibrary.DocumentLibraryModule'
           ,'~/Pages/Modules/DocumentLibrary/Default.aspx'
           ,'~/Pages/Modules/DocumentLibrary/Default.aspx'
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
           ('DocumentLibrary'
           ,'Libreria Documental Solver de Colombia'
           ,1
           ,1
           ,'1'
           ,GETDATE()
           ,'1'
           ,GETDATE()
           )


SELECT @moduleid  = Scope_Identity()




insert into TBL_Admin_ModuleRepository(IdModuleType,Repositorykey,RepositoryType,classtype,IsActive)
values(@moduletypeid,'DocumentLibrary.Carpetas','Domain.MainModule.DocumentLibrary.Contracts.ITBL_ModuloDocumentosAnexos_CarpetasRepository,Domain.MainModule.DocumentLibrary','Infraestructure.Data.DocumentLibrary.Repositories.TBL_ModuloDocumentosAnexos_CarpetasRepository,Infraestructure.Data.DocumentLibrary',1)

insert into TBL_Admin_ModuleRepository(IdModuleType,Repositorykey,RepositoryType,classtype,IsActive)
values(@moduletypeid,'DocumentLibrary.categorias','Domain.MainModule.DocumentLibrary.Contracts.ITBL_ModuloDocumentosAnexos_CategoriasRepository,Domain.MainModule.DocumentLibrary','Infraestructure.Data.DocumentLibrary.Repositories.TBL_ModuloDocumentosAnexos_CategoriasRepository,Infraestructure.Data.DocumentLibrary',1)

insert into TBL_Admin_ModuleRepository(IdModuleType,Repositorykey,RepositoryType,classtype,IsActive)
values(@moduletypeid,'DocumentLibrary.Contenido','Domain.MainModule.DocumentLibrary.Contracts.ITBL_ModuloDocumentosAnexos_ContenidoRepository,Domain.MainModule.DocumentLibrary','Infraestructure.Data.DocumentLibrary.Repositories.TBL_ModuloDocumentosAnexos_ContenidoRepository,Infraestructure.Data.DocumentLibrary',1)

insert into TBL_Admin_ModuleRepository(IdModuleType,Repositorykey,RepositoryType,classtype,IsActive)
values(@moduletypeid,'DocumentLibrary.Documento','Domain.MainModule.DocumentLibrary.Contracts.ITBL_ModuloDocumentosAnexos_DocumentoRepository,Domain.MainModule.DocumentLibrary','Infraestructure.Data.DocumentLibrary.Repositories.TBL_ModuloDocumentosAnexos_DocumentoRepository,Infraestructure.Data.DocumentLibrary',1)

--- FIN REPOSITORIOS --

insert into TBL_Admin_ModuleService(IdModuleType,servicekey,servicetype,classtype,IsActive)
values(@moduletypeid,'DocumentLibrary.Carpetas','Applcations.MainModule.DocumentLibrary.IServices.ISfTBL_ModuloDocumentosAnexos_CarpetasManagementServices,Applcations.MainModule.DocumentLibrary','Applcations.MainModule.DocumentLibrary.Services.SfTBL_ModuloDocumentosAnexos_CarpetasManagementServices,Applcations.MainModule.DocumentLibrary',1)

insert into TBL_Admin_ModuleService(IdModuleType,servicekey,servicetype,classtype,IsActive)
values(@moduletypeid,'DocumentLibrary.categorias','Applcations.MainModule.DocumentLibrary.IServices.ISfTBL_ModuloDocumentosAnexos_CategoriasManagementServices,Applcations.MainModule.DocumentLibrary','Applcations.MainModule.DocumentLibrary.Services.SfTBL_ModuloDocumentosAnexos_CategoriasManagementServices,Applcations.MainModule.DocumentLibrary',1)

insert into TBL_Admin_ModuleService(IdModuleType,servicekey,servicetype,classtype,IsActive)
values(@moduletypeid,'DocumentLibrary.Contenido','Applcations.MainModule.DocumentLibrary.IServices.ISfTBL_ModuloDocumentosAnexos_ContenidoManagementServices,Applcations.MainModule.DocumentLibrary','Applcations.MainModule.DocumentLibrary.Services.SfTBL_ModuloDocumentosAnexos_ContenidoManagementServices,Applcations.MainModule.DocumentLibrary',1)

insert into TBL_Admin_ModuleService(IdModuleType,servicekey,servicetype,classtype,IsActive)
values(@moduletypeid,'DocumentLibrary.Documento','Applcations.MainModule.DocumentLibrary.IServices.ISfTBL_ModuloDocumentosAnexos_DocumentoManagementServices,Applcations.MainModule.DocumentLibrary','Applcations.MainModule.DocumentLibrary.Services.SfTBL_ModuloDocumentosAnexos_DocumentoManagementServices,Applcations.MainModule.DocumentLibrary',1)

go

insert into TBL_Admin_ModuleVersion(assembly,major,minor,patch)
values('Modules.DocumentLibrary',1,0,0)
go

