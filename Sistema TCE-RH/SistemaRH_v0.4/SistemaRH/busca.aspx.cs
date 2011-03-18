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
using System.Collections.Generic;

namespace SistemaRH
{
    public partial class Busca : System.Web.UI.Page
    {
        string comando = "";
        string novoComando = "";
        string comandoMontado = "";
        string erro = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            string usuarioConectado = (String)Session["usuario"];
            string senhaConectado = (String)Session["senha"];
            GoldenAccess servicoDeAutenticacao = new GoldenAccess("http://localhost:3271/GoldenAccess.soap");
            User usuarioGoldenAccess = new User(usuarioConectado, senhaConectado);
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
                TextBoxBuscaPorPalavra.Focus();
            }
            else
            {
                erro = "Usuário não autenticado";
                Session.Add("erro", erro);
                Server.Transfer("Login.aspx");
                Session.Abandon();
            }
        }

        protected void ImageButtonEnviar_Click(object sender, ImageClickEventArgs e)
        {
            if (TextBoxBuscaPorExpressao.Text != "" || TextBoxBuscaPorPalavra.Text != "")
            {
                if (TextBoxBuscaPorExpressao.Text != "" && TextBoxBuscaPorPalavra.Text != "")
                {
                    LabelErro.Text = "Apenas um campo deve estar preenchido";
                    ImageAttention.Visible = true;
                }

                if (TextBoxBuscaPorPalavra.Text != "")
                {
                    novoComando = montarComandoPorPalavra(TextBoxBuscaPorPalavra.Text);
                    comando = "textsearch in documento " + novoComando;
                }

                if (TextBoxBuscaPorExpressao.Text != "")
                {
                    novoComando = montarComandoPorExpressao(TextBoxBuscaPorExpressao.Text);
                    comando = "textsearch in documento " + novoComando;
                }

                Session.Add("pesquisa", comando);
                try
                {
                    Server.Transfer("pesquisar.aspx");
                }
                catch (Exception ex)
                {
                    LabelErro.Text = ex.Message;
                    ImageAttention.Visible = true;
                }
            }
            else
            {
                LabelErro.Text = "Um dos campos deve estar preenchido";
                ImageAttention.Visible = true;
            }

        }

        public string montarComandoPorPalavra(string pComando)
        {
            string[] nomes = pComando.Split(' ');
            for (int i = 0; i < nomes.Length; i++)
            {
                if (nomes[i].Length > 2)
                {
                    comandoMontado = comandoMontado + " " + nomes[i] + " OU";
                }

                if (i == nomes.Length - 1 && pComando.Length >= 2)
                {
                    comandoMontado = comandoMontado.Substring(0, comandoMontado.LastIndexOf("OU") - 1);
                }
            }
            return comandoMontado;
        }

        public string montarComandoPorExpressao(string pComando)
        {
            string[] nomes = pComando.Split(' ');
            for (int i = 0; i < nomes.Length; i++)
            {
                if (nomes[i].Length > 2)
                {
                    comandoMontado = comandoMontado + " " + nomes[i] + " E";
                }

                if (i == nomes.Length - 1 && pComando.Length >= 2)
                {
                    comandoMontado = comandoMontado.Substring(0, comandoMontado.LastIndexOf("E") - 1);
                }
            }
            return comandoMontado;
        }
    }
}
