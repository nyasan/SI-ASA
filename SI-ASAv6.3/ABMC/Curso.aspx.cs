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
        txt_IDCurso.Text = CursoDao.MaxId().ToString();
    }

    protected void btn_Guardar_Click(object sender, EventArgs e)
    {
        Curso curso = new Curso();
        Horario horario = new Horario();
        curso.nombre = txt_nombre.Text;
        curso.descripcion = txt_Descripcion.Text;
        horario.desde = txt_Desde.Text;
        horario.hasta = txt_Hasta.Text;

        CursoDao.Insertar(curso, horario);
    }
    protected void btn_Eliminar_Click(object sender, EventArgs e)
    {
        Curso curso = new Curso();
        Horario horario = new Horario();
        curso.nombre = txt_nombre.Text;
        curso.descripcion = txt_Descripcion.Text;
        horario.desde = txt_Desde.Text;
        horario.hasta = txt_Hasta.Text;
        curso.horario = HorarioDao.obtener(HorarioDao.obtenerID(txt_Desde.Text, txt_Hasta.Text));

        CursoDao.delete(curso);
    }
}