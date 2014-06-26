using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SI_ASA_DAOv1;

public partial class Informes_ListadoCursos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void bt_filtrar_Click(object sender, EventArgs e)
    {
        //string nombre = null;

        DateTime? horaDesde = null;
        DateTime? horaHasta = null;
        int? docente = null;
        int? alumno = null;

        if (txt_horarioDesde.Text != string.Empty)
            horaDesde = DateTime.Parse(txt_horarioDesde.Text);
        if (txt_horarioHasta.Text != string.Empty)
            horaHasta = DateTime.Parse(txt_horarioHasta.Text);

        if (txt_docente.Text != string.Empty)
            docente = int.Parse(txt_docente.Text);
        if (txt_alumno.Text != string.Empty)
            alumno = int.Parse(txt_alumno.Text);


        grillaCursos.DataSource = CursosQueryDao.Informe(horaDesde, horaHasta, docente, alumno);
        grillaCursos.DataBind();
    }
}