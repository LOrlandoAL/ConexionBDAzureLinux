using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ConexionBDAzure
{
    public class ConexionBD
    {
        public static MySqlConnection conexion;
        public static bool Conectar()
        {

            Properties.Settings.Default.Host = "172.210.64.49";
            Properties.Settings.Default.Usuario = "OrlyBDA";
            Properties.Settings.Default.Contrasena = "OrlyADMON123";
            Properties.Settings.Default.Save();

            try
            {
                if (conexion != null && conexion.State == System.Data.ConnectionState.Open) return true;

                conexion = new MySqlConnection();
                conexion.ConnectionString = "server=" + Properties.Settings.Default.Host + ";uid="
                    + Properties.Settings.Default.Usuario + ";pwd=" + Properties.Settings.Default.Contrasena + "; database=refaccionaria";
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
