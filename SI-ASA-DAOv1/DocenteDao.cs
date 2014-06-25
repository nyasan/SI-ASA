using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SI_ASA_ENTIDADESv1;
using System.Data.SqlClient;
using System.Data;

namespace SI_ASA_DAOv1
{
    public class DocenteDao
    {
        public static List<Docente> obtenerTodo()
        {
            List<Docente> listDocentes = new List<Docente>();

            string sql = @"SELECT        d.legajo, d.id_persona, d.id_horario_trabajo, d.salario, p.id, p.nombre, p.apellido, p.nro_documento, p.domicilio, p.telefono, p.id_tipo_documento, p.celular, p.mail, p.fecha_nacimiento
                         FROM            docentes AS d INNER JOIN
                         personas AS p ON d.id_persona = p.id";

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
                    Docente docente = new Docente();

                    docente.docente = PersonaDao.obtenerPersona(int.Parse(dr["id_persona"].ToString()));
                    docente.legajo = int.Parse(dr["legajo"].ToString());
                    docente.horarioTrabajo = HorarioDao.obtener((int)dr["id_horario_trabajo"]);
                    docente.salario = float.Parse(dr["salario"].ToString());
                    
                    listDocentes.Add(docente);
                }
                dr.Close();
                cn.Close();
            }
            catch (SqlException ex)
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                throw new ApplicationException("Error al buscar los Docentes");
            }
            return listDocentes;


        }

        public static List<Docente> buscarPorParametros(String nombre, String apellido, int legajo,int tipo_documento, int numero)
        {
            List<Docente> listDocentes = new List<Docente>();

            string sql = @"SELECT        a.legajo, a.id_persona, id_horario_trabajo, salario
                         FROM            docentes AS a INNER JOIN
                         personas AS pA ON a.id_persona = pA.id
                         WHERE        (1 = 1) ";

            if (nombre != "")
                sql += " AND (pA.nombre LIKE @nombre)";
            if (apellido != "")
                sql += " AND (pA.apellido LIKE @apellido)";
            if (legajo != 0)
                sql += " AND (a.legajo = @legajo)";
            if (tipo_documento != 0 && numero != 0)
                sql += " AND (a.tipo_documento = @tipo_documento) AND (numero = @numero)";

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=ALEBELTRAMEN\\ALEJANDRA;Initial Catalog=ASA;Integrated Security=True";
            //PONER LA STRINGCONNECTION CORRECTA!!!

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
                if (tipo_documento != 0 && numero != 0)
                {
                    cmd.Parameters.AddWithValue("@tipo_documento", tipo_documento);
                    cmd.Parameters.AddWithValue("@numero", numero);
                }

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Docente docente = new Docente()
                    {
                        legajo = (int)dr["legajo"],
                        docente = PersonaDao.obtenerPersona((int)(dr["id_persona"])),
                        salario = float.Parse(dr["salario"].ToString()),
                        horarioTrabajo = HorarioDao.obtener(int.Parse(dr["id_horario_trabajo"].ToString()))
                    };
                }
                dr.Close();

            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Error al buscar los Docentes");
            }
            finally
            {
                cn.Close();
            }
            return listDocentes;

        }
        public static Docente obtener(int id)
        {
            Docente docente = new Docente();
            string sql = @"SELECT        d.legajo, d.id_persona, d.id_horario_trabajo, d.salario, p.id, p.nombre, p.apellido, p.nro_documento, p.domiclio, p.telefono, p.id_tipo_documento, p.celular, p.mail, p.fecha_nacimiento
                         FROM            docentes AS d INNER JOIN
                         personas AS p ON d.id_persona = p.id
                         WHERE        (d.id_persona = @id)";

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=ALEBELTRAMEN\\ALEJANDRA;Initial Catalog=ASA;Integrated Security=True";
            //PONER LA STRINGCONNECTION CORRECTA!!!

            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader dr = cmd.ExecuteReader();

                docente.legajo = (int)dr["legajo"];
                docente.docente = PersonaDao.obtenerPersona((int)(dr["id_persona"]));
                docente.horarioTrabajo = HorarioDao.obtener((int)(dr["id_horario_trabajo"]));
                docente.salario = (int)dr["salario"];
                
                dr.Close();
                cn.Close();
            }
            catch (SqlException ex)
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                throw new ApplicationException("Error al buscar el Docente");
            }
            return docente;
        }

        //Retorna TRUE si se insertó correctamente; FALSE en todo otro caso.
        public static int add(Docente docente, Persona docenteP, Horario horarioTrabajo)
        {
            int i = -1;
            string sql = @"INSERT INTO docentes
                         (id_persona, id_horario_trabajo, salario)
                         VALUES        (@id_persona,@id_horario_trabajo,@salario)";
            
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=ALEBELTRAMEN\\ALEJANDRA;Initial Catalog=ASA;Integrated Security=True";
            //PONER LA STRINGCONNECTION CORRECTA!!!

            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@id_persona", PersonaDao.add(docenteP));
                cmd.Parameters.AddWithValue("@id_horario_trabajo", HorarioDao.add(horarioTrabajo));
                cmd.Parameters.AddWithValue("@salario", docente.salario);

                i= (int) cmd.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Error al insertar al Docente");
            }
            finally
            {
                cn.Close();
            }

            return i;
        }

        public static int delete(Docente docente)
        {
            int i = -1;

            string sql = @"DELETE FROM docentes
                         WHERE        (legajo = @legajo)";

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=ALEBELTRAMEN\\ALEJANDRA;Initial Catalog=ASA;Integrated Security=True";

            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@legajo", docente.legajo);

                PersonaDao.delete(docente.docente);

                i = (int)cmd.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Error al eliminar el Docente");
            }
            finally
            {
                cn.Close();
            }
            return i;
        }

        public static int update(Docente docenteViejo, Docente docenteNuevo, Persona personaNueva, Persona personaVieja, Horario horarioViejo, Horario horarioNuevo)
        {
            int i = -1;

            string sql = @"UPDATE       docentes
                         SET                id_horario_trabajo = @id_horario_trabajo_nuevo, salario = @salario_nuevo
                         WHERE        (id_persona = @id_persona) AND (id_horario_trabajo = @horario_trabajo) AND (salario = @salario)";

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=ALEBELTRAMEN\\ALEJANDRA;Initial Catalog=ASA;Integrated Security=True";

            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@id_horario_trabajo_nuevo", HorarioDao.update(horarioViejo, horarioNuevo));
                cmd.Parameters.AddWithValue("@salario_nuevo", docenteNuevo.salario);

                cmd.Parameters.AddWithValue("@id_persona", PersonaDao.update(personaVieja, personaVieja));
                cmd.Parameters.AddWithValue("@id_horario_trabajo", HorarioDao.update(horarioViejo, horarioViejo));
                cmd.Parameters.AddWithValue("@salario", docenteViejo.salario);

                i = (int) cmd.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Error al actualizar los datos del Docente");
            }
            finally
            {
                cn.Close();
            }
            return i;
        }

        public static Docente obtenerPorLegajo(int legajo)
        {
            Docente docente = new Docente();
            string sql = @"SELECT        d.legajo, d.id_persona, d.id_horario_trabajo, d.salario, p.id, p.nombre, p.apellido, p.nro_documento, p.domiclio, p.telefono, p.id_tipo_documento, p.celular, p.mail, p.fecha_nacimiento
                         FROM            docentes AS d INNER JOIN
                         personas AS p ON d.id_persona = p.id
                         WHERE        (d.legajo = @legajo)";

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=ALEBELTRAMEN\\ALEJANDRA;Initial Catalog=ASA;Integrated Security=True";
            //PONER LA STRINGCONNECTION CORRECTA!!!

            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@legajo", legajo);
                SqlDataReader dr = cmd.ExecuteReader();

                docente.legajo = (int)dr["legajo"];
                docente.docente = PersonaDao.obtenerPersona((int)(dr["id_persona"]));
                docente.horarioTrabajo = HorarioDao.obtener((int)(dr["id_horario_trabajo"]));
                docente.salario = (int)dr["salario"];

                dr.Close();
                cn.Close();
            }
            catch (SqlException ex)
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                throw new ApplicationException("Error al buscar el Docente");
            }
            return docente;
        }

        public static int MaxLegajo()
        {
            int i = 0;

            string sql = @"SELECT        MAX(legajo) AS Expr1
                         FROM            docentes";

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
                throw new ApplicationException("Error al buscar los Docentes");
            }
            finally
            {
                cn.Close();
            }

            return i + 1;
        }
    }
}
