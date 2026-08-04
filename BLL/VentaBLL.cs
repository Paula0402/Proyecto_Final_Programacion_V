using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoFinalProgramacionV.DAL;
using ProyectoFinalProgramacionV.DTOs;

namespace ProyectoFinalProgramacionV.BLL
{
    internal class VentaBLL
    {
        private IVentaDAL ventaDAL;
        private ProductoBLL productoBLL;
        private ClienteBLL clienteBLL;

        public VentaBLL(ProductoBLL productoBLL, ClienteBLL clienteBLL)
        {
            ventaDAL = new VentaDAL();
            this.productoBLL = productoBLL;
            this.clienteBLL = clienteBLL;
        }

        // Registra en la BD una venta ya validada (el stock se descuenta antes de llamar esto)
        // y persiste el stock actualizado de cada producto.
        public Venta RegistrarVenta(Cliente clienteQueCompra, List<DetalleDeVenta> detalles)
        {
            if (detalles == null || detalles.Count == 0)
                throw new ArgumentException("La venta debe tener al menos un producto.");

            VentaDTO ventaDTO = new VentaDTO
            {
                FechaVenta = DateTime.Now,
                IdCliente = clienteQueCompra.IdentificadorDeCliente,
                TotalVenta = detalles.Sum(d => d.Subtotal)
            };

            List<DetalleVentaDTO> detallesDTO = detalles.Select(d => new DetalleVentaDTO
            {
                CodigoArticulo = d.ProductoVendido.CodigoDeArticulo,
                CantidadVendida = d.CantidadVendida,
                PrecioUnitarioVenta = d.PrecioUnitarioAlMomentoDeVenta,
                Subtotal = d.Subtotal
            }).ToList();

            int numeroVentaGenerado = ventaDAL.InsertarVentaConDetalle(ventaDTO, detallesDTO);

            Venta ventaRegistrada = new Venta(numeroVentaGenerado, clienteQueCompra);
            ventaRegistrada.FechaDeVenta = ventaDTO.FechaVenta;

            foreach (var detalle in detalles)
            {
                productoBLL.ActualizarProducto(detalle.ProductoVendido); // persiste el stock ya descontado
                ventaRegistrada.AgregarDetalle(detalle);
            }

            return ventaRegistrada;
        }

        // Reconstruye todas las ventas guardadas en la base de datos, con sus detalles
        public List<Venta> ObtenerTodasLasVentas()
        {
            List<Venta> ventas = new List<Venta>();
            List<VentaDTO> ventasDTO = ventaDAL.ObtenerTodas();

            foreach (var ventaDTO in ventasDTO)
            {
                Cliente cliente = clienteBLL.BuscarClientePorId(ventaDTO.IdCliente);
                if (cliente == null)
                    continue;

                Venta venta = new Venta(ventaDTO.NumeroVenta, cliente);
                venta.FechaDeVenta = ventaDTO.FechaVenta;

                List<DetalleVentaDTO> detallesDTO = ventaDAL.ObtenerDetallesPorVenta(ventaDTO.NumeroVenta);

                foreach (var detalleDTO in detallesDTO)
                {
                    Producto producto = productoBLL.BuscarProductoPorCodigo(detalleDTO.CodigoArticulo);
                    if (producto == null)
                        continue;

                    DetalleDeVenta detalle = new DetalleDeVenta(producto, detalleDTO.CantidadVendida);
                    venta.AgregarDetalle(detalle);
                }

                ventas.Add(venta);
            }

            return ventas;
        }
    }
}