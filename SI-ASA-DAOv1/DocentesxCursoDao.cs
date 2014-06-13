using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SI_ASA_DAOv1;
using SI_ASA_ENTIDADESv1;
using System.Data.SqlClient;


namespace SI_ASA_DAOv1
{
    public class DocentesxCursoDao
    {
        public static void insertar(List<Docente> listDocentes, int id_curso,DateTime fecha_hora)
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = "";
            SqlTransaction sqltran = conexion.BeginTransaction();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = sqltran;
                int i = 0;

                while (i < listDocentes.Count)
                {
                    cmd.CommandText = "Insert into docente_x_curso (id_docente,id_curso,fecha_hora) values (@id_docente,@id_curso,@fecha_hora)";
                    cmd.Parameters.AddWithValue("@id_docente", listDocentes.ElementAt(i).legajo);
                    cmd.Parameters.AddWithValue("@id_curso", id_curso);
                    cmd.Parameters.AddWithValue("@fecha_hora", fecha_hora);

                    cmd.ExecuteNonQuery();
                }

                sqltran.Commit();
                conexion.Close();


            }
            catch (SqlException ex)
            {
                sqltran.Rollback();
            }
            finally
            {
                conexion.Close()
            }

        }
    }
}
