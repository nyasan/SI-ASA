using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using SI_ASA_ENTIDADESv1;
namespace SI_ASA_DAOv1
{
    public class CursosQueryDao
    {
        public static List<CursoQuery> Informe(DateTime? horaDesde,DateTime?  horaHasta,int? docente,int? alumno)
        {
            List<CursoQuery> listCursos = new List<CursoQuery>();
            string sql = @"SELECT        horarios.horario_inicio, horarios.horario_fin, cursos.nombre
                            FROM            alumno_x_curso INNER JOIN
                         alumnos ON alumno_x_curso.legajo_alumno = alumnos.legajo INNER JOIN
                         cursos ON alumno_x_curso.id_curso = cursos.id_curso INNER JOIN
                         docente_x_curso ON cursos.id_curso = docente_x_curso.id_curso INNER JOIN
                         docentes ON docente_x_curso.legajo_docente = docentes.legajo INNER JOIN
                         horarios ON cursos.id_horario = horarios.id_horario AND docentes.id_horario_trabajo = horarios.id_horario INNER JOIN
                         personas ON alumnos.id_persona = personas.id AND alumnos.id_madre = personas.id AND alumnos.id_padre = personas.id AND docentes.id_persona = personas.id
                            where 1 = 1";



            if (horaDesde != null)
                sql += " AND  horarios.horario_inicio = @horaDesde";
            if (horaHasta != null)
                sql += " AND  horarios.horario_fin <= @horaHasta";
            if (docente != null)
                sql += " AND docentes.legajo = @legDocente";
            if (alumno != null)
                sql += " AND alumnos.legajo <= @legAlumno";
           


            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=TVD-PC;Initial Catalog=ASA;Integrated Security=True";
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                if (horaDesde != null)
                     cmd.Parameters.AddWithValue("@horaDesde",horaDesde.Value);
                if (horaHasta != null)
                    cmd.Parameters.AddWithValue("@horaHasta", horaHasta.Value);
                if (docente != null)
                     cmd.Parameters.AddWithValue("@legDocente",docente);
                if (alumno != null)
                     cmd.Parameters.AddWithValue("@legAlumno",alumno);

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    CursoQuery curso = new CursoQuery();
                    curso.nombre = dr["nombre"].ToString();
                    curso.horaIni = (DateTime)dr["horario_inicio"];
                    curso.horaFin = (DateTime)dr["horario_fin"];
                    
                    listCursos.Add(curso);
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
            return listCursos;

        }
    }
}
