using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SI_ASA_DAOv1;
using SI_ASA_ENTIDADESv1;

public partial class ABMC_Alumno : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        cargarCombo(ddl_TipoDoc, 1);
        cargarCombo(ddl_TipoDocMadre, 1);
        cargarCombo(ddl_TipoDocPadre, 1);
        cargarCombo(ddl_NivelEstudio, 0);

        //VALIDAR QUE SE INGRESAN UNICAMENTE NUMEROS
        txt_NumDoc.Attributes.Add("onkeypress", "javascript:return SoloNum(event); ");
        txt_NumDocMadre.Attributes.Add("onkeypress", "javascript:return SoloNum(event); ");
        txt_NumDocPadre.Attributes.Add("onkeypress", "javascript:return SoloNum(event); ");
        
        txt_FechaNacimiento.Attributes.Add("onkeypress", "fun_validarfecha_obj(this)");
        txt_FechaNacimientoMadre.Attributes.Add("onkeypress", "fun_validarfecha_obj(this)");
        txt_FechaNacimientoPadre.Attributes.Add("onkeypress", "fun_validarfecha_obj(this)");

        if (!IsPostBack)
        {
        }
    }

    protected void cargarCombo(DropDownList ddl, int i)
    {
        if (i == 1)
        {
            ddl.DataSource = TipoDocumentoDao.cargarCombo();
            ddl.DataValueField = "id_tipo_documento";
            ddl.DataTextField = "descripcion";
        }
        else
        {
            ddl.DataSource = NivelEstudioDao.cargarCombo();
            ddl.DataValueField = "id";
            ddl.DataTextField = "descripcion";
        }
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Elija una opción...", "0"));
    }
    protected void btn_Eliminar_Click(object sender, EventArgs e)
    {
        AlumnoDao.delete(AlumnoDao.obtenerPorLegajo(int.Parse(txt_legajo.Text)));
    }
    protected void btn_Guardar_Click(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "clave", "alert('I am here');", true);
        String tipoDoc;
        TipoDocumento TipoDoc;
        if (ddl_TipoDoc.SelectedItem != null)
        {
            tipoDoc = ddl_TipoDoc.SelectedItem.Text;
            TipoDoc = TipoDocumentoDao.obtenerTipoDocumento(ddl_TipoDoc.SelectedIndex).ElementAt(1);
            TipoDoc.descripcion = tipoDoc;
        }
        else
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "clave", "alert('Faltó ingresar el Tipo de Documento del Alumno. Ingrese nuevamente');", true);
            return;
        }

        String tipoDocMadre;
        TipoDocumento TipoDocMadre;

        if (ddl_TipoDocMadre.SelectedItem != null)
        {
            tipoDocMadre = ddl_TipoDocMadre.SelectedItem.Text;
            TipoDocMadre = TipoDocumentoDao.obtenerTipoDocumento(ddl_TipoDocMadre.SelectedIndex).ElementAt(1);
            TipoDocMadre.descripcion = tipoDocMadre;
        }
        else
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "clave", "alert('Faltó ingresar el Tipo de Documento de la madre del alumno. Ingrese nuevamente');", true);
            return;
        }

        String tipoDocPadre;
        TipoDocumento TipoDocPadre;
        if (ddl_TipoDocPadre.SelectedItem != null)
        {
            tipoDocPadre = ddl_TipoDocPadre.SelectedItem.Text;
            TipoDocPadre = TipoDocumentoDao.obtenerTipoDocumento(ddl_TipoDocPadre.SelectedIndex).ElementAt(1);
            TipoDocPadre.descripcion = tipoDocMadre;
        }
        else
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "clave", "alert('Faltó ingresar el Tipo de Documento del padre del alumno. Ingrese nuevamente');", true);
            return;
        }


        Persona alumnoPersona = new Persona();
        alumnoPersona.nombre = txt_Nombre.Text;
        alumnoPersona.apellido = txt_Apellido.Text;
        alumnoPersona.numDoc = int.Parse(txt_NumDoc.Text);
        alumnoPersona.domicilio = txt_Domicilio.Text;
        alumnoPersona.telefono = txt_Telefono.Text;
        alumnoPersona.tipoDoc = TipoDoc;
        alumnoPersona.celular = txt_Celular.Text;
        alumnoPersona.mail = txt_mail.Text;
        alumnoPersona.fechaNacimiento = DateTime.Parse(txt_FechaNacimiento.Text);

        Persona madre = new Persona();
        madre.nombre = txt_NombreMadre.Text;
        madre.apellido = txt_ApellidoMadre.Text;
        madre.numDoc = int.Parse(txt_NumDocMadre.Text);
        madre.domicilio = txt_DomicilioMadre.Text;
        madre.telefono = txt_TelefonoMadre.Text;
        madre.tipoDoc = TipoDocMadre;
        madre.celular = txt_CelularMadre.Text;
        madre.mail = txt_MailMadre.Text;
        madre.fechaNacimiento = DateTime.Parse(txt_FechaNacimientoMadre.Text);

        Persona padre = new Persona();
        padre.nombre = txt_NombrePadre.Text;
        padre.apellido = txt_ApellidoPadre.Text;
        padre.numDoc = int.Parse(txt_NumDocPadre.Text);
        padre.domicilio = txt_DomicilioPadre.Text;
        padre.telefono = txt_TelefonoPadre.Text;
        padre.tipoDoc = TipoDocPadre;
        padre.celular = txt_CelularPadre.Text;
        padre.mail = txt_mail.Text;
        padre.fechaNacimiento = DateTime.Parse(txt_FechaNacimientoPadre.Text);

        Alumno alumno = new Alumno();
        Boolean flag = false;
        if(opt_Si.Checked)
            flag = true;
        else
            flag = false;
        alumno.conoceMusica = flag;

        NivelEstudio nivelEstudio = new NivelEstudio(ddl_NivelEstudio.SelectedItem.Text);

        alumno.nivelEstudio = nivelEstudio;

        Alumno alumnoViejo = AlumnoDao.obtenerPorLegajo(int.Parse(txt_legajo.Text));
        Persona personaAlumnoViejo = PersonaDao.obtenerPorDatos(int.Parse(txt_NumDoc.Text),ddl_TipoDoc.SelectedItem.Text); 
        Persona madreVieja = PersonaDao.obtenerPorDatos(int.Parse(txt_NumDoc.Text), ddl_TipoDocMadre.SelectedItem.Text);
        Persona padreViejo = PersonaDao.obtenerPorDatos(int.Parse(txt_NumDoc.Text), ddl_TipoDocPadre.SelectedItem.Text);

        if (AlumnoDao.exists(int.Parse(txt_legajo.Text)))
            AlumnoDao.update(alumnoViejo, alumno, personaAlumnoViejo, alumnoPersona, madreVieja, padreViejo, madre, padre);
        else
            AlumnoDao.add(alumno, madre, alumnoPersona, padre);

    }

    
}