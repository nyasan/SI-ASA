using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SI_ASA_ENTIDADESv1;
using System.Data.SqlClient;
using System.Data;


namespace SI_ASA_DAOv1
{
    public class Docente_x_cursoDao
    {
        public static void registrarCursadoDocente(Docente docente, List<int> idCursos, DateTime fechaInscripcion)
        {
            int i = -1;
            String sql = @"INSERT INTO docente_x_curso
                         (legajo_docente, id_curso, fecha_inscripcion)
                         VALUES        (@legajo_docente,@id_curso,@fecha_inscripcion)";
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
                      sql = @"INSERT INTO docente_x_curso
                          (legajo_docente, id_curso, fecha_inscripcion)
                          VALUES        (@legajo_docente" + l + ",@id_curso" + l + ",@fecha_inscripcion" + l + ")";
                      cmd.CommandText = sql;
                      cmd.Parameters.AddWithValue("@legajo_docente" + l, docente.legajo);
                      cmd.Parameters.AddWithValue("@id_curso" + l, l);
                      cmd.Parameters.AddWithValue("@fecha_inscripcion" + l, fechaInscripcion);
                      cmd.ExecuteNonQuery();
                }

                sqltran.Commit();
                cn.Close();
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Error al registrar al Docente en el/los curso/s");
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
