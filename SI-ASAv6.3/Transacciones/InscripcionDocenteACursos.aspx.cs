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
        lista.Items.Insert(0, new ListItem("Elija una opción...", "0"));
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

    protected void list_cursos_SelectedIndexChanged(object sender, EventArgs e)
    {
        idCursos.AddLast(list_cursos.SelectedIndex);
    }
    protected void btnRegistrar_Click(object sender, EventArgs e)
    {
        if (gv_busqueda.SelectedRow != null || idCursos.Count > 0)
            Docente_x_cursoDao.registrarCursadoDocente(DocenteDao.obtenerPorLegajo(int.Parse(gv_busqueda.SelectedRow.Cells[1].Text)), idCursos, DateTime.Now);
    }
}