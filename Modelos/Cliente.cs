
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalProgramacionV
{

    /// Representa a un cliente registrado en el sistema de ventas.

    internal class Cliente : Persona
    {

        /// <summary>Identificador único del cliente (ej: C-001).</summary>
        public string IdentificadorDeCliente { get; set; }

        /// <summary>Lista de ventas realizadas por este cliente.</summary>
        public List<Venta> HistorialDeVentas { get; private set; }



        /// Crea un nuevo cliente con todos sus datos básicos.

        public Cliente(string identificadorDeCliente, string nombreCompleto,
                       string numeroDeTelefono, string correoElectronico)
            : base(nombreCompleto, numeroDeTelefono, correoElectronico)
        {
            IdentificadorDeCliente = identificadorDeCliente;
            HistorialDeVentas = new List<Venta>();
        }


        //Reescribe sobre la plantilla de cliente con la información nueva dada
        public override string ObtenerInformacionResumida()
        {
            return $"[Cliente] ID: {IdentificadorDeCliente} | " +
                   $"Nombre: {NombreCompleto} | " +
                   $"Teléfono: {NumeroDeTelefono} | " +
                   $"Correo: {CorreoElectronico}";
        }


        /// Agrega una venta al historial del cliente.

        public void AgregarVentaAlHistorial(Venta ventaRealizada)
        {
            if (ventaRealizada == null)
                throw new ArgumentNullException(nameof(ventaRealizada), "La venta no puede ser nula.");

            HistorialDeVentas.Add(ventaRealizada);
        }

        /// Calcula el total acumulado de todas las compras del cliente.

        public decimal CalcularTotalAcumuladoDeCompras()
        {
            decimal totalAcumulado = 0;
            foreach (var venta in HistorialDeVentas)
                totalAcumulado += venta.TotalDeLaVenta;

            return totalAcumulado;
        }


        /// Muestra en consola el historial completo de ventas del cliente.

        public void MostrarHistorialDeVentasEnConsola()
        {
            Console.WriteLine($"\n=== Historial de ventas de {NombreCompleto} ===");

            if (HistorialDeVentas.Count == 0)
            {
                Console.WriteLine("  Este cliente aún no tiene ventas registradas.");
                return;
            }

            foreach (var venta in HistorialDeVentas)
                Console.WriteLine($"  - {venta.ObtenerResumenDeVenta()}");

            Console.WriteLine($"  TOTAL ACUMULADO: ₡{CalcularTotalAcumuladoDeCompras():N2}");
        }
    }
}