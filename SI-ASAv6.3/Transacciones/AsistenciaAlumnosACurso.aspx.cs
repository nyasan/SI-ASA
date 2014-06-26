using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SI_ASA_DAOv1;
using SI_ASA_ENTIDADESv1;

public partial class Transacciones_AsistenciaAlumnosACurso : System.Web.UI.Page
{
    protected List<String> alumnos;
    protected void Page_Load(object sender, EventArgs e)
    {
        alumnos = new List<String>();
        if (!IsPostBack)
        {
            
            cargarGrilla();
            cargarCombos(ddl_Curso);
            cargarLista();
        }
    }

    protected void cargarGrilla()
    {
        gv_grillaAlumnos.DataSource = AlumnoDao.obtenerTodo();
        gv_grillaAlumnos.DataKeyNames = new string[] { "legajo" };
        gv_grillaAlumnos.DataBind();
    }

    public void cargarLista()
    {
        listaAsistencia.DataBind();
    }

    protected void cargarCombos(DropDownList ddl)
    {
        ddl.DataSource = CursoDao.ObtenerTodo();
        ddl.DataValueField = "id_curso";
        ddl.DataTextField = "nombre";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Elija una opción...", "0"));
    }
    protected void btn_registrar_Click(object sender, EventArgs e)
    {
        if (listaAsistencia.Items.Count != null)
        {
            foreach (ListItem strCol in listaAsistencia.Items)
            {
                alumnos.Add(strCol.Text);
            }
            falta_alumno_x_cursoDao.registrarAsistencia(alumnos, ddl_Curso.SelectedIndex, DateTime.Now);
        }
    }
    protected void gv_grillaAlumnos_SelectedIndexChanged(object sender, EventArgs e)
    {
        Alumno alumno = new Alumno();
        alumno = AlumnoDao.obtenerPorLegajo(int.Parse(gv_grillaAlumnos.SelectedRow.Cells[1].Text));
        if (alumno != null)
        {
            listaAsistencia.Items.Add(""+alumno.legajo);
        }
    }

    protected void btnQuitar_Click(object sender, EventArgs e)
    {
        listaAsistencia.Items.Remove(listaAsistencia.SelectedItem);
    }
}