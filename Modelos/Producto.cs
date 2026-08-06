using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalProgramacionV.Modelos
{

    /// Representa un producto físico con stock dentro del sistema de ventas.


    internal class Producto : Articulo
    {



        public int CantidadEnStock { get; private set; }


        public int StockMinimoPermitido { get; set; }



        /// Crea un nuevo producto con todos sus datos, incluyendo stock inicial.

        public Producto(string codigoDeArticulo, string nombreDelArticulo,
                        string descripcionDelArticulo, decimal precioUnitario,
                        int cantidadEnStock, int stockMinimoPermitido = 5)
            : base(codigoDeArticulo, nombreDelArticulo, descripcionDelArticulo, precioUnitario)
        {
            if (cantidadEnStock < 0)
                throw new ArgumentException("El stock no puede ser negativo.");

            CantidadEnStock = cantidadEnStock;
            StockMinimoPermitido = stockMinimoPermitido;
        }




        /// Devuelve la información completa del producto, incluyendo stock.

        public override string ObtenerInformacionCompleta()
        {
            string estadoDeStock = EstaDisponibleParaVenta() ? "Disponible" : "Sin stock";

            return $"[Producto] Código: {CodigoDeArticulo} | " +
                   $"Nombre: {NombreDelArticulo} | " +
                   $"Precio: ₡{PrecioUnitario:N2} | " +
                   $"Stock: {CantidadEnStock} unidades | " +
                   $"Estado: {estadoDeStock}";
        }

        /// Indica si el producto tiene al menos una unidad disponible en stock.

        public override bool EstaDisponibleParaVenta()
        {
            return CantidadEnStock > 0;
        }



        /// Reduce el stock del producto al realizar una venta.


        public void ReducirStockPorVenta(int cantidadVendida)
        {
            if (cantidadVendida <= 0)
                throw new ArgumentException("La cantidad vendida debe ser mayor a cero.");

            if (cantidadVendida > CantidadEnStock)
                throw new InvalidOperationException(
                    $"Stock insuficiente. Disponible: {CantidadEnStock}, solicitado: {cantidadVendida}.");

            CantidadEnStock -= cantidadVendida;

            if (StockEsBajo())
                Console.WriteLine($"ALERTA: El stock de '{NombreDelArticulo}' es bajo ({CantidadEnStock} unidades).");
        }


        /// Incrementa el stock del producto al realizar una reposición.

        public void ReponerStock(int cantidadAReponer)
        {
            if (cantidadAReponer <= 0)
                throw new ArgumentException("La cantidad a reponer debe ser mayor a cero.");

            CantidadEnStock += cantidadAReponer;
            Console.WriteLine($"Stock de '{NombreDelArticulo}' actualizado a {CantidadEnStock} unidades.");
        }

        // Establece directamente el stock (uso administrativo, ej. correccion desde el formulario)
        public void EstablecerStock(int nuevaCantidadEnStock)
        {
            if (nuevaCantidadEnStock < 0)
                throw new ArgumentException("El stock no puede ser negativo.");

            CantidadEnStock = nuevaCantidadEnStock;
        }


        /// Indica si el stock actual está por debajo del mínimo permitido.

        public bool StockEsBajo()
        {
            return CantidadEnStock <= StockMinimoPermitido;
        }


        /// Verifica si hay suficiente stock para vender una cantidad determinada.

        public bool HaySuficienteStockParaVender(int cantidadRequerida)
        {
            return CantidadEnStock >= cantidadRequerida;
        }
    }
}