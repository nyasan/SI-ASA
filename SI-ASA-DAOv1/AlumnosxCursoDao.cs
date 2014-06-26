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

        public static void registrarCursadoAlumno(Alumno alumno, List<int> idCursos, DateTime fechaInscripcion)
        {
            int i = 1;
            String sql = @"INSERT INTO alumno_x_curso
                         (id_curso, fecha_inscripcion, legajo_alumno)
                         VALUES        (@id_curso,@fecha_inscripcion,@legajo_alumno)";
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=ALEBELTRAMEN\\ALEJANDRA;Initial Catalog=ASA;Integrated Security=True";
            cn.Open();   
            SqlTransaction sqltran = cn.BeginTransaction();

            try
            {
                
                
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Transaction = sqltran;

                foreach (int l in idCursos)
                {
                    sql = @"INSERT INTO alumno_x_curso
                         (id_curso, fecha_inscripcion, legajo_alumno)
                         VALUES        (@id_curso" + l + ",@fecha_inscripcion" + l + ",@legajo_alumno" + l + ")";
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@legajo_alumno" + l, alumno.legajo);
                    cmd.Parameters.AddWithValue("@id_curso" + l, l.ToString());
                    cmd.Parameters.AddWithValue("@fecha_inscripcion" + l, fechaInscripcion);
                    cmd.ExecuteNonQuery(); 
                }

                sqltran.Commit();
                cn.Close();
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Error al registrar el Alumno a cursado " + ex.Message);
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
