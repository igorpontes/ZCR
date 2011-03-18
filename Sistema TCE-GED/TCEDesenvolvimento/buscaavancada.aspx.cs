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

namespace GED_TCESE
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        string comando = "";
        string novoComando = "";
        string comandoMontado = "";
        string erro = "";
        string pagina = "buscaAvancada";

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


        protected void Page_Load(object sender, EventArgs e)
        {
            ImageButton lkListar = (ImageButton)Master.FindControl("ImageButtonListar");
            lkListar.Visible = false;
            ImageButton lkCadastrar = (ImageButton)Master.FindControl("ImageButtonCadastrar");
            lkCadastrar.Visible = false;
            ImageButton lkAjuda = (ImageButton)Master.FindControl("ImageButtonAjuda");
            lkAjuda.Visible = false;
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
                TextBoxBuscaQualquerPalavra.Focus();
            }
            else
            {
                erro = "Usuário não autenticado";
                Session.Add("erro", erro);
                Server.Transfer("Login.aspx");
                Session.Abandon();
            }
        }

        protected void ImageButtonPesquisar_Click(object sender, ImageClickEventArgs e)
        {
            if (TextBoxBuscaComExpressao.Text != "" || TextBoxBuscaQualquerPalavra.Text != "")
            {
                int modulo = (Int32)Session["modulo"];

                if (modulo == 0)
                {
                    if (TextBoxBuscaComExpressao.Text != "" && TextBoxBuscaQualquerPalavra.Text != "")
                    {
                        LabelErro.Text = "Apenas um campo deve estar preenchido";
                    }

                    if (TextBoxBuscaQualquerPalavra.Text != "")
                    {
                        novoComando = montarComandoPorPalavra(TextBoxBuscaQualquerPalavra.Text);
                        if (novoComando == "")
                        {
                            LabelErro.Text = "Não foram encontrados resultados para essa pesquisa";
                        }
                        else
                        {
                            comando = "textsearch in tcejurisprudencia " + novoComando;
                        }
                    }

                    if (TextBoxBuscaComExpressao.Text != "")
                    {
                        novoComando = montarComandoPorExpressao(TextBoxBuscaComExpressao.Text);
                        if (novoComando == "")
                        {
                            LabelErro.Text = "Não foram encontrados resultados para essa pesquisa";
                        }
                        else
                        {
                            comando = "textsearch in tcejurisprudencia " + novoComando;
                            //comando = "textsearch in tcejurisprudencia " + "\"" + TextBoxBuscaComExpressao.Text + "\"";
                        }
                    }

                    Adaptador adpt = new Adaptador();
                    List<Processo> lista = adpt.PesquisaPorCampo(comando);
                    if (lista.Count == 0)
                    {
                        LabelErro.Text = "Não foram encontrados resultados para essa pesquisa";
                    }
                    else
                    {
                        Session.Add("lista", comando);
                        Session.Add("pagina", pagina);
                        try
                        {
                            Server.Transfer("pesquisar.aspx");
                        }
                        catch (Exception ex)
                        {
                            LabelErro.Text = ex.Message;
                        }
                    }
                }
                else if (modulo == 1)
                {
                    if (TextBoxBuscaComExpressao.Text != "" && TextBoxBuscaQualquerPalavra.Text != "")
                    {
                        LabelErro.Text = "Apenas um campo deve estar preenchido";
                    }

                    if (TextBoxBuscaQualquerPalavra.Text != "")
                    {
                        novoComando = montarComandoPorPalavra(TextBoxBuscaQualquerPalavra.Text);
                        if (novoComando == "")
                        {
                            LabelErro.Text = "Não foram encontrados resultados para essa pesquisa";
                        }
                        else
                        {
                            comando = "textsearch in folder245_indices5 " + novoComando;
                        }
                    }

                    if (TextBoxBuscaComExpressao.Text != "")
                    {
                        novoComando = montarComandoPorExpressao(TextBoxBuscaQualquerPalavra.Text);
                        if (novoComando == "")
                        {
                            LabelErro.Text = "Não foram encontrados resultados para essa pesquisa";
                        }
                        else
                        {
                            comando = "textsearch in folder245_indices5 " + novoComando;
                        }
                    }

                    AdaptadorDespesa adpt = new AdaptadorDespesa();
                    List<Despesa> lista = adpt.PesquisaPorCampo(comando);
                    if (lista.Count == 0)
                    {
                        LabelErro.Text = "Não foram encontrados resultados para essa pesquisa";
                    }
                    else
                    {
                        Session.Add("lista", comando);
                        Session.Add("pagina", pagina);
                        try
                        {
                            Server.Transfer("pesquisarprocessosdespesa.aspx");
                        }
                        catch (Exception ex)
                        {
                            LabelErro.Text = ex.Message;
                        }
                    }
                }
                else if (modulo == 2)
                {
                    if (TextBoxBuscaComExpressao.Text != "" && TextBoxBuscaQualquerPalavra.Text != "")
                    {
                        LabelErro.Text = "Apenas um campo deve estar preenchido";
                    }

                    if (TextBoxBuscaQualquerPalavra.Text != "")
                    {
                        novoComando = montarComandoPorPalavra(TextBoxBuscaQualquerPalavra.Text);
                        if (novoComando == "")
                        {
                            LabelErro.Text = "Não foram encontrados resultados para essa pesquisa";
                        }
                        else
                        {
                            comando = "textsearch in folder245_indices6 " + novoComando;
                        }
                    }

                    if (TextBoxBuscaComExpressao.Text != "")
                    {
                        novoComando = montarComandoPorExpressao(TextBoxBuscaComExpressao.Text);
                        if (novoComando == "")
                        {
                            LabelErro.Text = "Não foram encontrados resultados para essa pesquisa";
                        }
                        else
                        {
                            comando = "textsearch in folder245_indices6 \"" + novoComando;
                        }
                    }

                    AdaptadorProtocolo adpt = new AdaptadorProtocolo();
                    List<Protocolo> lista = adpt.PesquisaPorCampo(comando);
                    if (lista.Count == 0)
                    {
                        LabelErro.Text = "Não foram encontrados resultados para essa pesquisa";
                    }
                    else
                    {
                        Session.Add("lista", comando);
                        Session.Add("pagina", pagina);
                        try
                        {
                            Server.Transfer("pesquisarprotocolos.aspx");
                        }
                        catch (Exception ex)
                        {
                            LabelErro.Text = ex.Message;
                        }
                    }
                }
                else
                {
                    LabelErro.Text = "Erro não identificado";
                }
            }
            else
            {
                LabelErro.Text = "Um dos campos deve estar preenchido";
            }
        }

        protected void LinkButtonVoltar_Click(object sender, EventArgs e)
        {
            Server.Transfer("default.aspx");
        }
    }
}