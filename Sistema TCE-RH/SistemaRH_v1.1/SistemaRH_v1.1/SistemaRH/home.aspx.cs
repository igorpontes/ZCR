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
    /// Classe da página inicial do sistema após o usuário se logar.
    /// </summary>
    public partial class Home : System.Web.UI.Page
    {
        string comando = "";
        string comandoMontado = "";
        string erro;
        string usuarioConectado;
        string senhaConectado;

        /// <summary>
        /// Método usado para verificar se o usuário está autenticado.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                usuarioConectado = (String)Session["usuario"];
                senhaConectado = (String)Session["senha"];
                GoldenAccess goldenAccess = new GoldenAccess("http://localhost:3271/GoldenAccess.soap");
                User usuario = new User(usuarioConectado, senhaConectado);
                try
                {
                    usuario = goldenAccess.Authenticate(usuarioConectado, senhaConectado);
                }
                catch (Exception ex)
                {
                    //LabelErro.Text = ex.Message;
                    //ImageAttention.Visible = true;
                }
                if (usuario.IsAuthenticated && !usuario.Disabled)
                {

                }
                else
                {
                    erro = "Usuário não autenticado";
                    Session.Add("erro", erro);
                    Server.Transfer("Login.aspx");
                    Session.Abandon();
                }
            }
        }

        /// <summary>
        /// Método usado pelo campo de pesquisa rápida. Transfere para a página que mostra o resultado de pesquisa.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        protected void ImageButtonPesquisar_Click(object sender, ImageClickEventArgs e)
        {
            string novoComando = montarComando(TextBoxBuscaPorPalavra.Text);
            comando = "textsearch in documento " + novoComando;

            Session.Add("pesquisa", comando);
            try
            {
                Server.Transfer("pesquisar.aspx");
            }
            catch (Exception ex)
            {
                //LabelErro.Text = ex.Message;
                //ImageAttention.Visible = true;
            }
        }

        /// <summary>
        /// Método usado para tratar o comando por cada parâmetro passado.
        /// </summary>
        /// <param name="pComando">string com vários parâmetros a ser usado na busca.</param>
        /// <returns>o comando tratado</returns>
        public string montarComando(string pComando)
        {
            string[] nomes = pComando.Split(' ');
            for (int i = 0; i < nomes.Length; i++)
            {
                if (nomes[i] != "")
                {
                    comandoMontado = comandoMontado + " " + nomes[i] + " OU";
                }

                if (i == nomes.Length - 1)
                {
                    comandoMontado = comandoMontado.Substring(0, comandoMontado.LastIndexOf("OU") - 1);
                }
            }
            return comandoMontado;
        }
    }
}
