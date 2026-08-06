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
    public partial class FormularioPrincipal : Form
    {
        public FormularioPrincipal()
        {
            InitializeComponent();
        }

        private void BtnProductos_Click(object sender, EventArgs e)
        {
            FormularioProductos formularioProductos = new FormularioProductos();
            formularioProductos.ShowDialog();
        }

        private void BtnClientes_Click(object sender, EventArgs e)
        {
            FormularioClientes formularioClientes = new FormularioClientes();
            formularioClientes.ShowDialog();
        }

        private void BtnVentas_Click(object sender, EventArgs e)
        {
            FormularioVentas formularioVentas = new FormularioVentas();
            formularioVentas.ShowDialog();
        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}