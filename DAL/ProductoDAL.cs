using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using ProyectoFinalProgramacionV.DTOs;
using ProyectoFinalProgramacionV.Datos;

namespace ProyectoFinalProgramacionV.DAL
{
    internal interface IProductoDAL
    {
        void Insertar(ProductoDTO producto);
        void Actualizar(ProductoDTO producto);
        void Eliminar(string codigoArticulo);
        ProductoDTO BuscarPorCodigo(string codigoArticulo);
        List<ProductoDTO> ObtenerTodos();
    }

    internal class ProductoDAL : IProductoDAL
    {
        private ConexionBaseDatos conexionBaseDatos;

        public ProductoDAL()
        {
            conexionBaseDatos = new ConexionBaseDatos();
        }

        public void Insertar(ProductoDTO producto)
        {
            using (MySqlConnection cnx = conexionBaseDatos.ObtenerConexionAbierta())
            {
                string query = "INSERT INTO productos (codigo_articulo, nombre_articulo, descripcion_articulo, precio_unitario, cantidad_en_stock, stock_minimo) " +
                               "VALUES (@codigo, @nombre, @descripcion, @precio, @stock, @stockMinimo)";

                MySqlCommand cmd = new MySqlCommand(query, cnx);
                cmd.Parameters.AddWithValue("@codigo", producto.CodigoArticulo);
                cmd.Parameters.AddWithValue("@nombre", producto.NombreArticulo);
                cmd.Parameters.AddWithValue("@descripcion", producto.DescripcionArticulo);
                cmd.Parameters.AddWithValue("@precio", producto.PrecioUnitario);
                cmd.Parameters.AddWithValue("@stock", producto.CantidadEnStock);
                cmd.Parameters.AddWithValue("@stockMinimo", producto.StockMinimo);

                cmd.ExecuteNonQuery();
            }
        }

        public void Actualizar(ProductoDTO producto)
        {
            using (MySqlConnection cnx = conexionBaseDatos.ObtenerConexionAbierta())
            {
                string query = "UPDATE productos SET nombre_articulo = @nombre, descripcion_articulo = @descripcion, " +
                               "precio_unitario = @precio, cantidad_en_stock = @stock, stock_minimo = @stockMinimo " +
                               "WHERE codigo_articulo = @codigo";

                MySqlCommand cmd = new MySqlCommand(query, cnx);
                cmd.Parameters.AddWithValue("@codigo", producto.CodigoArticulo);
                cmd.Parameters.AddWithValue("@nombre", producto.NombreArticulo);
                cmd.Parameters.AddWithValue("@descripcion", producto.DescripcionArticulo);
                cmd.Parameters.AddWithValue("@precio", producto.PrecioUnitario);
                cmd.Parameters.AddWithValue("@stock", producto.CantidadEnStock);
                cmd.Parameters.AddWithValue("@stockMinimo", producto.StockMinimo);

                cmd.ExecuteNonQuery();
            }
        }

        public void Eliminar(string codigoArticulo)
        {
            using (MySqlConnection cnx = conexionBaseDatos.ObtenerConexionAbierta())
            {
                try
                {
                    string query = "DELETE FROM productos WHERE codigo_articulo = @codigo";
                    MySqlCommand cmd = new MySqlCommand(query, cnx);
                    cmd.Parameters.AddWithValue("@codigo", codigoArticulo);
                    cmd.ExecuteNonQuery();
                }
                catch (MySqlException ex) when (ex.Number == 1451)
                {
                    throw new Exception("No se puede eliminar este producto porque ya tiene ventas registradas asociadas.");
                }
            }
        }

        public ProductoDTO BuscarPorCodigo(string codigoArticulo)
        {
            using (MySqlConnection cnx = conexionBaseDatos.ObtenerConexionAbierta())
            {
                string query = "SELECT * FROM productos WHERE codigo_articulo = @codigo";
                MySqlCommand cmd = new MySqlCommand(query, cnx);
                cmd.Parameters.AddWithValue("@codigo", codigoArticulo);

                using (MySqlDataReader lector = cmd.ExecuteReader())
                {
                    if (lector.Read())
                        return MapearFilaAProductoDTO(lector);

                    return null;
                }
            }
        }

        public List<ProductoDTO> ObtenerTodos()
        {
            List<ProductoDTO> productos = new List<ProductoDTO>();

            using (MySqlConnection cnx = conexionBaseDatos.ObtenerConexionAbierta())
            {
                string query = "SELECT * FROM productos";
                MySqlCommand cmd = new MySqlCommand(query, cnx);

                using (MySqlDataReader lector = cmd.ExecuteReader())
                {
                    while (lector.Read())
                        productos.Add(MapearFilaAProductoDTO(lector));
                }
            }

            return productos;
        }

        // Convierte una fila del lector de MySQL en un ProductoDTO
        private ProductoDTO MapearFilaAProductoDTO(MySqlDataReader lector)
        {
            return new ProductoDTO
            {
                CodigoArticulo = lector.GetString("codigo_articulo"),
                NombreArticulo = lector.GetString("nombre_articulo"),
                DescripcionArticulo = lector.IsDBNull(lector.GetOrdinal("descripcion_articulo")) ? "" : lector.GetString("descripcion_articulo"),
                PrecioUnitario = lector.GetDecimal("precio_unitario"),
                CantidadEnStock = lector.GetInt32("cantidad_en_stock"),
                StockMinimo = lector.GetInt32("stock_minimo")
            };
        }
    }
}