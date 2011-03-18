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
    /// <summary>
    /// Classe responsável pelo Login no sistema.
    /// </summary>
    public partial class Login : System.Web.UI.Page
    {
        /// <summary>
        /// Método Load. Deixa o foco no campo do usuário.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            TextBoxUsuario.Focus();
        }

        /// <summary>
        /// Método usado para logar no sistema, e caso autenticado, transfere para a pagina inicial.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
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
                ImageAttention.Visible = true;
            }
            if (login == 1)
            {
                Session.Add("usuario", TextBoxUsuario.Text);
                Session.Add("senha", TextBoxSenha.Text);
                Session.Add("tipo_usuario", "RHADM");
                //Response.Write("<script>alert('Login de " + TextBoxUsuario.Text.ToUpper() + " efetuado com sucesso')</script>");
                Session.Add("login", 1);
                Server.Transfer("Home.aspx");
            }
            else if (login == 2)
            {
                Session.Add("usuario", TextBoxUsuario.Text);
                Session.Add("senha", TextBoxSenha.Text);
                Session.Add("tipo_usuario", "RHLIM");
                Response.Write("<script>alert('Login de " + TextBoxUsuario.Text.ToUpper() + " efetuado com sucesso')</script>");
                //Transferir para a página de acesso a usuário com permissão restrita.
                Server.Transfer("home.aspx");
            }
            else if (login == 3)
            {
                LabelErro.Text = "Usuário não tem permissão para esta aplicação";
                ImageAttention.Visible = true;
            }
            else if (login == 4)
            {
                LabelErro.Text = "Usuário inexistente";
                ImageAttention.Visible = true;
            }
            else
            {
                LabelErro.Text = "Erro não identificado";
                ImageAttention.Visible = true;
            }
        }
    }
}
