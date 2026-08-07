using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using ProyectoFinalProgramacionV.DTOs;
using ProyectoFinalProgramacionV.Datos;

namespace ProyectoFinalProgramacionV.DAL
{
    internal interface IUsuarioDAL
    {
        void Insertar(UsuarioDTO usuario);
        UsuarioDTO BuscarPorNombreUsuario(string nombreUsuario);
    }

    internal class UsuarioDAL : IUsuarioDAL
    {
        private ConexionBaseDatos conexionBaseDatos;

        public UsuarioDAL()
        {
            conexionBaseDatos = new ConexionBaseDatos();
        }

        // Guarda un usuario nuevo. Si el nombre de usuario ya existe, MySQL lo rechaza
        // (por el UNIQUE KEY) y aqui se lo traducimos a un mensaje entendible.
        public void Insertar(UsuarioDTO usuario)
        {
            using (MySqlConnection cnx = conexionBaseDatos.ObtenerConexionAbierta())
            {
                try
                {
                    string query = "INSERT INTO usuarios (nombre_usuario, contrasena_hash, nombre_completo) " +
                                   "VALUES (@nombreUsuario, @contrasenaHash, @nombreCompleto)";

                    MySqlCommand cmd = new MySqlCommand(query, cnx);
                    cmd.Parameters.AddWithValue("@nombreUsuario", usuario.NombreUsuario);
                    cmd.Parameters.AddWithValue("@contrasenaHash", usuario.ContrasenaHash);
                    cmd.Parameters.AddWithValue("@nombreCompleto", usuario.NombreCompleto);

                    cmd.ExecuteNonQuery();
                }
                catch (MySqlException ex) when (ex.Number == 1062)
                {
                    throw new Exception("Ese nombre de usuario ya esta en uso.");
                }
            }
        }

        // Busca un usuario por su nombre (para el login, o para revisar si ya existe en el registro)
        public UsuarioDTO BuscarPorNombreUsuario(string nombreUsuario)
        {
            using (MySqlConnection cnx = conexionBaseDatos.ObtenerConexionAbierta())
            {
                string query = "SELECT * FROM usuarios WHERE nombre_usuario = @nombreUsuario";
                MySqlCommand cmd = new MySqlCommand(query, cnx);
                cmd.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);

                using (MySqlDataReader lector = cmd.ExecuteReader())
                {
                    if (lector.Read())
                    {
                        return new UsuarioDTO
                        {
                            IdUsuario = lector.GetInt32("id_usuario"),
                            NombreUsuario = lector.GetString("nombre_usuario"),
                            ContrasenaHash = lector.GetString("contrasena_hash"),
                            NombreCompleto = lector.GetString("nombre_completo")
                        };
                    }

                    return null;
                }
            }
        }
    }
}