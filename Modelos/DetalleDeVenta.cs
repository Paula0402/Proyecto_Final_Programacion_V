using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProyectoFinalProgramacionV_Gitlab
{
    /// Representa una linea de detalle dentro de una venta: el producto vendido,
    /// la cantidad y el subtotal calculado en el momento de la venta.
    internal class DetalleDeVenta
    {
        public Producto ProductoVendido { get; set; }
        public int CantidadVendida { get; set; }
        public decimal PrecioUnitarioAlMomentoDeVenta { get; set; }
        public decimal Subtotal { get; private set; }

        public DetalleDeVenta(Producto productoVendido, int cantidadVendida)
        {
            if (cantidadVendida <= 0)
                throw new ArgumentException("La cantidad vendida debe ser mayor a cero.");

            ProductoVendido = productoVendido;
            CantidadVendida = cantidadVendida;
            PrecioUnitarioAlMomentoDeVenta = productoVendido.PrecioUnitario; // se guarda el precio actual por si cambia despues
            Subtotal = PrecioUnitarioAlMomentoDeVenta * cantidadVendida;
        }

        // Arma el texto para mostrar la linea de detalle en consola
        public string ObtenerDetalleFormateado()
        {
            return $"{ProductoVendido.NombreDelArticulo} x{CantidadVendida} " +
                   $"@ ₡{PrecioUnitarioAlMomentoDeVenta:N2} = ₡{Subtotal:N2}";
        }
    }
}