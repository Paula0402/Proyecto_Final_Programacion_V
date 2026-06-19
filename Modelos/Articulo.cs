using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalProgramacionV
{

    /// Clase base abstracta que representa cualquier artículo dentro del sistema.
    /// Permite aplicar polimorfismo si en el futuro se agregan tipos como Servicio y etc.

    internal abstract class Articulo
    {

        public string CodigoDeArticulo { get; set; }


        public string NombreDelArticulo { get; set; }


        public string DescripcionDelArticulo { get; set; }


        public decimal PrecioUnitario { get; set; }


        /// Constructor que inicializa los datos base de un artículo.

        protected Articulo(string codigoDeArticulo, string nombreDelArticulo,
                           string descripcionDelArticulo, decimal precioUnitario)
        {
            if (precioUnitario < 0)
                throw new ArgumentException("El precio unitario no puede ser negativo.");

            CodigoDeArticulo = codigoDeArticulo;
            NombreDelArticulo = nombreDelArticulo;
            DescripcionDelArticulo = descripcionDelArticulo;
            PrecioUnitario = precioUnitario;
        }


        public abstract string ObtenerInformacionCompleta();


        public abstract bool EstaDisponibleParaVenta();

        public decimal CalcularSubtotalPorCantidad(int cantidadDeUnidades)
        {
            if (cantidadDeUnidades <= 0)
                throw new ArgumentException("La cantidad de unidades debe ser mayor a cero.");

            return PrecioUnitario * cantidadDeUnidades;
        }


        public void MostrarInformacionEnConsola()
        {
            Console.WriteLine(ObtenerInformacionCompleta());
        }
    }
}
