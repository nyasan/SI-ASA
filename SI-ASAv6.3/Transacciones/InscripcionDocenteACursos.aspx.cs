using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SI_ASA_DAOv1;

public partial class Transacciones_InscripcionDocenteACursos : System.Web.UI.Page
{
    private LinkedList<int> idCursos = new LinkedList<int>();
    protected void Page_Load(object sender, EventArgs e)
    {
        cargarLista(list_cursos);
        cargarCombo(ddl_tipoDoc);
        cargarGrilla();
    }

    protected void cargarLista(ListBox lista)
    {
        lista.DataSource = CursoDao.ObtenerTodo();
        lista.DataValueField = "id_curso";
        lista.DataTextField = "nombre";
        lista.DataBind();
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
        gv_busqueda.DataSource = DocenteDao.obtenerTodo();
        gv_busqueda.DataKeyNames = new string[] { "legajo" };
        gv_busqueda.DataBind();
    }

    protected void list_cursos_SelectedIndexChanged(object sender, EventArgs e)
    {
        idCursos.AddLast(list_cursos.SelectedIndex);
    }
    protected void btnRegistrar_Click(object sender, EventArgs e)
    {
        if (gv_busqueda.SelectedRow != null || idCursos.Count > 0)
            Docente_x_cursoDao.registrarCursadoDocente(DocenteDao.obtenerPorLegajo(int.Parse(gv_busqueda.SelectedRow.Cells[1].Text)), idCursos, DateTime.Now);
    }
    protected void btn_Buscar_Click(object sender, EventArgs e)
    {
        int numDoc = 0;
        int legajo = 0;
        if (txt_numeroDoc.Text != "")
            numDoc = int.Parse(txt_numeroDoc.Text);
        if (txt_legajo.Text != "")
            legajo = int.Parse(txt_legajo.Text);
        gv_busqueda.DataSource = DocenteDao.buscarPorParametros(txt_nombre.Text, txt_apellido.Text, legajo, numDoc, ddl_tipoDoc.SelectedIndex);
        gv_busqueda.DataKeyNames = new string[] { "legajo" };
        gv_busqueda.DataBind();

        txt_apellido.Text = "";
        txt_legajo.Text = "";
        txt_nombre.Text = "";
        txt_numeroDoc.Text = "";
        ddl_tipoDoc.Focus();
    }
    protected void gv_busqueda_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Se debe incorporar una funcionalidad de manera que se des-seleccione la selección anterior 
        //ya que se inscribe un UNICO docente a VARIOS cursos
    }
}