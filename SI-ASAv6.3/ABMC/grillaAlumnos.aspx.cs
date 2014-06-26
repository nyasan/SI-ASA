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
       if(!IsPostBack)
         {
              cargarCombo(ddl_tipoDoc);
              cargarGrilla();
         }
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

    protected void gv_busqueda_SelectedIndexChanged(object sender, EventArgs e)
    {
        Alumno alumno = AlumnoDao.obtenerPorLegajo(int.Parse(gv_busqueda.SelectedRow.Cells[1].Text));
        Session ["legajo"] = alumno.legajo;
        Session["origen"] = "consulta";
        Response.Redirect("Alumno.aspx");
    }
    protected void btn_Buscar_Click(object sender, EventArgs e)
    {
        int numDoc = 0;
        int legajo = 0;
        int tipo_doc = 0;
         if (ddl_tipoDoc.SelectedItem.Value != "")
         {
             tipo_doc = int.Parse(ddl_tipoDoc.SelectedItem.Value);
         }
        if (txt_numeroDoc.Text != "")
             numDoc= int.Parse(txt_numeroDoc.Text);
        if (txt_legajo.Text != "")
            legajo = int.Parse(txt_legajo.Text);
        gv_busqueda.DataSource = AlumnoDao.buscarPorParametros(txt_nombre.Text, txt_apellido.Text, legajo, numDoc, tipo_doc);
        gv_busqueda.DataKeyNames = new string[] { "legajo" };
        gv_busqueda.DataBind();

        txt_apellido.Text = "";
        txt_legajo.Text = "";
        txt_nombre.Text = "";
        txt_numeroDoc.Text = "";
        ddl_tipoDoc.Focus();
    }
}