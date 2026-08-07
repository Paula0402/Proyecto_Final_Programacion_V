using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using ProyectoFinalProgramacionV.DAL;
using ProyectoFinalProgramacionV.DTOs;
using ProyectoFinalProgramacionV.Modelos;

namespace ProyectoFinalProgramacionV.BLL
{
    internal class UsuarioBLL
    {
        private IUsuarioDAL usuarioDAL;

        public UsuarioBLL()
        {
            usuarioDAL = new UsuarioDAL();
        }

        // Crea una cuenta nueva (esto es el "signup"). Valida los datos,
        // encripta la contrasena, y la guarda.
        public void RegistrarUsuario(Usuario usuario)
        {
            ValidarDatosDeRegistro(usuario);

            if (usuarioDAL.BuscarPorNombreUsuario(usuario.NombreUsuario) != null)
                throw new Exception($"El usuario '{usuario.NombreUsuario}' ya existe. Elige otro nombre.");

            UsuarioDTO dto = new UsuarioDTO
            {
                NombreUsuario = usuario.NombreUsuario,
                NombreCompleto = usuario.NombreCompleto,
                ContrasenaHash = EncriptarContrasena(usuario.ContrasenaSinEncriptar)
            };

            usuarioDAL.Insertar(dto);
        }

        // Revisa que el usuario y la contrasena sean correctos (esto es el "login").
        // Si todo esta bien devuelve el usuario, si no, avisa con un mensaje generico
        // (a proposito no decimos si fallo el usuario o la contrasena, por seguridad).
        public Usuario IniciarSesion(string nombreUsuario, string contrasena)
        {
            if (string.IsNullOrWhiteSpace(nombreUsuario) || string.IsNullOrWhiteSpace(contrasena))
                throw new Exception("Debe escribir el usuario y la contrasena.");

            UsuarioDTO dto = usuarioDAL.BuscarPorNombreUsuario(nombreUsuario);

            if (dto == null)
                throw new Exception("Usuario o contrasena incorrectos.");

            string contrasenaIngresadaEncriptada = EncriptarContrasena(contrasena);

            if (contrasenaIngresadaEncriptada != dto.ContrasenaHash)
                throw new Exception("Usuario o contrasena incorrectos.");

            Usuario usuarioValidado = new Usuario(dto.NombreUsuario, dto.NombreCompleto, contrasena);
            usuarioValidado.IdUsuario = dto.IdUsuario;
            return usuarioValidado;
        }

        // Reglas basicas antes de dejar crear una cuenta
        private void ValidarDatosDeRegistro(Usuario usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.NombreUsuario) || usuario.NombreUsuario.Length < 4)
                throw new Exception("El nombre de usuario debe tener al menos 4 caracteres.");

            if (string.IsNullOrWhiteSpace(usuario.NombreCompleto))
                throw new Exception("El nombre completo es obligatorio.");

            if (string.IsNullOrWhiteSpace(usuario.ContrasenaSinEncriptar) || usuario.ContrasenaSinEncriptar.Length < 6)
                throw new Exception("La contrasena debe tener al menos 6 caracteres.");
        }

        // Convierte la contrasena en un texto "revuelto" (hash) que no se puede
        // devolver a la contrasena original. Asi, aunque alguien vea la base de
        // datos, no ve las contrasenas reales de nadie.
        private string EncriptarContrasena(string contrasenaSinEncriptar)
        {
            using (SHA256 algoritmoDeEncriptado = SHA256.Create())
            {
                byte[] bytesEncriptados = algoritmoDeEncriptado.ComputeHash(Encoding.UTF8.GetBytes(contrasenaSinEncriptar));
                StringBuilder textoHash = new StringBuilder();

                foreach (byte b in bytesEncriptados)
                    textoHash.Append(b.ToString("x2"));

                return textoHash.ToString();
            }
        }
    }
}