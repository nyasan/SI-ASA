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
    LinkedList<Curso> cursos;
    protected void Page_Load(object sender, EventArgs e)
    {
        cargarLista(list_cursos);
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
    protected void cargarLista(ListBox lista)
    {
        lista.DataSource = CursoDao.ObtenerTodo();
        lista.DataValueField = "id_curso";
        lista.DataTextField = "nombre";
        lista.DataBind();
        lista.Items.Insert(0, new ListItem("Elija una opción...", "0"));
    }
    protected void cargarGrilla()
    {
        gv_busqueda.DataSource = AlumnoDao.obtenerTodo();
        gv_busqueda.DataKeyNames = new string[] { "legajo" };
        gv_busqueda.DataBind();
    }


    protected void list_cursos_SelectedIndexChanged(object sender, EventArgs e)
    {
        Curso curso = CursoDao.getCurso(list_cursos.SelectedItem.Text);
        cursos.AddLast(curso);
    }
}