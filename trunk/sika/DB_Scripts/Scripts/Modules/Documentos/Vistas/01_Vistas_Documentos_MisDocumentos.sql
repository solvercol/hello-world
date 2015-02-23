IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'Vistas_Documentos_MisDocumentos') AND type in (N'P', N'PC'))
DROP PROCEDURE Vistas_Documentos_MisDocumentos
GO
-- =============================================
-- Author:		Solver
-- Create date: 01-07-2013
-- Description:
-- =============================================
CREATE PROCEDURE Vistas_Documentos_MisDocumentos
(
	@IdUser				int
	,@IdEstado			int
	,@SearchText		varchar(512)
	,@FromView			varchar(512)
	,@ServerHostPath	varchar(512)
	,@ModuleId			varchar(4)
)
AS

/*************************** Variables De Prueba **********************/
--declare @IdUser int,@IdEstado int,@SearchText varchar(512),@ServerHostPath varchar(512),@ModuleId varchar(4),@FromView varchar(512)

--set @IdUser = 0
--set	@IdEstado = 0
--set @SearchText = ''
--set @ServerHostPath = 'http://localhost:8081/sikadev'
--set	@ModuleId = 2
--set @FromView = 'misdocs'
/***********************************************************************/

declare	@Vista	table
		(
			Categoria			varchar(512)
			,SubCategoria		varchar(512)
			,TipoDocumento		varchar(512)
			,IdDocumento		int
			,TituloDocumento	varchar(512)
			,Estado				varchar(512)
			,UsuarioCreacion	varchar(512)
			,UsuarioResponsable	varchar(512)
			,UrlDocumento		varchar(2048)
		)

-- Seleccionando datos
insert	into
		@Vista
		(
			Categoria
			,SubCategoria
			,TipoDocumento
			,IdDocumento
			,TituloDocumento
			,Estado
			,UsuarioCreacion
			,UsuarioResponsable	
			,UrlDocumento		
		)		
select	distinct
		categorias.Nombre			as Categoria
		,subCategorias.Nombre		as SubCategoria
		,tipo.Nombre				as TipoDocumento
		,documento.IdDocumento		as IdDocumento
		,documento.Titulo			as TituloDocumento
		,estado.Nombre				as Estado
		,userCreacion.Nombres		as UsuarioCreacion
		,documento.CargoResponsable	as UsuarioResponsable	
		,@ServerHostPath + '/Pages/Modules/Documentos/Consulta/FrmVerDocumento.aspx?ModuleId=' + @ModuleId + '&IdDocumento=' + cast(documento.IdDocumento as varchar(18)) + '&from=' + @FromView
from	TBL_ModuloDocumentos_Documento documento with(nolock)
		inner join TBL_ModuloDocumentos_Categorias categorias with(nolock)
			on documento.IdCategoria = categorias.IdCategoria
		inner join TBL_ModuloDocumentos_Categorias subCategorias with(nolock)
			on documento.IdSubCategoria = subCategorias.IdCategoria
		inner join TBL_ModuloDocumentos_Categorias tipo with(nolock)
			on documento.IdTipo = tipo.IdCategoria
		inner join TBL_ModuloDocumentos_Estados estado with(nolock)
			on documento.IdEstado = estado.IdEstado
		inner join TBL_Admin_Usuarios userCreacion with(nolock)
			on documento.IdUsuarioCreacion = userCreacion.IdUser
where	documento.IdUsuarioCreacion = case @IdUser when 0 then documento.IdUsuarioCreacion else @IdUser end		
		and
		(
			documento.Titulo like '%' + @SearchText + '%'
			or categorias.Nombre like '%' + @SearchText + '%'
			or subCategorias.Nombre like '%' + @SearchText + '%'
			or tipo.Nombre like '%' + @SearchText + '%'
		)
		and documento.IdEstado = case @IdEstado when 0 then documento.IdEstado else @IdEstado end
			
-- Seleccionando Datos a Presentar
select	Categoria
		,SubCategoria
		,TipoDocumento
		,IdDocumento
		,TituloDocumento
		,Estado
		,UsuarioCreacion
		,UsuarioResponsable
		,UrlDocumento
from	@Vista