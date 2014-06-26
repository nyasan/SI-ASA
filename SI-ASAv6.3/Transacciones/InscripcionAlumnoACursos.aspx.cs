using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SI_ASA_DAOv1;
using SI_ASA_ENTIDADESv1;

public partial class Transacciones_InscripcionAlumnoACursos : System.Web.UI.Page
{
    private List<int> idCursos=null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cargarLista(list_cursos);
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
    protected void cargarLista(ListBox lista)
    {
        lista.DataSource = CursoDao.ObtenerTodo();
        lista.DataValueField = "id_curso";
        lista.DataTextField = "nombre";
        lista.DataBind();
    }
    protected void cargarGrilla()
    {
        gv_busqueda.DataSource = AlumnoDao.obtenerTodo();
        gv_busqueda.DataKeyNames = new string[] { "legajo" };
        gv_busqueda.DataBind();
    }

    protected void list_cursos_SelectedIndexChanged(object sender, EventArgs e)
    {   
       
    }
    protected void btnRegistrar_Click(object sender, EventArgs e)
    {
        idCursos = new List<int>();
        foreach (ListItem i in list_cursos.Items)
        {
            if (i.Selected) idCursos.Add(int.Parse(i.Value));
        }
        if(gv_busqueda.SelectedRow != null || idCursos.Count > 0)
            AlumnosxCursoDao.registrarCursadoAlumno(AlumnoDao.obtenerPorLegajo(int.Parse(gv_busqueda.SelectedRow.Cells[1].Text)), idCursos, DateTime.Now);
    }
    protected void gv_busqueda_SelectedIndexChanged(object sender, EventArgs e)
    {
        //IMPLEMENTAR: si se selecciona uno, se deselecciona el anterior. No pueden haber mas de dos alumnos seleccionados.
        // Get the currently selected row using the SelectedRow property.
        GridViewRow row = gv_busqueda.SelectedRow;

        // Display the first name from the selected row.
        // In this example, the third column (index 2) contains
        // the first name.
        lblAlumno.Text = row.Cells[1].Text;
    }
    protected void btn_Buscar_Click(object sender, EventArgs e)
    {
        int numDoc = 0;
        int legajo = 0;
        if (txt_numeroDoc.Text != "")
            numDoc = int.Parse(txt_numeroDoc.Text);
        if (txt_legajo.Text != "")
            legajo = int.Parse(txt_legajo.Text);
        gv_busqueda.DataSource = AlumnoDao.buscarPorParametros(txt_nombre.Text, txt_apellido.Text, legajo, numDoc, ddl_tipoDoc.SelectedIndex);
        gv_busqueda.DataKeyNames = new string[] { "legajo" };
        gv_busqueda.DataBind();

        txt_apellido.Text = "";
        txt_legajo.Text = "";
        txt_nombre.Text = "";
        txt_numeroDoc.Text = "";
        ddl_tipoDoc.Focus();
    }
    protected void gv_busqueda_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
    }
}