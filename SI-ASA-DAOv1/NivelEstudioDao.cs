using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SI_ASA_ENTIDADESv1;
using System.Data.SqlClient;
using System.Data;

namespace SI_ASA_DAOv1
{
    public class NivelEstudioDao
    {
        public static NivelEstudio obtener(int id)
        {
            NivelEstudio nivelEstudio = new NivelEstudio();

            string sql = "SELECT * FROM nivel_estudio n WHERE n.id = @id";

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
                    {
                        nivelEstudio.descripcion = dr["descripcion"].ToString();
                    };
                }
                dr.Close();
                cn.Close();
            }
            catch (SqlException ex)
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                throw new ApplicationException("Error al buscar el Nivel de Estudio");
            }

            return nivelEstudio;
        }

        public static List<NivelEstudio> obtenerTodo()
        {
            List<NivelEstudio> listaNivelesEstudio = new List<NivelEstudio>();

            string sql = "SELECT * FROM nivel_estudio";

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
                    NivelEstudio nivelEstudio = new NivelEstudio()
                    {
                        descripcion = dr["descripcion"].ToString()
                    };
                    listaNivelesEstudio.Add(nivelEstudio); //lleno la coleccion en memoria
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
            return listaNivelesEstudio;
        }

        //Retorna TRUE si se insertó correctamente; FALSE en todo otro caso.
        public static int add(NivelEstudio nivelEstudio)
        {
            int i = -1;

            String sql = "INSERT INTO nivel_estudio (descripcion) VALUES (@descripcion)";
            
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=ALEBELTRAMEN\\ALEJANDRA;Initial Catalog=ASA;Integrated Security=True";
            //PONER LA STRINGCONNECTION CORRECTA!!!

            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("descripcion", nivelEstudio.descripcion);

                i = (int)cmd.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Error al insertar el Nivel de Estudio");
            }
            finally
            {
                cn.Close();
            }

            return i;
        }

        public static Object cargarCombo()
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=ALEBELTRAMEN\\ALEJANDRA;Initial Catalog=ASA;Integrated Security=True";
            SqlCommand sql = new SqlCommand("SELECT * FROM nivel_estudio", cn);
            SqlDataAdapter da = new SqlDataAdapter(sql.CommandText, cn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }


    }
}
