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
    public partial class WebForm2 : System.Web.UI.Page
    {
        string comando;
        string comandoMontado;
        string comandoMontadoPROX;
        int i = 0;
        string erro;
        string pagina = "default";
        string campos = "";

        public string montarComandoPROX(string pComandoPROX)
        {
            comandoMontadoPROX = "";
            int i = 0;
            string[] nomes = pComandoPROX.Split(' ');
            foreach (var item in nomes)
            {
                i++;
                if (nomes.Length > 1)
                {
                    comandoMontadoPROX = comandoMontadoPROX + " " + item + " prox";
                }
                else
                {
                    comandoMontadoPROX = pComandoPROX;
                }

                if (i == nomes.Length && pComandoPROX.Length > 1 && nomes.Length > 1)
                {
                    comandoMontadoPROX = comandoMontadoPROX.Substring(0, comandoMontadoPROX.LastIndexOf("prox") - 1);
                }
            }
            return comandoMontadoPROX;
        }

        public string montarComando(string pComando)
        {
            string[] nomes = pComando.Split(' ');
            for (int i = 0; i < nomes.Length; i++)
            {
                if (nomes[i].Length > 2)
                {
                    comandoMontado = comandoMontado + " " + "\"" + nomes[i] + "\"" + " E";
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
            //int codigo = (Int32)Session["login"];
            ImageButton lkPrincipal = (ImageButton)Master.FindControl("ImageButtonPrincipal");
            lkPrincipal.Visible = false;
            ImageButton lkCadastrar = (ImageButton)Master.FindControl("ImageButtonCadastrar");
            lkCadastrar.Visible = false;
            ImageButton lkListar = (ImageButton)Master.FindControl("ImageButtonListar");
            lkListar.Visible = true;
            ImageButton lkAlterarSenha = (ImageButton)Master.FindControl("ImageButtonAlterarSenha");
            lkAlterarSenha.Visible = true;
            ImageButton lkAjuda = (ImageButton)Master.FindControl("ImageButtonAjuda");
            lkAjuda.Visible = false;
            ImageButton lkSair = (ImageButton)Master.FindControl("ImageButtonSair");
            lkSair.Visible = true;

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
                TextBoxNumeroProcesso.Focus();
            }
            else
            {
                erro = "Usuário não autenticado";
                Session.Add("erro", erro);
                Server.Transfer("Login.aspx");
                Session.Abandon();
            }

            //if (RadioButtonListTipos.Items.FindByText("Jurisprudência").Selected)
            //{
            //    PanelJurisprudencia.Visible = true;
            //}
            //else if (RadioButtonListTipos.Items.FindByText("Protocolo").Selected)
            //{
            //    PanelProtocolo.Visible = true;
            //}
            //else if (RadioButtonListTipos.Items.FindByText("Processo de Despesa").Selected)
            //{
            //    PanelProcessoDespesa.Visible = true;
            //}
            //else
            //{
            //    PanelProcessoDespesa.Visible = false;
            //    PanelProtocolo.Visible = false;
            //    PanelJurisprudencia.Visible = false;
            //}
        }

        protected void ImageButtonBuscaAvancada_Click(object sender, ImageClickEventArgs e)
        {
            Session.Add("modulo", 0);
            Server.Transfer("buscaavancada.aspx");
        }

        protected void ImageButtonBuscaAvancadaDespesa_Click(object sender, ImageClickEventArgs e)
        {
            Session.Add("modulo", 1);
            Server.Transfer("buscaavancada.aspx");
        }

        protected void ImageButtonBuscaAvancadaProtocolo_Click(object sender, ImageClickEventArgs e)
        {
            Session.Add("modulo", 2);
            Server.Transfer("buscaavancada.aspx");
        }

        protected void ImageButtonPesquisar_Click(object sender, ImageClickEventArgs e)
        {
            if (TextBoxNumeroProcesso.Text == "" && TextBoxAnoProcesso.Text == "" && TextBoxOrigem.Text == "" && TextBoxAssunto.Text == ""
                && TextBoxDescricao.Text == "" && TextBoxInteressado.Text == "" && TextBoxQualquerCampo.Text == "")
            {
                LabelErro.Text = "Não existem campos para pesquisa";
            }
            else
            {
                comando = "textsearch in tcejurisprudencia ";

                if (TextBoxQualquerCampo.Text != "")
                {
                    comando += montarComando(TextBoxQualquerCampo.Text);

                    if (TextBoxNumeroProcesso.Text != "")
                    {
                        campos += TextBoxNumeroProcesso.Text + " ";
                        comando += " E " + TextBoxNumeroProcesso.Text + "[numero_Processo] ";
                    }

                    if (TextBoxAnoProcesso.Text != "")
                    {
                        campos += TextBoxAnoProcesso.Text + " ";
                        comando += " E " + TextBoxAnoProcesso.Text + "[ano_Processo] ";
                        i++;
                    }

                    if (TextBoxOrigem.Text != "")
                    {
                        campos += TextBoxOrigem.Text + " ";
                        comando += " E " + montarComandoPROX(TextBoxOrigem.Text) + "[origem] ";
                        i++;
                    }

                    if (TextBoxAssunto.Text != "")
                    {
                        campos += TextBoxAssunto.Text + " ";
                        comando += " E " + montarComandoPROX(TextBoxAssunto.Text) + " [assunto] ";
                        i++;
                    }

                    if (TextBoxDescricao.Text != "")
                    {
                        campos += TextBoxDescricao.Text + " ";
                        comando += " E " + montarComandoPROX(TextBoxDescricao.Text) + " [descricao] ";
                        i++;
                    }

                    if (TextBoxInteressado.Text != "")
                    {
                        campos += TextBoxInteressado.Text + " ";
                        comando += " E " + montarComandoPROX(TextBoxInteressado.Text) + "[nome]";
                        i++;
                    }
                    campos += TextBoxQualquerCampo.Text;
                }
                else
                {
                    if (TextBoxNumeroProcesso.Text != "")
                    {
                        campos += TextBoxNumeroProcesso.Text + " ";
                        comando += TextBoxNumeroProcesso.Text + "[numero_Processo] ";
                        i++;
                    }

                    if (TextBoxAnoProcesso.Text != "")
                    {
                        campos += TextBoxAnoProcesso.Text + " ";
                        if (i >= 1)
                        {
                            comando += " E " + TextBoxAnoProcesso.Text + "[ano_Processo] ";
                            i++;
                        }
                        else if(i == 0)
                        {
                            comando += TextBoxAnoProcesso.Text + "[ano_Processo] ";
                            i++;
                        }
                    }

                    if (TextBoxOrigem.Text != "")
                    {
                        campos += TextBoxOrigem.Text + " ";
                        if (i >= 1)
                        {
                            comando += " E " + montarComandoPROX(TextBoxOrigem.Text) + "[origem] ";
                            i++;
                        }
                        else if (i == 0)
                        {
                            comando += montarComandoPROX(TextBoxOrigem.Text) + "[origem] ";
                            i++;
                        }
                    }

                    if (TextBoxAssunto.Text != "")
                    {
                        campos += TextBoxAssunto.Text + " ";
                        if (i >= 1)
                        {
                            comando += " E " + montarComandoPROX(TextBoxAssunto.Text) + "[assunto] ";
                            i++;
                        }
                        else if (i == 0)
                        {
                            comando += montarComandoPROX(TextBoxAssunto.Text) + "[assunto] ";
                            i++;
                        }
                    }

                    if (TextBoxDescricao.Text != "")
                    {
                        campos += TextBoxDescricao.Text + " ";
                        if (i >= 1)
                        {
                            comando += " E " + montarComandoPROX(TextBoxDescricao.Text) + "[descricao] ";
                            i++;
                        }
                        else if (i == 0)
                        {
                            comando += montarComandoPROX(TextBoxDescricao.Text) + "[descricao] ";
                            i++;
                        }
                    }

                    if (TextBoxInteressado.Text != "")
                    {
                        campos += TextBoxInteressado.Text + " ";
                        if (i >= 1)
                        {
                            comando += " E " + montarComandoPROX(TextBoxInteressado.Text) + "[nome]";
                            i++;
                        }
                        else if(i == 0)
                        {
                            comando += montarComandoPROX(TextBoxInteressado.Text) + "[nome]";
                            i++;
                        }
                    }
                }
                Session.Add("ComandoPesquisa", comando);
                Session.Add("Campos", campos);
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
                Session.Add("modulo", 0);
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

        protected void ImageButtonPesquisarDespesa_Click(object sender, ImageClickEventArgs e)
        {
            if (TextBoxDespesaDocumento1.Text == ""
                && TextBoxDespesaDocumento2.Text == ""
                && TextBoxDespesaDocumento3.Text == ""
                && TextBoxDespesaDocumento4.Text == ""
                && TextBoxDespesaDocumento5.Text == ""
                && TextBoxDespesaDocumento6.Text == ""
                && TextBoxDespesaQualquerCampo.Text == "")
            {
                LabelErroDespesa.Text = "Não existem campos para pesquisa";
            }
            else
            {

                comando = "select id, arq_Arquivo, documento1, documento2, documento3, documento4, documento5, documento6 from folder245_indices5 where ";
                if (TextBoxDespesaQualquerCampo.Text != "")
                {
                    if (TextBoxDespesaQualquerCampo.Text == "*")
                    {
                        comando = "select id, arq_Arquivo, documento1, documento2, documento3, documento4, documento5, documento6 from folder245_indices5";
                    }
                    else
                    {
                        string novoComando = montarComando(TextBoxDespesaQualquerCampo.Text);
                        if (novoComando == null)
                        {
                            LabelErro.Text = "Não foram encontrados resultados para essa pesquisa";
                        }
                        else
                        {
                            comando = "textsearch in folder245_indices5 " + novoComando;
                        }
                    }
                }
                else
                {

                    if (TextBoxDespesaDocumento1.Text != "")
                    {
                        comando += "documento1 = " + "\"" + TextBoxDespesaDocumento1.Text + "\"";
                    }

                    if (TextBoxDespesaDocumento2.Text != "")
                    {
                        if (i > 1)
                        {
                            comando += " and documento2 = " + "\"" + TextBoxDespesaDocumento2.Text + "\"";
                            i++;
                        }
                        else
                        {
                            comando += " documento2 = " + "\"" + TextBoxDespesaDocumento2.Text + "\"";
                            i++;
                        }
                    }

                    if (TextBoxDespesaDocumento3.Text != "")
                    {
                        if (i > 1)
                        {
                            comando += " and documento3 = " + "\"" + TextBoxDespesaDocumento3.Text + "\"";
                            i++;
                        }
                        else
                        {
                            comando += " documento3 = " + "\"" + TextBoxDespesaDocumento3.Text + "\"";
                            i++;
                        }
                    }

                    if (TextBoxDespesaDocumento4.Text != "" && i > 1)
                    {
                        if (i > 1)
                        {
                            comando += " and documento4 = " + "\"" + TextBoxDespesaDocumento4.Text + "\"";
                            i++;
                        }
                        else
                        {
                            comando += " documento4 = " + "\"" + TextBoxDespesaDocumento4.Text + "\"";
                            i++;
                        }
                    }

                    if (TextBoxDespesaDocumento5.Text != "" && i > 1)
                    {
                        if (i > 1)
                        {
                            comando += " and documento5 = " + "\"" + TextBoxDespesaDocumento5.Text + "\"";
                            i++;
                        }
                        else
                        {
                            comando += " documento5 = " + "\"" + TextBoxDespesaDocumento5.Text + "\"";
                            i++;
                        }
                    }

                    if (TextBoxDespesaDocumento6.Text != "" && i > 1)
                    {
                        if (i > 1)
                        {
                            comando += " and documento6 = " + "\"" + TextBoxDespesaDocumento6.Text + "\"";
                            i++;
                        }
                        else
                        {
                            comando += " documento6 = " + "\"" + TextBoxDespesaDocumento6.Text + "\"";
                            i++;
                        }
                    }
                }
                AdaptadorDespesa adpt = new AdaptadorDespesa();
                List<Despesa> lista = adpt.PesquisaPorCampo(comando);
                if (lista.Count == 0)
                {
                    LabelErroDespesa.Text = "Não foram encontrados resultados para essa pesquisa";
                }
                else
                {
                    Session.Add("lista", comando);
                    Session.Add("modulo", 1);
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
        }

        protected void ImageButtonPesquisarProtocolo_Click(object sender, ImageClickEventArgs e)
        {
            if (TextBoxProtocoloDocumento1.Text == ""
                && TextBoxProtocoloDocumento2.Text == ""
                && TextBoxProtocoloDocumento3.Text == ""
                && TextBoxProtocoloDocumento4.Text == ""
                && TextBoxProtocoloDocumento5.Text == ""
                && TextBoxProtocoloDocumento6.Text == ""
                && TextBoxProtocoloQualquerCampo.Text == "")
            {
                LabelErroProtocolo.Text = "Não existem campos para pesquisa";
            }
            else
            {

                comando = "select id, arq_Arquivo, documento1, documento2, documento3, documento4, documento5, documento6 from folder245_indices6 where ";
                if (TextBoxProtocoloQualquerCampo.Text != "")
                {
                    if (TextBoxProtocoloQualquerCampo.Text == "*")
                    {
                        comando = "select id, arq_Arquivo, documento1, documento2, documento3, documento4, documento5, documento6 from folder245_indices6";
                    }
                    else
                    {
                        string novoComando = montarComando(TextBoxProtocoloQualquerCampo.Text);
                        if (novoComando == null)
                        {
                            LabelErro.Text = "Não foram encontrados resultados para essa pesquisa";
                        }
                        else
                        {
                            comando = "textsearch in folder245_indices6 " + novoComando;
                        }
                    }
                }
                else
                {

                    if (TextBoxProtocoloDocumento1.Text != "")
                    {
                        comando += "documento1 = " + "\"" + TextBoxProtocoloDocumento1.Text + "\"";
                    }

                    if (TextBoxProtocoloDocumento2.Text != "")
                    {
                        if (i > 1)
                        {
                            comando += " and documento2 = " + "\"" + TextBoxProtocoloDocumento2.Text + "\"";
                            i++;
                        }
                        else
                        {
                            comando += " documento2 = " + "\"" + TextBoxProtocoloDocumento2.Text + "\"";
                            i++;
                        }
                    }

                    if (TextBoxProtocoloDocumento3.Text != "")
                    {
                        if (i > 1)
                        {
                            comando += " and documento3 = " + "\"" + TextBoxProtocoloDocumento3.Text + "\"";
                            i++;
                        }
                        else
                        {
                            comando += " documento3 = " + "\"" + TextBoxProtocoloDocumento3.Text + "\"";
                            i++;
                        }
                    }

                    if (TextBoxProtocoloDocumento4.Text != "" && i > 1)
                    {
                        if (i > 1)
                        {
                            comando += " and documento4 = " + "\"" + TextBoxProtocoloDocumento4.Text + "\"";
                            i++;
                        }
                        else
                        {
                            comando += " documento4 = " + "\"" + TextBoxProtocoloDocumento4.Text + "\"";
                            i++;
                        }
                    }

                    if (TextBoxProtocoloDocumento5.Text != "" && i > 1)
                    {
                        if (i > 1)
                        {
                            comando += " and documento5 = " + "\"" + TextBoxProtocoloDocumento5.Text + "\"";
                            i++;
                        }
                        else
                        {
                            comando += " documento5 = " + "\"" + TextBoxProtocoloDocumento5.Text + "\"";
                            i++;
                        }
                    }

                    if (TextBoxProtocoloDocumento6.Text != "" && i > 1)
                    {
                        if (i > 1)
                        {
                            comando += " and documento6 = " + "\"" + TextBoxProtocoloDocumento6.Text + "\"";
                            i++;
                        }
                        else
                        {
                            comando += " documento6 = " + "\""  + TextBoxProtocoloDocumento6.Text + "\"";
                            i++;
                        }
                    }
                }
                AdaptadorProtocolo adpt = new AdaptadorProtocolo();
                List<Protocolo> lista = adpt.PesquisaPorCampo(comando);
                if (lista.Count == 0)
                {
                    LabelErroProtocolo.Text = "Não foram encontrados resultados para essa pesquisa";
                }
                else
                {
                    Session.Add("lista", comando);
                    Session.Add("modulo", 2);
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
        }
    }
}