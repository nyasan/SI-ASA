﻿using System;
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
        public static void Insertar(Curso curso)
        {
            int comprobar = 0;
            string sqlCurso = "INSERT INTO cursos (nombre,descripcion,id_horario) values (@nombre,@descripcion,@id_horario)";
            string sqlHorario = "insert into horario values (@horario_desde, @horario_hasta)";

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=ALEBELTRAMEN\\ALEJANDRA;Initial Catalog=ASA;Integrated Security=True";
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sqlHorario, cn);
                cmd.Parameters.AddWithValue("horario_desde", curso.hora_desde);
                cmd.Parameters.AddWithValue("horario_hasta", curso.hora_hasta);

                int idHorario = 0;
                //cmd.ExecuteNonQuery();
                idHorario = Convert.ToInt32(cmd.ExecuteScalar());

                cmd = null;

                cmd = new SqlCommand(sqlCurso, cn);

                cmd.Parameters.AddWithValue("nombre", curso.nombre);
                cmd.Parameters.AddWithValue("descripcion", curso.descripcion);
                cmd.Parameters.AddWithValue("id_horario", idHorario);

                comprobar = cmd.ExecuteNonQuery();

                if (comprobar > 0) ;



            } catch (SqlException ex)
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                throw new ApplicationException("Error al insertar el curso");
            }
            finally
            {
                cn.Close();
            }

        }
        public static List<Curso> buscarPorParametros(String nombre)
        {
            List<Curso> listCursos = new List<Curso>();
            string sql = "select * from cursos join horarios on (cursos.id_horario=horarios.id_horario) where cursos.nombre like @nombre";
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
                        hora_desde = dr["horario_inicio"].ToString(),
                        hora_hasta = dr["horario_fin"].ToString()
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
            string sql = "select * from cursos join horarios on (cursos.id_horario=horarios.id_horario)";
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
                        nombre = dr["nombre"].ToString(),
                        descripcion = dr["descripcion"].ToString(),
                        hora_desde = dr["horario_inicio"].ToString(),
                        hora_hasta = dr["horario_fin"].ToString()
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
                throw new ApplicationException("Error al buscar los cursos");
            }
            finally
            {
                cn.Close();
            }
            return listCursos;

        }

        public static void update(Curso cursoViejo, Curso cursoNuevo)
        {
            int comprobar = 0;
            string sqlCurso = "update curso set nombre=@nombre, descripcion=@descripcion where nombre like @nombre_viejo and descripcion like @descripcion_vieja";
            string sqlHorario = "update horario set hora_desde=@hora_desde, hora_hasta=@hora_hasta where hora_desde=@hora_desde_vieja and hora_hasta=hora_hasta_vieja";
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=maquis;Initial Catalog=Pymes;User ID=avisuales2;Password=avisuales2";
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sqlHorario, cn);
                cmd.Parameters.AddWithValue("horario_desde", cursoNuevo.hora_desde);
                cmd.Parameters.AddWithValue("horario_hasta", cursoNuevo.hora_hasta);
                cmd.Parameters.AddWithValue("hora_desde_vieja", cursoViejo.hora_desde);
                cmd.Parameters.AddWithValue("hora_hasta_vieja", cursoViejo.hora_hasta);

                cmd.ExecuteNonQuery();

                cmd = null;

                cmd = new SqlCommand(sqlCurso, cn);

                cmd.Parameters.AddWithValue("nombre", cursoNuevo.nombre);
                cmd.Parameters.AddWithValue("descripcion", cursoNuevo.descripcion);
                cmd.Parameters.AddWithValue("nombre_viejo", cursoViejo.nombre);
                cmd.Parameters.AddWithValue("descripcion_vieja", cursoViejo.descripcion);


                comprobar = cmd.ExecuteNonQuery();

                //if (comprobar > 0) ;



            }
            catch (SqlException ex)
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                throw new ApplicationException("Error al modificar el curso");
            }
            finally
            {
                cn.Close();
            }
        }

        public static void delete(Curso curso)
        {
            int comprobar = 0;
            string sqlCurso = "delete on curso where nombre like @nombre and descripcion like @descripcion";
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=maquis;Initial Catalog=Pymes;User ID=avisuales2;Password=avisuales2";
            try
            {
                cn.Open();

                SqlCommand cmd = new SqlCommand(sqlCurso, cn);

                cmd.Parameters.AddWithValue("nombre", curso.nombre);
                cmd.Parameters.AddWithValue("descripcion", curso.descripcion);


                comprobar = cmd.ExecuteNonQuery();

                if (comprobar > 0) ;



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
    }
}
