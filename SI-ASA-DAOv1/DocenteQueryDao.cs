using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using SI_ASA_ENTIDADESv1;
namespace SI_ASA_DAOv1
{
   public class DocenteQueryDao
    {
        public static List<DocenteQuery> Informe(DateTime? inscDesde, DateTime? inscHasta, int? legDesde, int? legHasta, List<string> cursos)
        {
            List<DocenteQuery> listDocentes = new List<DocenteQuery>();
            string sql = @"SELECT        cursos.nombre, docentes.legajo, docente_x_curso.fecha_inscripcion, personas.nombre AS nombreCurso, personas.apellido
                            FROM            cursos INNER JOIN
                         docente_x_curso ON cursos.id_curso = docente_x_curso.id_curso INNER JOIN
                         docentes ON docente_x_curso.legajo_docente = docentes.legajo INNER JOIN
                         personas ON docentes.id_persona = personas.id
                            where 1 = 1";



            if (inscDesde != null)
                sql += " AND docente_x_curso.fecha_inscripcion >= @fechaInscDesde";
            if (inscHasta != null)
                sql += " AND docente_x_curso.fecha_inscripcion <= @fechaInscHasta";
            if (legDesde != null)
                sql += " AND docentes.legajo >= @legDesde";
            if (legHasta != null)
                sql += " AND docentes.legajo <= @legHasta";
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
                if (legDesde != null)
                    cmd.Parameters.AddWithValue("@legDesde", legDesde.Value);
                if (legHasta != null)
                   cmd.Parameters.AddWithValue("@legHasta", legHasta.Value);
                if (cursos != null)
                {
                    foreach (string i in cursos)
                    {
                        cmd.Parameters.AddWithValue("@nombreCurso", i);
                    }
                }

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    DocenteQuery Docente = new DocenteQuery();
                    Docente.Nombre = dr["nombre"].ToString();
                    Docente.Apellido = dr["apellido"].ToString();
                    Docente.FechaInsc = (DateTime)dr["fecha_inscripcion"];
                    Docente.Curso = dr["nombreCurso"].ToString();                   
                    Docente.Legajo = (int)dr["legajo"];

                    listDocentes.Add(Docente);
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
            return listDocentes;

        }
    }
}
