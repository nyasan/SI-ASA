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
        
        if (!IsPostBack)
        {
            cargarCombo(ddl_TipoDoc);
            if (Session["origen"] == null)
            {
                Session["origen"] = "default";
            }
            string origen = Session["origen"].ToString();
            if (origen.Equals("consulta"))
            {
                Docente docenteConsulta = (Docente)Session["docente"];
                txt_legajo.Text = docenteConsulta.legajo.ToString();
                txt_Nombre.Text = docenteConsulta.docente.nombre.ToString();
                txt_Apellido.Text = docenteConsulta.docente.apellido.ToString();
                ddl_TipoDoc.SelectedIndex = docenteConsulta.docente.tipoDoc.id;
                txt_NumDoc.Text = docenteConsulta.docente.numDoc.ToString();
                txt_Domicilio.Text = docenteConsulta.docente.domicilio.ToString();
                txt_Telefono.Text = docenteConsulta.docente.telefono.ToString();
                txt_Celular.Text = docenteConsulta.docente.celular.ToString();
                txt_mail.Text = docenteConsulta.docente.mail.ToString();
                txt_FechaNacimiento.Text = docenteConsulta.docente.fechaNacimiento.ToString();
                txt_salario.Text = docenteConsulta.salario.ToString();
                txt_horaDesde.Text = docenteConsulta.horarioTrabajo.desde;
                txt_horaHasta.Text = docenteConsulta.horarioTrabajo.hasta;

                Session["origen"] = "default";
            }
            else
            {
                
                txt_legajo.Text = DocenteDao.MaxLegajo().ToString();
            }
        }
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
        if (ddl_TipoDoc.SelectedIndex != 0)
        {
            docentePersona.tipoDoc = TipoDocumentoDao.obtenerTipoDocumento(ddl_TipoDoc.SelectedIndex);
        }
        else
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "clave", "alert('Faltó ingresar el Tipo de Documento del Docente. Ingrese nuevamente');", true);
            return;
        }
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
        docente.horarioTrabajo = horario;


        Docente DocenteViejo = DocenteDao.obtenerPorLegajo(int.Parse(txt_legajo.Text));
        if (DocenteViejo != null)
        {

            Persona personaDocenteViejo = DocenteViejo.docente;

            DocenteDao.update(DocenteViejo, docente);
        }
        else
            DocenteDao.add(docente, docentePersona, horario);
    }
    protected void btn_Eliminar_Click(object sender, EventArgs e)
    {
        DocenteDao.delete(DocenteDao.obtenerPorLegajo(int.Parse(txt_legajo.Text)));
    }
}