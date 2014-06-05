using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SI_ASA_DAOv1;
using SI_ASA_ENTIDADESv1;

public partial class grillaDocentes : System.Web.UI.Page
{
    List<Docente> listDocente = new List<Docente>();
    protected void Page_Load(object sender, EventArgs e)
    {

        listDocente = DocenteDao.obtenerTodo();
        grillaDocentes.DataSource = listDocente;
        grillaDocentes.DataBind();
    }
}