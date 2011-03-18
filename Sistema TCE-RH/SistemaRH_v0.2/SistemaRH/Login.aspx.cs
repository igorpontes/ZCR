using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace SistemaRH
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TextBoxUsuario.Focus();
        }

        protected void ImageButtonEnviar_Click(object sender, ImageClickEventArgs e)
        {
            Adaptador adpt = new Adaptador();
            int login = 0;
            try
            {
                login = adpt.EfetuaLogin(TextBoxUsuario.Text, TextBoxSenha.Text);
            }
            catch (Exception ex)
            {
                LabelErro.Text = ex.Message;
            }
            if (login == 1)
            {
                Session.Add("usuario", TextBoxUsuario.Text);
                Session.Add("senha", TextBoxSenha.Text);
                //Response.Write("<script>alert('Login de " + TextBoxUsuario.Text.ToUpper() + " efetuado com sucesso')</script>");
                Session.Add("login", 1);
                Server.Transfer("Default.aspx");
            }
            else if (login == 2)
            {
                Session.Add("usuario", TextBoxUsuario.Text);
                Session.Add("senha", TextBoxSenha.Text);
                Response.Write("<script>alert('Login de " + TextBoxUsuario.Text.ToUpper() + " efetuado com sucesso')</script>");
                //Transferir para a página de acesso a usuário com permissão restrita.
                Server.Transfer("Default.aspx");
            }
            else if (login == 3)
            {
                LabelErro.Text = "Usuário não tem permissão para esta aplicação";
            }
            else if (login == 4)
            {
                LabelErro.Text = "Usuário inexistente";
            }
            else
            {
                LabelErro.Text = "Erro não identificado";
            }
        }
    }
}
