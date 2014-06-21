using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using SI_ASA_ENTIDADESv1;

namespace SI_ASA_DAOv1
{
    public class TipoDocumentoDao
    {
        static string sql = "SELECT * FROM tipo_documento t ";
        static SqlCommand cmd = new SqlCommand();
        public static List<TipoDocumento> Obtener(string misql)
        {

            List<TipoDocumento> listTipoDocumento = new List<TipoDocumento>();
                       

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=ALEBELTRAMEN\\ALEJANDRA;Initial Catalog=ASA;Integrated Security=True";
            //PONER LA STRINGCONNECTION CORRECTA!!!
            try
            {
                cn.Open();
                cmd.CommandText = misql;

                cmd.Connection = cn;
                
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    TipoDocumento tipoDoc = new TipoDocumento()
                    {
                        //id_tipo_documento = (int)dr["id_tipo_documento"],
                        descripcion = dr["descripcion"].ToString()
                    };

                    listTipoDocumento.Add(tipoDoc); //lleno la coleccion en memoria
                }
                dr.Close();
                cn.Close();
            }
            catch (SqlException ex)
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                throw new ApplicationException("Error al buscar los Tipos de Documento" + ex.Message);
            }
            return listTipoDocumento;
        }

        public static List<TipoDocumento> obtenerTodo()
        {
          return Obtener(sql);
        }
        
        public static List<TipoDocumento> obtenerTipoDocumento(string descripcion)
        {
            cmd.Parameters.Add(new SqlParameter("@descripcion", descripcion));
            return Obtener(sql + "WHERE t.descrpcion = @descripcion");
        }
        public static List<TipoDocumento> obtenerTipoDocumento(int id)
        {
            cmd.Parameters.Add(new SqlParameter("@idTipoDoc", id));
            return Obtener(sql + "WHERE t.id_tipo_documento = @idTipoDoc");
        }
        public static TipoDocumento obtenerTipoDocumentoPorId(int id)
        {
            TipoDocumento tipoDoc = new TipoDocumento();
            SqlConnection cn = new SqlConnection();
            sql = "SELECT * FROM tipo_documento t WHERE t.id_tipo_documento = @idTipoDocu";
            cn.ConnectionString = "Data Source=ALEBELTRAMEN\\ALEJANDRA;Initial Catalog=ASA;Integrated Security=True";
            try
            {
                cn.Open();
                cmd.CommandText = sql;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@idTipoDocu", id);

                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                tipoDoc.descripcion = dr["descripcion"].ToString();
                dr.Close();
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Error al buscar los Tipos de Documento" + ex.Message);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
            return tipoDoc;
        }

        public static int add(TipoDocumento tdoc)
        {
            int id = -1;
            string sql = "INSERT INTO tipo_documento (descripcion) VALUES (@descripcion)";
            
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=ALEBELTRAMEN\\ALEJANDRA;Initial Catalog=ASA;Integrated Security=True";

            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("descripcion", tdoc.descripcion.ToString());
                
                id = (int)cmd.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Error al insertar el Tipo de Documento");
            }
            finally
            {
                cn.Close();
            }
            return id;
        }

        public static Object cargarCombo ()
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=ALEBELTRAMEN\\ALEJANDRA;Initial Catalog=ASA;Integrated Security=True";
            SqlCommand sql = new SqlCommand("SELECT * FROM tipo_documento", cn);
            SqlDataAdapter da = new SqlDataAdapter(sql.CommandText, cn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
    }
}
