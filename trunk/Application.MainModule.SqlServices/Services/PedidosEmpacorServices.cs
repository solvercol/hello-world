using System.Data;
using Application.MainModule.SqlServices.Domain;
using Application.MainModule.SqlServices.IServices;
using Infraestructure.Data.Core;

namespace Application.MainModule.SqlServices.Services
{
    public class PedidosEmpacorServices : IPedidosEmpacorServices
    {
        private readonly ISqlHelper _sql;

        public PedidosEmpacorServices(ISqlHelper sql)
        {
            _sql = sql;
        }

        #region Clientes

        /// <summary>
        /// Obtiene el listado de Clientes y Pedidos del Esquema Pedidos_Empacor.
        /// Esta consulta se utiliza en el Buscador  de clientes.
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="registrosPorPagina"></param>
        /// <param name="codigoCliente"></param>
        /// <param name="nombreCliente"></param>
        /// <returns></returns>
        public DataTable  ListadoClientesPedido(int pagina, int registrosPorPagina, string codigoCliente, string nombreCliente)
        {
            var cliente = new PedidoClientes(_sql);
            return cliente.ListadoClientesPedido(pagina, registrosPorPagina, codigoCliente, nombreCliente);
        }

        public int  CountClientesPedido(string codigoCliente, string nombreCliente)
        {
            var cliente = new PedidoClientes(_sql);
            return cliente.CountClientesPedido(codigoCliente, nombreCliente);
        }

        /// <summary>
        /// Obtiene el listado de clientes que tienen registrado pedidos en la base de datos de PedidosEmpacor
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="registrosPorPagina"></param>
        /// <param name="codigoCliente"></param>
        /// <param name="nombreCliente"></param>
        /// <returns></returns>
        public DataTable ListadoClientes(int pagina, int registrosPorPagina, string codigoCliente, string nombreCliente)
        {
            var cliente = new Clientes(_sql);
            return cliente.ListadoClientes(pagina, registrosPorPagina, codigoCliente, nombreCliente);
        }

        /// <summary>
        /// Cuenta el numero de clientes con pedidos en la Base de datos EmpacorPedidos
        /// </summary>
        /// <param name="codigoCliente"></param>
        /// <param name="nombreCliente"></param>
        /// <returns></returns>
        public int CountClientes( string codigoCliente, string nombreCliente)
        {
            var cliente = new Clientes(_sql);
            return cliente.CountClientes(codigoCliente, nombreCliente);
        }

        /// <summary>
        /// Retorna un unico pedido utilizando el Id del Pedido como filtro
        /// </summary>
        /// <param name="idPedido"></param>
        /// <returns></returns>
        public DataTable RetornarPedidoPorIdPedido( string idPedido)
        {
            var pedido = new PedidoClientes(_sql);
            return pedido.RetornarPedidoPorIdPedido(idPedido);
        }

        /// <summary>
        /// Retorna la información completa del pedido, con las secciones de Informacion de referencia e Informacion Pedido.
        /// </summary>
        /// <param name="idPedido"></param>
        /// <returns></returns>
        public DataTable CargarPedidoCompletoPorIdPedido(string idPedido)
        {
            var pedido = new PedidoClientes(_sql);
            return pedido.CargarPedidoCompetoPorIdPedido(idPedido);
        }

        #endregion

        #region Despachos

        public DataTable ListadoDespacharA(int pagina, int registrosPorPagina, string codigoCliente, string empresaDespacho)
        {
            var cliente = new DespacharA(_sql);
            return cliente.ListadoDespacharA(pagina, registrosPorPagina, codigoCliente, empresaDespacho);
        }

        public int CountDespacharA(string codigoCliente, string empresaDespacho)
        {
            var cliente = new DespacharA(_sql);
            return cliente.CountDespacharA(codigoCliente, empresaDespacho);
        }

        public DataTable SitiosDeDespacho(string codigoCliente)
        {
            var cliente = new DespacharA(_sql);
            return cliente.ListadoSitiosDespacho( codigoCliente);
        }

        #endregion

        #region Cotizaciones

        public DataTable ListadoCotizaciones(int pagina, int registrosPorPagina, string codigoCliente, string descripcion)
        {
            var cotizaciones = new Cotizaciones(_sql);
            return cotizaciones.ListadoCotizaciones(pagina, registrosPorPagina, codigoCliente, descripcion);
        }

        public int CountCotizaciones(string codigoCliente, string descripcion)
        {
            var cotizaciones = new Cotizaciones(_sql);
            return cotizaciones.CountCotizaciones(codigoCliente, descripcion);
        }

        public DataTable ObtenerCotizacionByNoCotizacion(string numeroCotizacion)
        {
            var cotizaciones = new Cotizaciones(_sql);
            return cotizaciones.ObtenerCotizacionByNoCotizacion(numeroCotizacion);
        }

        public DataTable ObtenerListadoMensajesPornumeroCotizacion(string numeroCotizacion)
        {
            var cotizaciones = new Cotizaciones(_sql);
            return cotizaciones.ObtenerListadoMensajesPornumeroCotizacion(numeroCotizacion);
        }
        #endregion

        #region Partes

        public DataTable ListadoPartesPorNumeroCotizacion( string numeroCotizacion)
        {
            var parte = new Partes(_sql);
            return parte.ListadoPartesPorNumeroCotizacion(numeroCotizacion);
        }


        #endregion

    }
}