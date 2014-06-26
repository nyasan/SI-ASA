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
            string sql = @"SELECT        alumnos.legajo, personas.nombre, personas.apellido, cursos.nombre AS nombreCurso, alumno_x_curso.fecha_inscripcion, falta_alumno_x_curso.fecha_falta
                            FROM            cursos INNER JOIN
                         alumno_x_curso ON cursos.id_curso = alumno_x_curso.id_curso RIGHT OUTER JOIN
                         alumnos INNER JOIN
                         personas ON alumnos.id_persona = personas.id ON alumno_x_curso.legajo_alumno = alumnos.legajo LEFT OUTER JOIN
                         falta_alumno_x_curso ON alumnos.legajo = falta_alumno_x_curso.legajo_alumno AND cursos.id_curso = falta_alumno_x_curso.id_curso
                            where 1 = 1";
           
        
            
            if (inscDesde != null)
                sql += " AND alumno_x_curso.fecha_inscripcion >= @fechaInscDesde";
            if (inscHasta != null)
                sql += " AND alumno_x_curso.fecha_inscripcion <= @fechaInscHasta";
            if (ausDesde != null)
                sql += " AND falta_alumno_x_curso.fecha_falta >= @fechaAuscDesde";
            if (ausHasta != null)
                sql += " AND falta_alumno_x_curso.fecha_falta <= @fechaAuscHasta";
            if (cursos != null)
            {
                sql += " AND ( 1 > 2 ";
                foreach (string i in cursos)
                {
                    
                    sql += " OR cursos.nombre LIKE @nombreCurso"+i;
                }
                sql += " )";
            }


            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=NICO;Initial Catalog=ASA;Integrated Security=True";
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                if (inscDesde != null)
                    cmd.Parameters.AddWithValue("@fechaInscDesde", inscDesde.Value);
                if (inscHasta != null)
                    cmd.Parameters.AddWithValue("@fechaInscHasta", inscHasta.Value);
                if (ausDesde != null)
                    cmd.Parameters.AddWithValue("@fechaAuscDesde", ausDesde.Value);
                if (ausHasta != null)
                    cmd.Parameters.AddWithValue("@fechaAuscHasta", ausHasta.Value);
                if (cursos != null)
                {
                   
                        foreach (string i in cursos)
                        {
                            cmd.Parameters.AddWithValue("@nombreCurso" + i, i);
                        }
                    
                }

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    AlumnoQuery alumno = new AlumnoQuery();
                 
                    alumno.Nombre = dr["nombre"].ToString();
                    alumno.Apellido = dr["apellido"].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("fecha_inscripcion")))
                    {
                        alumno.FechaInsc = DateTime.Parse(dr["fecha_inscripcion"].ToString());
                    
                    }
                    else
                    {
                        alumno.FechaInsc = null;
                    }
                    alumno.Curso = dr["nombreCurso"].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("fecha_falta")))
                    {
                        alumno.FechaFalta = DateTime.Parse(dr["fecha_falta"].ToString());
                    }
                    else
                    {
                        alumno.FechaFalta = null;
                    }
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
