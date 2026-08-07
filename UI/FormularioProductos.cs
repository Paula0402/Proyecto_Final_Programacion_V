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

            // Conectamos los 5 campos para que en cuanto todos queden vacios
            // el boton Agregar se prenda solo, sin importar si fue por click,
            // por flechita del grid o porque el usuario borro todo a mano.
            // El SelectionChanged del grid NO se engancha aqui porque el
            // Designer.cs ya lo hace (dgvProductos.SelectionChanged += DgvProductos_SelectionChanged).
            // Si lo agregas tambien aca, el metodo se dispara dos veces por cada seleccion.
            // Solo el código determina si se puede agregar un producto.
            txtCodigo.TextChanged += (s, e) => ActualizarEstadoBotonAgregar();

            CargarProductosEnGrilla();
        }

        /// <summary>
        /// Carga la lista de productos en el DataGridView.
        /// Nota de diseno: se usa productoBLL.ObtenerTodosLosProductos() en vez de
        /// MySqlDataAdapter + DataTable directo en el formulario (como en el ejemplo de
        /// la diapositiva "Llenando un DataGridView"). Esto es intencional: meter SQL
        /// directo en la UI rompe la regla de oro de arquitectura en capas ("la UI nunca
        /// debe saltarse niveles"). Aqui la UI solo le pide los datos a la BLL, que a su
        /// vez se los pide a la DAL, respetando el flujo UI -> BLL -> DAL.
        /// </summary>
        // Trae los productos de la BD y los pone en la tabla. Se llama cada vez
        // que se agrega/edita/elimina algo, para que la tabla no quede desactualizada.
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

            // Evita que al cargar la tabla se seleccione automaticamente
            // un producto y se llenen los campos.
            dgvProductos.ClearSelection();
            LimpiarCampos();
        }

        // Cuando el usuario selecciona una fila, sea con click o con la
        // flechita del grid, esto agarra los datos de esa fila y los mete
        // en los TextBox para verlos o editarlos. Como ya hay un producto
        // seleccionado, aqui apagamos el boton Agregar, porque lo que toca
        // en ese caso es Actualizar, no crear uno nuevo.
        private void DgvProductos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProductos.CurrentRow == null)
                return;

            txtCodigo.Text = dgvProductos.CurrentRow.Cells["Codigo"].Value?.ToString() ?? "";
            txtNombre.Text = dgvProductos.CurrentRow.Cells["Nombre"].Value?.ToString() ?? "";
            txtDescripcion.Text = dgvProductos.CurrentRow.Cells["Descripcion"].Value?.ToString() ?? "";
            txtPrecio.Text = dgvProductos.CurrentRow.Cells["Precio"].Value?.ToString() ?? "";
            txtStock.Text = dgvProductos.CurrentRow.Cells["Stock"].Value?.ToString() ?? "";

            btnAgregar.Enabled = false;
        }

        // Revisa los 5 campos y solo prende el boton Agregar si estan todos
        // vacios. Si hay aunque sea un dato de un producto seleccionado, se
        // queda apagado para evitar que dupliques el registro por error.
        private void ActualizarEstadoBotonAgregar()
        {
            if (string.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                btnAgregar.Enabled = false;
                return;
            }

            btnAgregar.Enabled = true;
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCodigo.Text) ||
                string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtDescripcion.Text) ||
                string.IsNullOrWhiteSpace(txtPrecio.Text) ||
                string.IsNullOrWhiteSpace(txtStock.Text))
            {
                MessageBox.Show("Debe completar todos los campos antes de agregar el producto.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmacion = MessageBox.Show(
                $"¿Está seguro de que desea agregar el producto {txtCodigo.Text}?",
                "Confirmar registro",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

                if (confirmacion != DialogResult.Yes)
                        return;


            try
            {
                Producto nuevoProducto = new Producto(
                    txtCodigo.Text,
                    txtNombre.Text,
                    txtDescripcion.Text,
                    Convert.ToDecimal(txtPrecio.Text),
                    Convert.ToInt32(txtStock.Text));

                productoBLL.AgregarProducto(nuevoProducto);

                MessageBox.Show("Producto agregado correctamente.",
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LimpiarCampos();
                CargarProductosEnGrilla();
            }
            catch (FormatException)
            {
                MessageBox.Show(
                    "El precio debe ser un número decimal (por ejemplo: 100 o 100.10) y el stock debe ser un número entero.",
                    "Datos inválidos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el producto: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
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


            DialogResult confirmacion = MessageBox.Show(
                $"¿Está seguro de que desea actualizar el producto {txtCodigo.Text}?",
                "Confirmar actualización",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

                if (confirmacion != DialogResult.Yes)
                    return;


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
            DialogResult confirmacion = MessageBox.Show(
                "¿Está seguro de que desea limpiar todos los campos?",
                "Confirmar limpieza",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacion != DialogResult.Yes)
                return;

            dgvProductos.ClearSelection();
            LimpiarCampos();
            ActualizarEstadoBotonAgregar();
        }

        private void BtnRegresar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Solo vacia los 5 TextBox. El TextChanged de cada uno se encarga
        // de avisarle a ActualizarEstadoBotonAgregar que revise si ya
        // puede prender el boton.
        private void LimpiarCampos()
        {
            txtCodigo.Clear();
            txtNombre.Clear();
            txtDescripcion.Clear();
            txtPrecio.Clear();
            txtStock.Clear();
        }

        // El grid selecciona la primera fila solo hasta que la ventana se
        // muestra de verdad (eso pasa despues del constructor), asi que sin
        // esto los campos aparecian llenos al abrir el formulario. Por eso
        // hay que volver a limpiar aqui, igual que en Clientes.
        private void FormularioProductos_Load(object sender, EventArgs e)
        {
            dgvProductos.ClearSelection();
            LimpiarCampos();
            ActualizarEstadoBotonAgregar();
        }
    }
}