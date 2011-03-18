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
    public partial class AlterarColaborador : System.Web.UI.Page
    {
        string mensagem = "";
        string erro;
        string diretorio = HttpContext.Current.Server.MapPath("~/arquivos/");
        string ext = ".pdf";
        string id;

        protected void Page_Init(object sender, EventArgs e)
        {
            if ((String)Session["id"] != null)
            {
                id = (String)Session["id"];
            }
            else
            {
                Server.Transfer("default.aspx");
                erro = "Identificador do campo não encontrado";
                Session.Add("erro", erro);
            }

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
                Session.Add("cadastro", "nao");
                MontaTela(id);

            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //MontaTela(id);
            }
        }

        public void MontaTela(string id)
        {
            IDataReader reader;
            IDbConnection minhaConexao = new LightBaseConnection("user=lbw;password=lbw;udb=defudb;server=localhost");
            try
            {
                minhaConexao.Open();
                IDbCommand meuComando = new LightBaseCommand("select id , matricula_Colaborador , foto, nome_Colaborador, cpf_Colaborador, arquivos from documento where id =" + id);
                meuComando.Connection = minhaConexao;
                reader = meuComando.ExecuteReader();

                while (reader.Read())
                {
                    TextBoxMatricula.Text = Convert.ToString(reader["matricula_Colaborador"]);
                    ImageFoto.ImageUrl = Convert.ToString(reader["foto"]);
                    TextNome_Colaborador.Text = Convert.ToString(reader["nome_Colaborador"]);
                    TextBoxCPF.Text = Convert.ToString(reader["cpf_Colaborador"]);
                    DataTable dt_Arquivos = (DataTable)reader["arquivos"];
                    List<Arquivo> list_arq = new List<Arquivo>();
                    for (int i = 0; i < dt_Arquivos.Rows.Count; i++)
                    {
                        Arquivo arquivo = new Arquivo();
                        arquivo.nome_Arquivo = dt_Arquivos.Rows[i]["nome_Arquivo"].ToString();
                        arquivo.conteudo_Arquivo = dt_Arquivos.Rows[i]["conteudo_Arquivo"].ToString();
                        arquivo.tipo_Arquivo = dt_Arquivos.Rows[i]["tipo_Arquivo"].ToString();
                        list_arq.Add(arquivo);
                    }

                    if (list_arq != null)
                    {
                        foreach (Arquivo item in list_arq)
                        {
                            if (item.tipo_Arquivo == "docsPessoais")
                            {
                                atualizaSessaoDeArquivos("docsPessoais", item);
                            }
                            else if (item.tipo_Arquivo == "titulacoes")
                            {
                                atualizaSessaoDeArquivos("titulacoes", item);
                            }
                            else if (item.tipo_Arquivo == "portarias")
                            {
                                atualizaSessaoDeArquivos("portarias", item);
                            }
                            else if (item.tipo_Arquivo == "cis")
                            {
                                atualizaSessaoDeArquivos("cis", item);
                            }
                            else if (item.tipo_Arquivo == "avisoFerias")
                            {
                                atualizaSessaoDeArquivos("avisoFerias", item);
                            }
                            else if (item.tipo_Arquivo == "requerimentos")
                            {
                                atualizaSessaoDeArquivos("requerimentos", item);
                            }
                            else if (item.tipo_Arquivo == "outros")
                            {
                                atualizaSessaoDeArquivos("outros", item);
                            }
                        }
                    }
                }
            }
            finally
            {
                minhaConexao.Close();
            }
        }

        protected void ImageButtonEnviar_Click(object sender, ImageClickEventArgs e)
        {


            Documento documento = new Documento();
            List<Arquivo> list_arquivo = new List<Arquivo>();

            documento.matricula_Colaborador = TextBoxMatricula.Text;
            documento.nome_Colaborador = TextNome_Colaborador.Text;
            documento.cpf_Colaborador = TextBoxCPF.Text;
            documento.foto = ImageFoto.ImageUrl;
            /*
             * Salvando arquivos postados no banco 
             * ( ver como faz pra colocar um identificador do tipo pra qnd fazer um update saber buscar esse tipo)
             */
            if (Session["postou_docsPessoais"].ToString().Equals((string)"sim"))
            {
                Arquivo arq_temp = new Arquivo();
                arq_temp = retornaArquivos("docsPessoais");
                if (arq_temp != null)
                {
                    list_arquivo.Add(arq_temp);
                }
            }
            if (Session["postou_titulacoes"].ToString().Equals((string)"sim"))
            {
                Arquivo arq_temp = new Arquivo();
                arq_temp = retornaArquivos("titulacoes");
                if (arq_temp != null)
                {
                    list_arquivo.Add(arq_temp);
                }
            }
            if (Session["postou_portarias"].ToString().Equals((string)"sim"))
            {
                Arquivo arq_temp = new Arquivo();
                arq_temp = retornaArquivos("portarias");
                if (arq_temp != null)
                {
                    list_arquivo.Add(arq_temp);
                }
            }
            if (Session["postou_cis"].ToString().Equals((string)"sim"))
            {
                Arquivo arq_temp = new Arquivo();
                arq_temp = retornaArquivos("cis");
                if (arq_temp != null)
                {
                    list_arquivo.Add(arq_temp);
                }
            }
            if (Session["postou_avisoFerias"].ToString().Equals((string)"sim"))
            {
                Arquivo arq_temp = new Arquivo();
                arq_temp = retornaArquivos("avisoFerias");
                if (arq_temp != null)
                {
                    list_arquivo.Add(arq_temp);
                }
            }
            if (Session["postou_requerimentos"].ToString().Equals((string)"sim"))
            {
                Arquivo arq_temp = new Arquivo();
                arq_temp = retornaArquivos("requerimentos");
                if (arq_temp != null)
                {
                    list_arquivo.Add(arq_temp);
                }
            }
            if (Session["postou_outros"].ToString().Equals((string)"sim"))
            {
                Arquivo arq_temp = new Arquivo();
                arq_temp = retornaArquivos("outros");
                if (arq_temp != null)
                {
                    list_arquivo.Add(arq_temp);
                }
            }
            //Adicionando a lista dos arquivos aos documentos pra depois adicionar no banco

            //list_arquivo = (List<Arquivo>)Session["arquivos"];
            if (list_arquivo == null)
            {
                list_arquivo = new List<Arquivo>();
            }
            documento.arquivos = list_arquivo;

            Adaptador adpt = new Adaptador();
            try
            {
                adpt.AtualizarDocumento(documento, id);
                Log log = new Log();
                log.data_log = DateTime.Now;
                log.tipo_acao_log = "Editar";
                log.usuario_log = (String)Session["usuario"];
                log.mensagem_acao_log = "O usuário " + log.usuario_log + " atualizou o colaborador de matrícula " + documento.matricula_Colaborador;
                adpt.InserirLog(log);
            }
            catch (Exception ex)
            {
                LabelErro.Text = ex.Message;
                ImageAttention.Visible = true;
            }

            /*foreach (Arquivo arq in list_arquivo)
            {
                versiona_indexa(arq.nome_Arquivo);
            }*/

            mensagem = "Registro modificado com sucesso";
            Session.Add("mensagem", mensagem);
            Server.Transfer("listar.aspx");
        }

        protected void atualizaSessaoDeArquivos(string opcao, Arquivo item)
        {
            List<Arquivo> list_temp = new List<Arquivo>();
            list_temp = (List<Arquivo>)Session["arquivos"];
            if (list_temp == null)
            {
                list_temp = new List<Arquivo>();
            }
            list_temp.Add(item);
            Session["arquivos"] = list_temp;
            Session.Add("postou_" + opcao, "sim");
        }

        protected Arquivo retornaArquivos(string opcao)
        {
            Arquivo arq = null;
            List<Arquivo> list_temp = new List<Arquivo>();
            list_temp = (List<Arquivo>)Session["arquivos"];
            if (list_temp == null)
            {
                arq = null;
            }
            else
            {
                foreach (Arquivo item in list_temp)
                {
                    if (item.tipo_Arquivo.Equals((String)opcao))
                    {
                        arq = item;
                    }
                }
            }
            return arq;
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

        protected void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
        {
            string abaAtiva = TabContainer1.ActiveTab.ID;
            Session.Add("abaAtiva", abaAtiva);
        }

        protected void ImageButtonCarregarImagem_Click(object sender, ImageClickEventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                String nameFile = FileUpload1.FileName;
                try
                {
                    String pathDir = HttpContext.Current.Server.MapPath("~/arquivos/");
                    FileUpload1.SaveAs(pathDir + nameFile);
                    ImageFoto.ImageUrl = "~/arquivos/" + nameFile;
                    ImageFoto.DataBind();
                }
                catch (Exception ex)
                {
                    LabelErro.Text = ex.Message;
                    ImageAttention.Visible = true;
                }
            }
            else
            {
                LabelErro.Text = "Selecione o arquivo";
                ImageAttention.Visible = true;
            }
        }
    }
}
