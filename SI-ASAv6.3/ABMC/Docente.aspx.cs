using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SI_ASA_DAOv1;
using SI_ASA_ENTIDADESv1;

public partial class ABMC_Docente : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        cargarCombo(ddl_TipoDoc);
        txt_legajo.Text = DocenteDao.MaxLegajo().ToString();
    }

    protected void cargarCombo(DropDownList ddl)
    {
        ddl.DataSource = TipoDocumentoDao.cargarCombo();
        ddl.DataValueField = "id_tipo_documento";
        ddl.DataTextField = "descripcion";
        
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Elija una opción...", "0"));
    }

    protected void btn_Guardar_Click(object sender, EventArgs e)
    {
        Persona docentePersona = new Persona();
        docentePersona.nombre = txt_Nombre.Text;
        docentePersona.apellido = txt_Apellido.Text;
        docentePersona.numDoc = int.Parse(txt_NumDoc.Text);
        docentePersona.tipoDoc = TipoDocumentoDao.obtenerTipoDocumento(ddl_TipoDoc.SelectedIndex);
        docentePersona.domicilio = txt_Domicilio.Text;
        docentePersona.telefono = txt_Telefono.Text;
        docentePersona.celular = txt_Celular.Text;
        docentePersona.mail = txt_mail.Text;
        docentePersona.fechaNacimiento = DateTime.Parse(txt_FechaNacimiento.Text);

        Docente docente = new Docente();
        docente.docente = docentePersona;
        docente.legajo = int.Parse(txt_legajo.Text);
        docente.salario = int.Parse(txt_salario.Text);

        Horario horario = new Horario();
        horario.desde = txt_horaDesde.Text;
        horario.hasta = txt_horaHasta.Text;

        DocenteDao.add(docente, docentePersona, horario);
    }
    protected void btn_Eliminar_Click(object sender, EventArgs e)
    {
        DocenteDao.delete(DocenteDao.obtenerPorLegajo(int.Parse(txt_legajo.Text)));
    }
}