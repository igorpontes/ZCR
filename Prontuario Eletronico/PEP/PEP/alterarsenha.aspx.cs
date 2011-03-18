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

namespace PEP
{
    public partial class WebForm8 : System.Web.UI.Page
    {
        int cont = 0;
        bool validos = true;
        string erro;

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
            ImageButton lkAjuda = (ImageButton)Master.FindControl("ImageButtonAjuda");
            lkAjuda.Visible = false;
            string usuarioConectado = (String)Session["usuario"];
            string senhaConectado = (String)Session["senha"];
            GoldenAccess servicoDeAutenticacao = new GoldenAccess("http://localhost:3271/GoldenAccess.soap");
            LightInfocon.GoldenAccess.General.User usuarioGoldenAccess = new LightInfocon.GoldenAccess.General.User(usuarioConectado, senhaConectado);
            try
            {
                usuarioGoldenAccess = servicoDeAutenticacao.Authenticate(usuarioConectado, senhaConectado);
            }
            catch (Exception ex)
            {
                LabelErro.Text = ex.Message;
            }
            if (usuarioGoldenAccess.IsAuthenticated)
            {
                TextBoxSenhaAtual.Focus();
            }
            else
            {
                Server.Transfer("login.aspx");
                erro = "Usuário não autenticado";
                Session.Add("erro", erro);
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
