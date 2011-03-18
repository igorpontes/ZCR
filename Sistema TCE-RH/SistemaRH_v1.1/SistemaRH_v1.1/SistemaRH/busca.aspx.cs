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
    /// <summary>
    /// Classe usada na busca de colaboradores e dos seus respectivos documentos.
    /// </summary>
    public partial class Busca : System.Web.UI.Page
    {
        string comando = "";
        string novoComando = "";
        string comandoMontado = "";
        string erro = "";
        bool parametroInvalido = false;

        /// <summary>
        /// Verifica se o usuario está autenticado
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
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

        /// <summary>
        /// Método que verifica se o formulário de busca foi feito de maneira correta e envia os parâmetros para próxima página.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
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
                    if (novoComando.Length == 0)
                    {
                        LabelErro.Text = "Parâmetro inválido";
                        ImageAttention.Visible = true;
                        parametroInvalido = true;
                    }
                    else
                    {
                        comando = "textsearch in documento " + novoComando;
                    }

                }

                if (TextBoxBuscaPorExpressao.Text != "")
                {
                    novoComando = montarComandoPorExpressao(TextBoxBuscaPorExpressao.Text);
                    if (novoComando.Length == 0)
                    {
                        LabelErro.Text = "Parâmetro inválido";
                        ImageAttention.Visible = true;
                        parametroInvalido = true;
                    }
                    else
                    {
                        comando = "textsearch in documento " + novoComando;
                    }
                }
                if (!parametroInvalido)
                {
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
            }
            else
            {
                LabelErro.Text = "Um dos campos deve estar preenchido";
                ImageAttention.Visible = true;
            }

        }

        /// <summary>
        /// Trata o comando por cada parâmetro passado.
        /// </summary>
        /// <param name="pComando">string com vários parâmetros a ser usado na busca.</param>
        /// <returns>o comando tratado</returns>
        public string montarComandoPorPalavra(string pComando)
        {
            string[] nomes = pComando.Split(' ');
            for (int i = 0; i < nomes.Length; i++)
            {
                if (nomes[i].Length > 2)
                {
                    comandoMontado = comandoMontado + " " + nomes[i] + " OU";
                }
                else
                {
                    comandoMontado += "";
                }

                if (i == nomes.Length - 1 && pComando.Length >= 2)
                {
                    try
                    {
                        comandoMontado = comandoMontado.Substring(0, comandoMontado.LastIndexOf("OU") - 1);
                    }
                    catch (Exception ex)
                    {
                        string erro = ex.Message;
                    }
                }
            }
            return comandoMontado;
        }

        /// <summary>
        /// Trata o comando montando uma expressão.
        /// </summary>
        /// <param name="pComando">string com vários parâmetros a ser usado na busca.</param>
        /// <returns>o comando tratado</returns>
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
                    try
                    {
                        comandoMontado = comandoMontado.Substring(0, comandoMontado.LastIndexOf("E") - 1);
                    }
                    catch (Exception ex)
                    {
                        string erro = ex.Message;
                    }
                }
            }
            return comandoMontado;
        }
    }
}
