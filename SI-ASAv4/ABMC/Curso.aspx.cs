using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SI_ASA_ENTIDADESv1;
using SI_ASA_DAOv1;

public partial class ABMC_Curso : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_Guardar_Click(object sender, EventArgs e)
    {
        Curso curso = new Curso();

        curso.nombre = this.txt_nombre.Text;
        curso.descripcion = this.txt_Descripcion.Text;
        curso.hora_desde = this.txt_Desde.Text;
        curso.hora_hasta = this.txt_Hasta.Text;

        CursoDao.Insertar(curso);
    }
    protected void btn_Eliminar_Click(object sender, EventArgs e)
    {
        Curso curso = new Curso();

        curso.nombre = this.txt_nombre.Text;
        curso.descripcion = this.txt_Descripcion.Text;
        curso.hora_desde = this.txt_Desde.Text;
        curso.hora_hasta = this.txt_Hasta.Text;

        CursoDao.delete(curso);
    }
}