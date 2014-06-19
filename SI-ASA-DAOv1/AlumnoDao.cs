using System;
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
                throw new ApplicationException("Error al buscar los Alumnos");
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
                throw new ApplicationException("Error al buscar los Alumnos");
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
            string sql = @"SELECT        a.legajo, a.id_persona, a.id_madre, a.id_padre, a.conoce_musica, a.id_nivel_estudio, pA.id, pA.nombre, pA.apellido, pA.nro_documento, pA.domiclio, pA.telefono, pA.id_tipo_documento, pA.celular, pA.mail, 
                         pA.fecha_nacimiento
FROM                     alumnos AS a INNER JOIN
                         personas AS pA ON a.id_persona = pA.id
WHERE                    (a.id_persona = @id)";

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=ALEBELTRAMEN\\ALEJANDRA;Initial Catalog=ASA;Integrated Security=True";
            //PONER LA STRINGCONNECTION CORRECTA!!!

            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
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
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                throw new ApplicationException("Error al buscar al Alumno");
            }
            return alumno;
        }

        public static Alumno obtenerPorLegajo(int legajo)
        {
            Alumno alumno = new Alumno();
            string sql = @"SELECT        a.legajo, a.id_persona, a.id_madre, a.id_padre, a.conoce_musica, a.id_nivel_estudio, pA.id, pA.nombre, pA.apellido, pA.nro_documento, pA.domiclio, pA.telefono, pA.id_tipo_documento, pA.celular, pA.mail, 
                         pA.fecha_nacimiento
                         FROM            alumnos AS a INNER JOIN
                         personas AS pA ON a.id_persona = pA.id
                         WHERE        (a.id_persona = @id)";

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=ALEBELTRAMEN\\ALEJANDRA;Initial Catalog=ASA;Integrated Security=True";
            //PONER LA STRINGCONNECTION CORRECTA!!!

            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                SqlDataReader dr = cmd.ExecuteReader();

                alumno.legajo = (int)dr["legajo"];
                alumno.conoceMusica = (Boolean)dr["conoce_musica"];
                alumno.madre = PersonaDao.obtenerPersona((int)(dr["id_madre"]));
                alumno.padre = PersonaDao.obtenerPersona((int)(dr["id_padre"]));
                alumno.nivelEstudio = NivelEstudioDao.obtener((int)(dr["id_nivel_estudio"]));

                dr.Close();
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Error al buscar al Alumno");
            }
            finally
            {
                cn.Close();
            }

            return alumno;
        }

        public static void add(Alumno alumno, Persona madre, Persona alumnoP, Persona padre)
        {
            int i = -1;
            String sql = "INSERT INTO alumnos (id_persona, id_madre, id_padre, conoce_musica, id_nivel_estudio)";
            sql += "VALUES (@id_persona, @id_madre, @id_padre, @conoce_musica, @id_nivel_estudio)";
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=ALEBELTRAMEN\\ALEJANDRA;Initial Catalog=ASA;Integrated Security=True";
            //PONER LA STRINGCONNECTION CORRECTA!!!

            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("id_persona", PersonaDao.add(alumnoP));
                cmd.Parameters.AddWithValue("id_madre", PersonaDao.add(madre));
                cmd.Parameters.AddWithValue("id_padre", PersonaDao.add(padre));
                cmd.Parameters.AddWithValue("conoce_musica", (Boolean)alumno.conoceMusica);
                cmd.Parameters.AddWithValue("legajo", (int)alumno.legajo);

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
                cmd.Parameters.AddWithValue("id_nivel_estudio", idNivelEstudio);
                i = (int)cmd.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Error al insertar el Alumno");
            }
            finally
            {
                cn.Close();
            }
        }

        public static int delete(Alumno alumno)
        {
            int i = -1;

            string sql = "DELETE FROM alumnos a WHERE a.legajo=@legajo)";

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=ALEBELTRAMEN\\ALEJANDRA;Initial Catalog=ASA;Integrated Security=True";

            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("legajo", alumno.legajo);

                PersonaDao.delete(alumno.alumno);
                PersonaDao.delete(alumno.madre);
                PersonaDao.delete(alumno.padre);

                i = (int)cmd.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Error al eliminar el Alumno");
            }
            finally
            {
                cn.Close();
            }
            return i;
        }

        public static int update(Alumno alumnoViejo, Alumno alumnoNuevo, Persona personaAlumnoViejo, Persona personaAlumnoNuevo, Persona madreVieja, Persona padreViejo, Persona madreNueva, Persona padreNuevo)
        {
            int i = -1;

            string sql = "UPDATE alumnos a SET a.id_persona=@id_persona_nuevo, a.id_madre=@id_madre_nuevo, a.id_padre=@id_padre_nuevo, ";
            sql += "a.conoce_musica=@conoce_musica_nuevo, a.id_nivel_estudio=@id_nivel_estudio_nuevo ";
            sql += "WHERE a.id_persona=@id_persona AND a.id_madre=@id_madre AND a.id_padre=@id_padre AND ";
            sql += "a.conoce_musica=@conoce_musica AND a.id_nivel_estudio=@id_nivel_estudio";

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=ALEBELTRAMEN\\ALEJANDRA;Initial Catalog=ASA;Integrated Security=True";

            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);

                int idNivelEstudioNuevo = 0;
                int idNivelEstudioViejo = 0;
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

                switch (alumnoViejo.nivelEstudio.descripcion)
                {
                    case "Sin Estudios": idNivelEstudioViejo = 1;
                        break;
                    case "Primario Incompleto": idNivelEstudioViejo = 2;
                        break;
                    case "Primario Completo": idNivelEstudioViejo = 3;
                        break;
                    case "Secundario Incompleto": idNivelEstudioViejo = 4;
                        break;
                    case "Secundario Completo": idNivelEstudioViejo = 5;
                        break;
                    case "Terciario Incompleto": idNivelEstudioViejo = 6;
                        break;
                    case "Terciario Completo": idNivelEstudioViejo = 7;
                        break;
                    case "Universitario Incompleto": idNivelEstudioViejo = 8;
                        break;
                    case "Universitario Completo": idNivelEstudioViejo = 9;
                        break;
                    case "Posgrado": idNivelEstudioViejo = 10;
                        break;
                }
                
                cmd.Parameters.AddWithValue("id_persona_nuevo", PersonaDao.update(personaAlumnoViejo, personaAlumnoNuevo));
                cmd.Parameters.AddWithValue("id_madre_nuevo", PersonaDao.update(madreVieja, madreNueva));
                cmd.Parameters.AddWithValue("id_padre_nuevo", PersonaDao.update(padreViejo, padreNuevo));
                cmd.Parameters.AddWithValue("conoce_musica_nuevo", (Boolean)alumnoNuevo.conoceMusica);
                cmd.Parameters.AddWithValue("id_nivel_estudio_nuevo", idNivelEstudioNuevo);

                cmd.Parameters.AddWithValue("id_persona", PersonaDao.update(personaAlumnoViejo, personaAlumnoViejo));
                cmd.Parameters.AddWithValue("id_madre", PersonaDao.update(madreVieja, madreVieja));
                cmd.Parameters.AddWithValue("id_padre", PersonaDao.update(padreViejo, padreViejo));
                cmd.Parameters.AddWithValue("conoce_musica", (Boolean)alumnoViejo.conoceMusica);
                cmd.Parameters.AddWithValue("id_nivel_estudio", idNivelEstudioViejo);

                i = (int) cmd.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Error al actualizar los datos del Alumno");
            }
            finally
            {
                cn.Close();
            }
            return i;
        }

        public static Boolean exists(int legajo)
        {
            Boolean flag = false;

            string sql = "SELECT * FROM alumnos a WHERE a.legajo=@legajo";

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=ALEBELTRAMEN\\ALEJANDRA;Initial Catalog=ASA;Integrated Security=True";
            //PONER LA STRINGCONNECTION CORRECTA!!!

            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("legajo", legajo);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    flag = true;
                }

                dr.Close();
                
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Error al buscar en los Alumnos existentes");
            }
            finally
            {
                cn.Close();
            }

            return flag;
        }

    }

}
