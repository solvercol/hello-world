using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Management;
using Application.MainModule.SqlServices.IServices;
using Domain.MainModule.Reclamos.DTO;
using Infraestructure.Data.Core;

namespace Application.MainModule.SqlServices.Services
{
    public class ReclamosExternalInterfacesService : IReclamosExternalInterfacesService
    {
        private readonly ISqlHelper _sql;

        public ReclamosExternalInterfacesService(ISqlHelper sql)
        {
            _sql = sql;
        }

        public List<Dto_Producto> GetAllProducts()
        {
            var items = new List<Dto_Producto>();
            var dt = new DataTable();

            try
            {
                dt = _sql.ExecuteDataTable("Interface_GetAllProducts", CommandType.StoredProcedure);               
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("Interface_GetAllProducts", ex);
            }

            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var producto = new Dto_Producto();

                    producto.CodigoProducto = string.Format("{0}", dt.Rows[i]["CODIGOPRODUCTO"]);
                    producto.Producto = string.Format("{0}", dt.Rows[i]["PRODUCTO"]);
                    producto.Unidad = string.Format("{0}", dt.Rows[i]["UNIDAD"]);
                    producto.PesoNeto = string.IsNullOrEmpty(string.Format("{0}", dt.Rows[i]["PESONETO"])) ? 0 : Convert.ToDecimal(string.Format("{0}", dt.Rows[i]["PESONETO"]));
                    producto.PrecioLista = string.IsNullOrEmpty(string.Format("{0}", dt.Rows[i]["PRECIOLISTA"])) ? 0 : Convert.ToDecimal(string.Format("{0}", dt.Rows[i]["PRECIOLISTA"]));
                    producto.GrupoCompradores = string.Format("{0}", dt.Rows[i]["GRUPOCOMPRADORES"]);
                    producto.CampoApl = string.Format("{0}", dt.Rows[i]["CATEGORIA"]);
                    producto.Categoria = string.Format("{0}", dt.Rows[i]["CAMPOAPL"]);
                    producto.SubCategoria = string.Format("{0}", dt.Rows[i]["SUBCATEGORIA"]);

                    items.Add(producto);
                }
            }

            return items;
        }

        public Dto_Producto GetProductByCodigoProducto(string codigoProducto)
        {
            var producto = new Dto_Producto();
            var dt = new DataTable();

            try
            {
                dt = _sql.ExecuteDataTable("Interface_GetProductByCodProducto", CommandType.StoredProcedure, new SqlParameter("@CodProducto", codigoProducto));
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("Interface_GetProductByCodProducto", ex);
            }

            if (dt != null && dt.Rows.Count > 0)
            {
                int i = 0;

                producto.CodigoProducto = string.Format("{0}", dt.Rows[i]["CODIGOPRODUCTO"]);
                producto.Producto = string.Format("{0}", dt.Rows[i]["PRODUCTO"]);
                producto.Unidad = string.Format("{0}", dt.Rows[i]["UNIDAD"]);
                producto.PesoNeto = string.IsNullOrEmpty(string.Format("{0}", dt.Rows[i]["PESONETO"])) ? 0 : Convert.ToDecimal(string.Format("{0}", dt.Rows[i]["PESONETO"]));
                producto.PrecioLista = string.IsNullOrEmpty(string.Format("{0}", dt.Rows[i]["PRECIOLISTA"])) ? 0 : Convert.ToDecimal(string.Format("{0}", dt.Rows[i]["PRECIOLISTA"]));
                producto.GrupoCompradores = string.Format("{0}", dt.Rows[i]["GRUPOCOMPRADORES"]);
                producto.CampoApl = string.Format("{0}", dt.Rows[i]["CATEGORIA"]);
                producto.Categoria = string.Format("{0}", dt.Rows[i]["CAMPOAPL"]);
                producto.SubCategoria = string.Format("{0}", dt.Rows[i]["SUBCATEGORIA"]);
                if (dt.Rows[i]["IdCategoria"] != null && !string.IsNullOrEmpty(dt.Rows[i]["IdCategoria"].ToString()))
                {
                    producto.IdCategoriaProducto = Convert.ToInt32(dt.Rows[i]["IdCategoria"]);
                }
            }

            return producto;
        }

        public List<Dto_Producto> GetAllProductsByFilter(string filter, int pagesize, int pageindex)
        {
            var items = new List<Dto_Producto>();
            var dt = new DataTable();

            try
            {
                dt = _sql.ExecuteDataTable("Interface_GetProductsByFilters", CommandType.StoredProcedure
                                            , new SqlParameter("@filter", filter)
                                            , new SqlParameter("@PageSize", pagesize)
                                            , new SqlParameter("@PageIndex", pageindex));
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("Interface_GetProductsByFilters", ex);
            }

            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var producto = new Dto_Producto();

                    producto.CodigoProducto = string.Format("{0}", dt.Rows[i]["CODIGOPRODUCTO"]);
                    producto.Producto = string.Format("{0}", dt.Rows[i]["PRODUCTO"]);
                    producto.Unidad = string.Format("{0}", dt.Rows[i]["UNIDAD"]);
                    producto.PesoNeto = string.IsNullOrEmpty(string.Format("{0}", dt.Rows[i]["PESONETO"])) ? 0 : Convert.ToDecimal(string.Format("{0}", dt.Rows[i]["PESONETO"]));
                    producto.PrecioLista = string.IsNullOrEmpty(string.Format("{0}", dt.Rows[i]["PRECIOLISTA"])) ? 0 : Convert.ToDecimal(string.Format("{0}", dt.Rows[i]["PRECIOLISTA"]));
                    producto.GrupoCompradores = string.Format("{0}", dt.Rows[i]["GRUPOCOMPRADORES"]);
                    producto.CampoApl = string.Format("{0}", dt.Rows[i]["CATEGORIA"]);
                    producto.Categoria = string.Format("{0}", dt.Rows[i]["CAMPOAPL"]);
                    producto.SubCategoria = string.Format("{0}", dt.Rows[i]["SUBCATEGORIA"]);

                    items.Add(producto);
                }
            }

            return items;
        }

        public int GetAllProductsByFilterCount(string filter)
        {            
            try
            {
                var total = _sql.ExecuteScalar("Interface_GetProductsByFiltersCount", CommandType.StoredProcedure, new SqlParameter("@filter", filter));

                return Convert.ToInt32(total);
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("Interface_GetProductsByFiltersCount", ex);
            }
        }

        public List<Dto_Cliente> GetAllClients()
        {
            var items = new List<Dto_Cliente>();
            var dt = new DataTable();

            try
            {
                dt = _sql.ExecuteDataTable("Interface_GetAllClients", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("Interface_GetAllClients", ex);
            }

            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var cliente = new Dto_Cliente();

                    cliente.CodigoCliente = string.Format("{0}", dt.Rows[i]["CODIGOCLIENTE"]);
                    cliente.Cliente = string.Format("{0}", dt.Rows[i]["CLIENTE"]);
                    cliente.Contacto = string.Format("{0}", dt.Rows[i]["CONTACTO"]);
                    cliente.Email = string.Format("{0}", dt.Rows[i]["EMAIL"]);
                    cliente.Unidad = string.Format("{0}", dt.Rows[i]["UNIDAD"]);
                    cliente.Zona = string.Format("{0}", dt.Rows[i]["ZONA"]);

                    items.Add(cliente);
                }
            }

            return items;
        }

        public Dto_Cliente GetClientByCodigoCliente(string codigoCliente)
        {
            var cliente = new Dto_Cliente();
            var dt = new DataTable();

            try
            {
                dt = _sql.ExecuteDataTable("Interface_GetClienteByCodCliente", CommandType.StoredProcedure, new SqlParameter("@CodCliente", codigoCliente));
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("Interface_GetClienteByCodCliente", ex);
            }

            if (dt != null && dt.Rows.Count > 0)
            {
                int i = 0;

                cliente.CodigoCliente = string.Format("{0}", dt.Rows[i]["CODIGOCLIENTE"]);
                cliente.Cliente = string.Format("{0}", dt.Rows[i]["CLIENTE"]);
                cliente.Contacto = string.Format("{0}", dt.Rows[i]["CONTACTO"]);
                cliente.Email = string.Format("{0}", dt.Rows[i]["EMAIL"]);
                cliente.Unidad = string.Format("{0}", dt.Rows[i]["UNIDAD"]);
                cliente.Zona = string.Format("{0}", dt.Rows[i]["ZONA"]);
            }

            return cliente;
        }

        public List<Dto_Cliente> GetAllClientsByFilter(string filter, int pagesize, int pageindex)
        {
            var items = new List<Dto_Cliente>();
            var dt = new DataTable();

            try
            {
                dt = _sql.ExecuteDataTable("Interface_GetClientsByFilters", CommandType.StoredProcedure
                                            , new SqlParameter("@filter", filter)
                                            , new SqlParameter("@PageSize", pagesize)
                                            , new SqlParameter("@PageIndex", pageindex));
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("Interface_GetClientsByFilters", ex);
            }

            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var cliente = new Dto_Cliente();

                    cliente.CodigoCliente = string.Format("{0}", dt.Rows[i]["CODIGOCLIENTE"]);
                    cliente.Cliente = string.Format("{0}", dt.Rows[i]["CLIENTE"]);
                    cliente.Contacto = string.Format("{0}", dt.Rows[i]["CONTACTO"]);
                    cliente.Email = string.Format("{0}", dt.Rows[i]["EMAIL"]);
                    cliente.Unidad = string.Format("{0}", dt.Rows[i]["UNIDAD"]);
                    cliente.Zona = string.Format("{0}", dt.Rows[i]["ZONA"]);

                    items.Add(cliente);
                }
            }

            return items;
        }

        public int GetAllClientsByFilterCount(string filter)
        {
            try
            {
                var total = _sql.ExecuteScalar("Interface_GetClientsByFiltersCount", CommandType.StoredProcedure, new SqlParameter("@filter", filter));

                return Convert.ToInt32(total);
            }
            catch (Exception ex)
            {
                throw new SqlExecutionException("Interface_GetClientsByFiltersCount", ex);
            }
        }

    }
}