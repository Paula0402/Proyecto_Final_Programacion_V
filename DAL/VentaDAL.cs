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
    internal interface IVentaDAL
    {
        int InsertarVentaConDetalle(VentaDTO venta, List<DetalleVentaDTO> detalles);
        List<VentaDTO> ObtenerTodas();
        List<DetalleVentaDTO> ObtenerDetallesPorVenta(int numeroVenta);
    }

    internal class VentaDAL : IVentaDAL
    {
        private ConexionBaseDatos conexionBaseDatos;

        public VentaDAL()
        {
            conexionBaseDatos = new ConexionBaseDatos();
        }

        public int InsertarVentaConDetalle(VentaDTO venta, List<DetalleVentaDTO> detalles)
        {
            using (MySqlConnection cnx = conexionBaseDatos.ObtenerConexionAbierta())
            {
                MySqlTransaction transaccion = cnx.BeginTransaction();

                try
                {
                    string queryVenta = "INSERT INTO ventas (fecha_venta, id_cliente, total_venta) " +
                                         "VALUES (@fecha, @idCliente, @total); SELECT LAST_INSERT_ID();";

                    MySqlCommand cmdVenta = new MySqlCommand(queryVenta, cnx, transaccion);
                    cmdVenta.Parameters.AddWithValue("@fecha", venta.FechaVenta);
                    cmdVenta.Parameters.AddWithValue("@idCliente", venta.IdCliente);
                    cmdVenta.Parameters.AddWithValue("@total", venta.TotalVenta);

                    int numeroVentaGenerado = Convert.ToInt32(cmdVenta.ExecuteScalar());

                    string queryDetalle = "INSERT INTO detalle_venta (numero_venta, codigo_articulo, cantidad_vendida, precio_unitario_venta, subtotal) " +
                                           "VALUES (@numeroVenta, @codigo, @cantidad, @precio, @subtotal)";

                    foreach (var detalle in detalles)
                    {
                        MySqlCommand cmdDetalle = new MySqlCommand(queryDetalle, cnx, transaccion);
                        cmdDetalle.Parameters.AddWithValue("@numeroVenta", numeroVentaGenerado);
                        cmdDetalle.Parameters.AddWithValue("@codigo", detalle.CodigoArticulo);
                        cmdDetalle.Parameters.AddWithValue("@cantidad", detalle.CantidadVendida);
                        cmdDetalle.Parameters.AddWithValue("@precio", detalle.PrecioUnitarioVenta);
                        cmdDetalle.Parameters.AddWithValue("@subtotal", detalle.Subtotal);
                        cmdDetalle.ExecuteNonQuery();
                    }

                    transaccion.Commit();
                    return numeroVentaGenerado;
                }
                catch (Exception)
                {
                    transaccion.Rollback();
                    throw;
                }
            }
        }

        public List<VentaDTO> ObtenerTodas()
        {
            List<VentaDTO> ventas = new List<VentaDTO>();

            using (MySqlConnection cnx = conexionBaseDatos.ObtenerConexionAbierta())
            {
                string query = "SELECT * FROM ventas";
                MySqlCommand cmd = new MySqlCommand(query, cnx);

                using (MySqlDataReader lector = cmd.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        ventas.Add(new VentaDTO
                        {
                            NumeroVenta = lector.GetInt32("numero_venta"),
                            FechaVenta = lector.GetDateTime("fecha_venta"),
                            IdCliente = lector.GetString("id_cliente"),
                            TotalVenta = lector.GetDecimal("total_venta")
                        });
                    }
                }
            }

            return ventas;
        }

        public List<DetalleVentaDTO> ObtenerDetallesPorVenta(int numeroVenta)
        {
            List<DetalleVentaDTO> detalles = new List<DetalleVentaDTO>();

            using (MySqlConnection cnx = conexionBaseDatos.ObtenerConexionAbierta())
            {
                string query = "SELECT * FROM detalle_venta WHERE numero_venta = @numeroVenta";
                MySqlCommand cmd = new MySqlCommand(query, cnx);
                cmd.Parameters.AddWithValue("@numeroVenta", numeroVenta);

                using (MySqlDataReader lector = cmd.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        detalles.Add(new DetalleVentaDTO
                        {
                            NumeroVenta = lector.GetInt32("numero_venta"),
                            CodigoArticulo = lector.GetString("codigo_articulo"),
                            CantidadVendida = lector.GetInt32("cantidad_vendida"),
                            PrecioUnitarioVenta = lector.GetDecimal("precio_unitario_venta"),
                            Subtotal = lector.GetDecimal("subtotal")
                        });
                    }
                }
            }

            return detalles;
        }
    }
}