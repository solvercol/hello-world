using System.Data;

namespace Application.MainModule.SqlServices.IServices
{
    public interface IPedidosEmpacorServices
    {

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
        DataTable ListadoClientesPedido(int pagina, int registrosPorPagina, string codigoCliente, string nombreCliente);

        /// <summary>
        /// Cuenta el numero de registros de clientes y pedidos del esquema Pedidos_Empacor
        /// </summary>
        /// <param name="codigoCliente"></param>
        /// <param name="nombreCliente"></param>
        /// <returns></returns>
        int CountClientesPedido(string codigoCliente, string nombreCliente);

        /// <summary>
        /// Obtiene el listado de clientes de la base de datos intermedia cuando existan pedidos creados en la Base de Datos de PedidosEmpacor
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="registrosPorPagina"></param>
        /// <param name="codigoCliente"></param>
        /// <param name="nombreCliente"></param>
        /// <returns></returns>
        DataTable ListadoClientes(int pagina, int registrosPorPagina, string codigoCliente, string nombreCliente);

        /// <summary>
        /// Cuenta el numero de clientes que tienen registrados pedidos en la base de datos PedidosEmpacor
        /// </summary>
        /// <param name="codigoCliente"></param>
        /// <param name="nombreCliente"></param>
        /// <returns></returns>
        int CountClientes(string codigoCliente, string nombreCliente);

        /// <summary>
        /// Retorna un unico pedido utilizando el Id del mismo como filtro
        /// </summary>
        /// <param name="idPedido"></param>
        /// <returns></returns>
        DataTable RetornarPedidoPorIdPedido(string idPedido);

        /// <summary>
        ///  Retorna la información completa del pedido, con las secciones de Informacion de referencia e Informacion Pedido.
        /// </summary>
        /// <param name="idPedido"></param>
        /// <returns></returns>
        DataTable CargarPedidoCompletoPorIdPedido(string idPedido);
        #endregion

        #region Despachar A

        /// <summary>
        /// Muestra un listado de despachos filtradas por el codigo del cliente
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="registrosPorPagina"></param>
        /// <param name="codigoCliente"></param>
        /// <param name="empresaDespacho"></param>
        /// <returns></returns>
        DataTable ListadoDespacharA(int pagina, int registrosPorPagina, string codigoCliente, string empresaDespacho);

        /// <summary>
        /// Cuenta el listado de Despachos filtrados por codigo del cliente
        /// </summary>
        /// <param name="codigoCliente"></param>
        /// <param name="empresaDespacho"></param>
        /// <returns></returns>
        int CountDespacharA(string codigoCliente, string empresaDespacho);

        /// <summary>
        /// Obtiene el listado de sitios de despacho filtrados por el codigo del cliente.
        /// </summary>
        /// <param name="codigoCliente"></param>
        /// <returns></returns>
        DataTable SitiosDeDespacho(string codigoCliente);

        #endregion

        #region Cotizaciones
        /// <summary>
        /// Obtiene el listado de cotizaciones filtradas por el codigo de cliente
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="registrosPorPagina"></param>
        /// <param name="codigoCliente"></param>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        DataTable ListadoCotizaciones(int pagina, int registrosPorPagina, string codigoCliente, string descripcion);

        /// <summary>
        /// Cuenta el numero de cotizaciones filtradas por el numero de cliente.
        /// </summary>
        /// <param name="codigoCliente"></param>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        int CountCotizaciones(string codigoCliente, string descripcion);

        /// <summary>
        /// Obtiene un unico registro desde la Base de Datos Pedidos_Empacor filtrado por el numero de la cotización 
        /// </summary>
        /// <param name="numeroCotizacion"></param>
        /// <returns></returns>
        DataTable ObtenerCotizacionByNoCotizacion(string numeroCotizacion);

        /// <summary>
        /// Obtiene el listado de mensajes para el numero de cotización pasado como parámetro.
        /// </summary>
        /// <param name="numeroCotizacion"></param>
        /// <returns></returns>
        DataTable ObtenerListadoMensajesPornumeroCotizacion(string numeroCotizacion);
        #endregion

        #region Partes

        /// <summary>
        /// Obtiene las partes asociadas a la referencia seleccionada.
        /// </summary>
        /// <param name="numeroCotizacion"></param>
        /// <returns></returns>
        DataTable ListadoPartesPorNumeroCotizacion(string numeroCotizacion);

        #endregion
    }
}