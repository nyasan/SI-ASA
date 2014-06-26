using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SI_ASA_DAOv1;
using SI_ASA_ENTIDADESv1;

public partial class Informes_grillaAlumnos : System.Web.UI.Page
{
    List<Alumno> listAlumno = new List<Alumno>();
    protected void Page_Load(object sender, EventArgs e)
    {

        listAlumno = AlumnoDao.obtenerTodo();
        grillaAlumnos.DataSource = listAlumno;
        grillaAlumnos.DataBind();
    }


    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        listAlumno = AlumnoDao.buscarPorParametros(busquedaPersona.Nombre,busquedaPersona.Apellido,int.Parse(busquedaPersona.Legajo),int.Parse(busquedaPersona.NumeroDoc),busquedaPersona.tipoDoc);
        grillaAlumnos.DataSource = listAlumno;
        grillaAlumnos.DataBind();
    }
}