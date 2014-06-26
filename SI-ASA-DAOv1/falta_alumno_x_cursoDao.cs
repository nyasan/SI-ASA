using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SI_ASA_ENTIDADESv1;
using System.Data.SqlClient;
using System.Data;

namespace SI_ASA_DAOv1
{
    public class falta_alumno_x_cursoDao
    {
        public static void registrarAsistencia(List<String> listAlumnos, int idCurso, DateTime fechaAsistencia)
        {
            int i = -1;
            String sql = @"INSERT INTO falta_alumno_x_curso
                         (legajo_alumno, id_curso, fecha_falta)
                         VALUES        (@legajo_alumno,@id_curso,@fecha_falta)";
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=CESAR-PC\\SQLSERVER;Initial Catalog=ASA;Integrated Security=True";
            cn.Open();
            SqlTransaction sqltran = cn.BeginTransaction();

            try
            {
               

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Transaction = sqltran;

                foreach (String l in listAlumnos)
                {
                    sql = @"INSERT INTO falta_alumno_x_curso
                         (legajo_alumno, id_curso, fecha_falta)
                         VALUES        (@legajo_alumno"+l+",@id_curso"+l+",@fecha_falta"+l+")";
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@legajo_alumno"+l, l);
                    cmd.Parameters.AddWithValue("@id_curso"+l, idCurso);
                    cmd.Parameters.AddWithValue("@fecha_falta"+l, fechaAsistencia);
                    cmd.ExecuteNonQuery();
                    i++;
                }

                sqltran.Commit();
                cn.Close();
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Error al registrar la asistencia de los alumnos");
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
