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
    public partial class Home : System.Web.UI.Page
    {
        string comando = "";
        string comandoMontado = "";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

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
                LabelErro.Text = ex.Message;
                ImageAttention.Visible = true;
            }
        }

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
