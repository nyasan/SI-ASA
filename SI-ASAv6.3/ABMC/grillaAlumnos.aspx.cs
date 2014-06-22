using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SI_ASA_DAOv1;
using SI_ASA_ENTIDADESv1;

public partial class Informes_grillaAlumnos : System.Web.UI.Page
{
    List<Alumno> listAlumno = new List<Alumno>();
    protected void Page_Load(object sender, EventArgs e)
    {
        cargarCombo(ddl_tipoDoc);
        cargarGrilla();
    }

    protected void cargarCombo(DropDownList ddl)
    {
        ddl.DataSource = TipoDocumentoDao.cargarCombo();
        ddl.DataValueField = "id_tipo_documento";
        ddl.DataTextField = "descripcion";

        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Elija una opción...", "0"));
    }

    protected void cargarGrilla()
    {
        gv_busqueda.DataSource = AlumnoDao.obtenerTodo();
        gv_busqueda.DataKeyNames = new string[] { "legajo" };
        gv_busqueda.DataBind();
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        //cargar en la grilla, la colección de alumnos resultante de todas las coincidencias 
        //de todos los textbox ingresados
    }

    protected void gv_busqueda_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        Response.Redirect("Alumno.aspx");
    }
}