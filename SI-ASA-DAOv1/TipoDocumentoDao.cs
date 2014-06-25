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
        
        
        public static List<TipoDocumento> Obtener(string misql)
        {
            
            List<TipoDocumento> listTipoDocumento = new List<TipoDocumento>();
            SqlCommand cmd = new SqlCommand();           

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=ALEBELTRAMEN\\ALEJANDRA;Initial Catalog=ASA;Integrated Security=True";
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
                        descripcion = dr["descripcion"].ToString()
                    };

                    listTipoDocumento.Add(tipoDoc);
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
          return Obtener("SELECT * FROM tipo_documento t ");
        }
        
        public static List<TipoDocumento> obtenerTipoDocumento(string descripcion)
        {
            SqlCommand cmd = new SqlCommand(); 
            cmd.Parameters.Add(new SqlParameter("@descripcion", descripcion));
            return Obtener("SELECT * FROM tipo_documento t WHERE t.descrpcion = @descripcion");
        }
        public static TipoDocumento obtenerTipoDocumento(int id)
        {
            TipoDocumento tipoDoc = new TipoDocumento();
            SqlCommand cmd = new SqlCommand();

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=ALEBELTRAMEN\\ALEJANDRA;Initial Catalog=ASA;Integrated Security=True";
            try
            {
                cn.Open();
                cmd.CommandText = "SELECT * FROM tipo_documento WHERE id_tipo_documento = @idTipoDoc";
                cmd.Connection = cn;
                cmd.Parameters.Add(new SqlParameter("@idTipoDoc", id));

                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                tipoDoc.descripcion = dr["descripcion"].ToString();
                dr.Close();
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Error al buscar el Tipo de Documento" + ex.Message);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
            return tipoDoc;
        }
        public static TipoDocumento obtenerTipoDocumentoPorId(int id)
        {
            SqlCommand cmd = new SqlCommand(); 
            TipoDocumento tipoDoc = new TipoDocumento();
            SqlConnection cn = new SqlConnection();
            string sql = "SELECT * FROM tipo_documento t WHERE t.id_tipo_documento = @idTipoDocu";
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
