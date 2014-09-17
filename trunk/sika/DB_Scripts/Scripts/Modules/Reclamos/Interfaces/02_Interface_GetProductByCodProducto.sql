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

SELECT	producto.CODIGOPRODUCTO
		,producto.PRODUCTO
		,producto.UNIDAD
		,producto.PESONETO
		,producto.PRECIOLISTA
		,producto.GRUPOCOMPRADORES
		,producto.CAMPOAPL
		,producto.CATEGORIA
		,producto.SUBCATEGORIA
		,categoria.IdCategoria
FROM	Sika_SolverNET..Productos producto with(nolock)
		left join TBL_ModuloReclamos_CategoriaProducto categoria with(nolock)
			on upper(producto.CATEGORIA) = upper(categoria.Nombre)
WHERE	CODIGOPRODUCTO = @CodProducto