using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ProyectoFinalProgramacionV.Datos
{
    /// Clase encargada de crear y entregar conexiones a la base de datos MySQL.
    internal class ConexionBaseDatos
    {
        private static readonly string cadenaDeConexion = "server=localhost;Database=sistemaventasdb;Uid=root;Pwd=";

        // Crea y abre una nueva conexion, lista para usar en un DAO
        public MySqlConnection ObtenerConexionAbierta()
        {
            MySqlConnection cnx = new MySqlConnection(cadenaDeConexion);

            try
            {
                cnx.Open();
                return cnx;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al conectar con la base de datos: " + ex.Message);
                throw;
            }
        }
    }
}
