using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProyectoFinalProgramacionV.BLL;
using ProyectoFinalProgramacionV.Fabrica;
using ProyectoFinalProgramacionV.Modelos;

namespace ProyectoFinalProgramacionV.UI
{
    public partial class FormularioRegistro : Form
    {
        private UsuarioBLL usuarioBLL;

        public FormularioRegistro()
        {
            InitializeComponent();
            usuarioBLL = FabricaBLL.CrearUsuarioBLL();
        }

        // Revisa los campos y crea la cuenta nueva si todo esta correcto
        private void BtnRegistrar_Click(object sender, EventArgs e)
        {
            if (txtContrasena.Text != txtConfirmarContrasena.Text)
            {
                lblMensaje.Text = "Las contrasenas no coinciden.";
                return;
            }

            try
            {
                Usuario usuarioNuevo = new Usuario(txtUsuario.Text, txtNombreCompleto.Text, txtContrasena.Text);
                usuarioBLL.RegistrarUsuario(usuarioNuevo);

                MessageBox.Show("Cuenta creada correctamente. Ya puedes iniciar sesion.", "Exito");
                this.Close();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }
        }

        // Cierra esta ventana y regresa al login, sin crear ninguna cuenta
        private void BtnRegresarLogin_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}