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
            string sql = @"SELECT        h.horario_inicio, h.horario_fin, c.nombre
                            FROM                     alumnos AS a INNER JOIN
                         alumno_x_curso AS axc ON a.legajo = axc.legajo_alumno INNER JOIN
                         cursos AS c ON c.id_curso = axc.id_curso INNER JOIN
                         horarios AS h ON h.id_horario = c.id_horario INNER JOIN
                         docente_x_curso AS dxc ON dxc.id_curso = c.id_curso INNER JOIN
                         docentes AS d ON d.legajo = dxc.legajo_docente
                         WHERE        (1 = 1)";



            if (horaDesde != null)
                sql += " AND  h.horario_inicio >= @horaDesde";
            if (horaHasta != null)
                sql += " AND  h.horario_fin <= @horaHasta";
            if (docente != null)
                sql += " AND d.legajo = @legDocente";
            if (alumno != null)
                sql += " AND a.legajo <= @legAlumno";
           


            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=ALEBELTRAMEN\\ALEJANDRA;Initial Catalog=ASA;Integrated Security=True";
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                if (horaDesde != null)
                     cmd.Parameters.AddWithValue("@horaDesde",horaDesde.ToString());
                if (horaHasta != null)
                    cmd.Parameters.AddWithValue("@horaHasta", horaHasta.ToString());
                if (docente != null)
                     cmd.Parameters.AddWithValue("@legDocente",docente);
                if (alumno != null)
                     cmd.Parameters.AddWithValue("@legAlumno",alumno);

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    CursoQuery curso = new CursoQuery();
                    curso.nombre = dr["nombre"].ToString();
                    curso.horaIni = DateTime.Parse(dr["horario_inicio"].ToString());
                    curso.horaFin = DateTime.Parse(dr["horario_fin"].ToString());
                    
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
