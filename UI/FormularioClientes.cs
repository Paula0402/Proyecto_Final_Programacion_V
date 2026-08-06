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
            CargarClientesEnGrilla();
        }

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
        }

        private void DgvClientes_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvClientes.CurrentRow == null)
                return;

            txtId.Text = dgvClientes.CurrentRow.Cells["Id"].Value?.ToString() ?? "";
            txtNombre.Text = dgvClientes.CurrentRow.Cells["Nombre"].Value?.ToString() ?? "";
            txtTelefono.Text = dgvClientes.CurrentRow.Cells["Telefono"].Value?.ToString() ?? "";
            txtCorreo.Text = dgvClientes.CurrentRow.Cells["Correo"].Value?.ToString() ?? "";
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
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
            Cliente clienteEncontrado = clienteBLL.BuscarClientePorId(txtId.Text);

            if (clienteEncontrado == null)
            {
                MessageBox.Show("No se encontro un cliente con ese identificador.", "Aviso");
                return;
            }

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
            LimpiarCampos();
        }

        private void BtnRegresar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LimpiarCampos()
        {
            txtId.Clear();
            txtNombre.Clear();
            txtTelefono.Clear();
            txtCorreo.Clear();
        }
    }
}