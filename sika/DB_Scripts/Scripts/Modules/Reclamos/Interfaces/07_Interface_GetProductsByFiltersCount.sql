IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'Interface_GetProductsByFiltersCount') AND type in (N'P', N'PC'))
DROP PROCEDURE Interface_GetProductsByFiltersCount
GO
-- =============================================
-- Author:		Solver
-- Create date: 01-07-2013
-- Description:
-- =============================================
CREATE PROCEDURE Interface_GetProductsByFiltersCount
(
	@filter nvarchar(50)
)
AS

--declare @filter nvarchar(50), @PageSize int, @PageIndex int
--set @filter = 'plasti  25'
--set @PageIndex = 2
--set @PageSize = 100

SELECT	COUNT(*)
FROM	Sika_SolverNET..Productos
WHERE	(UPPER(CODIGOPRODUCTO) like '%' + UPPER(@filter) + '%')
		OR (UPPER(PRODUCTO) like '%' + UPPER(@filter) + '%')
		OR (UPPER(UNIDAD) like '%' + UPPER(@filter) + '%')
		OR (UPPER(GRUPOCOMPRADORES) like '%' + UPPER(@filter) + '%')
		OR (UPPER(CAMPOAPL) like '%' + UPPER(@filter) + '%')
		OR (UPPER(CATEGORIA) like '%' + UPPER(@filter) + '%')
		OR (UPPER(SUBCATEGORIA) like '%' + UPPER(@filter) + '%')