using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using SI_ASA_ENTIDADESv1;

namespace SI_ASA_DAOv1
{
   public class AlumnosQueryDao
    {
        public static List<AlumnoQuery> Informe(DateTime? inscDesde, DateTime? inscHasta, DateTime? ausDesde, DateTime? ausHasta, List<string> cursos)
        {
            List<AlumnoQuery> listAlumnos = new List<AlumnoQuery>();
            string sql = @"SELECT        personas.nombre, personas.apellido, cursos.nombre AS nombreCurso, alumno_x_curso.fecha_inscripcion, falta_alumno_x_curso.fecha_falta, alumnos.legajo
                            FROM            alumno_x_curso INNER JOIN
                         alumnos ON alumno_x_curso.legajo_alumno = alumnos.legajo INNER JOIN
                         cursos ON alumno_x_curso.id_curso = cursos.id_curso INNER JOIN
                         falta_alumno_x_curso ON alumnos.legajo = falta_alumno_x_curso.legajo_alumno AND cursos.id_curso = falta_alumno_x_curso.id_curso INNER JOIN
                         horarios ON cursos.id_horario = horarios.id_horario INNER JOIN
                         personas ON alumnos.id_persona = personas.id AND alumnos.id_madre = personas.id AND alumnos.id_padre = personas.id
                            where 1 = 1";
           
        
            
            if (inscDesde != null)
                sql += " AND alumno_x_curso.fecha_inscripcion >= @fechaInscDesde";
            if (inscHasta != null)
                sql += " AND alumno_x_curso.fecha_inscripcion <= @fechaInscHasta";
            if (ausDesde != null)
                sql += " AND falta_x_alumno_curso.fecha_falta >= @fechalAuscDesde";
            if (ausHasta != null)
                sql += " AND falta_x_alumno_curso.fecha_falta <= @fechaAuscHasta";
            if (cursos != null)
            {
                foreach (string i in cursos)
                {
                    sql += " AND cursos.nombre LIKE @nombreCurso";
                }
            }


            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=TVD-PC;Initial Catalog=ASA;Integrated Security=True";
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                if (inscDesde != null)
                    cmd.Parameters.AddWithValue("@fechaInscDesde", inscDesde.Value);
                if (inscHasta != null)
                    cmd.Parameters.AddWithValue("@fechaInscHasta", inscHasta.Value);
                if (ausDesde != null)
                    cmd.Parameters.AddWithValue("@fechaAusDesde", ausDesde.Value);
                if (ausHasta != null)
                    cmd.Parameters.AddWithValue("@fechaAusHasta", ausHasta.Value);
                if (cursos.Count != 0)
                {
                    foreach (string i in cursos)
                    {
                        cmd.Parameters.AddWithValue("@nombreCurso", i);
                    }
                }

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    AlumnoQuery alumno = new AlumnoQuery();
                    alumno.Nombre = dr["nombre"].ToString();
                    alumno.Apellido = dr["apellido"].ToString();
                    alumno.FechaInsc = (DateTime)dr["fecha_inscripcion"];
                    alumno.Curso = dr["nombreCurso"].ToString();
                    alumno.FechaFalta = (DateTime)dr["fecha_falta"];
                    alumno.Legajo = (int)dr["legajo"];

                    listAlumnos.Add(alumno);
                }
                dr.Close();
               
            }
            catch (SqlException ex)
            {
                
                    
                throw new ApplicationException(ex.Message);
            }
            finally
            {
                cn.Close();
            }
            return listAlumnos;

        }
    }
}
