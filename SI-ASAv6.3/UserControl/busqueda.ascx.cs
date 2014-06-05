using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class User_Control_busqueda : System.Web.UI.UserControl
{

    public event EventHandler Seleccion;
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    public string Nombre
    {
        get
        {
            return txt_nombre.Text;
        }
        set
        {
            txt_nombre.Text = value;
        }
    }
    public string Apellido
    {
        get
        {
            return txt_apellido.Text;
        }
        set
        {
            txt_apellido.Text = value;
        }
    }
    public string Legajo
    {
        get
        {
            return txt_legajo.Text;//HAY QUE HACER QUE EL TEXTBOX RECIBA NUMEROS, NO STRING!!
        }
        set
        {
            txt_legajo.Text = value;
        }
    }
    public String NumeroDoc
    {
        get
        {
            return txt_numeroDoc.Text;//HAY QUE HACER QUE EL TEXTBOX RECIBA NUMEROS, NO STRING!!
        }
        set
        {
            txt_numeroDoc.Text = value;
        }
    }
    public Object tipoDoc
    {
        //OJO CON OBJECT ACA!!
        //ESTE METODO ESTA PARA EL ORT...MEPA :P
        get
        {
            return ddl_tipoDoc.SelectedItem.Value;
        }
        set
        {
        }
    }
    protected void btn_Buscar_Click(object sender, EventArgs e)
    {
        if (Seleccion != null) //existe un suscriptor
            Seleccion(this, new EventArgs()); //Seleccion (this, new EventoArgumento() { Valor = txtNombre.text }); 
    }
}