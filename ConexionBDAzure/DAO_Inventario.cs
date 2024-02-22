using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ConexionBDAzure
{
    public class DAO_Inventario
    {

        /*-------------------------------------- Obtener Todo --------------------------------------*/
        public List<Mod_Inventario> GetAll()
        {
            List<Mod_Inventario> lista = new List<Mod_Inventario>();
            if (ConexionBD.Conectar())
            {
                try
                {
                    String select = @"SELECT I.id, I.nombrecorto, I.Descripcion, I.serie, I.color, I.FechaAdquision, I.TipoAdquision,
                        I.Observaciones,A.Nombre FROM Areas A JOIN Inventario I ON A.id = I.Areas_id;";

                    DataTable dt = new DataTable();
                    MySqlCommand sentencia = new MySqlCommand();
                    sentencia.CommandText = select;
                    sentencia.Connection = ConexionBD.conexion;
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = sentencia;
                    da.Fill(dt);
                    foreach (DataRow fila in dt.Rows)
                    {
                        Mod_Inventario producto = new Mod_Inventario(
                            Convert.ToInt32(fila["id"]),
                            fila["nombrecorto"].ToString(),
                            fila["Descripcion"].ToString(),
                            fila["serie"].ToString(),
                            fila["color"].ToString(),
                            fila["FechaAdquision"].ToString(),
                            fila["TipoAdquision"].ToString(),
                            fila["Observaciones"].ToString(),
                            fila["Nombre"].ToString()
                            );
                        lista.Add(producto);
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

        /*-------------------------------------- Insertar Nuevo --------------------------------------*/

        public bool Insert(Mod_Inventario NewInvent)
        {
            //Conectarme

            if (ConexionBD.Conectar())
            {
                MySqlTransaction tran = ConexionBD.conexion.BeginTransaction();
                try
                {
                    String select = @"INSERT INTO Inventario 
                                    VALUES(0,@Nombre, @Desc, @Serie, @Color, @FechaAd, @TipoAd, @Observacion, @AreaID); select last_insert_id();";

                    MySqlCommand sentencia = new MySqlCommand(select, ConexionBD.conexion);

                    sentencia.Parameters.AddWithValue("@Nombre", NewInvent.NombreCorto);
                    sentencia.Parameters.AddWithValue("@Desc", NewInvent.Descripcion);
                    sentencia.Parameters.AddWithValue("@Serie", NewInvent.Serie);
                    sentencia.Parameters.AddWithValue("@Color", NewInvent.Color);
                    sentencia.Parameters.AddWithValue("@FechaAd", NewInvent.FechaAdquision);
                    sentencia.Parameters.AddWithValue("@TipoAd", NewInvent.TipoAdquision);
                    sentencia.Parameters.AddWithValue("@Observacion", NewInvent.observaciones);
                    sentencia.Parameters.AddWithValue("@AreaID", NewInvent.Area_id);
                    sentencia.Connection = ConexionBD.conexion;

                    //Ejercutar el comando 
                    //Cuando nos interesa obtener un valor adicional en el comando (como en el ejemplo de arriba que obtiene el último id generado por autoincrement podemos usar ExecuteScalar
                    int prod = Convert.ToInt32(sentencia.ExecuteScalar());
                    int id = prod;


                    return true;
                }
                finally
                {
                    tran.Commit();
                    ConexionBD.Desconectar();
                }
            }
            else
            {
                return false;
            }
        }

        /*-------------------------------------- Actualizar Inventario --------------------------------------*/
        public bool Actualizar(Mod_Inventario InvUp)
        {
            if (ConexionBD.Conectar())
            {
                MySqlTransaction Tran = ConexionBD.conexion.BeginTransaction();
                try
                {
                    String select = @"UPDATE Inventario SET nombreCorto = @Nombre, descripcion = @Descripcion, serie = @Serie, color = @Color, 
                                    fechaAdquision = @FechaAd, tipoAdquision = @TipoAd, observaciones = @Observaciones, AREAS_id = @AreaID WHERE id = @id;";

                    MySqlCommand sentencia = new MySqlCommand(select, ConexionBD.conexion);

                    sentencia.Parameters.AddWithValue("@Nombre", InvUp.NombreCorto);
                    sentencia.Parameters.AddWithValue("@Descripcion", InvUp.Descripcion);
                    sentencia.Parameters.AddWithValue("@Serie", InvUp.Serie);
                    sentencia.Parameters.AddWithValue("@Color", InvUp.Color);
                    sentencia.Parameters.AddWithValue("@FechaAd", InvUp.FechaAdquision);
                    sentencia.Parameters.AddWithValue("@TipoAd", InvUp.TipoAdquision);
                    sentencia.Parameters.AddWithValue("@Observaciones", InvUp.observaciones);
                    sentencia.Parameters.AddWithValue("@AreaID", InvUp.Area_id);
                    sentencia.Parameters.AddWithValue("@id", InvUp.IdInventario);
                    sentencia.Connection = ConexionBD.conexion;
                    sentencia.ExecuteScalar();

                }
                finally
                {
                    Tran.Commit();
                    ConexionBD.Desconectar();
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        /*-------------------------------------- Borrar Inventario --------------------------------------*/
        public bool Borrar(int id)
        {
            if (ConexionBD.Conectar())
            {
                MySqlTransaction tran = ConexionBD.conexion.BeginTransaction();
                try
                {
                    String select = @"DELETE FROM Inventario WHERE id = @id;";
                    MySqlCommand sentencia = new MySqlCommand(select, ConexionBD.conexion);
                    sentencia.Parameters.AddWithValue("@id", id);
                    sentencia.Connection = ConexionBD.conexion;
                    sentencia.ExecuteScalar();
                }
                finally
                {
                    tran.Commit();
                    ConexionBD.Desconectar();
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public Mod_Inventario BusquedaMod(int id)
        {
            Mod_Inventario inv = null;
            if (ConexionBD.Conectar())
            {
                try
                {
                    String select = @"SELECT * FROM Inventario WHERE id=@id";

                    DataTable dt = new DataTable();
                    MySqlCommand sentencia = new MySqlCommand(select);
                    sentencia.Parameters.AddWithValue("@id", id);
                    sentencia.Connection = ConexionBD.conexion;

                    MySqlDataAdapter da = new MySqlDataAdapter(sentencia);
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        DataRow fila = dt.Rows[0];
                        int IDMod = Convert.ToInt32(fila["Areas_id"]);
                        inv = new Mod_Inventario(
                            Convert.ToInt32(fila["id"]),
                            fila["nombrecorto"].ToString(),
                            fila["Descripcion"].ToString(),
                            fila["serie"].ToString(),
                            fila["color"].ToString(),
                            fila["FechaAdquision"].ToString(),
                            fila["TipoAdquision"].ToString(),
                            fila["Observaciones"].ToString(),
                            fila["Areas_id"].ToString()
                            );

                    }

                    return inv;
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
