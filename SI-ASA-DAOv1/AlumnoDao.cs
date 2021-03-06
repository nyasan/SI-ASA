﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SI_ASA_ENTIDADESv1;
using System.Data.SqlClient;
using System.Data;

namespace SI_ASA_DAOv1
{
    public class AlumnoDao
    {
        
        public static List<Alumno> obtenerTodo()
        {
            List<Alumno> listAlumnos = new List<Alumno>();

            string sql = "SELECT * FROM alumnos a JOIN personas pA ON (a.id_persona = pA.id) ";

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=ALEBELTRAMEN\\ALEJANDRA;Initial Catalog=ASA;Integrated Security=True";
            //PONER LA STRINGCONNECTION CORRECTA!!!

            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Alumno alumno = new Alumno()
                    {
                        alumno = PersonaDao.obtenerPersona((int)(dr["id_persona"])),
                        legajo = (int)dr["legajo"],
                        conoceMusica = (Boolean)dr["conoce_musica"],
                        madre = PersonaDao.obtenerPersona((int)(dr["id_madre"])),
                        padre = PersonaDao.obtenerPersona((int)(dr["id_padre"])),
                        nivelEstudio = NivelEstudioDao.obtener((int)(dr["id_nivel_estudio"]))
                    };
                    listAlumnos.Add(alumno);
                }
                dr.Close();
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Error al buscar los Alumnos" + ex.Message);
            }
            finally
            {
                cn.Close();
            }
            return listAlumnos;


        }

        public static List<Alumno> buscarPorParametros(String nombre, String apellido, int legajo)
        {
            List<Alumno> listAlumnos = new List<Alumno>();

            string sql = @"SELECT        a.legajo, a.id_persona, a.id_madre, a.id_padre, a.conoce_musica, a.id_nivel_estudio
                         FROM            alumnos AS a INNER JOIN
                         personas AS pA ON a.id_persona = pA.id
                         WHERE        (1 = 1) ";

            if (nombre != "")
                sql += " AND (pA.nombre LIKE @nombre)";
            if (apellido != "")
                sql += " AND (pA.apellido LIKE @apellido)";
            if (legajo != 0)
                sql += " AND (a.legajo = @legajo)";

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=ALEBELTRAMEN\\ALEJANDRA;Initial Catalog=ASA;Integrated Security=True";
            //PONER LA STRINGCONNECTION CORRECTA!!!

            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);

                if(nombre!="")
                    cmd.Parameters.AddWithValue("@nombre", "%" + nombre + "%");
                if(apellido!="")
                    cmd.Parameters.AddWithValue("@apellido", "%" + apellido + "%");
                if(legajo!=0)
                    cmd.Parameters.AddWithValue("@legajo", legajo);

                SqlDataReader dr = cmd.ExecuteReader();
                int c = 0;
                while (dr.Read())
                {
                    Alumno alumno = new Alumno()
                    {
                        legajo = (int)dr["legajo"],
                        conoceMusica = (Boolean)dr["conoce_musica"],
                        madre = PersonaDao.obtenerPersona((int)(dr["id_madre"])),
                        padre = PersonaDao.obtenerPersona((int)(dr["id_padre"])),
                        nivelEstudio = NivelEstudioDao.obtener((int)(dr["id_nivel_estudio"]))
                    };

                    listAlumnos.Add(alumno); //lleno la coleccion en memoria
                    c++;
                }
                dr.Close();

            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Error al buscar los Alumnos" + ex.Message);
            }
            finally
            {
                cn.Close();
            }
            return listAlumnos;


        }
        public static Alumno obtener(int id)
        {
            Alumno alumno = new Alumno();
            string sql = @"SELECT        a.legajo, a.id_persona, a.id_madre, a.id_padre, a.conoce_musica, a.id_nivel_estudio, pA.id, pA.nombre, pA.apellido, pA.nro_documento, pA.domicilio, pA.telefono, pA.id_tipo_documento, pA.celular, pA.mail, 
                         pA.fecha_nacimiento
                         FROM                     alumnos AS a INNER JOIN
                         personas AS pA ON a.id_persona = pA.id
                         WHERE                    (a.id_persona = @idPersona)";

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=ALEBELTRAMEN\\ALEJANDRA;Initial Catalog=ASA;Integrated Security=True";
            //PONER LA STRINGCONNECTION CORRECTA!!!

            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@idPersona", id);
                SqlDataReader dr = cmd.ExecuteReader();

                alumno.legajo = (int)dr["legajo"];
                alumno.conoceMusica = (Boolean)dr["conoce_musica"];
                alumno.madre = PersonaDao.obtenerPersona((int)(dr["id_madre"]));
                alumno.padre = PersonaDao.obtenerPersona((int)(dr["id_padre"]));
                alumno.nivelEstudio = NivelEstudioDao.obtener((int)(dr["id_nivel_estudio"]));

                dr.Close();
                cn.Close();
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Error al buscar al Alumno" + ex.Message);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
            return alumno;
        }

        public static Alumno obtenerPorLegajo(int legajo)
        {
            Alumno alumno = new Alumno();
            string sql = @"SELECT        a.legajo, a.id_persona, a.id_madre, a.id_padre, a.conoce_musica, a.id_nivel_estudio, pA.id, pA.nombre, pA.apellido, pA.nro_documento, pA.domicilio, pA.telefono, pA.id_tipo_documento, pA.celular, pA.mail, 
                         pA.fecha_nacimiento
                         FROM            alumnos AS a INNER JOIN
                         personas AS pA ON a.id_persona = pA.id
                         WHERE        (a.legajo = @legajo)";

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=ALEBELTRAMEN\\ALEJANDRA;Initial Catalog=ASA;Integrated Security=True";
            //PONER LA STRINGCONNECTION CORRECTA!!!

            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@legajo", legajo);
                SqlDataReader dr = cmd.ExecuteReader();
                if (!dr.HasRows)
                {
                    alumno = null;
                }
                else
                {
                    dr.Read();

                    //alumno.legajo = (int)dr["legajo"];
                    //alumno.legajo = legajo;
                    //alumno.alumno = PersonaDao.obtenerPersona(int.Parse(dr["id_persona"].ToString()));
                    //alumno.conoceMusica = Boolean.Parse(dr["conoce_musica"].ToString());
                    //alumno.madre = PersonaDao.obtenerPersona(int.Parse(dr["id_madre"].ToString()));
                    //alumno.padre = PersonaDao.obtenerPersona(int.Parse(dr["id_padre"].ToString()));
                    //alumno.nivelEstudio = NivelEstudioDao.obtener(int.Parse(dr["id_nivel_estudio"].ToString()));

                    alumno.alumno = PersonaDao.obtenerPersona((int)(dr["id_persona"]));
                    alumno.legajo = (int)dr["legajo"];
                    alumno.conoceMusica = (Boolean)dr["conoce_musica"];
                    alumno.madre = PersonaDao.obtenerPersona((int)(dr["id_madre"]));
                    alumno.padre = PersonaDao.obtenerPersona((int)(dr["id_padre"]));
                    alumno.nivelEstudio = NivelEstudioDao.obtener((int)(dr["id_nivel_estudio"]));

                   
                }
                dr.Close();
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Error al buscar al Alumno" + ex.Message);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }

            return alumno;
        }

        public static void add(Alumno alumno, Persona madre, Persona alumnoP, Persona padre)
        {
            int i = -1;
            String sql = @"INSERT INTO alumnos
                         (id_persona, id_madre, id_padre, conoce_musica, id_nivel_estudio)
                         VALUES        (@id_persona,@id_madre,@id_padre,@conoce_musica,@id_nivel_estudio) SELECT CAST(scope_identity() AS int)";
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=ALEBELTRAMEN\\ALEJANDRA;Initial Catalog=ASA;Integrated Security=True";
            //PONER LA STRINGCONNECTION CORRECTA!!!

            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@id_persona", PersonaDao.add(alumnoP));
                cmd.Parameters.AddWithValue("@id_madre", PersonaDao.add(madre));
                cmd.Parameters.AddWithValue("@id_padre", PersonaDao.add(padre));
                cmd.Parameters.AddWithValue("@conoce_musica", (Boolean)alumno.conoceMusica);
                cmd.Parameters.AddWithValue("@legajo", (int)alumno.legajo); //NO SE PARA QUE ESTA SI NO HACE FALTA EL LEGAJO

                int idNivelEstudio = 0;
                switch (alumno.nivelEstudio.descripcion)
                {
                    case "Sin Estudios": idNivelEstudio = 1;
                        break;
                    case "Primario Incompleto": idNivelEstudio = 2;
                        break;
                    case "Primario Completo": idNivelEstudio = 3;
                        break;
                    case "Secundario Incompleto": idNivelEstudio = 4;
                        break;
                    case "Secundario Completo": idNivelEstudio = 5;
                        break;
                    case "Terciario Incompleto": idNivelEstudio = 6;
                        break;
                    case "Terciario Completo": idNivelEstudio = 7;
                        break;
                    case "Universitario Incompleto": idNivelEstudio = 8;
                        break;
                    case "Universitario Completo": idNivelEstudio = 9;
                        break;
                    case "Posgrado": idNivelEstudio = 10;
                        break;
                }
                cmd.Parameters.AddWithValue("@id_nivel_estudio", idNivelEstudio);
                resetearAutoIncrement(MaxLegajo()-1); // aca le pone el autoincrement en el ultimo legajo de la tabla, pido el max legajo, -1 es el ultimo de la tabla
                
                i = (Int32)cmd.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Error al insertar el Alumno" + ex.Message);
            }
            finally
            {
                cn.Close();
            }
        }

        public static void delete(Alumno alumno)
        {
            

            string sql = @"DELETE FROM alumnos
                         WHERE        (legajo = @legajo) SELECT CAST(scope_identity() AS int)";

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=ALEBELTRAMEN\\ALEJANDRA;Initial Catalog=ASA;Integrated Security=True";

            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@legajo", alumno.legajo);

                cmd.ExecuteScalar();

                PersonaDao.delete(alumno.alumno);
                PersonaDao.delete(alumno.madre);
                PersonaDao.delete(alumno.padre);
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Error al eliminar el Alumno"+ex.Message);
            }
            finally
            {
                cn.Close();
            }
            
        }

        public static void update(Alumno alumnoViejo, Alumno alumnoNuevo, Persona personaAlumnoViejo, Persona personaAlumnoNuevo, Persona madreVieja, Persona padreViejo, Persona madreNueva, Persona padreNuevo)
        {
            

            string sql = @"UPDATE       alumnos
                         SET                conoce_musica = @conoce_musica_nuevo, id_nivel_estudio = @id_nivel_estudio_nuevo
                         WHERE        (legajo = @legajoAlumno)";

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=ALEBELTRAMEN\\ALEJANDRA;Initial Catalog=ASA;Integrated Security=True";

            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);

                int idNivelEstudioNuevo = 0;
                
                switch (alumnoNuevo.nivelEstudio.descripcion)
                {
                    case "Sin Estudios": idNivelEstudioNuevo = 1;
                        break;
                    case "Primario Incompleto": idNivelEstudioNuevo = 2;
                        break;
                    case "Primario Completo": idNivelEstudioNuevo = 3;
                        break;
                    case "Secundario Incompleto": idNivelEstudioNuevo = 4;
                        break;
                    case "Secundario Completo": idNivelEstudioNuevo = 5;
                        break;
                    case "Terciario Incompleto": idNivelEstudioNuevo = 6;
                        break;
                    case "Terciario Completo": idNivelEstudioNuevo = 7;
                        break;
                    case "Universitario Incompleto": idNivelEstudioNuevo = 8;
                        break;
                    case "Universitario Completo": idNivelEstudioNuevo = 9;
                        break;
                    case "Posgrado": idNivelEstudioNuevo = 10;
                        break;
                }
                
                PersonaDao.update(madreVieja, madreNueva);
                PersonaDao.update(padreViejo, padreNuevo);
                PersonaDao.update(personaAlumnoViejo, personaAlumnoNuevo);
                cmd.Parameters.AddWithValue("@legajoAlumno", alumnoViejo.legajo);
                cmd.Parameters.AddWithValue("@conoce_musica_nuevo", (Boolean)alumnoNuevo.conoceMusica);
                cmd.Parameters.AddWithValue("@id_nivel_estudio_nuevo", idNivelEstudioNuevo);

                

                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Error al actualizar los datos del Alumno"+ex.Message);
            }
            finally
            {
                cn.Close();
            }
            
        }

        public static Boolean exists(int legajo)
        {
            Boolean flag = false;

            string sql = @"SELECT        legajo, id_persona, id_madre, id_padre, conoce_musica, id_nivel_estudio
                         FROM            alumnos
                         WHERE        (legajo = @legajo)";

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=ALEBELTRAMEN\\ALEJANDRA;Initial Catalog=ASA;Integrated Security=True";
            //PONER LA STRINGCONNECTION CORRECTA!!!

            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@legajo", legajo);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    flag = true;
                }

                dr.Close();
                
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Error al buscar en los Alumnos existentes"+ex.Message);
            }
            finally
            {
                cn.Close();
            }

            return flag;
        }

        public static List<Alumno> buscarPorParametros(String nombre, String apellido, int legajo, int numero, int tipo_documento)
        {
            List<Alumno> listAlumnos = new List<Alumno>();

            string sql = @"SELECT        a.legajo, a.id_persona, a.id_madre, a.id_padre, a.conoce_musica, a.id_nivel_estudio
                         FROM            alumnos AS a INNER JOIN
                         personas AS pA ON a.id_persona = pA.id
                         WHERE        (1 = 1) ";

            if (nombre != "")
                sql += " AND (pA.nombre LIKE @nombre)";
            if (apellido != "")
                sql += " AND (pA.apellido LIKE @apellido)";
            if (legajo != 0)
                sql += " AND (a.legajo = @legajo)";
            if (numero != 0 && tipo_documento != 0)
                sql += " AND (pA.nro_documento = @numero) AND (pA.id_tipo_documento = @tipo_documento)";
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=ALEBELTRAMEN\\ALEJANDRA;Initial Catalog=ASA;Integrated Security=True";

            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);

                if (nombre != "")
                    cmd.Parameters.AddWithValue("@nombre", "%" + nombre + "%");
                if (apellido != "")
                    cmd.Parameters.AddWithValue("@apellido", "%" + apellido + "%");
                if (legajo != 0)
                    cmd.Parameters.AddWithValue("@legajo", legajo);
                if (numero != 0 && tipo_documento != 0)
                {
                    cmd.Parameters.AddWithValue("@numero", numero);
                    cmd.Parameters.AddWithValue("@tipo_documento", tipo_documento);
                }

                SqlDataReader dr = cmd.ExecuteReader();
                int c = 0;
                while (dr.Read())
                {
                    Alumno alumno = new Alumno()
                    {
                        legajo = (int)dr["legajo"],
                        conoceMusica = (Boolean)dr["conoce_musica"],
                        alumno = PersonaDao.obtenerPersona((int)dr["id_persona"]),
                        madre = PersonaDao.obtenerPersona((int)(dr["id_madre"])),
                        padre = PersonaDao.obtenerPersona((int)(dr["id_padre"])),
                        nivelEstudio = NivelEstudioDao.obtener((int)(dr["id_nivel_estudio"]))
                    };

                    listAlumnos.Add(alumno); //lleno la coleccion en memoria
                    c++;
                }
                dr.Close();

            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Error al buscar los Alumnos" + ex.Message );
            }
            finally
            {
                cn.Close();
            }
            return listAlumnos;
        }

        public static int MaxLegajo()
        {
            int i = 0;

            string sql = @"SELECT        MAX(legajo) AS Expr1
                         FROM            alumnos";

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=ALEBELTRAMEN\\ALEJANDRA;Initial Catalog=ASA;Integrated Security=True";

            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                i = (int) cmd.ExecuteScalar();
            }

            catch (SqlException ex)
            {
                throw new ApplicationException("Error en el MaxLegajo" + ex.Message);
            }
            finally
            {
                cn.Close();
            }

            return i+1;
        }
        public static void resetearAutoIncrement(int ultimoVal)
        {
            string sql = "DBCC CHECKIDENT ('alumnos', RESEED, @val);";
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=ALEBELTRAMEN\\ALEJANDRA;Initial Catalog=ASA;Integrated Security=True";
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@val", ultimoVal);
                cmd.ExecuteNonQuery();
               
            }

            catch (SqlException ex)
            {
                throw new ApplicationException("Error al resetear autoincrmental en el ultimo valor"+ex.Message);
            }
            finally
            {
                cn.Close();
            }

         
        }

    }

}
