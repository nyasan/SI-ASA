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
        cargarCombo(ddl_tipoDoc);
        listDocente = DocenteDao.obtenerTodo();
        grillaDocente.DataSource = listDocente;
        grillaDocente.DataBind();
    }

    protected void cargarCombo(DropDownList ddl)
    {
        ddl.DataSource = TipoDocumentoDao.cargarCombo();
        ddl.DataValueField = "id_tipo_documento";
        ddl.DataTextField = "descripcion";

        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Elija una opción...", "0"));
    }

    protected void grillaDocente_SelectedIndexChanged(object sender, EventArgs e)
    {
        Docente docente = DocenteDao.obtenerPorLegajo(int.Parse(grillaDocente.SelectedRow.Cells[1].Text));
        Session["legajo"] = docente.legajo;
        Session["origen"] = "consulta";
        Response.Redirect("Docente.aspx");
    }
}