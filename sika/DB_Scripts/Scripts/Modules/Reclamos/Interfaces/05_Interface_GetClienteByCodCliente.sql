IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'Interface_GetClienteByCodCliente') AND type in (N'P', N'PC'))
DROP PROCEDURE Interface_GetClienteByCodCliente
GO
-- =============================================
-- Author:		Solver
-- Create date: 01-07-2013
-- Description:
-- =============================================
CREATE PROCEDURE Interface_GetClienteByCodCliente
(
	@CodCliente nvarchar(50)
)
AS

--declare @CodCliente nvarchar(50)
--set @CodCliente = '019091'

SELECT	CODIGOCLIENTE
		,CLIENTE
		,CONTACTO
		,EMAIL
		,UNIDAD
		,ZONA
FROM	Sika_SolverNET..Clientes
WHERE	CODIGOCLIENTE = @CodCliente