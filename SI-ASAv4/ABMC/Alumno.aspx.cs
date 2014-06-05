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
        if (!IsPostBack)
        {
        }
    }

    protected void btn_Eliminar_Click(object sender, EventArgs e)
    {

    }
    protected void btn_Guardar_Click(object sender, EventArgs e)
    {
        String tipoDoc = ddl_TipoDoc.SelectedItem.Text;
        TipoDocumento TipoDoc = new TipoDocumento();
        TipoDoc.descripcion = tipoDoc;

        String tipoDocMadre = ddl_TipoDocMadre.SelectedItem.Text;
        TipoDocumento TipoDocMadre = new TipoDocumento();
        TipoDocMadre.descripcion = tipoDocMadre;

        String tipoDocPadre = ddl_TipoDocPadre.SelectedItem.Text;
        TipoDocumento TipoDocPadre = new TipoDocumento();
        TipoDocPadre.descripcion = tipoDocMadre;

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
        //Persona personaAlumnoViejo = 


        //if (AlumnoDao.exists(int.Parse(txt_legajo.Text)))
        //    AlumnoDao.update(alumnoViejo, alumno,  
        //else
        //    AlumnoDao.add(alumno, madre, alumnoPersona, padre);

    }

    
}