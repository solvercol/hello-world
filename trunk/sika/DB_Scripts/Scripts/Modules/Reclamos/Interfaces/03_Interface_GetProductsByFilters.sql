IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'Interface_GetProductsByFilters') AND type in (N'P', N'PC'))
DROP PROCEDURE Interface_GetProductsByFilters
GO
-- =============================================
-- Author:		Solver
-- Create date: 01-07-2013
-- Description:
-- =============================================
CREATE PROCEDURE Interface_GetProductsByFilters
(
	@filter nvarchar(50),
	@PageSize int,
	@PageIndex int
)
AS

--declare @filter nvarchar(50), @PageSize int, @PageIndex int
--set @filter = 'plasti  25'
--set @PageIndex = 2
--set @PageSize = 100

SELECT	CODIGOPRODUCTO
		,PRODUCTO
		,UNIDAD
		,PESONETO
		,PRECIOLISTA
		,GRUPOCOMPRADORES
		,CAMPOAPL
		,CATEGORIA
		,SUBCATEGORIA
FROM	(
		SELECT	ROW_NUMBER() OVER ( order by CODIGOPRODUCTO desc )	as RowNumber
				,CODIGOPRODUCTO
				,PRODUCTO
				,UNIDAD
				,PESONETO
				,PRECIOLISTA
				,GRUPOCOMPRADORES
				,CAMPOAPL
				,CATEGORIA
				,SUBCATEGORIA
		FROM	Sika_SolverNET..Productos
		WHERE	(UPPER(CODIGOPRODUCTO) like '%' + UPPER(@filter) + '%')
				OR (UPPER(PRODUCTO) like '%' + UPPER(@filter) + '%')
				OR (UPPER(UNIDAD) like '%' + UPPER(@filter) + '%')
				OR (UPPER(GRUPOCOMPRADORES) like '%' + UPPER(@filter) + '%')
				OR (UPPER(CAMPOAPL) like '%' + UPPER(@filter) + '%')
				OR (UPPER(CATEGORIA) like '%' + UPPER(@filter) + '%')
				OR (UPPER(SUBCATEGORIA) like '%' + UPPER(@filter) + '%')
		) tt
WHERE		tt.RowNumber between (@PageSize * @PageIndex + 1 ) and (@PageSize * (@PageIndex + 1))
ORDER BY	tt.CODIGOPRODUCTO
			,tt.PRODUCTO
