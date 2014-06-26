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
        Session["docente"] = docente;
        Session["origen"] = "consulta";
        Response.Redirect("Docente.aspx");
    }
    protected void btn_Buscar_Click(object sender, EventArgs e)
    {
        int numDoc = 0;
        int legajo = 0;
        if (txt_numeroDoc.Text != "")
            numDoc = int.Parse(txt_numeroDoc.Text);
        if (txt_legajo.Text != "")
            legajo = int.Parse(txt_legajo.Text);
        grillaDocente.DataSource = DocenteDao.buscarPorParametros(txt_nombre.Text, txt_apellido.Text, legajo, numDoc, ddl_tipoDoc.SelectedIndex);
        grillaDocente.DataKeyNames = new string[] { "legajo" };
        grillaDocente.DataBind();

        txt_apellido.Text = "";
        txt_legajo.Text = "";
        txt_nombre.Text = "";
        txt_numeroDoc.Text = "";
        ddl_tipoDoc.Focus();
    }
}