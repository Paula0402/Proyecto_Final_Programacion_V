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
    internal interface IClienteDAL
    {
        void Insertar(ClienteDTO cliente);
        void Actualizar(ClienteDTO cliente);
        void Eliminar(string idCliente);
        ClienteDTO BuscarPorId(string idCliente);
        List<ClienteDTO> ObtenerTodos();
    }

    internal class ClienteDAL : IClienteDAL
    {
        private ConexionBaseDatos conexionBaseDatos;

        public ClienteDAL()
        {
            conexionBaseDatos = new ConexionBaseDatos();
        }

        public void Insertar(ClienteDTO cliente)
        {
            using (MySqlConnection cnx = conexionBaseDatos.ObtenerConexionAbierta())
            {
                string query = "INSERT INTO clientes (id_cliente, nombre_completo, numero_telefono, correo_electronico) " +
                               "VALUES (@id, @nombre, @telefono, @correo)";

                MySqlCommand cmd = new MySqlCommand(query, cnx);
                cmd.Parameters.AddWithValue("@id", cliente.IdCliente);
                cmd.Parameters.AddWithValue("@nombre", cliente.NombreCompleto);
                cmd.Parameters.AddWithValue("@telefono", cliente.NumeroTelefono);
                cmd.Parameters.AddWithValue("@correo", cliente.CorreoElectronico);

                cmd.ExecuteNonQuery();
            }
        }

        public void Actualizar(ClienteDTO cliente)
        {
            using (MySqlConnection cnx = conexionBaseDatos.ObtenerConexionAbierta())
            {
                string query = "UPDATE clientes SET nombre_completo = @nombre, numero_telefono = @telefono, " +
                               "correo_electronico = @correo WHERE id_cliente = @id";

                MySqlCommand cmd = new MySqlCommand(query, cnx);
                cmd.Parameters.AddWithValue("@id", cliente.IdCliente);
                cmd.Parameters.AddWithValue("@nombre", cliente.NombreCompleto);
                cmd.Parameters.AddWithValue("@telefono", cliente.NumeroTelefono);
                cmd.Parameters.AddWithValue("@correo", cliente.CorreoElectronico);

                cmd.ExecuteNonQuery();
            }
        }

        public void Eliminar(string idCliente)
        {
            using (MySqlConnection cnx = conexionBaseDatos.ObtenerConexionAbierta())
            {
                string query = "DELETE FROM clientes WHERE id_cliente = @id";
                MySqlCommand cmd = new MySqlCommand(query, cnx);
                cmd.Parameters.AddWithValue("@id", idCliente);
                cmd.ExecuteNonQuery();
            }
        }

        public ClienteDTO BuscarPorId(string idCliente)
        {
            using (MySqlConnection cnx = conexionBaseDatos.ObtenerConexionAbierta())
            {
                string query = "SELECT * FROM clientes WHERE id_cliente = @id";
                MySqlCommand cmd = new MySqlCommand(query, cnx);
                cmd.Parameters.AddWithValue("@id", idCliente);

                using (MySqlDataReader lector = cmd.ExecuteReader())
                {
                    if (lector.Read())
                        return MapearFilaAClienteDTO(lector);

                    return null;
                }
            }
        }

        public List<ClienteDTO> ObtenerTodos()
        {
            List<ClienteDTO> clientes = new List<ClienteDTO>();

            using (MySqlConnection cnx = conexionBaseDatos.ObtenerConexionAbierta())
            {
                string query = "SELECT * FROM clientes";
                MySqlCommand cmd = new MySqlCommand(query, cnx);

                using (MySqlDataReader lector = cmd.ExecuteReader())
                {
                    while (lector.Read())
                        clientes.Add(MapearFilaAClienteDTO(lector));
                }
            }

            return clientes;
        }

        private ClienteDTO MapearFilaAClienteDTO(MySqlDataReader lector)
        {
            return new ClienteDTO
            {
                IdCliente = lector.GetString("id_cliente"),
                NombreCompleto = lector.GetString("nombre_completo"),
                NumeroTelefono = lector.IsDBNull(lector.GetOrdinal("numero_telefono")) ? "" : lector.GetString("numero_telefono"),
                CorreoElectronico = lector.IsDBNull(lector.GetOrdinal("correo_electronico")) ? "" : lector.GetString("correo_electronico")
            };
        }
    }
}