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
using LightInfocon.GoldenAccess.General;

namespace GED_TCESE
{
    public partial class WebForm12 : System.Web.UI.Page
    {
        int cont = 0;
        bool validos = true;

        public bool validarDados()
        {
            if (TextBoxSenhaAtual.Text == "")
            {
                LabelErroSenhaAtual.Visible = true;
                validos = false;
            }
            
            if (TextBoxNovaSenha.Text == "")
            {
                LabelErroNovaSenha.Visible = true;
                validos = false;
            }
            
            if (TextBoxConfirmacaoSenha.Text == "")
            {
                LabelErroConfirmacaoSenha.Visible = true;
                validos = false;
            }
            return validos;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ImageButton lkListar = (ImageButton)Master.FindControl("ImageButtonListar");
            lkListar.Visible = false;
            ImageButton lkCadastrar = (ImageButton)Master.FindControl("ImageButtonCadastrar");
            lkCadastrar.Visible = false;
            ImageButton lkAlterarSenha = (ImageButton)Master.FindControl("ImageButtonAlterarSenha");
            lkAlterarSenha.Visible = false;
            ImageButton lkAjuda = (ImageButton)Master.FindControl("ImageButtonAjuda");
            lkAjuda.Visible = false;
            TextBoxSenhaAtual.Focus();
            GoldenAccess goldenAccess;
            User usuario = new User("", ""); ;
            string nome = (String)Session["usuario"];
            string senha = (String)Session["senha"];
            try
            {
                goldenAccess = new GoldenAccess("http://localhost:3271/GoldenAccess.soap");
                usuario = goldenAccess.Authenticate(nome, senha);
            }
            catch (Exception ex)
            {
                LabelErro.Text = ex.Message;
            }
            if (usuario.IsAuthenticated)
            {
                LabelUsuario.Text = usuario.Login;
            }
            else
            {
                Server.Transfer("Login.aspx");
                Session.Abandon();
            }
        }

        protected void LinkButtonVoltar_Click(object sender, EventArgs e)
        {
            Server.Transfer("default.aspx");
        }

        protected void ImageButtonConfirmar_Click(object sender, ImageClickEventArgs e)
        {
            if (validarDados())
            {
                GoldenAccess goldenAccess = new GoldenAccess("http://localhost:3271/GoldenAccess.soap");
                try
                {
                    User usuario = goldenAccess.Authenticate(LabelUsuario.Text, TextBoxSenhaAtual.Text);
                    if (usuario.IsAuthenticated && cont <= 3)
                    {
                        GoldenAccessService servicoGoldenAccess = new GoldenAccessService(usuario);
                        if (TextBoxNovaSenha.Text == TextBoxConfirmacaoSenha.Text)
                        {
                            cont++;
                            servicoGoldenAccess.ChangePassword(LabelUsuario.Text, TextBoxNovaSenha.Text);
                            LabelErro.Text = "Senha alterada com sucesso";
                        }
                        else
                        {
                            LabelErro.Text = "Senha e confirmação precissam ser iguais";
                        }
                    }
                    else
                    {
                        LabelErro.Text = "Tentativas de mudança de senha excedidas";
                    }
                }
                catch (Exception ex)
                {
                    LabelErro.Text = ex.Message;
                }

            }
            else
            {

            }
        }
    }
}
