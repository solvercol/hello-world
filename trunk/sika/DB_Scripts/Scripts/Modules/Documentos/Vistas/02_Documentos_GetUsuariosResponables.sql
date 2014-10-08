IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'Documentos_GetUsuariosResponables') AND type in (N'P', N'PC'))
DROP PROCEDURE Documentos_GetUsuariosResponables
GO
-- =============================================
-- Author:		Solver
-- Create date: 01-07-2013
-- Description:
-- =============================================
CREATE PROCEDURE Documentos_GetUsuariosResponables

AS

select	distinct
		userResponsable.IdUser
		,userResponsable.Nombres
from	TBL_ModuloDocumentos_Documento documento with(nolock)
		inner join TBL_Admin_Usuarios userResponsable with(nolock)
			on documento.IdUsuarioResponsable = userResponsable.IdUser