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
using System.Collections.Generic;

namespace SistemaRH
{
    public partial class CadastroDocs : System.Web.UI.Page
    {
        string mensagem = "";
        //string nomeArquivo = "";
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session.Add("abaAtiva", "TabPanelPessoais");
                Session.Add("postou_docsPessoais", "nao");
                Session.Add("postou_titulacoes", "nao");
                Session.Add("postou_portarias", "nao");
                Session.Add("postou_cis", "nao");
                Session.Add("postou_avisoFerias", "nao");
                Session.Add("postou_requerimentos", "nao");
                Session.Add("postou_outros", "nao");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

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
            //Session.Add("abaAtiva", "TabPanelPessoais");
        }

        protected void btnDisparaUCTitulacao_Click(object sender, EventArgs e)
        {
            //Session.Add("abaAtiva", "TabPanelTitulacao");
        }

        protected void btnDisparaUCPortaria_Click(object sender, EventArgs e)
        {
            //Session.Add("abaAtiva", "TabPanelPortaria");
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

        public void versiona_indexa(string nome_arquivo)
        {
            Versao versao = new Versao();
            versao.CaminhoDoArquivo = Settings.Default.CaminhoDoRepositorioDocumento;
            versao.Extensao = ".pdf";
            versao.NomeDoArquivo = nome_arquivo;
            versao.Id = Convert.ToInt32(obterIdCadastrado());

            Indexador indexador = new Indexador();
            indexador.Indexe(versao);
        }

        
        protected void TabContainer1_Init(object sender, EventArgs e)
        {
            string abaAtiva = TabContainer1.ActiveTab.ID;
            Session.Add("abaAtiva", abaAtiva);
            tab_default1.Visible = true;
            tab_default2.Visible = false;
            tab_default3.Visible = false;
            tab_default4.Visible = false;
            tab_default5.Visible = false;
            tab_default6.Visible = false;
            tab_default7.Visible = false;
        }

        protected void TabContainer1_Load(object sender, EventArgs e)
        {
            string abaAtiva = TabContainer1.ActiveTab.ID;
            Session.Add("abaAtiva", abaAtiva);
            if (Session["abaAtiva"].ToString().Equals((string)"TabPanelPessoais"))
            {
                tab_default1.Visible = true;
                tab_default2.Visible = false;
                tab_default3.Visible = false;
                tab_default4.Visible = false;
                tab_default5.Visible = false;
                tab_default6.Visible = false;
                tab_default7.Visible = false;
            }
            else if (Session["abaAtiva"].ToString().Equals((string)"TabPanelTitulacao"))
            {
                tab_default1.Visible = false;
                tab_default2.Visible = true;
                tab_default3.Visible = false;
                tab_default4.Visible = false;
                tab_default5.Visible = false;
                tab_default6.Visible = false;
                tab_default7.Visible = false;
            }
            else if (Session["abaAtiva"].ToString().Equals((string)"TabPanelPortaria"))
            {
                tab_default1.Visible = false;
                tab_default2.Visible = false;
                tab_default3.Visible = true;
                tab_default4.Visible = false;
                tab_default5.Visible = false;
                tab_default6.Visible = false;
                tab_default7.Visible = false;
            }
            else if (Session["abaAtiva"].ToString().Equals((string)"TabPanelCI"))
            {
                tab_default1.Visible = false;
                tab_default2.Visible = false;
                tab_default3.Visible = false;
                tab_default4.Visible = true;
                tab_default5.Visible = false;
                tab_default6.Visible = false;
                tab_default7.Visible = false;
            }
            else if (Session["abaAtiva"].ToString().Equals((string)"TabPanelAviso"))
            {
                tab_default1.Visible = false;
                tab_default2.Visible = false;
                tab_default3.Visible = false;
                tab_default4.Visible = false;
                tab_default5.Visible = true;
                tab_default6.Visible = false;
                tab_default7.Visible = false;
            }
            else if (Session["abaAtiva"].ToString().Equals((string)"TabPanelRequerimento"))
            {
                tab_default1.Visible = false;
                tab_default2.Visible = false;
                tab_default3.Visible = false;
                tab_default4.Visible = false;
                tab_default5.Visible = false;
                tab_default6.Visible = true;
                tab_default7.Visible = false;
            }
            else if (Session["abaAtiva"].ToString().Equals((string)"TabPanelOutros"))
            {
                tab_default1.Visible = false;
                tab_default2.Visible = false;
                tab_default3.Visible = false;
                tab_default4.Visible = false;
                tab_default5.Visible = false;
                tab_default6.Visible = false;
                tab_default7.Visible = true;
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            string id = "";

            try
            {
                id = obterIdCadastrado();
            }
            catch (Exception ex)
            {
                LabelErro.Text = ex.Message;
            }

            Documento documento = new Documento();
            List<Arquivo> list_arquivo = new List<Arquivo>();
            documento.matricula_Colaborador = TextBoxMatricula.Text;
            documento.nome_Colaborador = TextNome_Colaborador.Text;
            /*
             * Salvando arquivos postados no banco 
             * ( ver como faz pra colocar um identificador do tipo pra qnd fazer um update saber buscar esse tipo)
             */
            if (Session["postou_docsPessoais"].ToString().Equals((string)"sim"))
            {
                Arquivo arq_temp = new Arquivo();
                arq_temp.nome_Arquivo = montarFormatoGD(id, "docsPessoais.pdf");
                arq_temp.tipo_Arquivo = "pessoais";
                list_arquivo.Add(arq_temp);
            }
            if (Session["postou_titulacoes"].ToString().Equals((string)"sim"))
            {
                Arquivo arq_temp = new Arquivo();
                arq_temp.nome_Arquivo = montarFormatoGD(id, "titulacoes.pdf");
                arq_temp.tipo_Arquivo = "titulacoes";
                list_arquivo.Add(arq_temp);
            }
            if (Session["postou_portarias"].ToString().Equals((string)"sim"))
            {
                Arquivo arq_temp = new Arquivo();
                arq_temp.nome_Arquivo = montarFormatoGD(id, "portarias.pdf");
                arq_temp.tipo_Arquivo = "portarias";
                list_arquivo.Add(arq_temp);
            }
            if (Session["postou_cis"].ToString().Equals((string)"sim"))
            {
                Arquivo arq_temp = new Arquivo();
                arq_temp.nome_Arquivo = montarFormatoGD(id, "cis.pdf");
                arq_temp.tipo_Arquivo = "cis";
                list_arquivo.Add(arq_temp);
            }
            if (Session["postou_avisoFerias"].ToString().Equals((string)"sim"))
            {
                Arquivo arq_temp = new Arquivo();
                arq_temp.nome_Arquivo = montarFormatoGD(id, "avisoFerias.pdf");
                arq_temp.tipo_Arquivo = "avisoFerias";
                list_arquivo.Add(arq_temp);
            }
            if (Session["postou_requerimentos"].ToString().Equals((string)"sim"))
            {
                Arquivo arq_temp = new Arquivo();
                arq_temp.nome_Arquivo = montarFormatoGD(id, "requerimentos.pdf");
                arq_temp.tipo_Arquivo = "requerimentos";
                list_arquivo.Add(arq_temp);
            }
            if (Session["postou_outros"].ToString().Equals((string)"sim"))
            {
                Arquivo arq_temp = new Arquivo();
                arq_temp.nome_Arquivo = montarFormatoGD(id, "outros.pdf");
                arq_temp.tipo_Arquivo = "outros";
                list_arquivo.Add(arq_temp);
            }
            //Adicionando a lista dos arquivos aos documentos pra depois adicionar no banco
            documento.arquivos = list_arquivo;

            Adaptador adpt = new Adaptador();
            try
            {
                adpt.InserirDocumento(documento);
            }
            catch (Exception ex)
            {
                LabelErro.Text = ex.Message;
            }

            /*foreach (Arquivo arq in list_arquivo)
            {
                versiona_indexa(arq.nome_Arquivo);
            }*/

            mensagem = "Colaborador inserido com sucesso";
            Session.Add("mensagem", mensagem);
            Server.Transfer("listar.aspx");
        }

   
    }
}
