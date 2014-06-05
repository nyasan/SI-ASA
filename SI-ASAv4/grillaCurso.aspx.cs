using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SI_ASA_DAOv1;
using SI_ASA_ENTIDADESv1;

public partial class grillaCurso : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        List<Curso> listCurso = new List<Curso>();
        listCurso = CursoDao.ObtenerTodo();
        grillaCursos.DataSource = listCurso;
        grillaCursos.DataBind();
    }
    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        if (txtNombre.Text == "")
            CursoDao.ObtenerTodo();
        else
            CursoDao.buscarPorParametros(txtNombre.Text);
    }
}