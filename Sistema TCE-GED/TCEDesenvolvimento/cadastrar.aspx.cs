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
using LightInfocon.Data.LightBaseProvider;
using LightInfocon.GoldenAccess.General;
using GED_TCESE.Properties;

namespace GED_TCESE
{
    public partial class WebForm10 : System.Web.UI.Page
    {
        string mensagem = "";
        string erro = "";

        public string obterIdCadastrado()
        {
            IDataReader reader;
            IDbConnection con = new LightBaseConnection("user=lbw;password=lbw;udb=defudb;server=localhost");
            try
            {
                con.Open();
                IDbCommand comm = new LightBaseCommand("select last id from tcese");
                comm.Connection = con;
                reader = comm.ExecuteReader();
                reader.Read();
                return reader["id"].ToString();
            }
            finally
            {
                con.Close();
            }
        }

        public string montarFormatoGD(string id, string extensao)
        {
            int novoId = Convert.ToInt16(id) + 1;
            return novoId + "_" + "0001" + "_" + "arq_Arquivo" + extensao;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ImageButton lkCadastrar = (ImageButton)Master.FindControl("ImageButtonCadastrar");
            lkCadastrar.Visible = false;
            ImageButton lkPrincipal = (ImageButton)Master.FindControl("ImageButtonPrincipal");
            lkPrincipal.Visible = true;
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
                TextBoxNumeroProcesso.Focus();
            }
            else
            {
                erro = "Usuário não autenticado";
                Session.Add("erro", erro);
                Server.Transfer("Login.aspx");
                Session.Abandon();
            }

            if (TextBoxQtdPessoas.Text == "")
            {
                TextBoxQtdPessoas.Text = "0";
            }
            LabelPessoa1.Visible = false;
            LabelPessoa2.Visible = false;
            LabelPessoa3.Visible = false;
            LabelPessoa4.Visible = false;
            TextBoxPessoa1.Visible = false;
            TextBoxPessoa2.Visible = false;
            TextBoxPessoa3.Visible = false;
            TextBoxPessoa4.Visible = false;
        }

        protected void ButtonQtdPessoasInserir_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt16(TextBoxQtdPessoas.Text) == 4)
            {
                LabelPessoa1.Visible = true;
                LabelPessoa2.Visible = true;
                LabelPessoa3.Visible = true;
                LabelPessoa4.Visible = true;
                TextBoxPessoa1.Visible = true;
                TextBoxPessoa2.Visible = true;
                TextBoxPessoa3.Visible = true;
                TextBoxPessoa4.Visible = true;
            }
            else if (Convert.ToInt16(TextBoxQtdPessoas.Text) == 3)
            {
                LabelPessoa1.Visible = true;
                LabelPessoa2.Visible = true;
                LabelPessoa3.Visible = true;
                TextBoxPessoa1.Visible = true;
                TextBoxPessoa2.Visible = true;
                TextBoxPessoa3.Visible = true;
            }
            else if (Convert.ToInt16(TextBoxQtdPessoas.Text) == 2)
            {
                LabelPessoa1.Visible = true;
                LabelPessoa2.Visible = true;
                TextBoxPessoa1.Visible = true;
                TextBoxPessoa2.Visible = true;
            }
            else if (Convert.ToInt16(TextBoxQtdPessoas.Text) == 1)
            {
                LabelPessoa1.Visible = true;
                TextBoxPessoa1.Visible = true;
            }
            else
            {
                LabelPessoa1.Visible = false;
                LabelPessoa2.Visible = false;
                LabelPessoa3.Visible = false;
                TextBoxPessoa1.Visible = false;
                TextBoxPessoa2.Visible = false;
                TextBoxPessoa3.Visible = false;
            }
        }

        protected void ImageButtonEnviar_Click(object sender, ImageClickEventArgs e)
        {
            string ext = "";
            string id = "";
            string arquivo = "";
            string nomeArquivo = "";
            int tamanho = 0;
            string nomeArquivoLBW = "";

            if (FileUpload.PostedFile.ContentLength == 0)
            {
                LabelErro.Text = "É necessário selecionar um arquivo";
            }
            else
            {
                try
                {
                    id = obterIdCadastrado();
                }
                catch (Exception ex)
                {
                    LabelErro.Text = ex.Message;
                }
                arquivo = FileUpload.PostedFile.FileName;
                nomeArquivo = System.IO.Path.GetFileName(arquivo);
                int pos = 0;
                pos = nomeArquivo.LastIndexOf('.');
                ext = nomeArquivo.Substring(pos).ToLower();
                tamanho = nomeArquivo.Length;
                nomeArquivoLBW = montarFormatoGD(id, ext);
                Processo processo = new Processo();
                processo.arq_Arquivo = nomeArquivoLBW;
                processo.numero_Processo = TextBoxNumeroProcesso.Text;
                processo.ano_Processo = TextBoxAnoProcesso.Text;
                processo.origem = TextBoxOrigem.Text;
                processo.assunto = TextBoxAssunto.Text;
                processo.descricao = TextBoxDescricao.Text;
                processo.pessoa1 = TextBoxPessoa1.Text;
                processo.pessoa2 = TextBoxPessoa2.Text;
                processo.pessoa3 = TextBoxPessoa3.Text;
                processo.pessoa4 = TextBoxPessoa4.Text;
                processo.qtdPessoas = Convert.ToInt16(TextBoxQtdPessoas.Text);
                Adaptador adpt = new Adaptador();
                try
                {
                    adpt.InsereProcesso(processo);
                }
                catch (Exception ex)
                {
                    LabelErro.Text = ex.Message;
                }

                Versao versao = new Versao();

                FileUpload.PostedFile.SaveAs(Settings.Default.CaminhoDoRepositorioJurisprudencia + nomeArquivoLBW);

                versao.CaminhoDoArquivo = Settings.Default.CaminhoDoRepositorioJurisprudencia;
                versao.Extensao = ext;
                versao.NomeDoArquivo = nomeArquivoLBW;

                versao.Id = Convert.ToInt32(obterIdCadastrado());

                Indexador indexador = new Indexador();
                indexador.Indexe(versao);

                mensagem = "Registro inserido com sucesso";
                Session.Add("mensagem", mensagem);
                Server.Transfer("Listar.aspx");
            }
        }

        protected void LinkButtonVoltar_Click(object sender, EventArgs e)
        {
            Server.Transfer("default.aspx");
        }
    }
}
