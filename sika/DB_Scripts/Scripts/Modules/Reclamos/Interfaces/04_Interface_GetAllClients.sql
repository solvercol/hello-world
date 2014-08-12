USE Sika_Dev
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'Interface_GetAllClients') AND type in (N'P', N'PC'))
DROP PROCEDURE Interface_GetAllClients
GO
-- =============================================
-- Author:		Solver
-- Create date: 01-07-2013
-- Description:
-- =============================================
CREATE PROCEDURE Interface_GetAllClients

AS

SELECT	CODIGOCLIENTE
		,CLIENTE
		,CONTACTO
		,EMAIL
		,UNIDAD
		,ZONA
FROM	Sika_SolverNET..Clientes