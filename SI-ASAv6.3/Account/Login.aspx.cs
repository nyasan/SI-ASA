using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using SI_ASA_ENTIDADESv1;
using SI_ASA_DAOv1;

public partial class Account_Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        RegisterHyperLink.NavigateUrl = "Register.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
    }

    //protected Usuario ValidarUsuarios (string nombreUsuario, string contrasenia)
    //{
        
    //    Usuario usuario = new Usuario().Convertir(new UsuarioDao().Mostrar(nombreUsuario));
    //    if (usuario == null)
    //        usuario.Estado = NO_EXISTE;
    //    else if (usuario.Contrasenia.CompareTo(contrasenia) != 0)
    //        usuario.Estado = CONTRANIA_INVALIDA;
    //    else if (!usuario.Habilitado)
    //        usuario.Estado = INHABILITADO;
    //    else
    //        usuario.Estado = CORRECTO;
    //    return usuario;
    //}
    //protected void  LoginButton_Click(object sender, EventArgs e)
    //{
    //    if (Page.IsValid)
    //        {
    //            Usuario usuario = ValidarUsuario(UserName.Text, Password.Text);
    //            if (usuario.Estado == Usuario.NO_EXISTE)
    //                UserName.Text = "Usuario incorrecto. Recuerde que el sistema reconoce entre mayúsculas y minúsculas.";
    //            else if (usuario.Estado == Usuario.CONTRANIA_INVALIDA)
    //                Password.Text = "La contraseña no es correcta.";
    //            else if (usuario.Estado == Usuario.INHABILITADO)
    //                UserName.Text = "El usuario ingresado se encuentra inhabilitado para iniciar sesión.";
    //            else
    //            {
    //                Session["usuario"] = usuario;
    //                FormsAuthentication.RedirectFromLoginPage(usuario.NombreUsuario, true);
    //            }
    //        }
    //}
    //protected void btn_usuarioAnonimo_Click(object sender, EventArgs e)
    //{
    //    Usuario usuario = ValidarUsuario("anonimo", "123456");
    //    Session["usuario"] = usuario;
    //    FormsAuthentication.RedirectFromLoginPage(usuario.NombreUsuario, true);
    //}
}
