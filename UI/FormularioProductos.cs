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
    public partial class FormularioProductos : Form
    {
        private ProductoBLL productoBLL;

        public FormularioProductos()
        {
            InitializeComponent();
            productoBLL = FabricaBLL.CrearProductoBLL();
            CargarProductosEnGrilla();
        }

        private void CargarProductosEnGrilla()
        {
            List<Producto> productos = productoBLL.ObtenerTodosLosProductos();

            dgvProductos.DataSource = null;
            dgvProductos.DataSource = productos.Select(p => new
            {
                Codigo = p.CodigoDeArticulo,
                Nombre = p.NombreDelArticulo,
                Descripcion = p.DescripcionDelArticulo,
                Precio = p.PrecioUnitario,
                Stock = p.CantidadEnStock
            }).ToList();
        }

        private void DgvProductos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProductos.CurrentRow == null)
                return;

            txtCodigo.Text = dgvProductos.CurrentRow.Cells["Codigo"].Value?.ToString() ?? "";
            txtNombre.Text = dgvProductos.CurrentRow.Cells["Nombre"].Value?.ToString() ?? "";
            txtDescripcion.Text = dgvProductos.CurrentRow.Cells["Descripcion"].Value?.ToString() ?? "";
            txtPrecio.Text = dgvProductos.CurrentRow.Cells["Precio"].Value?.ToString() ?? "";
            txtStock.Text = dgvProductos.CurrentRow.Cells["Stock"].Value?.ToString() ?? "";
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                Producto nuevoProducto = new Producto(
                    txtCodigo.Text,
                    txtNombre.Text,
                    txtDescripcion.Text,
                    Convert.ToDecimal(txtPrecio.Text),
                    Convert.ToInt32(txtStock.Text));

                productoBLL.AgregarProducto(nuevoProducto);
                MessageBox.Show("Producto agregado correctamente.", "Exito");
                LimpiarCampos();
                CargarProductosEnGrilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el producto: " + ex.Message, "Error");
            }
        }

        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            Producto productoEncontrado = productoBLL.BuscarProductoPorCodigo(txtCodigo.Text);

            if (productoEncontrado == null)
            {
                MessageBox.Show("No se encontro un producto con ese codigo.", "Aviso");
                return;
            }

            try
            {
                productoEncontrado.NombreDelArticulo = txtNombre.Text;
                productoEncontrado.DescripcionDelArticulo = txtDescripcion.Text;
                productoEncontrado.PrecioUnitario = Convert.ToDecimal(txtPrecio.Text);
                productoEncontrado.EstablecerStock(Convert.ToInt32(txtStock.Text));

                productoBLL.ActualizarProducto(productoEncontrado);
                MessageBox.Show("Producto actualizado correctamente.", "Exito");
                LimpiarCampos();
                CargarProductosEnGrilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el producto: " + ex.Message, "Error");
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                MessageBox.Show("Selecciona un producto de la lista primero.", "Aviso");
                return;
            }

            DialogResult confirmacion = MessageBox.Show(
                $"Seguro que quieres eliminar el producto {txtCodigo.Text}?",
                "Confirmar eliminacion",
                MessageBoxButtons.YesNo);

            if (confirmacion != DialogResult.Yes)
                return;

            try
            {
                productoBLL.EliminarProducto(txtCodigo.Text);
                MessageBox.Show("Producto eliminado correctamente.", "Exito");
                LimpiarCampos();
                CargarProductosEnGrilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el producto: " + ex.Message, "Error");
            }
        }

        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void BtnRegresar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LimpiarCampos()
        {
            txtCodigo.Clear();
            txtNombre.Clear();
            txtDescripcion.Clear();
            txtPrecio.Clear();
            txtStock.Clear();
        }
    }
}
