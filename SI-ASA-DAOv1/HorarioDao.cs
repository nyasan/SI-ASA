using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SI_ASA_ENTIDADESv1;
using System.Data.SqlClient;
using System.Data;

namespace SI_ASA_DAOv1
{
    public class HorarioDao
    {
        public static List<Horario> obtenerTodo()
        {
            List<Horario> listHorarios = new List<Horario>();

            string sql = "SELECT * FROM horarios";

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=NICO;Initial Catalog=ASA;Integrated Security=True";
            //PONER LA STRINGCONNECTION CORRECTA!!!

            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Horario horario = new Horario()
                    {
                        desde = dr["horario_inicio"].ToString(),
                        hasta = dr["horario_baja"].ToString()
                    };
                    listHorarios.Add(horario); //lleno la coleccion en memoria
                }
                dr.Close();
                cn.Close();
            }
            catch (SqlException ex)
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                throw new ApplicationException("Error al buscar los Horarios");
            }
            return listHorarios;

        }
        public static Horario obtener(int id)
        {
            Horario horario = new Horario();
            string sql = "SELECT * FROM horarios h WHERE h.id_horario = @id";

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=NICO;Initial Catalog=ASA;Integrated Security=True";
            //PONER LA STRINGCONNECTION CORRECTA!!!

            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                horario.desde = dr["horario_inicio"].ToString();
                horario.hasta = dr["horario_fin"].ToString();

                dr.Close();
                cn.Close();
            }
            catch (SqlException ex)
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                throw new ApplicationException("Error al buscar el Horario");
            }
            return horario;
        }

        public static int obtenerID(string desde, string hasta)
        {
            int id = -1;
            string sql = "SELECT * FROM horarios WHERE horario_inicio = @desde AND horario_fin = @hasta";

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=NICO;Initial Catalog=ASA;Integrated Security=True";
            //PONER LA STRINGCONNECTION CORRECTA!!!

            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@desde", desde);
                cmd.Parameters.AddWithValue("@hasta", hasta);                

                int scalar = (Int32)cmd.ExecuteScalar();
                if (scalar > 0)
                {
                    id = (int)scalar;
                }

                cn.Close();
            }
            catch (SqlException ex)
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                throw new ApplicationException("Error al buscar el Horario" + ex.Message);
            }
            catch (NullReferenceException exNRE)
            {
                cn.Close();
            }

            return id;
        }

        //Retorna TRUE si se insertó correctamente; FALSE en todo otro caso.
        public static int add(Horario horario)
        {
            int i = -1;
            int busqueda =obtenerID(horario.desde, horario.hasta);

            if (busqueda == -1)
            {
                string sql = "INSERT INTO horarios (horario_inicio, horario_fin) VALUES (@horario_inicio, @horario_fin) SELECT CAST(scope_identity() AS int)";
                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = "Data Source=NICO;Initial Catalog=ASA;Integrated Security=True";
                //PONER LA STRINGCONNECTION CORRECTA!!!

                try
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(sql, cn);
                    cmd.Parameters.AddWithValue("@horario_inicio", horario.desde);
                    cmd.Parameters.AddWithValue("@horario_fin", horario.hasta);
                    resetearAutoIncrement(maxID() - 1); // aca le pone el autoincrement en el ultimo legajo de la tabla, pido el max legajo, -1 es el ultimo de la tabla
                    i = (Int32)cmd.ExecuteScalar();
                }
                catch (SqlException ex)
                {
                    throw new ApplicationException("Error al insertar el Horario");
                }
                finally
                {
                    cn.Close();
                }
            }
            else i = busqueda;
            return i;
        }


        //retorna !=0 eliminado, 0=no eliminado
        public static int delete(Horario horario)
        {
            int i = -1;

            string sql = "DELETE FROM horarios h WHERE h.horario_inicio=@horario_inicio AND h.horario_fin=@horario_fin)";

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=NICO;Initial Catalog=ASA;Integrated Security=True";

            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@horario_inicio", horario.desde);
                cmd.Parameters.AddWithValue("@horario_fin", horario.hasta);

                i = (int) cmd.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Error al eliminar el Horario");
            }
            finally
            {
                cn.Close();
            }
            return i;
        }

        public static int update( Horario horarioNuevo)
        {
            int idBusqueda = obtenerID(horarioNuevo.desde,horarioNuevo.hasta);
            if (idBusqueda != 1)
            {
                return idBusqueda;
            }
            else
            {
                return add(horarioNuevo);
            }

            //int i = -1;
            //string sql = "UPDATE horarios h SET h.horario_inicio=@horario_inicioNuevo, h.horario_fin=@horario_finNuevo ";
            //sql += "WHERE h.horario_inicio=@horario_inicio AND h.horario_fin=@horario_fin";

            //SqlConnection cn = new SqlConnection();
            //cn.ConnectionString = "Data Source=NICO;Initial Catalog=ASA;Integrated Security=True";

            //try
            //{
            //    cn.Open();
            //    SqlCommand cmd = new SqlCommand(sql, cn);

            //    cmd.Parameters.AddWithValue("@horario_inicio", horarioViejo.desde);
            //    cmd.Parameters.AddWithValue("@horario_fin", horarioViejo.hasta);
            //    cmd.Parameters.AddWithValue("@horario_inicioNuevo", horarioNuevo.desde);
            //    cmd.Parameters.AddWithValue("@horario_finNuevo", horarioNuevo.hasta);

            //    i = (int)cmd.ExecuteScalar();
            //}
            //catch (SqlException ex)
            //{
            //    throw new ApplicationException("Error al actualizar el Horario");
            //}
            //finally
            //{
            //    cn.Close();
            //}
            
            //return i;
        }
        public static int maxID()
        {
            int i = 0;

            string sql = @"SELECT        MAX(id_horario) AS Expr1
                         FROM            horarios";

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=NICO;Initial Catalog=ASA;Integrated Security=True";

            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                i = (int)cmd.ExecuteScalar();
            }

            catch (SqlException ex)
            {
                throw new ApplicationException("Error al buscar los horarios" + ex.Message);
            }
            finally
            {
                cn.Close();
            }

            return i + 1;
        }
        public static void resetearAutoIncrement(int ultimoVal)
        {
            string sql = "DBCC CHECKIDENT ('horarios', RESEED, @val);";
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=NICO;Initial Catalog=ASA;Integrated Security=True";
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@val", ultimoVal);
                cmd.ExecuteNonQuery();

            }

            catch (SqlException ex)
            {
                throw new ApplicationException("Error al resetear autoincrmental en el ultimo valor" + ex.Message);
            }
            finally
            {
                cn.Close();
            }


        }
    }
}
