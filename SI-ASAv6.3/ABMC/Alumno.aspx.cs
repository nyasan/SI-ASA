﻿using System;
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
        

        

        

        //VALIDAR QUE SE INGRESAN UNICAMENTE NUMEROS
        txt_NumDoc.Attributes.Add("onkeypress", "javascript:return SoloNum(event); ");
        txt_NumDocMadre.Attributes.Add("onkeypress", "javascript:return SoloNum(event); ");
        txt_NumDocPadre.Attributes.Add("onkeypress", "javascript:return SoloNum(event); ");
        
        txt_FechaNacimiento.Attributes.Add("onkeypress", "fun_validarfecha_obj(this)");
        txt_FechaNacimientoMadre.Attributes.Add("onkeypress", "fun_validarfecha_obj(this)");
        txt_FechaNacimientoPadre.Attributes.Add("onkeypress", "fun_validarfecha_obj(this)");

        if (!IsPostBack)
        {
            cargarCombo(ddl_TipoDoc, 1);
            cargarCombo(ddl_TipoDocMadre, 1);
            cargarCombo(ddl_TipoDocPadre, 1);
            cargarCombo(ddl_NivelEstudio, 0);
            if (Session["origen"] == null)
            {
                Session["origen"] = "default";
            }
            string origen = Session["origen"].ToString();
            if (origen.Equals("consulta"))
            {
                Alumno alumnoConsulta = AlumnoDao.obtenerPorLegajo(int.Parse(Session["legajo"].ToString()));

                txt_legajo.Text = alumnoConsulta.legajo.ToString();
                txt_Nombre.Text = alumnoConsulta.alumno.nombre.ToString();
                txt_Apellido.Text = alumnoConsulta.alumno.apellido.ToString();
                ddl_TipoDoc.SelectedIndex = alumnoConsulta.alumno.tipoDoc.id;
                txt_NumDoc.Text = alumnoConsulta.alumno.numDoc.ToString();
                txt_Domicilio.Text = alumnoConsulta.alumno.domicilio.ToString();
                txt_Telefono.Text = alumnoConsulta.alumno.telefono.ToString();
                txt_Celular.Text = alumnoConsulta.alumno.celular.ToString();
                txt_mail.Text = alumnoConsulta.alumno.mail.ToString();
                txt_FechaNacimiento.Text = alumnoConsulta.alumno.fechaNacimiento.ToString();

                if (alumnoConsulta.conoceMusica == true)
                {
                    opt_Si.Checked = true;
                    opt_No.Checked = false;
                }
                else
                {
                    opt_No.Checked = true;
                    opt_Si.Checked = false;
                }
                ddl_NivelEstudio.SelectedIndex = alumnoConsulta.nivelEstudio.id;

                txt_NombreMadre.Text = alumnoConsulta.madre.nombre.ToString();
                txt_ApellidoMadre.Text = alumnoConsulta.madre.apellido.ToString();
                ddl_TipoDocMadre.SelectedIndex = alumnoConsulta.madre.tipoDoc.id;
                txt_NumDocMadre.Text = alumnoConsulta.madre.numDoc.ToString();
                txt_DomicilioMadre.Text = alumnoConsulta.madre.domicilio.ToString();
                txt_TelefonoMadre.Text = alumnoConsulta.madre.telefono.ToString();
                txt_CelularMadre.Text = alumnoConsulta.madre.celular.ToString();
                txt_MailMadre.Text = alumnoConsulta.madre.mail.ToString();
                txt_FechaNacimientoMadre.Text = alumnoConsulta.madre.fechaNacimiento.ToString();

                txt_NombrePadre.Text = alumnoConsulta.padre.nombre.ToString();
                txt_ApellidoPadre.Text = alumnoConsulta.padre.apellido.ToString();
                ddl_TipoDocPadre.SelectedIndex = alumnoConsulta.padre.tipoDoc.id;
                txt_NumDocPadre.Text = alumnoConsulta.padre.numDoc.ToString();
                txt_DomicilioPadre.Text = alumnoConsulta.padre.domicilio.ToString();
                txt_TelefonoPadre.Text = alumnoConsulta.padre.telefono.ToString();
                txt_CelularPadre.Text = alumnoConsulta.padre.celular.ToString();
                txt_MailPadre.Text = alumnoConsulta.padre.mail.ToString();
                txt_FechaNacimientoPadre.Text = alumnoConsulta.padre.fechaNacimiento.ToString();

                Session["origen"] = "desconocido";
            }
            else
            {
                
                txt_legajo.Text = AlumnoDao.MaxLegajo().ToString();
            
            }
            
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
        //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "clave", "alert('I am here');", true);// no hace falta esto
     
        TipoDocumento TipoDoc;
        if (ddl_TipoDoc.SelectedValue != "0")
        {
            TipoDoc = TipoDocumentoDao.obtenerTipoDocumento(ddl_TipoDoc.SelectedIndex);
        }
        else
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "clave", "alert('Faltó ingresar el Tipo de Documento del Alumno. Ingrese nuevamente');", true);
            return;
        }
 
        TipoDocumento TipoDocMadre;

        if (ddl_TipoDocMadre.SelectedValue != "0")
        {
            TipoDocMadre = TipoDocumentoDao.obtenerTipoDocumento(ddl_TipoDocMadre.SelectedIndex);
        }
        else
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "clave", "alert('Faltó ingresar el Tipo de Documento de la madre del alumno. Ingrese nuevamente');", true);
            return;
        }

        TipoDocumento TipoDocPadre;
        if (ddl_TipoDocPadre.SelectedValue != null)
        {          
            TipoDocPadre = TipoDocumentoDao.obtenerTipoDocumento(ddl_TipoDocPadre.SelectedIndex);
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
        alumnoPersona.tipoDoc = TipoDoc;

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
        madre.tipoDoc = TipoDocMadre;

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
        padre.tipoDoc = TipoDocPadre;

        Alumno alumno = new Alumno();
        Boolean flag = false;
        if(opt_Si.Checked)
            flag = true;
        else
            flag = false;
        alumno.conoceMusica = flag;

        //NivelEstudio nivelEstudio = new NivelEstudio(NivelEstudioDao.obtener(ddl_NivelEstudio.SelectedIndex + 1).descripcion);
        NivelEstudio nivelEstudio = new NivelEstudio();
        if (ddl_NivelEstudio.SelectedValue != "0")
            nivelEstudio.descripcion = ddl_NivelEstudio.SelectedItem.Text;
        
        alumno.nivelEstudio = nivelEstudio;

       
        alumno.alumno = alumnoPersona;
        alumno.madre = madre;
        alumno.padre = padre;


        Alumno alumnoViejo = AlumnoDao.obtenerPorLegajo(int.Parse(txt_legajo.Text));
        if (alumnoViejo != null)
        {
            
            Persona personaAlumnoViejo = alumnoViejo.alumno;
            Persona madreVieja = alumnoViejo.madre;
            Persona padreViejo = alumnoViejo.padre;
            
            AlumnoDao.update(alumnoViejo, alumno, personaAlumnoViejo, alumnoPersona, madreVieja, padreViejo, madre, padre);
        }
        else
            AlumnoDao.add(alumno, madre, alumnoPersona, padre);
    }

    
}