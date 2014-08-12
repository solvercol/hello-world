IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'Interface_GetProductByCodProducto') AND type in (N'P', N'PC'))
DROP PROCEDURE Interface_GetProductByCodProducto
GO
-- =============================================
-- Author:		Solver
-- Create date: 01-07-2013
-- Description:
-- =============================================
CREATE PROCEDURE Interface_GetProductByCodProducto
(
	@CodProducto nvarchar(50)
)
AS

--declare @CodProducto nvarchar(50)
--set @CodProducto = 'T514148-J087'

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
WHERE	CODIGOPRODUCTO = @CodProducto