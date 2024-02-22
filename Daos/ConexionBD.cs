using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class ConexionBD
    {
        public static MySqlConnection conexion;

        public static bool Conectar()
        {

            Properties.Settings.Default.Host = "germansensei.mysql.database.azure.com";
            Properties.Settings.Default.Usuario = "OrlandoA";
            Properties.Settings.Default.Contrasena = "A1b2c3d4e5";
            Properties.Settings.Default.Save();

            try
            {
                if (conexion != null && conexion.State == System.Data.ConnectionState.Open) return true;

                conexion = new MySqlConnection();
                conexion.ConnectionString = "server=" + Properties.Settings.Default.Host + ";uid="
                    + Properties.Settings.Default.Usuario + ";pwd=" + Properties.Settings.Default.Contrasena + "; database=Datos";
                conexion.Open();
                return true;
            }
            catch (MySqlException ex)
            {

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public static void Desconectar()
        {
            if (conexion != null && conexion.State == System.Data.ConnectionState.Open)
                conexion.Close();

        }
    }
}