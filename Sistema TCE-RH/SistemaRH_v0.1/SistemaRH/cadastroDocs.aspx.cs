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
using ceTe.DynamicPDF;
using ceTe.DynamicPDF.Merger;
using ceTe.DynamicPDF.PageElements;
using ceTe.DynamicPDF.IO;
using SistemaRH.Properties;
using LightInfocon.Data.LightBaseProvider;

namespace SistemaRH
{
    public partial class CadastroDocs : System.Web.UI.Page
    {
        string mensagem = "";
        string nomeArquivo = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session.Add("abaAtiva", "TabPanelPessoais");
            }
        }

        public string obterIdCadastrado()
        {
            IDataReader reader;
            IDbConnection con = new LightBaseConnection("user=lbw;password=lbw;udb=defudb;server=localhost");
            try
            {
                con.Open();
                IDbCommand comm = new LightBaseCommand("select last id from documento");
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

        public string montarFormatoGD(string id, string nome_Arquivo)
        {
            int novoId = Convert.ToInt16(id) + 1;
            return novoId + "_" + nome_Arquivo;
        }

        protected void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
        {
            string abaAtiva = TabContainer1.ActiveTab.ID;
            Session.Add("abaAtiva", abaAtiva);
        }

        protected void btnDisparaUCPessoais_Click(object sender, EventArgs e)
        {
            Session.Add("abaAtiva", "TabPanelPessoais");
        }

        protected void btnDisparaUCTitulacao_Click(object sender, EventArgs e)
        {
            Session.Add("abaAtiva", "TabPanelTitulacao");
        }

        protected void btnDisparaUCPortaria_Click(object sender, EventArgs e)
        {
            Session.Add("abaAtiva", "TabPanelPortaria");
        }

        protected void btnDisparaUCCI_Click(object sender, EventArgs e)
        {
            Session.Add("abaAtiva", "TabPanelCI");
        }

        protected void btnDisparaUCAviso_Click(object sender, EventArgs e)
        {
            Session.Add("abaAtiva", "TabPanelAviso");
        }

        protected void btnDisparaUCRequerimento_Click(object sender, EventArgs e)
        {
            Session.Add("abaAtiva", "TabPanelRequerimento");
        }

        protected void btnDisparaUCOutros_Click(object sender, EventArgs e)
        {
            Session.Add("abaAtiva", "TabPanelOutros");
        }

        protected void ImageButtonCadastrar_Click(object sender, ImageClickEventArgs e)
        {
            string ext = "";
            string id = "";
            string arquivo = "";
            int tamanho = 0;
            string nomeArquivoLBW = "";



            try
            {
                id = obterIdCadastrado();
            }
            catch (Exception ex)
            {
                LabelErro.Text = ex.Message;
            }
            /* Ver como pegar os arquivos pra salvar no banco
            arquivo = FileUploadArquivo.PostedFile.FileName;
            nomeArquivo = System.IO.Path.GetFileName(arquivo);
            tamanho = nomeArquivo.Length;
            nomeArquivoLBW = montarFormatoGD(id, nomeArquivo);
             * */

            //criar classe Documento
            Pessoa pessoa = new Pessoa();
            pessoa.arq_Arquivo = nomeArquivoLBW;

            pessoa.nome_Colaborador = TextBoxNome_Colaborador.Text;

           


            Adaptador adpt = new Adaptador();
            try
            {
                adpt.InserirPessoa(pessoa);
            }
            catch (Exception ex)
            {
                LabelErro.Text = ex.Message;
            }

            Versao versao = new Versao();

            //FileUploadArquivo.PostedFile.SaveAs(@"c:/temp/" + nomeArquivoLBW);

            versao.CaminhoDoArquivo = Settings.Default.CaminhoDoRepositorioPessoa;
            versao.Extensao = ext;
            versao.NomeDoArquivo = nomeArquivoLBW;

            versao.Id = Convert.ToInt32(obterIdCadastrado());

            Indexador indexador = new Indexador();
            indexador.Indexe(versao);

            mensagem = "Colaborador inserido com sucesso";
            Session.Add("mensagem", mensagem);
            Server.Transfer("listar.aspx");

        }
    }
}
