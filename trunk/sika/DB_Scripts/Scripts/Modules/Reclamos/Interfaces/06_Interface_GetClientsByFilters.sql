IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'Interface_GetClientsByFilters') AND type in (N'P', N'PC'))
DROP PROCEDURE Interface_GetClientsByFilters
GO
-- =============================================
-- Author:		Solver
-- Create date: 01-07-2013
-- Description:
-- =============================================
CREATE PROCEDURE Interface_GetClientsByFilters
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

SELECT	CODIGOCLIENTE
		,CLIENTE
		,CONTACTO
		,EMAIL
		,UNIDAD
		,ZONA
FROM	(
		SELECT	ROW_NUMBER() OVER ( order by CODIGOCLIENTE desc )	as RowNumber
				,CODIGOCLIENTE
				,CLIENTE
				,CONTACTO
				,EMAIL
				,UNIDAD
				,ZONA
		FROM	Sika_SolverNET..Clientes
		WHERE	(UPPER(CODIGOCLIENTE) like '%' + UPPER(@filter) + '%')
				OR (UPPER(CLIENTE) like '%' + UPPER(@filter) + '%')
				OR (UPPER(CONTACTO) like '%' + UPPER(@filter) + '%')
				OR (UPPER(EMAIL) like '%' + UPPER(@filter) + '%')
				OR (UPPER(UNIDAD) like '%' + UPPER(@filter) + '%')
				OR (UPPER(ZONA) like '%' + UPPER(@filter) + '%')
		) tt
WHERE		tt.RowNumber between (@PageSize * @PageIndex + 1 ) and (@PageSize * (@PageIndex + 1))
ORDER BY	tt.CLIENTE