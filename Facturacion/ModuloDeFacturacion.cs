using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProyectoFinalProgramacionV.Facturacion
{
    /// Modulo principal del sistema: contiene el menu de consola y coordina
    /// las operaciones sobre productos, clientes y ventas.
    internal class ModuloDeFacturacion
    {
        private List<Producto> listaDeProductos;
        private List<Cliente> listaDeClientes;
        private List<Venta> listaDeVentas;
        private int contadorDeVentas;

        public ModuloDeFacturacion()
        {
            listaDeProductos = new List<Producto>();
            listaDeClientes = new List<Cliente>();
            listaDeVentas = new List<Venta>();
            contadorDeVentas = 1;
        }

        public void IniciarMenuPrincipal()
        {
            bool continuarEjecutando = true;

            while (continuarEjecutando)
            {
                Console.WriteLine("\n===== SISTEMA DE VENTAS =====");
                Console.WriteLine("1. Gestionar productos");
                Console.WriteLine("2. Gestionar clientes");
                Console.WriteLine("3. Registrar venta");
                Console.WriteLine("4. Listar ventas");
                Console.WriteLine("5. Salir");
                Console.Write("Seleccione una opcion: ");

                int opcionSeleccionada = Convert.ToInt32(Console.ReadLine());

                switch (opcionSeleccionada)
                {
                    case 1: MenuDeProductos(); break;
                    case 2: MenuDeClientes(); break;
                    case 3: RegistrarNuevaVenta(); break;
                    case 4: ListarTodasLasVentas(); break;
                    case 5:
                        continuarEjecutando = false;
                        Console.WriteLine("Saliendo del sistema...");
                        break;
                    default:
                        Console.WriteLine("Opcion invalida, intente de nuevo.");
                        break;
                }
            }
        }

        // ===================== PRODUCTOS =====================

        private void MenuDeProductos()
        {
            bool regresar = false;

            while (!regresar)
            {
                Console.WriteLine("\n--- Gestion de productos ---");
                Console.WriteLine("1. Agregar producto");
                Console.WriteLine("2. Listar productos");
                Console.WriteLine("3. Actualizar producto");
                Console.WriteLine("4. Eliminar producto");
                Console.WriteLine("5. Regresar");
                Console.Write("Seleccione una opcion: ");

                int opcion = Convert.ToInt32(Console.ReadLine());

                switch (opcion)
                {
                    case 1: AgregarProducto(); break;
                    case 2: ListarProductos(); break;
                    case 3: ActualizarProducto(); break;
                    case 4: EliminarProducto(); break;
                    case 5: regresar = true; break;
                    default: Console.WriteLine("Opcion invalida."); break;
                }
            }
        }

        private void AgregarProducto()
        {
            Console.Write("Codigo del producto: ");
            string codigo = Console.ReadLine();

            Console.Write("Nombre del producto: ");
            string nombre = Console.ReadLine();

            Console.Write("Descripcion: ");
            string descripcion = Console.ReadLine();

            Console.Write("Precio unitario: ");
            decimal precio = Convert.ToDecimal(Console.ReadLine());

            Console.Write("Cantidad en stock: ");
            int stock = Convert.ToInt32(Console.ReadLine());

            try
            {
                Producto nuevoProducto = new Producto(codigo, nombre, descripcion, precio, stock);
                listaDeProductos.Add(nuevoProducto);
                Console.WriteLine("Producto agregado correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al agregar el producto: " + ex.Message);
            }
        }

        private void ListarProductos()
        {
            if (listaDeProductos.Count == 0)
            {
                Console.WriteLine("No hay productos registrados.");
                return;
            }

            Console.WriteLine("\n--- Listado de productos ---");
            foreach (var producto in listaDeProductos)
                producto.MostrarInformacionEnConsola();
        }

        private void ActualizarProducto()
        {
            Console.Write("Codigo del producto a actualizar: ");
            string codigo = Console.ReadLine();

            Producto productoEncontrado = listaDeProductos.FirstOrDefault(p => p.CodigoDeArticulo == codigo);

            if (productoEncontrado == null)
            {
                Console.WriteLine("No se encontro un producto con ese codigo.");
                return;
            }

            Console.Write($"Nuevo nombre ({productoEncontrado.NombreDelArticulo}), enter para dejar igual: ");
            string nuevoNombre = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nuevoNombre))
                productoEncontrado.NombreDelArticulo = nuevoNombre;

            Console.Write($"Nuevo precio ({productoEncontrado.PrecioUnitario}), enter para dejar igual: ");
            string nuevoPrecioTexto = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nuevoPrecioTexto))
                productoEncontrado.PrecioUnitario = Convert.ToDecimal(nuevoPrecioTexto);

            Console.WriteLine("Producto actualizado correctamente.");
        }

        private void EliminarProducto()
        {
            Console.Write("Codigo del producto a eliminar: ");
            string codigo = Console.ReadLine();

            Producto productoEncontrado = listaDeProductos.FirstOrDefault(p => p.CodigoDeArticulo == codigo);

            if (productoEncontrado == null)
            {
                Console.WriteLine("No se encontro un producto con ese codigo.");
                return;
            }

            listaDeProductos.Remove(productoEncontrado);
            Console.WriteLine("Producto eliminado correctamente.");
        }

        // ===================== CLIENTES =====================

        private void MenuDeClientes()
        {
            bool regresar = false;

            while (!regresar)
            {
                Console.WriteLine("\n--- Gestion de clientes ---");
                Console.WriteLine("1. Agregar cliente");
                Console.WriteLine("2. Listar clientes");
                Console.WriteLine("3. Actualizar cliente");
                Console.WriteLine("4. Eliminar cliente");
                Console.WriteLine("5. Regresar");
                Console.Write("Seleccione una opcion: ");

                int opcion = Convert.ToInt32(Console.ReadLine());

                switch (opcion)
                {
                    case 1: AgregarCliente(); break;
                    case 2: ListarClientes(); break;
                    case 3: ActualizarCliente(); break;
                    case 4: EliminarCliente(); break;
                    case 5: regresar = true; break;
                    default: Console.WriteLine("Opcion invalida."); break;
                }
            }
        }

        private void AgregarCliente()
        {
            Console.Write("Identificador del cliente (ej: C-001): ");
            string id = Console.ReadLine();

            Console.Write("Nombre completo: ");
            string nombre = Console.ReadLine();

            Console.Write("Telefono: ");
            string telefono = Console.ReadLine();

            Console.Write("Correo electronico: ");
            string correo = Console.ReadLine();

            try
            {
                Cliente nuevoCliente = new Cliente(id, nombre, telefono, correo);

                if (!nuevoCliente.CorreoElectronicoEsValido())
                    Console.WriteLine("Advertencia: el correo no parece valido, se guardo de todas formas.");

                listaDeClientes.Add(nuevoCliente);
                Console.WriteLine("Cliente agregado correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al agregar el cliente: " + ex.Message);
            }
        }

        private void ListarClientes()
        {
            if (listaDeClientes.Count == 0)
            {
                Console.WriteLine("No hay clientes registrados.");
                return;
            }

            Console.WriteLine("\n--- Listado de clientes ---");
            foreach (var cliente in listaDeClientes)
                cliente.MostrarInformacionEnConsola();
        }

        private void ActualizarCliente()
        {
            Console.Write("Identificador del cliente a actualizar: ");
            string id = Console.ReadLine();

            Cliente clienteEncontrado = listaDeClientes.FirstOrDefault(c => c.IdentificadorDeCliente == id);

            if (clienteEncontrado == null)
            {
                Console.WriteLine("No se encontro un cliente con ese identificador.");
                return;
            }

            Console.Write($"Nuevo telefono ({clienteEncontrado.NumeroDeTelefono}), enter para dejar igual: ");
            string nuevoTelefono = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nuevoTelefono))
                clienteEncontrado.NumeroDeTelefono = nuevoTelefono;

            Console.Write($"Nuevo correo ({clienteEncontrado.CorreoElectronico}), enter para dejar igual: ");
            string nuevoCorreo = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nuevoCorreo))
                clienteEncontrado.CorreoElectronico = nuevoCorreo;

            Console.WriteLine("Cliente actualizado correctamente.");
        }

        private void EliminarCliente()
        {
            Console.Write("Identificador del cliente a eliminar: ");
            string id = Console.ReadLine();

            Cliente clienteEncontrado = listaDeClientes.FirstOrDefault(c => c.IdentificadorDeCliente == id);

            if (clienteEncontrado == null)
            {
                Console.WriteLine("No se encontro un cliente con ese identificador.");
                return;
            }

            listaDeClientes.Remove(clienteEncontrado);
            Console.WriteLine("Cliente eliminado correctamente.");
        }

        // ===================== VENTAS =====================

        private void RegistrarNuevaVenta()
        {
            if (listaDeClientes.Count == 0)
            {
                Console.WriteLine("Debe existir al menos un cliente registrado antes de vender.");
                return;
            }

            if (listaDeProductos.Count == 0)
            {
                Console.WriteLine("Debe existir al menos un producto registrado antes de vender.");
                return;
            }

            ListarClientes();
            Console.Write("Identificador del cliente que compra: ");
            string idCliente = Console.ReadLine();

            Cliente clienteSeleccionado = listaDeClientes.FirstOrDefault(c => c.IdentificadorDeCliente == idCliente);

            if (clienteSeleccionado == null)
            {
                Console.WriteLine("Cliente no encontrado.");
                return;
            }

            Venta nuevaVenta = new Venta(contadorDeVentas, clienteSeleccionado);
            bool seguirAgregandoProductos = true;

            while (seguirAgregandoProductos)
            {
                ListarProductos();
                Console.Write("Codigo del producto a vender (0 para terminar): ");
                string codigoProducto = Console.ReadLine();

                if (codigoProducto == "0")
                {
                    seguirAgregandoProductos = false;
                    continue;
                }

                Producto productoSeleccionado = listaDeProductos.FirstOrDefault(p => p.CodigoDeArticulo == codigoProducto);

                if (productoSeleccionado == null)
                {
                    Console.WriteLine("Producto no encontrado.");
                    continue;
                }

                Console.Write("Cantidad a vender: ");
                int cantidad = Convert.ToInt32(Console.ReadLine());

                try
                {
                    DetalleDeVenta detalle = new DetalleDeVenta(productoSeleccionado, cantidad);
                    nuevaVenta.AgregarDetalle(detalle);
                    productoSeleccionado.ReducirStockPorVenta(cantidad); // se descuenta el stock al momento
                    Console.WriteLine("Producto agregado a la venta.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            if (nuevaVenta.DetallesDeLaVenta.Count == 0)
            {
                Console.WriteLine("La venta se cancelo porque no se agrego ningun producto.");
                return;
            }

            listaDeVentas.Add(nuevaVenta);
            clienteSeleccionado.AgregarVentaAlHistorial(nuevaVenta);
            contadorDeVentas++;

            Console.WriteLine("\nVenta registrada con exito:");
            nuevaVenta.MostrarDetalleCompletoEnConsola();
        }

        private void ListarTodasLasVentas()
        {
            if (listaDeVentas.Count == 0)
            {
                Console.WriteLine("No hay ventas registradas.");
                return;
            }

            Console.WriteLine("\n--- Listado de ventas ---");
            foreach (var venta in listaDeVentas)
                Console.WriteLine(venta.ObtenerResumenDeVenta());
        }
    }
}