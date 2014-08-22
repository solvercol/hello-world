IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'Reclamos_GetAsesorByIdUsuario') AND type in (N'P', N'PC'))
DROP PROCEDURE Reclamos_GetAsesorByIdUsuario
GO
-- =============================================
-- Author:		Solver
-- Create date: 01-07-2013
-- Description:
-- =============================================
CREATE PROCEDURE Reclamos_GetAsesorByIdUsuario
(
	@IdAsesor int
)
AS

select	distinct
		usr.IdUser
		,usr.Nombres
		,ase.IdUnidad
		,ase.IdZona
		,unidad.Nombre as Unidad
		,zona.Descripcion as Zona
from	TBL_ModuloReclamos_Asesores ase with(nolock)
		inner join TBL_Admin_Usuarios usr with(nolock)
			on ase.IdUsuario = usr.IdUser
		inner join TBL_ModuloReclamos_Zona zona with(nolock)
			on ase.IdZona = zona.IdZona
		inner join TBL_ModuloReclamos_Unidad unidad with(nolock)
			on ase.IdUnidad = unidad.IdUnidad
where	usr.IdUser = @IdAsesor