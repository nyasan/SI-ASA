using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SI_ASA_DAOv1;
using SI_ASA_ENTIDADESv1;

public partial class Informes_ListadoDocentes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lb_cursos.DataSource = CursoDao.ObtenerTodo();
            lb_cursos.DataValueField = "id_curso";
            lb_cursos.DataTextField = "nombre";
            lb_cursos.DataBind();
            lb_cursos.Items.Insert(0, new ListItem("Todos", "0"));
        }

    }

    protected void bt_filtrar_Click(object sender, EventArgs e)
    {
        string nombre = null;

        DateTime? inscDesde = null;
        DateTime? inscHasta = null;
        int? legDesde = null;
        int? legHasta = null;
        List<string> cursos = null;
        if (txt_inscDesde.Text != string.Empty)
            inscDesde = DateTime.Parse(txt_inscDesde.Text);
        if (txt_inscHasta.Text != string.Empty)
            inscHasta = DateTime.Parse(txt_inscHasta.Text);
        if (txt_legDesde.Text != string.Empty)
            legDesde = int.Parse(txt_legDesde.Text);
        if (txt_legHasta.Text != string.Empty)
            legHasta = int.Parse(txt_legHasta.Text);
        //if (lb_cursos.SelectedIndex == -1)
        if (lb_cursos.SelectedIndex != -1 && lb_cursos.SelectedIndex != 0)
        {
            cursos = new List<string>();
            foreach (ListItem i in lb_cursos.Items)
            {
                if (i.Selected) cursos.Add(i.Text);
            }
        }


        grillaDocentes.DataSource = DocenteQueryDao.Informe(inscDesde, inscHasta, legDesde, legHasta, cursos);
        grillaDocentes.DataBind();
    }
}