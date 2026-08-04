using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalProgramacionV.DTOs
{
    internal class ProductoDTO
    {
        public string CodigoArticulo { get; set; }
        public string NombreArticulo { get; set; }
        public string DescripcionArticulo { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int CantidadEnStock { get; set; }
        public int StockMinimo { get; set; }
    }
}