using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

namespace PEP
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        string msgErro = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            ImageButton lkPrincipal = (ImageButton)Master.FindControl("ImageButtonPrincipal");
            lkPrincipal.Visible = false;
            ImageButton lkCadastrar = (ImageButton)Master.FindControl("ImageButtonCadastrar");
            lkCadastrar.Visible = false;
            ImageButton lkListar = (ImageButton)Master.FindControl("ImageButtonListar");
            lkListar.Visible = false;
            ImageButton lkAlterarSenha = (ImageButton)Master.FindControl("ImageButtonAlterarSenha");
            lkAlterarSenha.Visible = false;
            ImageButton lkAjuda = (ImageButton)Master.FindControl("ImageButtonAjuda");
            lkAjuda.Visible = false;
            ImageButton lkSair = (ImageButton)Master.FindControl("ImageButtonSair");
            lkSair.Visible = false;
            TextBoxUsuario.Focus();

            string msg = (String)Session["mensagem"];
            if (msgErro != null)
            {
                LabelErro.Text = msg;
            }

            TextBoxUsuario.Focus();
        }

        /*
         * Método que faz o tratamento do acesso do usuário após o click no botão enviar.
         */ 
        protected void ImageButtonEnviar_Click(object sender, ImageClickEventArgs e)
        {
            Adaptador adpt = new Adaptador();
            int login = 0;
            try
            {
                login = adpt.EfetuaLogin(TextBoxUsuario.Text, TextBoxSenha.Text);//chamada do método que vai validar o dados do usuário.
            }
            catch (Exception ex)
            {
                LabelErro.Text = ex.Message;
            }
            if (login == 1)//acesso do usuário com permissão de administrador.
            {
                Session.Add("usuario", TextBoxUsuario.Text);
                Session.Add("senha", TextBoxSenha.Text);
                //Response.Write("<script>alert('Login de " + TextBoxUsuario.Text.ToUpper() + " efetuado com sucesso')</script>");
                Session.Add("login", 1);
                Server.Transfer("default.aspx");
            }
            else if (login == 2)//acesso de usuário com permissão apenas para consulta.
            {
                Session.Add("usuario", TextBoxUsuario.Text);
                Session.Add("senha", TextBoxSenha.Text);
                //Response.Write("<script>alert('Login de " + TextBoxUsuario.Text.ToUpper() + " efetuado com sucesso')</script>");
                //Transferir para a página de acesso a usuário com permissão restrita.
                Server.Transfer("default.aspx");
            }
            else if (login == 3)//usuário sem permissão de acessar o sistema.
            {
                LabelErro.Text = "Usuário não tem permissão para esta aplicação";
            }
            else if (login == 4)//usuário não existe.
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