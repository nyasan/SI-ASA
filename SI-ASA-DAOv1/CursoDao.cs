using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SI_ASA_ENTIDADESv1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace SI_ASA_DAOv1
{
    public class CursoDao
    {
        public static void Insertar(Curso curso, Horario horarioTrabajo)
        {
            string sqlCurso = @"INSERT INTO cursos
                              (nombre, descripcion, id_horario)
                              VALUES        (@nombre,@descripcion,@id_horario)";
            //string sqlHorario = "insert into horario values (@horario_desde, @horario_hasta)";

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=ALEBELTRAMEN\\ALEJANDRA;Initial Catalog=ASA;Integrated Security=True";
            try
            {
                cn.Open();
                //Se reemplaza por la llamada al metodo add() de HorarioDao:
                //SqlCommand cmd = new SqlCommand(sqlHorario, cn);
                //cmd.Parameters.AddWithValue("horario_desde", curso.hora_desde);
                //cmd.Parameters.AddWithValue("horario_hasta", curso.hora_hasta);
                //int idHorario = 0;
                //cmd.ExecuteNonQuery();
                //idHorario = Convert.ToInt32(cmd.ExecuteScalar());
                //cmd = null;

                SqlCommand cmd = new SqlCommand(sqlCurso, cn);

                cmd.Parameters.AddWithValue("@nombre", curso.nombre);
                cmd.Parameters.AddWithValue("@descripcion", curso.descripcion);
                cmd.Parameters.AddWithValue("@id_horario", HorarioDao.add(horarioTrabajo));

                cmd.ExecuteNonQuery();

            } catch (SqlException ex)
            {
                
                throw new ApplicationException("Error al insertar el curso");
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }

        }
        public static List<Curso> buscarPorParametros(String nombre)
        {
            List<Curso> listCursos = new List<Curso>();
            string sql = @"SELECT        cursos.id_curso, cursos.nombre, cursos.descripcion, cursos.id_horario, horarios.id_horario AS Expr1, horarios.horario_inicio, horarios.horario_fin
                         FROM            cursos INNER JOIN
                         horarios ON cursos.id_horario = horarios.id_horario
                         WHERE        (cursos.nombre LIKE @nombre)";
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=ALEBELTRAMEN\\ALEJANDRA;Initial Catalog=ASA;Integrated Security=True";
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@nombre", "%"+nombre+"%");
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Curso curso = new Curso()
                    {
                        nombre = dr["nombre"].ToString(),
                        descripcion = dr["descripcion"].ToString(),
                        horario = HorarioDao.obtener((int)dr["id_horario"]),
                        //hora_desde = dr["horario_inicio"].ToString(),
                        //hora_hasta = dr["horario_fin"].ToString()
                    };
                    listCursos.Add(curso); //lleno la coleccion en memoria
                }
                dr.Close();
            }
            catch (SqlException ex)
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                ex.StackTrace.ToString();
                throw new ApplicationException("Error al buscar los cursos por nombre"+ex.Message);
            }
            finally
            {
                cn.Close();
            }
            return listCursos;

        }

        public static List<Curso> ObtenerTodo()
        {
            List<Curso> listCursos = new List<Curso>();
            string sql = @"SELECT        cursos.id_curso, cursos.nombre, cursos.descripcion, cursos.id_horario, horarios.id_horario AS Expr1, horarios.horario_inicio, horarios.horario_fin
                         FROM            cursos INNER JOIN
                         horarios ON cursos.id_horario = horarios.id_horario";
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=ALEBELTRAMEN\\ALEJANDRA;Initial Catalog=ASA;Integrated Security=True";
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Curso curso = new Curso()
                    {
                        id_curso = int.Parse(dr["id_curso"].ToString()),
                        nombre = dr["nombre"].ToString(),
                        descripcion = dr["descripcion"].ToString(),
                        horario = HorarioDao.obtener((int)dr["id_horario"]),
                    };
                    listCursos.Add(curso);
                }
                dr.Close();
            }
            catch (SqlException ex)
            {
                ex.StackTrace.ToString();
                throw new ApplicationException("Error al buscar los cursos");
            }
            finally
            {
                cn.Close();
            }
            return listCursos;

        }

        public static void update(Curso cursoViejo, Curso cursoNuevo, Horario horarioViejo, Horario horarioNuevo)
        {
            int comprobar = 0;
            string sqlCurso = @"UPDATE       cursos
                              SET          nombre = @nombre, descripcion = @descripcion, id_horario = @id_horario
                              WHERE        (nombre LIKE @nombre_viejo) AND (descripcion LIKE @descripcion_vieja) AND (id_horario = @id_horario_viejo)";
            //string sqlHorario = "update horario set hora_desde=@hora_desde, hora_hasta=@hora_hasta where hora_desde=@hora_desde_vieja and hora_hasta=hora_hasta_vieja";
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=ALEBELTRAMEN\\ALEJANDRA;Initial Catalog=ASA;Integrated Security=True";
            try
            {
                cn.Open();
                //Se reemplaza por el metodo update() de HorarioDao:
                //SqlCommand cmd = new SqlCommand(sqlHorario, cn);
                //cmd.Parameters.AddWithValue("horario_desde", cursoNuevo.hora_desde);
                //cmd.Parameters.AddWithValue("horario_hasta", cursoNuevo.hora_hasta);
                //cmd.Parameters.AddWithValue("hora_desde_vieja", cursoViejo.hora_desde);
                //cmd.Parameters.AddWithValue("hora_hasta_vieja", cursoViejo.hora_hasta);
                //cmd.ExecuteNonQuery();
                //cmd = null;

                SqlCommand cmd = new SqlCommand(sqlCurso, cn);
                cmd.Parameters.AddWithValue("id_horario", HorarioDao.update( horarioNuevo));
                cmd.Parameters.AddWithValue("id_horario_viejo", HorarioDao.obtenerID(horarioViejo.desde, horarioViejo.hasta));
                cmd.Parameters.AddWithValue("nombre", cursoNuevo.nombre);
                cmd.Parameters.AddWithValue("descripcion", cursoNuevo.descripcion);
                cmd.Parameters.AddWithValue("nombre_viejo", cursoViejo.nombre);
                cmd.Parameters.AddWithValue("descripcion_vieja", cursoViejo.descripcion);

                comprobar = cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Error al modificar el curso");
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public static void delete(Curso curso)
        {
            string sqlCurso = @"DELETE FROM cursos
                              WHERE                         (nombre LIKE @nombre) AND (descripcion LIKE @descripcion)";
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=ALEBELTRAMEN\\ALEJANDRA;Initial Catalog=ASA;Integrated Security=True";
            try
            {
                cn.Open();

                SqlCommand cmd = new SqlCommand(sqlCurso, cn);

                cmd.Parameters.AddWithValue("nombre", curso.nombre);
                cmd.Parameters.AddWithValue("descripcion", curso.descripcion);
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                throw new ApplicationException("Error al eliminar el curso");
            }
            finally
            {
                cn.Close();
            }
        }

        public static Curso getCurso(string nombre)
        {
            Curso curso = new Curso();
            string sql = @"SELECT        cursos.id_curso, cursos.nombre, cursos.descripcion, cursos.id_horario, horarios.id_horario AS Expr1, horarios.horario_inicio, horarios.horario_fin
                         FROM            cursos INNER JOIN
                                                horarios ON cursos.id_horario = horarios.id_horario
                         WHERE        (cursos.nombre LIKE @nombre)";
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=ALEBELTRAMEN\\ALEJANDRA;Initial Catalog=ASA;Integrated Security=True";
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@nombre", "%" + nombre + "%");
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                curso.nombre = dr["nombre"].ToString();
                curso.descripcion = dr["descripcion"].ToString();
                curso.horario = HorarioDao.obtener((int)dr["id_horario"]);
                //curso.hora_desde = dr["horario_inicio"].ToString();
                //curso.hora_hasta = dr["horario_fin"].ToString();

                dr.Close();
            }
            catch (SqlException ex)
            {
                ex.StackTrace.ToString();
                throw new ApplicationException("Error al buscar los cursos por nombre" + ex.Message);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
            return curso;
        }

        public static int MaxId()
        {
            int i = 0;

            string sql = @"SELECT        MAX(id_curso) AS Expr1
                         FROM            cursos";

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=ALEBELTRAMEN\\ALEJANDRA;Initial Catalog=ASA;Integrated Security=True";

            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                i = (int)cmd.ExecuteScalar();
            }

            catch (SqlException ex)
            {
                throw new ApplicationException("Error al buscar los Alumnos");
            }
            finally
            {
                cn.Close();
            }

            return i + 1;
        }
    }
}
