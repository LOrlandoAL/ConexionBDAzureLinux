using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexionBDAzure
{
    public class DAO_Area
    {
        public List<Mod_Areas> GetAll()
        {
            List<Mod_Areas> lista = new List<Mod_Areas>();
            if (ConexionBD.Conectar())
            {
                try
                {
                    String select = "SELECT id,Nombre,Ubicacion FROM Areas;";
                    DataTable dt = new DataTable();
                    MySqlCommand sentencia = new MySqlCommand();
                    sentencia.CommandText = select;
                    sentencia.Connection = ConexionBD.conexion;
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = sentencia;
                    da.Fill(dt);
                    foreach (DataRow fila in dt.Rows)
                    {
                        Mod_Areas Area = new Mod_Areas(
                            Convert.ToInt32(fila["id"]),
                            fila["Nombre"].ToString(),
                            fila["Ubicacion"].ToString()
                            );
                        lista.Add(Area);
                    }

                    return lista;
                }
                finally
                {
                    ConexionBD.Desconectar();
                }
            }
            else
            {
                return null;
            }

        }
    }
}
