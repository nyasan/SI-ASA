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

        public static void registrarCursadoAlumno(Alumno alumno, LinkedList<int> idCursos, DateTime fechaInscripcion)
        {
            int i = -1;
            String sql = @"INSERT INTO alumno_x_curso
                         (id_curso, fecha_inscripcion, legajo_alumno)
                         VALUES        (@id_curso,@fecha_inscripcion,@legajo_alumno)";
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=ALEBELTRAMEN\\ALEJANDRA;Initial Catalog=ASA;Integrated Security=True";
            SqlTransaction sqltran = cn.BeginTransaction();

            try
            {
                cn.Open();
                
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Transaction = sqltran;
             
                while(i<idCursos.Count)
                {
                    cmd.Parameters.AddWithValue("@legajo_alumno", alumno.legajo);
                    cmd.Parameters.AddWithValue("@id_curso", idCursos.ElementAt(i));
                    cmd.Parameters.AddWithValue("@fecha_inscripcion", fechaInscripcion);
                    cmd.ExecuteNonQuery();
                    i++;
                }

                sqltran.Commit();
                cn.Close();
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Error al registrar el Alumno a cursado");
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
