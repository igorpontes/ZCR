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
using System.Collections.Generic;
using LightInfocon.GoldenAccess.General;

namespace PEP
{
    public partial class WebForm7 : System.Web.UI.Page
    {
        string comando; //Campo que receberá o comando lightbase para consulta.
        string comandoMontado; //Campo que receberá o comando lightbase já com tratamento para consulta.
        string novoComando; //Campo que receberá o novo comando lightbase após o tratamento e como será pesquisado no banco.
        string erro;
        string campo;

        /*
         * Método que monta a consulta lightbase utilizando o operador OU.
         */
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

        /*
         * Método que monta a consulta lightbase utilizando o operador E.
         */
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

        /*
         * Definição das funcionalidades que serão realizadas na carga da página.
         */
        protected void Page_Load(object sender, EventArgs e)
        {
            // Mostrando e ocultando botões do menu dinamicamente.
            ImageButton lkPrincipal = (ImageButton)Master.FindControl("ImageButtonPrincipal");
            lkPrincipal.Visible = false;
            ImageButton lkCadastrar = (ImageButton)Master.FindControl("ImageButtonCadastrar");
            lkCadastrar.Visible = true;
            ImageButton lkListar = (ImageButton)Master.FindControl("ImageButtonListar");
            lkListar.Visible = true;
            ImageButton lkAlterarSenha = (ImageButton)Master.FindControl("ImageButtonAlterarSenha");
            lkAlterarSenha.Visible = true;
            ImageButton lkAjuda = (ImageButton)Master.FindControl("ImageButtonAjuda");
            lkAjuda.Visible = true;
            ImageButton lkSair = (ImageButton)Master.FindControl("ImageButtonSair");
            lkSair.Visible = true;
            TextBoxBuscaPorPalavra.Focus();//Dexiando o foco no campo de busca por palavra 

            //Verificando se o usuário que está manipulando a página é o mesmo que acessou o sistema.
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
                TextBoxBuscaPorPalavra.Focus();
            }
            else
            {
                //Caso não seja o mesmo usuário ele será redirecionado para o login e toda sessão será limpa.
                erro = "Usuário não autenticado";
                Session.Add("erro", erro);
                Session.Abandon();
                Server.Transfer("login.aspx");                
            }
        }

        /*
         * Método define a ações após o click no botão enviar dados.
         */
        protected void ImageButtonEnviar_Click(object sender, ImageClickEventArgs e)
        {
            // Verificando se os campos de consulta estão vazios.
            if (TextBoxBuscaPorExpressao.Text != "" || TextBoxBuscaPorPalavra.Text != "")
            {
                //Informando que apenas um campo pode estar preenchido.
                if (TextBoxBuscaPorExpressao.Text != "" && TextBoxBuscaPorPalavra.Text != "")
                {
                    LabelErro.Text = "Apenas um campo deve estar preenchido";
                }

                //Verificação de preenchimento de campo e montagem do texto de consulta por palavra (OU).
                if (TextBoxBuscaPorPalavra.Text != "")
                {
                    novoComando = montarComandoPorPalavra(TextBoxBuscaPorPalavra.Text);
                    if (novoComando == "")
                    {
                        LabelErro.Text = "Não foram encontrados resultados para essa pesquisa";
                    }
                    else
                    {
                        campo = TextBoxBuscaPorPalavra.Text;
                        comando = "textsearch in prontuario " + novoComando;
                    }
                }

                //Verificação de preenchimento de campo e montagem do texto de consulta por expressão (E).
                if (TextBoxBuscaPorExpressao.Text != "")
                {
                    novoComando = montarComandoPorExpressao(TextBoxBuscaPorExpressao.Text);
                    if (novoComando == "")
                    {
                        LabelErro.Text = "Não foram encontrados resultados para essa pesquisa";
                    }
                    else
                    {
                        campo = TextBoxBuscaPorExpressao.Text;
                        comando = "textsearch in prontuario " + novoComando;
                        //comando = "textsearch in tcejurisprudencia " + "\"" + TextBoxBuscaComExpressao.Text + "\"";
                    }
                }

                //Chamada do método de pesquisa de prontuário, passando como parâmetro o comando a ser consultado.
                Adaptador adpt = new Adaptador();
                List<Prontuario> lista = adpt.PesquisaPorCampo(comando);
                
                //Caso a pesquisa não retorne nenhum registro.
                if (lista.Count == 0)
                {
                    LabelErro.Text = "Não foram encontrados resultados para essa pesquisa";
                }
                else
                {   
                    Session.Add("lista", comando); //O comando deverá ser colocado em sessão para poder ser resgatado na página de pesquisa.
                    Session.Add("campo", campo); //O comando deverá ser colocado em sessão para poder ser resgatado na página de pesquisa.
                    try
                    {
                        Server.Transfer("pesquisar.aspx"); //Redirecioando para a página de pesquisa.
                    }
                    catch (Exception ex)
                    {
                        LabelErro.Text = ex.Message;//Caso a página contenha erro será informado ao usuário.
                    }
                }
            }
        }
    }
}