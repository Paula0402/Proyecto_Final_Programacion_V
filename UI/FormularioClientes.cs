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
    public partial class FormularioClientes : Form
    {
        private ClienteBLL clienteBLL;

        public FormularioClientes()
        {
            InitializeComponent();
            clienteBLL = FabricaBLL.CrearClienteBLL();

            // Conectamos los 4 campos para que en cuanto los 4 queden vacios
            // el boton Agregar se prenda solo, sin importar si fue por click,
            // por flechita del grid o porque el usuario borro todo a mano.
            // Ojo: el SelectionChanged del grid NO lo enganchamos aqui porque
            // el Designer.cs ya lo hace (dgvClientes.SelectionChanged += DgvClientes_SelectionChanged;).
            // Si lo agregas tambien aca, el metodo se va a disparar dos veces por cada seleccion.
            // Solo el ID determina si el cliente ya existe.
            txtId.TextChanged += (s, e) => ActualizarEstadoBotonAgregar();

            CargarClientesEnGrilla();
        }

        /// <summary>
        /// Carga la lista de clientes en el DataGridView.
        /// Nota de diseno: se usa clienteBLL.ObtenerTodosLosClientes() en vez de
        /// MySqlDataAdapter + DataTable directo en el formulario, para no romper la
        /// separacion de capas (UI -> BLL -> DAL) explicada en la diapositiva de
        /// "Arquitectura en Capas I".
        /// </summary>
        // Trae los clientes de la BD y los pone en la tabla. Se llama cada vez
        // que se agrega/edita/elimina algo, para que la tabla no quede desactualizada.
        private void CargarClientesEnGrilla()
        {
            List<Cliente> clientes = clienteBLL.ObtenerTodosLosClientes();

            dgvClientes.DataSource = null;
            dgvClientes.DataSource = clientes.Select(c => new
            {
                Id = c.IdentificadorDeCliente,
                Nombre = c.NombreCompleto,
                Telefono = c.NumeroDeTelefono,
                Correo = c.CorreoElectronico
            }).ToList();

            // Evita que al cargar la tabla se seleccione automaticamente
            // un cliente y se llenen los campos.
            dgvClientes.ClearSelection();
            LimpiarCampos();
        }

        // Cuando el usuario selecciona una fila, sea con click o con la
        // flechita del grid, esto agarra los datos de esa fila y los mete
        // en los TextBox para verlos o editarlos. Como ya hay un cliente
        // seleccionado, aqui apagamos el boton Agregar, porque lo que toca
        // en ese caso es Actualizar, no crear uno nuevo.
        private void DgvClientes_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count == 0)
                return;

            txtId.Text = dgvClientes.CurrentRow.Cells["Id"].Value?.ToString() ?? "";
            txtNombre.Text = dgvClientes.CurrentRow.Cells["Nombre"].Value?.ToString() ?? "";
            txtTelefono.Text = dgvClientes.CurrentRow.Cells["Telefono"].Value?.ToString() ?? "";
            txtCorreo.Text = dgvClientes.CurrentRow.Cells["Correo"].Value?.ToString() ?? "";

            btnAgregar.Enabled = false;
        }

        // Revisa los 4 campos y solo prende el boton Agregar si estan todos
        // vacios. Si hay aunque sea un dato de un cliente seleccionado, se
        // queda apagado para evitar que dupliques el registro por error.
        private void ActualizarEstadoBotonAgregar()
        {
            if (string.IsNullOrWhiteSpace(txtId.Text))
            {
                btnAgregar.Enabled = false;
                return;
            }

            btnAgregar.Enabled = true;
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            DialogResult confirmacion = MessageBox.Show(
                $"Seguro que quieres agregar el cliente {txtNombre.Text}?",
                "Confirmar agregar",
                MessageBoxButtons.YesNo);

            if (confirmacion != DialogResult.Yes)
                return;

            try
            {
                Cliente nuevoCliente = new Cliente(txtId.Text, txtNombre.Text, txtTelefono.Text, txtCorreo.Text);
                clienteBLL.AgregarCliente(nuevoCliente);

                MessageBox.Show("Cliente agregado correctamente.", "Exito");

                LimpiarCampos();
                CargarClientesEnGrilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el cliente: " + ex.Message, "Error");
            }
        }

        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text))
            {
                MessageBox.Show("Selecciona un cliente de la lista primero.", "Aviso");
                return;
            }

            Cliente clienteEncontrado = clienteBLL.BuscarClientePorId(txtId.Text);

            if (clienteEncontrado == null)
            {
                MessageBox.Show("No se encontro un cliente con ese identificador.", "Aviso");
                return;
            }

            DialogResult confirmacion = MessageBox.Show(
                $"Seguro que quieres actualizar el cliente {txtId.Text}?",
                "Confirmar actualizacion",
                MessageBoxButtons.YesNo);

            if (confirmacion != DialogResult.Yes)
                return;

            try
            {
                clienteEncontrado.NombreCompleto = txtNombre.Text;
                clienteEncontrado.NumeroDeTelefono = txtTelefono.Text;
                clienteEncontrado.CorreoElectronico = txtCorreo.Text;

                clienteBLL.ActualizarCliente(clienteEncontrado);

                MessageBox.Show("Cliente actualizado correctamente.", "Exito");

                LimpiarCampos();
                CargarClientesEnGrilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el cliente: " + ex.Message, "Error");
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text))
            {
                MessageBox.Show("Selecciona un cliente de la lista primero.", "Aviso");
                return;
            }

            DialogResult confirmacion = MessageBox.Show(
                $"Seguro que quieres eliminar el cliente {txtId.Text}?",
                "Confirmar eliminacion",
                MessageBoxButtons.YesNo);

            if (confirmacion != DialogResult.Yes)
                return;

            try
            {
                clienteBLL.EliminarCliente(txtId.Text);
                MessageBox.Show("Cliente eliminado correctamente.", "Exito");
                LimpiarCampos();
                CargarClientesEnGrilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el cliente: " + ex.Message, "Error");
            }
        }

        private void BtnLimpiar_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtId.Text) &&
                string.IsNullOrWhiteSpace(txtNombre.Text) &&
                string.IsNullOrWhiteSpace(txtTelefono.Text) &&
                string.IsNullOrWhiteSpace(txtCorreo.Text))
            {
                MessageBox.Show("Los campos ya se encuentran vacios.", "Aviso");
                return;
            }

            dgvClientes.ClearSelection();
            LimpiarCampos();
            ActualizarEstadoBotonAgregar();

            MessageBox.Show("Campos limpiados correctamente.", "Exito");
        }

        private void BtnRegresar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Solo vacia los 4 TextBox. El TextChanged de cada uno se encarga
        // de avisarle a ActualizarEstadoBotonAgregar que revise si ya
        // puede prender el boton.
        private void LimpiarCampos()
        {
            txtId.Clear();
            txtNombre.Clear();
            txtTelefono.Clear();
            txtCorreo.Clear();
        }

        private void FormularioClientes_Load(object sender, EventArgs e)
        {
            dgvClientes.ClearSelection();
            LimpiarCampos();
            ActualizarEstadoBotonAgregar();
        }

        // El nombre tiene que ser exactamente este porque el Designer.cs
        // lo engancha como dgvClientes.CellContentClick += dgvClientes_CellContentClick.
        // Ya no controla el boton Agregar, eso ahora lo hace SelectionChanged,
        // asi que lo dejamos vacio nada mas para no romper esa conexion.
        private void dgvClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}