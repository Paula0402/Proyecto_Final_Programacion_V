using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProyectoFinalProgramacionV
{
    /// Representa la cabecera de una venta: fecha, cliente, detalles y total.
    internal class Venta
    {
        public int NumeroDeVenta { get; set; }
        public DateTime FechaDeVenta { get; set; }
        public Cliente ClienteQueCompra { get; set; }
        public List<DetalleDeVenta> DetallesDeLaVenta { get; private set; }
        public decimal TotalDeLaVenta { get; private set; }

        public Venta(int numeroDeVenta, Cliente clienteQueCompra)
        {
            NumeroDeVenta = numeroDeVenta;
            FechaDeVenta = DateTime.Now; // fecha y hora del sistema al momento de crear la venta
            ClienteQueCompra = clienteQueCompra;
            DetallesDeLaVenta = new List<DetalleDeVenta>();
            TotalDeLaVenta = 0;
        }

        // Agrega un detalle (producto + cantidad) y recalcula el total de la venta
        public void AgregarDetalle(DetalleDeVenta detalle)
        {
            if (detalle == null)
                throw new ArgumentNullException(nameof(detalle), "El detalle no puede ser nulo.");

            DetallesDeLaVenta.Add(detalle);
            RecalcularTotalDeVenta();
        }

        // Suma los subtotales de todos los detalles
        public void RecalcularTotalDeVenta()
        {
            decimal total = 0;
            foreach (var detalle in DetallesDeLaVenta)
                total += detalle.Subtotal;

            TotalDeLaVenta = total;
        }

        // Resumen corto para listados de ventas
        public string ObtenerResumenDeVenta()
        {
            return $"Venta #{NumeroDeVenta} | Fecha: {FechaDeVenta:dd/MM/yyyy HH:mm} | " +
                   $"Cliente: {ClienteQueCompra.NombreCompleto} | Total: ₡{TotalDeLaVenta:N2}";
        }

        // Muestra en consola el resumen y cada linea de detalle
        public void MostrarDetalleCompletoEnConsola()
        {
            Console.WriteLine(ObtenerResumenDeVenta());
            foreach (var detalle in DetallesDeLaVenta)
                Console.WriteLine("  - " + detalle.ObtenerDetalleFormateado());
        }
    }
}