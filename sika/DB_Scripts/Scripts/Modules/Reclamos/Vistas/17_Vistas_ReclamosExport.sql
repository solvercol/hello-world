IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'Vistas_ReclamosExport') AND type in (N'P', N'PC'))
DROP PROCEDURE Vistas_ReclamosExport
GO
-- =============================================
-- Author:		Solver
-- Create date: 01-07-2013
-- Description:
-- =============================================
CREATE PROCEDURE Vistas_ReclamosExport
(
	@ServerHostPath varchar(512)
	,@ModuleId varchar(4)
	,@UsuarioCreacion int
	,@dateFrom datetime
	,@dateEnd datetime
	,@NoReclamo varchar(50)
	,@NoRelacion varchar(50)
	,@Cliente varchar(512)
	,@Producto varchar(512)
	,@Servicio varchar(512)
	,@FromView varchar(50)
)
AS

/*************************** Variables De Prueba **********************/
--declare @ServerHostPath varchar(512),@ModuleId varchar(4), @UsuarioCreacion int,@dateFrom datetime,@dateEnd datetime,@NoReclamo varchar(50),@NoRelacion varchar(50),@Cliente varchar(512),@Producto varchar(512),@Servicio varchar(512),@FromView varchar(50)

--set @ServerHostPath = 'http://localhost:8081/sikadev'
--set	@ModuleId = '23'
--set @UsuarioCreacion = 1
--set @dateFrom = '2014-01-01'
--set @dateEnd = '2014-12-01'
--set @NoReclamo = ''
--set @NoRelacion = ''
--set @Cliente = ''
--set @Producto = ''
--set @Servicio = ''
--set @FromView = 'altpersona'
/***********************************************************************/

-- Definiendo variables de trabajo
declare	@TblReport table
		(
			IdReclamo				decimal(18,0)
			,Asesor					varchar(512)
			,AtendidoPor			varchar(512)
			,Solicitante			varchar(512)
			,IngenieroResponsable	varchar(512)
			,ResponsableActual		varchar(512)
			,Autor					varchar(512)
			,TipoReclamo			varchar(50)
			,CodProducto			varchar(512)
			,Producto				varchar(512)
			,TargetMarket			varchar(512)
			,SubCampoAplicacion		varchar(512)
			,CantidadVendida		int
			,CantidadReclamada		int
			,Planta					varchar(512)
			,Aplicado				varchar(4)
			,AplicadoPor			varchar(512)
			,TipoContacto			varchar(512)
			,CampoAplicacion		varchar(512)
			,Presentacion			varchar(512)
			,Categoria				varchar(512)
			,Area					varchar(512)
			,NumeroReclamo			varchar(50)
			,NumeroRelacion			varchar(50)
			,CodCliente				varchar(50)
			,Cliente				varchar(512)
			,UnidadZona				varchar(512)
			,NombreObra				varchar(512)
			,PropietarioObra		varchar(512)
			,Contacto				varchar(512)
			,EmailContacto			varchar(512)
			,AspectoEnvase			varchar(512)
			,AspectoProducto		varchar(512)
			,Lote					varchar(50)
			,Lote2					varchar(50)
			,Lote3					varchar(50)
			,MuestraDisponible		varchar(4)
			,DescripcionProblema	varchar(512)
			,DiagnosticoPrevio		varchar(512)
			,ConclusionesPrevias	varchar(512)
			,ProblemaSolucionado	varchar(512)
			,ObservacionesSolucion	varchar(512)
			,Estado					varchar(50)
			,TotalCostos			decimal(18,2)
			,FechaReclamo			datetime
			,FechaVenta				datetime
			,FechaCierre			datetime
			,IdMes					int
			,Mes					varchar(50)
			,Trimestre				varchar(50)
			,DescripcionProducto	varchar(1024)
			,DescripcionServicio	varchar(1024)
			,DescripcionReclamo		varchar(1024)
			,UrlReclamo				varchar(2048)
		)

-- Seleccionado Datos
insert	into
		@TblReport
		(
			Asesor
			,AtendidoPor
			,Solicitante
			,IngenieroResponsable
			,ResponsableActual
			,Autor
			,IdReclamo			
			,TipoReclamo
			,CodProducto
			,Producto
			,TargetMarket
			,SubCampoAplicacion
			,CampoAplicacion
			,Presentacion
			,CantidadVendida
			,CantidadReclamada
			,Planta
			,Aplicado
			,AplicadoPor
			,TipoContacto
			,Categoria
			,Area
			,NumeroReclamo
			,NumeroRelacion
			,Cliente
			,CodCliente
			,UnidadZona
			,NombreObra
			,PropietarioObra
			,Contacto
			,EmailContacto
			,AspectoEnvase
			,AspectoProducto
			,Lote
			,Lote2
			,Lote3
			,MuestraDisponible
			,DescripcionProblema
			,DiagnosticoPrevio
			,ConclusionesPrevias
			,ProblemaSolucionado
			,ObservacionesSolucion
			,Estado
			,TotalCostos
			,FechaReclamo
			,FechaVenta
			,FechaCierre
			,IdMes
			,Mes
			,Trimestre
			,DescripcionProducto
			,DescripcionServicio
			,UrlReclamo
		)
select	distinct
		asesor.Nombres																			as Asesor
		,atendidoPor.Nombres																	as AtendidoPor
		,solicitante.Nombres																	as Solicitante
		,isnull(ingenieroResponsable.Nombres,'')												as IngenieroResponsable
		,responsable.Nombres																	as Responsable
		,autor.Nombres																			as Autor
		,reclamo.IdReclamo																		as IdReclamo
		,tipoReclamo.Nombre																		as TipoReclamo	
		,isnull(reclamo.CodigoProducto,'')														as CodProducto
		,isnull(producto.PRODUCTO,'')															as Producto
		,isnull(producto.GRUPOCOMPRADORES,'')													as TargetMarket
		,isnull(producto.CAMPOAPL,'')															as SubCampoAplicacion
		,isnull(producto.CATEGORIA,'')															as CampoAplicacion
		,isnull(producto.UNIDAD,'')																as Presentacion
		,isnull(reclamo.CantidadVendida,0)														as CantidadVendida
		,isnull(reclamo.CantidadReclamada,0)													as CantidadReclamada
		,isnull(reclamo.Planta,'')																as Planta
		,case reclamo.Aplicado when 1 then 'SI' when 0 then 'NO' else '' end					as Aplicado
		,reclamo.AplicadoPor																	as AplicadoPor
		,reclamo.TipoContacto																	as TipoContacto
		,isnull(categorias.Nombre,'')															as Categoria
		,isnull(categorias.Area,'')																as Area
		,reclamo.NumeroReclamo																	as NumeroReclamo
		,reclamo.CampoRelacion																	as NumeroRelacion
		,isnull(cliente.CLIENTE,'')																as Cliente
		,isnull(cliente.CODIGOCLIENTE,'')														as CodCliente
		,isnull(reclamo.UnidadZona,'')															as UnidadZona
		,isnull(reclamo.NombreObra,'')															as NombreObra
		,isnull(reclamo.PropietarioObra,'')														as PropietarioObra
		,isnull(reclamo.Contacto,'')															as Contacto
		,isnull(reclamo.EmailContacto,'')														as EmailContacto
		,isnull(reclamo.AspectoEnvase,'')														as AspectoEnvase
		,isnull(reclamo.AspectoProducto,'')														as AspectoProducto
		,isnull(reclamo.Lote,'')																as Lote
		,isnull(reclamo.Lote2,'')																as Lote2
		,isnull(reclamo.Lote3,'')																as Lote3
		,case reclamo.MuestraDisponible when 1 then 'SI' when 0 then 'NO' else '' end			as MuestraDisponible
		,isnull(reclamo.DescripcionProblema,'')													as DescripcionProblema
		,isnull(reclamo.DiagnosticoPrevio,'')													as DiagnosticoPrevio
		,isnull(reclamo.ConclusionesPrevias,'')													as ConclusionesPrevias
		,case reclamo.ProblemaSolucionado when 1 then 'SI' when 0 then 'NO' else '' end			as ProblemaSolucionado
		,isnull(reclamo.ObservacionesSolucion,'')												as ObservacionesSolucion		
		,estado.Descripcion																		as Estado
		,reclamo.CostoTotal																		as TotalCostos
		,reclamo.FechaReclamo																	as FechaReclamo
		,reclamo.FechaVenta																		as FechaVenta
		,reclamo.FechaCierre																	as FechaCierre
		,month(reclamo.FechaReclamo)
		,case month(reclamo.FechaReclamo)
				when 1
				then 'Enero'
				when 2
				then 'Febrero'
				when 3
				then 'Marzo'
				when 4
				then 'Abril'
				when 5
				then 'Mayo'
				when 6
				then 'Junio'
				when 7
				then 'Julio'
				when 8
				then 'Agosto'
				when 9
				then 'Septiembre'
				when 10
				then 'Octubre'
				when 11
				then 'Noviembre'
				when 12
				then 'Diciembre'
			end	
		,case 
				when month(reclamo.FechaReclamo) between 1 and 3
				then 'I Trimestre'
				when month(reclamo.FechaReclamo) between 4 and 6
				then 'II Trimestre'
				when month(reclamo.FechaReclamo) between 7 and 9
				then 'III Trimestre'
				when month(reclamo.FechaReclamo) between 10 and 12
				then 'IV Trimestre'
			end	
		,producto.PRODUCTO + ' (' + producto.CATEGORIA + ') - Cliente: ' + cliente.CLIENTE		as DescripcionProducto
		,categorias.Nombre + ' - Area: ' + categorias.Area										as DescripcionArea
		,@ServerHostPath + '/Pages/Modules/Reclamos/Admin/FrmReclamo.aspx?ModuleId=' + @ModuleId + '&IdReclamo=' + cast(reclamo.IdReclamo as varchar(18)) + '&from=' + @FromView
from	TBL_ModuloReclamos_Reclamo reclamo with(nolock)
		inner join TBL_Admin_EstadosProceso estado with(nolock)
			on reclamo.IdEstado = estado.IdEstado
		inner join TBL_Admin_Usuarios solicitante with(nolock)
			on reclamo.IdSolicitante = solicitante.IdUser
		inner join TBL_Admin_Usuarios atendidoPor with(nolock)
			on reclamo.IdAtendidoPor = atendidoPor.IdUser
		inner join TBL_Admin_Usuarios asesor with(nolock)
			on reclamo.IdAsesoradoPor = asesor.IdUser
		left join TBL_Admin_Usuarios responsable with(nolock)
			on reclamo.IdResponsableActual = responsable.IdUser
		inner join TBL_Admin_Usuarios autor with(nolock)
			on reclamo.CreateBy = autor.IdUser
		inner join TBL_ModuloReclamos_TipoReclamo tipoReclamo with(nolock)
			on reclamo.IdTipoReclamo = tipoReclamo.IdTipoReclamo
		left join TBL_Admin_Usuarios ingenieroResponsable with(nolock)
			on reclamo.IdIngenieroResponsable = ingenieroResponsable.IdUser
		left join Sika_SolverNET..Productos producto with(nolock)
			on reclamo.CodigoProducto = producto.CODIGOPRODUCTO
		left join Sika_SolverNET..Clientes cliente with(nolock)
			on reclamo.CodigoCliente = cliente.CODIGOCLIENTE
		left join TBL_ModuloReclamos_CategoriasReclamo categorias with(nolock)
			on reclamo.IdCategoriaReclamo = categorias.IdCategoriaReclamo
where	reclamo.FechaReclamo >= @dateFrom
		and reclamo.FechaReclamo <= @dateEnd
		and reclamo.NumeroReclamo like '%' + case @NoReclamo when '' then reclamo.NumeroReclamo else @NoReclamo end + '%'
		and (
				isnull(cliente.CLIENTE,'') like '%' + case @Cliente when '' then isnull(cliente.CLIENTE,'') else @Cliente end + '%'
			or
				isnull(cliente.CODIGOCLIENTE,'') like '%' + case @Cliente when '' then isnull(cliente.CODIGOCLIENTE,'') else @Cliente end + '%'
			)
		and (
				isnull(producto.PRODUCTO,'') like '%' + case @Producto when '' then isnull(producto.PRODUCTO,'') else @Producto end + '%'
			or
				isnull(producto.CODIGOPRODUCTO,'') like '%' + case @Producto when '' then isnull(producto.CODIGOPRODUCTO,'') else @Producto end + '%'
			)
		and (
				isnull(categorias.Nombre,'') like '%' + case @Servicio when '' then isnull(categorias.Nombre,'') else @Servicio end + '%'
			or
				isnull(categorias.SubCategoria,'') like '%' + case @Servicio when '' then isnull(categorias.SubCategoria,'') else @Servicio end + '%'
			)
		and (
				isnull(reclamo.CampoRelacion,'') like '%' + case @NoRelacion when '' then isnull(reclamo.CampoRelacion,'') else @NoRelacion end + '%'
			)
			
			
update	@TblReport
set		DescripcionReclamo = DescripcionProducto
where	TipoReclamo = 'Producto'

update	@TblReport
set		DescripcionProducto = ''
where	DescripcionProducto is null

update	@TblReport
set		DescripcionReclamo = DescripcionServicio
where	TipoReclamo = 'Servicio'

update	@TblReport
set		DescripcionServicio = ''
where	DescripcionServicio is null

-- Seleccionando Datos
select	IdReclamo
		,Asesor
		,AtendidoPor
		,Solicitante
		,IngenieroResponsable
		,ResponsableActual
		,Autor
		,TipoReclamo
		,CodProducto
		,Producto
		,TargetMarket
		,SubCampoAplicacion
		,CampoAplicacion
		,Presentacion
		,CantidadVendida
		,CantidadReclamada
		,Planta
		,Aplicado
		,AplicadoPor
		,TipoContacto
		,Categoria
		,Area
		,NumeroReclamo
		,NumeroRelacion
		,Cliente
		,CodCliente
		,UnidadZona
		,NombreObra
		,PropietarioObra
		,Contacto
		,EmailContacto
		,AspectoEnvase
		,AspectoProducto
		,Lote
		,Lote2
		,Lote3
		,MuestraDisponible
		,DescripcionProblema
		,DiagnosticoPrevio
		,ConclusionesPrevias
		,ProblemaSolucionado
		,ObservacionesSolucion
		,Estado
		,TotalCostos
		,FechaReclamo
		,FechaVenta
		,FechaCierre
		,IdMes
		,Mes
		,Trimestre		
		,DescripcionProducto
		,DescripcionServicio
		,DescripcionReclamo
		,UrlReclamo
from	@TblReport
order	by
		FechaReclamo asc