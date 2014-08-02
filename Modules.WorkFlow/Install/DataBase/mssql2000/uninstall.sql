
 
DELETE FROM TBL_Admin_ModuleVersion WHERE assembly = 'Modules.WorkFlow'
go

DELETE TBL_Admin_ModuleService
FROM TBL_Admin_ModuleService ms
	INNER JOIN TBL_Admin_ModuleType mt ON mt.IdModuleType = ms.IdModuleType AND mt.NombreEnsamblado = 'Modules.WorkFlow'
go

DELETE TBL_Admin_ModuleRepository
FROM TBL_Admin_ModuleRepository mr
	INNER JOIN TBL_Admin_ModuleType mt ON mt.IdModuleType = mr.IdModuleType AND mt.NombreEnsamblado = 'Modules.WorkFlow'
go

DELETE TBL_Admin_TypeByModules
FROM TBL_Admin_TypeByModules tm
	INNER JOIN TBL_Admin_ModuleType mt ON mt.IdModuleType = tm.IdModuleType AND mt.NombreEnsamblado = 'Modules.WorkFlow'
go


DELETE TBL_Admin_RolesPorOpcionMenu
FROM TBL_Admin_RolesPorOpcionMenu rom
INNER JOIN TBL_Admin_OpcionesMenu opc ON opc.IdOpcionMenu = rom.IdopcionMenu
INNER JOIN TBL_Admin_Modulos mod ON mod.IdModulo = opc.AplicationId
WHERE mod.NombreModulo = 'WorkFlow'

go

DELETE TBL_Admin_OpcionesMenu 
FROM TBL_Admin_OpcionesMenu opc
INNER JOIN TBL_Admin_Modulos mod ON mod.IdModulo = opc.AplicationId
WHERE mod.NombreModulo = 'WorkFlow'

go
DELETE TBL_Admin_Secciones 
FROM TBL_Admin_Secciones sec
INNER JOIN TBL_Admin_Modulos mod ON mod.IdModulo = sec.IdModule
WHERE mod.NombreModulo = 'WorkFlow'

go

DELETE FROM TBL_Admin_Modulos WHERE NombreModulo= 'WorkFlow'
go

DELETE FROM TBL_Admin_ModuleType WHERE NombreEnsamblado = 'Modules.WorkFlow'
go