using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SI_ASA_ENTIDADESv1;
using System.Data.SqlClient;
using System.Data;

namespace SI_ASA_DAOv1
{
    class HorarioDao
    {
        public static List<Horario> obtenerTodo()
        {
            List<Horario> listHorarios = new List<Horario>();

            string sql = "SELECT * FROM horarios";

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=ALEBELTRAMEN\\ALEJANDRA;Initial Catalog=ASA;Integrated Security=True";
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
                        desde = (DateTime)dr["horario_inicio"],
                        hasta = (DateTime)dr["horario_baja"]
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
            cn.ConnectionString = "Data Source=ALEBELTRAMEN\\ALEJANDRA;Initial Catalog=ASA;Integrated Security=True";
            //PONER LA STRINGCONNECTION CORRECTA!!!

            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                SqlDataReader dr = cmd.ExecuteReader();

                horario.desde = (DateTime)dr["horario_inicio"];
                horario.hasta = (DateTime)dr["horario_baja"];

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

        //Retorna TRUE si se insertó correctamente; FALSE en todo otro caso.
        public static int add(Horario horario)
        {
            int i = -1;
            string sql = "INSERT INTO horarios (horario_inicio, horario_fin) VALUES (@horario_inicio, @horario_fin)";

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=ALEBELTRAMEN\\ALEJANDRA;Initial Catalog=ASA;Integrated Security=True";
            //PONER LA STRINGCONNECTION CORRECTA!!!

            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("horario_inicio", horario.desde);
                cmd.Parameters.AddWithValue("horario_fin", horario.hasta);
                i = (int) cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Error al insertar el Horario");
            }
            finally
            {
                cn.Close();
            }
            return i;
        }


        //retorna !=0 eliminado, 0=no eliminado
        public static int delete(Horario horario)
        {
            int i = -1;

            string sql = "DELETE FROM horarios h WHERE h.horario_inicio=@horario_inicio AND h.horario_fin=@horario_fin)";

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=ALEBELTRAMEN\\ALEJANDRA;Initial Catalog=ASA;Integrated Security=True";

            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("horario_inicio", horario.desde);
                cmd.Parameters.AddWithValue("horario_fin", horario.hasta);

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

        public static int update(Horario horarioViejo, Horario horarioNuevo)
        {
            int i = -1;
            string sql = "UPDATE horarios h SET h.horario_inicio=@horario_inicioNuevo, h.horario_fin=@horario_finNuevo ";
            sql += "WHERE h.horario_inicio=@horario_inicio AND h.horario_fin=@horario_fin";

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=ALEBELTRAMEN\\ALEJANDRA;Initial Catalog=ASA;Integrated Security=True";

            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("horario_inicio", horarioViejo.desde);
                cmd.Parameters.AddWithValue("horario_fin", horarioViejo.hasta);
                cmd.Parameters.AddWithValue("horario_inicioNuevo", horarioNuevo.desde);
                cmd.Parameters.AddWithValue("horario_finNuevo", horarioNuevo.hasta);

                i = (int)cmd.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Error al actualizar el Horario");
            }
            finally
            {
                cn.Close();
            }
            
            return i;
        }
    }
}
