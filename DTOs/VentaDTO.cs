using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalProgramacionV.DTOs
{
    internal class VentaDTO
    {
        public int NumeroVenta { get; set; }
        public DateTime FechaVenta { get; set; }
        public string IdCliente { get; set; }
        public decimal TotalVenta { get; set; }
    }

    internal class DetalleVentaDTO
    {
        public int NumeroVenta { get; set; }
        public string CodigoArticulo { get; set; }
        public int CantidadVendida { get; set; }
        public decimal PrecioUnitarioVenta { get; set; }
        public decimal Subtotal { get; set; }
    }
}