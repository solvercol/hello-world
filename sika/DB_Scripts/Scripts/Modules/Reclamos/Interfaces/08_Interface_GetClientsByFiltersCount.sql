IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'Interface_GetClientsByFiltersCount') AND type in (N'P', N'PC'))
DROP PROCEDURE Interface_GetClientsByFiltersCount
GO
-- =============================================
-- Author:		Solver
-- Create date: 01-07-2013
-- Description:
-- =============================================
CREATE PROCEDURE Interface_GetClientsByFiltersCount
(
	@filter nvarchar(50)
)
AS

--declare @filter nvarchar(50), @PageSize int, @PageIndex int
--set @filter = 'plasti  25'
--set @PageIndex = 2
--set @PageSize = 100


SELECT	COUNT(*)
FROM	Sika_SolverNET..Clientes
WHERE	(UPPER(CODIGOCLIENTE) like '%' + UPPER(@filter) + '%')
		OR (UPPER(CLIENTE) like '%' + UPPER(@filter) + '%')
		OR (UPPER(CONTACTO) like '%' + UPPER(@filter) + '%')
		OR (UPPER(EMAIL) like '%' + UPPER(@filter) + '%')
		OR (UPPER(UNIDAD) like '%' + UPPER(@filter) + '%')
		OR (UPPER(ZONA) like '%' + UPPER(@filter) + '%')

