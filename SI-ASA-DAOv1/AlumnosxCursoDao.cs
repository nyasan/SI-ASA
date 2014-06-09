using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SI_ASA_ENTIDADESv1;
using System.Data.SqlClient;
using System.Data;

namespace SI_ASA_DAOv1
{
    public class AlumnosxCursoDao
    {
        private String conexion = "";

        public static void add(List<Alumno> listAlumnos, int id_curso, DateTime fechaInscripcion)
        {
            int i = -1;
            String sql = "INSERT INTO alumnos_x_curso (legajo, id_curso, fecha_inscripcion)";
            sql += "VALUES (@legajo, @id_curso, @fecha_inscripcion)";
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=ALEBELTRAMEN\\ALEJANDRA;Initial Catalog=ASA;Integrated Security=True";
            SqlTransaction sqltran = cn.BeginTransaction();

            try
            {
                cn.Open();
                
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Transaction = sqltran;
             
                while(i<listAlumnos.Count)
                {
                    cmd.Parameters.AddWithValue("@legajo", listAlumnos.ElementAt(i).legajo);
                    cmd.Parameters.AddWithValue("@id_curso", id_curso);
                    cmd.Parameters.AddWithValue("@fecha_inscripcion", fechaInscripcion);
                    cmd.ExecuteNonQuery();
                    i++;
                }

                sqltran.Commit();
                cn.Close();
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Error al insertar el Alumno");
                try
                {
                    // Attempt to roll back the transaction.
                    sqltran.Rollback();
                }
                catch (Exception exRollback)
                {
                    // Throws an InvalidOperationException if the connection 
                    // is closed or the transaction has already been rolled 
                    // back on the server.
                    Console.WriteLine(exRollback.Message);
                }
            }
            finally
            {
                cn.Close();
            }
        }
    }
}
