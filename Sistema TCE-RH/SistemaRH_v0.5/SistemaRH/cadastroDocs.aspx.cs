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
        string diretorio = HttpContext.Current.Server.MapPath("~/arquivos/");
        string ext = ".pdf";
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

                Session.Add("docsPessoais", null);
                Session.Add("titulacoes", null);
                Session.Add("portarias", null);
                Session.Add("cis", null);
                Session.Add("avisoFerias", null);
                Session.Add("requerimentos", null);
                Session.Add("outros", null);

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
                arq_temp = inserirArquivosNaLista(id, "docsPessoais");
                if (arq_temp != null)
                {
                    list_arquivo.Add(arq_temp);
                }
            }
            if (Session["postou_titulacoes"].ToString().Equals((string)"sim"))
            {
                Arquivo arq_temp = new Arquivo();
                arq_temp = inserirArquivosNaLista(id, "titulacoes");
                if (arq_temp != null)
                {
                    list_arquivo.Add(arq_temp);
                }
            }
            if (Session["postou_portarias"].ToString().Equals((string)"sim"))
            {
                Arquivo arq_temp = new Arquivo();
                arq_temp = inserirArquivosNaLista(id, "portarias");
                if (arq_temp != null)
                {
                    list_arquivo.Add(arq_temp);
                }
            }
            if (Session["postou_cis"].ToString().Equals((string)"sim"))
            {
                Arquivo arq_temp = new Arquivo();
                arq_temp = inserirArquivosNaLista(id, "cis");
                if (arq_temp != null)
                {
                    list_arquivo.Add(arq_temp);
                }
            }
            if (Session["postou_avisoFerias"].ToString().Equals((string)"sim"))
            {
                Arquivo arq_temp = new Arquivo();
                arq_temp = inserirArquivosNaLista(id, "avisoFerias");
                if (arq_temp != null)
                {
                    list_arquivo.Add(arq_temp);
                }
            }
            if (Session["postou_requerimentos"].ToString().Equals((string)"sim"))
            {
                Arquivo arq_temp = new Arquivo();
                arq_temp = inserirArquivosNaLista(id, "requerimentos");
                if (arq_temp != null)
                {
                    list_arquivo.Add(arq_temp);
                }
            }
            if (Session["postou_outros"].ToString().Equals((string)"sim"))
            {
                Arquivo arq_temp = new Arquivo();
                arq_temp = inserirArquivosNaLista(id, "outros");
                if (arq_temp != null)
                {
                    list_arquivo.Add(arq_temp);
                }
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

        protected Arquivo inserirArquivosNaLista(string id, string opcao)
        {
            List<Arquivo> list_temp = new List<Arquivo>();
            list_temp = (List<Arquivo>)Session[opcao];
            Arquivo arq_temp = new Arquivo();

            if (list_temp != null)
            {
                string nomeArquivo = montarFormatoGD(id, opcao + ext);
                int count = list_temp.Count;
                if (count == 1)
                {
                    System.IO.File.Move(diretorio + list_temp.ElementAt(0).nome_Arquivo, diretorio + nomeArquivo);
                }
                if (count >= 2)
                {
                    MergeDocument document = MergeDocument.Merge(diretorio + list_temp.ElementAt(0).nome_Arquivo, diretorio + list_temp.ElementAt(1).nome_Arquivo);
                    if (count > 2)
                    {
                        for (int i = 2; i < count; i++)
                        {
                            document.Append(diretorio + list_temp.ElementAt(i).nome_Arquivo);
                            
                        }
                    }
                    document.Draw(nomeArquivo);

                    //Deletando os arquivos temporarios usados no merge
                    for (int i = 0; i < count; i++)
                    {
                        System.IO.File.Delete(diretorio + list_temp.ElementAt(i).nome_Arquivo);
                    }
                    System.IO.File.Move(nomeArquivo, diretorio + nomeArquivo);
                }
                arq_temp.nome_Arquivo = nomeArquivo;
                arq_temp.tipo_Arquivo = opcao;
            }
            return arq_temp;
        }
    }
}
