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
    public partial class FormularioLogin : Form
    {
        private UsuarioBLL usuarioBLL;

        public FormularioLogin()
        {
            InitializeComponent();
            usuarioBLL = FabricaBLL.CrearUsuarioBLL();
        }

        // Intenta iniciar sesion con lo que el usuario escribio. Si esta bien,
        // abre el sistema; si esta mal, muestra el error debajo del formulario.
        private void BtnIniciarSesion_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario usuarioValidado = usuarioBLL.IniciarSesion(txtUsuario.Text, txtContrasena.Text);
                lblMensaje.Text = "";

                this.Hide();
                FormularioPrincipal formularioPrincipal = new FormularioPrincipal();
                formularioPrincipal.ShowDialog();
                this.Close();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }
        }

        // Abre la pantalla de registro para crear una cuenta nueva
        private void BtnIrARegistro_Click(object sender, EventArgs e)
        {
            FormularioRegistro formularioRegistro = new FormularioRegistro();
            formularioRegistro.ShowDialog();
        }
    }
}