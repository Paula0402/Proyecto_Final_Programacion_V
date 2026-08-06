using ProyectoFinalProgramacionV.BLL;
using ProyectoFinalProgramacionV.Fabrica;
using ProyectoFinalProgramacionV.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoFinalProgramacionV.UI
{
    public partial class FormularioVentas : Form
    {
        private ProductoBLL productoBLL;
        private ClienteBLL clienteBLL;
        private VentaBLL ventaBLL;

        private List<DetalleDeVenta> detallesDeLaVentaActual;

        public FormularioVentas()
        {
            InitializeComponent();

            productoBLL = FabricaBLL.CrearProductoBLL();
            clienteBLL = FabricaBLL.CrearClienteBLL();
            ventaBLL = FabricaBLL.CrearVentaBLL(productoBLL, clienteBLL);

            detallesDeLaVentaActual = new List<DetalleDeVenta>();

            CargarClientesEnCombo();
            CargarProductosEnCombo();
        }

        private void CargarClientesEnCombo()
        {
            List<Cliente> clientes = clienteBLL.ObtenerTodosLosClientes();

            var itemsCombo = clientes.Select(c => new
            {
                Texto = c.IdentificadorDeCliente + " - " + c.NombreCompleto,
                Cliente = c
            }).ToList();

            cmbClientes.DataSource = itemsCombo;
            cmbClientes.DisplayMember = "Texto";
            cmbClientes.ValueMember = "Cliente";
        }

        private void CargarProductosEnCombo()
        {
            List<Producto> productos = productoBLL.ObtenerTodosLosProductos();

            var itemsCombo = productos.Select(p => new
            {
                Texto = p.CodigoDeArticulo + " - " + p.NombreDelArticulo + " (Stock: " + p.CantidadEnStock + ")",
                Producto = p
            }).ToList();

            cmbProductos.DataSource = itemsCombo;
            cmbProductos.DisplayMember = "Texto";
            cmbProductos.ValueMember = "Producto";
        }

        // Agrega el producto seleccionado, con la cantidad indicada, a la venta que se esta armando
        private void BtnAgregarProducto_Click(object sender, EventArgs e)
        {
            if (cmbProductos.SelectedValue == null)
            {
                MessageBox.Show("Selecciona un producto.", "Aviso");
                return;
            }

            if (!int.TryParse(txtCantidad.Text, out int cantidad))
            {
                MessageBox.Show("La cantidad debe ser un numero entero valido.", "Aviso");
                return;
            }

            Producto productoSeleccionado = (Producto)cmbProductos.SelectedValue;

            try
            {
                DetalleDeVenta detalle = new DetalleDeVenta(productoSeleccionado, cantidad);
                productoSeleccionado.ReducirStockPorVenta(cantidad); // valida y descuenta el stock en memoria
                detallesDeLaVentaActual.Add(detalle);

                txtCantidad.Clear();
                CargarProductosEnCombo(); // refresca el stock mostrado en el combo
                ActualizarGrillaDeDetalle();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error");
            }
        }

        // Refresca la tabla de detalle y el total con lo que lleva la venta actual
        private void ActualizarGrillaDeDetalle()
        {
            dgvDetalle.DataSource = null;
            dgvDetalle.DataSource = detallesDeLaVentaActual.Select(d => new
            {
                Producto = d.ProductoVendido.NombreDelArticulo,
                Cantidad = d.CantidadVendida,
                PrecioUnitario = d.PrecioUnitarioAlMomentoDeVenta,
                Subtotal = d.Subtotal
            }).ToList();

            decimal total = detallesDeLaVentaActual.Sum(d => d.Subtotal);
            lblTotal.Text = "Total: " + total.ToString("C2");
        }

        private void BtnRegistrarVenta_Click(object sender, EventArgs e)
        {
            if (cmbClientes.SelectedValue == null)
            {
                MessageBox.Show("Selecciona un cliente.", "Aviso");
                return;
            }

            if (detallesDeLaVentaActual.Count == 0)
            {
                MessageBox.Show("Agrega al menos un producto a la venta.", "Aviso");
                return;
            }

            Cliente clienteSeleccionado = (Cliente)cmbClientes.SelectedValue;

            try
            {
                Venta ventaRegistrada = ventaBLL.RegistrarVenta(clienteSeleccionado, detallesDeLaVentaActual);
                MessageBox.Show("Venta #" + ventaRegistrada.NumeroDeVenta + " registrada correctamente.", "Exito");

                detallesDeLaVentaActual = new List<DetalleDeVenta>();
                ActualizarGrillaDeDetalle();
                CargarProductosEnCombo();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar la venta: " + ex.Message, "Error");
            }
        }

        private void BtnRegresar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}