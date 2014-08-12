IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'Interface_GetAllProducts') AND type in (N'P', N'PC'))
DROP PROCEDURE Interface_GetAllProducts
GO
-- =============================================
-- Author:		Solver
-- Create date: 01-07-2013
-- Description:
-- =============================================
CREATE PROCEDURE Interface_GetAllProducts

AS

SELECT	CODIGOPRODUCTO
		,PRODUCTO
		,UNIDAD
		,PESONETO
		,PRECIOLISTA
		,GRUPOCOMPRADORES
		,CAMPOAPL
		,CATEGORIA
		,SUBCATEGORIA
FROM	Sika_SolverNET..Productos