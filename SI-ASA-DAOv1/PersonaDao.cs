﻿using System;
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

            string sql = "SELECT * FROM personas p JOIN tipo_documento t ON (p.id_tipo_documento = t.id_tipo_documento) ";
            
            //(if (tipoDoc.hasValue() || numDoc == -1)
            if (tipoDoc == null || numDoc == -1)
            {
                String tipoDocu = tipoDoc.ToString();

                sql += "WHERE t.descripcion = @tipoDocu AND p.nro_documento = @numDoc";

            }

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
                    Persona persona = new Persona()
                    {
                        //IdPersona = (int)dr["IdPersona"],
                        nombre = dr["nombre"].ToString(),
                        apellido = dr["apellido"].ToString(),
                        numDoc = (int)dr["nro_documento"],
                        tipoDoc = TipoDocumentoDao.obtenerTipoDocumento((int)dr["id_tipo_documento"]).ElementAt(c),
                        domicilio = dr["domicilio"].ToString(),
                        telefono = dr["telefono"].ToString(),
                        celular = dr["celular"].ToString(),
                        mail = dr["mail"].ToString(),
                        fechaNacimiento = (DateTime) dr["fecha_nacimiento"]
                    };//OJO ACÁ!! VER Q EL PASAJE DE PARAMETROS ESTÉ BIEN HECHO. POR EJ "tipoDoc"

                    listPersonas.Add(persona); //lleno la coleccion en memoria
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
            string sql = "SELECT * FROM personas p WHERE p.id = @id";

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
                    {
                        persona.nombre = dr["nombre"].ToString();
                        persona.apellido = dr["apellido"].ToString();
                        persona.numDoc = (int)dr["nro_documento"];
                        persona.tipoDoc = TipoDocumentoDao.obtenerTipoDocumento((int)dr["id_tipo_documento"]).ElementAt(c);
                        persona.domicilio = dr["domicilio"].ToString();
                        persona.telefono = dr["telefono"].ToString();
                        persona.celular = dr["celular"].ToString();
                        persona.mail = dr["mail"].ToString();
                        persona.fechaNacimiento = (DateTime)dr["fecha_nacimiento"];
                    };

                    c++;
                }
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

            String sql = "INSERT INTO personas (nombre, apellido, nro_documento, domicilio, telefono, ";
            sql += "id_tipo_documento, celular, mail, fecha_nacimiento) ";
            sql += "VALUES (@nombre, @apellido, @nro_documento, @domicilio, @telefono, ";
            sql += "@id_tipo_documento, @celular, @mail, @fecha_nacimiento)";
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=ALEBELTRAMEN\\ALEJANDRA;Initial Catalog=ASA;Integrated Security=True";
            //PONER LA STRINGCONNECTION CORRECTA!!!

            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("nombre", persona.nombre.ToString());
                cmd.Parameters.AddWithValue("apellido", persona.apellido.ToString());
                cmd.Parameters.AddWithValue("nro_documento", (int)persona.numDoc);
                cmd.Parameters.AddWithValue("domicilio", persona.domicilio.ToString());
                cmd.Parameters.AddWithValue("telefono", persona.telefono.ToString());
                int idDoc=0;
                switch(persona.tipoDoc.descripcion)
                {
                    case "DNI": idDoc = 1;
                        break;
                    case "LE": idDoc = 2;
                        break;
                    case "LC": idDoc = 3;
                        break;
                }
                cmd.Parameters.AddWithValue("id_tipo_documento", (int)idDoc);
                cmd.Parameters.AddWithValue("celular", persona.celular.ToString());
                cmd.Parameters.AddWithValue("mail", persona.mail.ToString());
                cmd.Parameters.AddWithValue("fecha_nacimiento", (DateTime)persona.fechaNacimiento);

                i = (int) cmd.ExecuteScalar();
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

        public static int delete(Persona persona)
        {
            int i = -1;

            string sql = "DELETE FROM personas p WHERE p.nro_documento=@nro_documento AND p.id_tipo_documento=@id_tipo_documento)";

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=ALEBELTRAMEN\\ALEJANDRA;Initial Catalog=ASA;Integrated Security=True";

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

                cmd.Parameters.AddWithValue("nro_documento", persona.numDoc);
                cmd.Parameters.AddWithValue("id_tipo_documento", idDoc);

                i = (int)cmd.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Error al eliminar la Persona");
            }
            finally
            {
                cn.Close();
            }
            return i;
        }

        public static int update(Persona personaVieja, Persona personaNueva)
        {
            int i = -1;
            string sql = "UPDATE personas p SET p.nombre=@nombre_nuevo, p.apellido=@apellido_nuevo, p.nro_documento=@nro_documento_nuevo, ";
            sql += "p.domicilio=@domicilio_nuevo, p.telefono=@telefono_nuevo, p.id_tipo_documento=id_tipo_documento_nuevo, ";
            sql += "p.celular=@celular_nuevo, p.mail=@mail_nuevo, p.fecha_nacimiento=@fecha_nacimiento_nuevo ";
            sql += "WHERE p.nombre=@nombre AND p.apellido=@apellido AND p.nro_documento=@nro_documento AND p.domicilio=@domicilio AND p.telefono=@telefono AND ";
            sql += "p.id_tipo_documento=@id_tipo_documento AND p.celular=@celular AND p.mail=@mail AND p.fecha_nacimiento=@fecha_nacimiento";

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=ALEBELTRAMEN\\ALEJANDRA;Initial Catalog=ASA;Integrated Security=True";

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

                cmd.Parameters.AddWithValue("nombre_nuevo", personaNueva.nombre.ToString());
                cmd.Parameters.AddWithValue("apellido_nuevo", personaNueva.apellido.ToString());
                cmd.Parameters.AddWithValue("nro_documento_nuevo", (int)personaNueva.numDoc);
                cmd.Parameters.AddWithValue("domicilio_nuevo", personaNueva.domicilio.ToString());
                cmd.Parameters.AddWithValue("telefono_nuevo", personaNueva.telefono.ToString());
                cmd.Parameters.AddWithValue("id_tipo_documento_nuevo", idTDNuevo);
                cmd.Parameters.AddWithValue("celular_nuevo", personaNueva.celular.ToString());
                cmd.Parameters.AddWithValue("mail_nuevo", personaNueva.mail.ToString());
                cmd.Parameters.AddWithValue("fecha_nacimiento_nuevo", (DateTime)personaNueva.fechaNacimiento);

                cmd.Parameters.AddWithValue("nombre", personaVieja.nombre.ToString());
                cmd.Parameters.AddWithValue("apellido", personaVieja.apellido.ToString());
                cmd.Parameters.AddWithValue("nro_documento", (int)personaVieja.numDoc);
                cmd.Parameters.AddWithValue("domicilio", personaVieja.domicilio.ToString());
                cmd.Parameters.AddWithValue("telefono", personaVieja.telefono.ToString());
                cmd.Parameters.AddWithValue("id_tipo_documento", idTDViejo);
                cmd.Parameters.AddWithValue("celular", personaVieja.celular.ToString());
                cmd.Parameters.AddWithValue("mail", personaVieja.mail.ToString());
                cmd.Parameters.AddWithValue("fecha_nacimiento", (DateTime)personaVieja.fechaNacimiento);

                i = (int)cmd.ExecuteScalar();
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
            string sql = "SELECT * FROM personas p WHERE p.nro_documento = @nro_documento AND p.id_tipo_documento = @id_tipo_documento";

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=ALEBELTRAMEN\\ALEJANDRA;Initial Catalog=ASA;Integrated Security=True";
            //PONER LA STRINGCONNECTION CORRECTA!!!
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                SqlDataReader dr = cmd.ExecuteReader();

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

                persona.nombre = dr["nombre"].ToString();
                persona.apellido = dr["apellido"].ToString();
                persona.numDoc = numDoc;
                persona.domicilio = dr["domicilio"].ToString();
                persona.telefono = dr["telefono"].ToString();
                persona.tipoDoc = TipoDocumentoDao.obtenerTipoDocumento(idDoc).ElementAt(1);

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
    }
}