using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SI_ASA_ENTIDADESv1;
using System.Data.SqlClient;
using System.Data;


namespace SI_ASA_DAOv1
{
    public class PersonaDao
    {

        public static List<Persona> Obtener(TipoDocumento tipoDoc, int numDoc)
        {
            List<Persona> listPersonas = new List<Persona>();

            string sql = @"SELECT        p.id, p.nombre, p.apellido, p.nro_documento, p.domicilio, p.telefono, p.id_tipo_documento, p.celular, p.mail, p.fecha_nacimiento, t.id_tipo_documento AS Expr1, t.descripcion
                         FROM            personas AS p INNER JOIN
                         tipo_documento AS t ON p.id_tipo_documento = t.id_tipo_documento";
            
            //(if (tipoDoc.hasValue() || numDoc == -1)
            if (tipoDoc == null || numDoc == -1)
            {
                String tipoDocu = tipoDoc.ToString();

                sql += "WHERE t.descripcion = @tipoDocu AND p.nro_documento = @numDoc";

            }

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=NICO;Initial Catalog=ASA;Integrated Security=True";
            //PONER LA STRINGCONNECTION CORRECTA!!!
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@tipoDocu", tipoDoc.descripcion);
                cmd.Parameters.AddWithValue("@numDoc", numDoc);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Persona persona = new Persona()
                    {
                        nombre = dr["nombre"].ToString(),
                        apellido = dr["apellido"].ToString(),
                        numDoc = (int)dr["nro_documento"],
                        tipoDoc = TipoDocumentoDao.obtenerTipoDocumento((int)dr["id_tipo_documento"]),
                        domicilio = dr["domicilio"].ToString(),
                        telefono = dr["telefono"].ToString(),
                        celular = dr["celular"].ToString(),
                        mail = dr["mail"].ToString(),
                        fechaNacimiento = (DateTime) dr["fecha_nacimiento"]
                    };

                    listPersonas.Add(persona); 
                }
                dr.Close();
                cn.Close();
            }
            catch (SqlException ex)
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                throw new ApplicationException("Error al buscar las Personas");
            }
            return listPersonas;
        }

        //Busca TODAS las personas existentes en la BD
        public static void obtenerTodo()
        {
            Obtener(null, -1);

        }

        //Busca UNA persona por su Tipo y Número de Documento
        public static void obtenerPersona(TipoDocumento t, int num)
        {
            Obtener(t, num);
        }

        //Busca UNA persona por su ID
        public static Persona obtenerPersona(int id)
        {
            Persona persona = new Persona();
            string sql = "SELECT * FROM personas p WHERE p.id = @idPersona";

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=CESAR-PC\\SQLSERVER;Initial Catalog=ASA;Integrated Security=True";
            //PONER LA STRINGCONNECTION CORRECTA!!!
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@idPersona", id);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                        persona.nombre = dr["nombre"].ToString();
                        persona.apellido = dr["apellido"].ToString();
                        persona.numDoc = int.Parse(dr["nro_documento"].ToString());
                        persona.tipoDoc = TipoDocumentoDao.obtenerTipoDocumentoPorId(int.Parse(dr["id_tipo_documento"].ToString()));
                        persona.domicilio = dr["domiclio"].ToString();
                        persona.telefono = dr["telefono"].ToString();
                        persona.celular = dr["celular"].ToString();
                        persona.mail = dr["mail"].ToString();
                        String fecha = dr["fecha_nacimiento"].ToString();
                        persona.fechaNacimiento = DateTime.Parse(fecha);
                
                dr.Close();
                cn.Close();
            }
            catch (SqlException ex)
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                throw new ApplicationException("Error al buscar la Persona");
            }

            return persona;
        }

        public static int add(Persona persona)
        {
            int i = 0;

            String sql = @"INSERT INTO personas
                         (nombre, apellido, nro_documento, telefono, id_tipo_documento, celular, mail, fecha_nacimiento, domicilio)
                         VALUES        (@nombre,@apellido,@nro_documento,@telefono,@id_tipo_documento,@celular,@mail,@fecha_nacimiento,@domicilio) SELECT CAST(scope_identity() AS int)";
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=NICO;Initial Catalog=ASA;Integrated Security=True";
            //PONER LA STRINGCONNECTION CORRECTA!!!

            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@nombre", persona.nombre.ToString());
                cmd.Parameters.AddWithValue("@apellido", persona.apellido.ToString());
                cmd.Parameters.AddWithValue("@nro_documento", (int)persona.numDoc);
                cmd.Parameters.AddWithValue("@domicilio", persona.domicilio.ToString());
                cmd.Parameters.AddWithValue("@telefono", persona.telefono.ToString());
                int idDoc=0;
                switch(persona.tipoDoc.descripcion)
                {
                    case "DNI": 
                        idDoc = 1;
                        break;
                    case "LE": 
                        idDoc = 2;
                        break;
                    case "LC": 
                        idDoc = 3;
                        break;
                }
                cmd.Parameters.AddWithValue("@id_tipo_documento", idDoc);
                cmd.Parameters.AddWithValue("@celular", persona.celular.ToString());
                cmd.Parameters.AddWithValue("@mail", persona.mail.ToString());
                cmd.Parameters.AddWithValue("@fecha_nacimiento", (DateTime)persona.fechaNacimiento);
                resetearAutoIncrement(MaxID());
                i = (Int32)cmd.ExecuteScalar();
                cn.Close();
            }
            catch (SqlException ex)
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                throw new ApplicationException("Error al insertar la Persona");
            }

            return i;
        }

        public static void delete(Persona persona)
        {
            

            string sql = @"DELETE FROM personas
                           WHERE        (nro_documento = @nro_documento) AND (id_tipo_documento = @id_tipo_documento) ";

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=NICO;Initial Catalog=ASA;Integrated Security=True";

            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);

                int idDoc = 0;
                switch (persona.tipoDoc.descripcion.ToString())
                {
                    case "DNI": idDoc = 1;
                        break;
                    case "LE": idDoc = 2;
                        break;
                    case "LC": idDoc = 3;
                        break;
                }

                cmd.Parameters.AddWithValue("@nro_documento", persona.numDoc);
                cmd.Parameters.AddWithValue("@id_tipo_documento", idDoc);

                cmd.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Error al eliminar la Persona");
            }
            finally
            {
                cn.Close();
            }
           
        }

        public static int update(Persona personaVieja, Persona personaNueva)
        {
            int i = -1;
            string sql = @"UPDATE       personas
                         SET                nombre = @nombre_nuevo, apellido = @apellido_nuevo, nro_documento = @nro_documento_nuevo, telefono = @telefono_nuevo, id_tipo_documento = @id_tipo_documento_nuevo, celular = @celular_nuevo, 
                         mail = @mail_nuevo, fecha_nacimiento = @fecha_nacimiento_nuevo, domicilio = @domicilio_nuevo
                            OUTPUT INSERTED.id
                         WHERE         (nro_documento = @nro_documento)  AND (id_tipo_documento = @id_tipo_documento)  ";

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=NICO;Initial Catalog=ASA;Integrated Security=True";

            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);

                int idTDNuevo = 0;
                int idTDViejo = 0;

                switch (personaNueva.tipoDoc.descripcion.ToString())
                {
                    case "DNI": idTDNuevo = 1;
                        break;
                    case "LE": idTDNuevo = 2;
                        break;
                    case "LC": idTDNuevo = 3;
                        break;
                }

                switch (personaVieja.tipoDoc.descripcion.ToString())
                {
                    case "DNI": idTDViejo = 1;
                        break;
                    case "LE": idTDViejo = 2;
                        break;
                    case "LC": idTDViejo = 3;
                        break;
                }

                cmd.Parameters.AddWithValue("@nombre_nuevo", personaNueva.nombre.ToString());
                cmd.Parameters.AddWithValue("@apellido_nuevo", personaNueva.apellido.ToString());
                cmd.Parameters.AddWithValue("@nro_documento_nuevo", (int)personaNueva.numDoc);
                cmd.Parameters.AddWithValue("@domicilio_nuevo", personaNueva.domicilio.ToString());
                cmd.Parameters.AddWithValue("@telefono_nuevo", personaNueva.telefono.ToString());
                cmd.Parameters.AddWithValue("@id_tipo_documento_nuevo", idTDNuevo);
                cmd.Parameters.AddWithValue("@celular_nuevo", personaNueva.celular.ToString());
                cmd.Parameters.AddWithValue("@mail_nuevo", personaNueva.mail.ToString());
                cmd.Parameters.AddWithValue("@fecha_nacimiento_nuevo", (DateTime)personaNueva.fechaNacimiento);

                
                cmd.Parameters.AddWithValue("@nro_documento", (int)personaVieja.numDoc);               
                cmd.Parameters.AddWithValue("@id_tipo_documento", idTDViejo);
                

                i = (Int32)cmd.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Error al actualizar los datos personales");
            }
            finally
            {
                cn.Close();
            }
            return i;
        }

        public static Persona obtenerPorDatos(int numDoc, String descripcionTipoDoc)
        {
            Persona persona = new Persona();
            string sql = @"SELECT        id, nombre, apellido, nro_documento, domicilio, telefono, id_tipo_documento, celular, mail, fecha_nacimiento
                         FROM            personas
                         WHERE        (nro_documento = @nro_documento) AND (id_tipo_documento = @id_tipo_documento)";

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=NICO;Initial Catalog=ASA;Integrated Security=True";
            //PONER LA STRINGCONNECTION CORRECTA!!!
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@nro_documento", numDoc);

                int idDoc = 0;
                switch (descripcionTipoDoc)
                {
                    case "DNI": idDoc = 1;
                        break;
                    case "LE": idDoc = 2;
                        break;
                    case "LC": idDoc = 3;
                        break;
                }

                cmd.Parameters.AddWithValue("@id_tipo_documento", idDoc);
                
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                persona.nombre = dr["nombre"].ToString();
                persona.apellido = dr["apellido"].ToString();
                persona.numDoc = numDoc;
                persona.domicilio = dr["domicilio"].ToString();
                persona.telefono = dr["telefono"].ToString();
                persona.tipoDoc = TipoDocumentoDao.obtenerTipoDocumento(idDoc);

                dr.Close();
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Error al buscar la Persona");
            }
            finally
            {
                cn.Close();
            }
            return persona;
        }
        public static void resetearAutoIncrement(int ultimoVal)
        {
            string sql = "DBCC CHECKIDENT ('personas', RESEED, @val);";
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=NICO;Initial Catalog=ASA;Integrated Security=True";
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@val", ultimoVal);
                cmd.ExecuteNonQuery();

            }

            catch (SqlException ex)
            {
                throw new ApplicationException("Error al buscar los Personas");
            }
            finally
            {
                cn.Close();
            }


        }
        public static int MaxID()
        {
            int i = 0;

            string sql = @"SELECT        MAX(id) AS Expr1
                         FROM            personas";

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=NICO;Initial Catalog=ASA;Integrated Security=True";

            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                i = (int)cmd.ExecuteScalar();
            }

            catch (SqlException ex)
            {
                throw new ApplicationException("Error al buscar los Personas");
            }
            finally
            {
                cn.Close();
            }

            return i ;
        }
    }
}