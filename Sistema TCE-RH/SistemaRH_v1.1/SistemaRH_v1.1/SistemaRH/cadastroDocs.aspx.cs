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
using LightInfocon.GoldenAccess.General;

namespace SistemaRH
{
    /// <summary>
    /// Classe responsável pelo cadastro dos Colaboradores e dos sesus respectivos documentos no banco de dados.
    /// </summary>
    public partial class CadastroDocs : System.Web.UI.Page
    {
        string mensagem = "";
        string erro;
        string usuarioConectado;
        string senhaConectado;
        String diretorio = HttpContext.Current.Server.MapPath("~/arquivos/");
        public static string matricula = "";

        /// <summary>
        /// Inicia com a inicialização de algumas sessões usadas mais a frente.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                
                Session.Add("abaAtiva", "TabPanelPessoais");
                Session.Add("postou_docsPessoais", "nao");
                Session.Add("postou_titulacoes", "nao");
                Session.Add("postou_portarias", "nao");
                Session.Add("postou_portariasComProcesso", "nao");
                Session.Add("postou_cis", "nao");
                Session.Add("postou_avisoFerias", "nao");
                Session.Add("postou_requerimentos", "nao");
                Session.Add("postou_outros", "nao");
                Session.Add("cadastro", "sim");
            }
        }

        /// <summary>
        /// Verifica se o usuário está autenticado.
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
                    LabelErro.Text = ex.Message;
                    ImageAttention.Visible = true;
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
        /// Método usado para obter o último ID cadastrado
        /// </summary>
        /// <returns>o id</returns>
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

        /// <summary>
        /// Montar o formato de nome dos documentos.
        /// </summary>
        /// <param name="id">A matrícula do colaborador.</param>
        /// <param name="nome_Arquivo">O nome do arquivo.</param>
        /// <returns>o nome já formatado</returns>
        public string montarFormatoGD(string id, string nome_Arquivo)
        {
            return id + "_" + nome_Arquivo;
        }

        /// <summary>
        /// Método usado para atualizar a aba ativa.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
        {
            string abaAtiva = TabContainer1.ActiveTab.ID;
            Session.Add("abaAtiva", abaAtiva);

        }

        #region Botões que atualizam a sessão com aba ativa

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

        protected void btnDisparaUCPortariasComProcesso_Click(object sender, EventArgs e)
        {
            Session.Add("abaAtiva", "TabPanelPortariaComProcesso");
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

        #endregion

        /// <summary>
        /// Versiona um arquivo para futura indexação com o GoldenIndex.
        /// </summary>
        /// <param name="nome_arquivo">nome do arquivo.</param>
        public void versiona_indexa(string nome_arquivo)
        {
            Versao versao = new Versao();
            versao.CaminhoDoArquivo = diretorio;
            versao.Extensao = ".pdf";
            versao.NomeDoArquivo = nome_arquivo;
            versao.Id = Convert.ToInt32(obterIdCadastrado());

            //Indexador indexador = new Indexador();
            //indexador.Indexe(versao);
        }


        /// <summary>
        /// Inicializa o TabContainer1 control com a primeira aba visível e o resto invisível.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
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
            tab_default8.Visible = false;
        }

        /// <summary>
        /// Carrega o TabContainer1 control dependendo da aba ativa.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
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
                tab_default8.Visible = false;
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
                tab_default8.Visible = false;
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
                tab_default8.Visible = false;
            }
            else if (Session["abaAtiva"].ToString().Equals((string)"TabPanelPortariaComProcesso"))
            {
                tab_default1.Visible = false;
                tab_default2.Visible = false;
                tab_default3.Visible = false;
                tab_default4.Visible = true;
                tab_default5.Visible = false;
                tab_default6.Visible = false;
                tab_default7.Visible = false;
                tab_default8.Visible = false;
            }
            else if (Session["abaAtiva"].ToString().Equals((string)"TabPanelCI"))
            {
                tab_default1.Visible = false;
                tab_default2.Visible = false;
                tab_default3.Visible = false;
                tab_default4.Visible = false;
                tab_default5.Visible = true;
                tab_default6.Visible = false;
                tab_default7.Visible = false;
                tab_default8.Visible = false;
            }
            else if (Session["abaAtiva"].ToString().Equals((string)"TabPanelAviso"))
            {
                tab_default1.Visible = false;
                tab_default2.Visible = false;
                tab_default3.Visible = false;
                tab_default4.Visible = false;
                tab_default5.Visible = false;
                tab_default6.Visible = true;
                tab_default7.Visible = false;
                tab_default8.Visible = false;
            }
            else if (Session["abaAtiva"].ToString().Equals((string)"TabPanelRequerimento"))
            {
                tab_default1.Visible = false;
                tab_default2.Visible = false;
                tab_default3.Visible = false;
                tab_default4.Visible = false;
                tab_default5.Visible = false;
                tab_default6.Visible = false;
                tab_default7.Visible = true;
                tab_default8.Visible = false;
            }
            else if (Session["abaAtiva"].ToString().Equals((string)"TabPanelOutros"))
            {
                tab_default1.Visible = false;
                tab_default2.Visible = false;
                tab_default3.Visible = false;
                tab_default4.Visible = false;
                tab_default5.Visible = false;
                tab_default6.Visible = false;
                tab_default7.Visible = false;
                tab_default8.Visible = true;
            }
        }

        /// <summary>
        /// Método usado para cadastrar o colaborador e seus respectivos documentos.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Documento documento = new Documento();
            List<Arquivo> list_arquivo = new List<Arquivo>();
            documento.matricula_Colaborador = matricula;
            documento.nome_Colaborador = TextNome_Colaborador.Text;
            documento.foto = ImageFoto.ImageUrl;
            documento.cpf_Colaborador = TextBoxCPF.Text;

            if ((TextBoxMatricula.Text.Equals("")) || (TextBoxCPF.Text.Equals("")) || (TextNome_Colaborador.Text.Equals("")))
            {
                LabelErro.Text = "Preencha todos os campos!";
                ImageAttention.Visible = true;
            }
            else
            {
                /*
                * Salvando arquivos postados no banco 
                */
                if (Session["postou_docsPessoais"].ToString().Equals((string)"sim"))
                {
                    Arquivo arq_temp = new Arquivo();
                    arq_temp.nome_Arquivo = montarFormatoGD(matricula, "docsPessoais.pdf");
                    arq_temp.tipo_Arquivo = "docsPessoais";
                    list_arquivo.Add(arq_temp);
                }
                if (Session["postou_titulacoes"].ToString().Equals((string)"sim"))
                {
                    Arquivo arq_temp = new Arquivo();
                    arq_temp.nome_Arquivo = montarFormatoGD(matricula, "titulacoes.pdf");
                    arq_temp.tipo_Arquivo = "titulacoes";
                    list_arquivo.Add(arq_temp);
                }
                if (Session["postou_portarias"].ToString().Equals((string)"sim"))
                {
                    Arquivo arq_temp = new Arquivo();
                    arq_temp.nome_Arquivo = montarFormatoGD(matricula, "portarias.pdf");
                    arq_temp.tipo_Arquivo = "portarias";
                    list_arquivo.Add(arq_temp);
                }
                if (Session["postou_portariasComProcesso"].ToString().Equals((string)"sim"))
                {
                    Arquivo arq_temp = new Arquivo();
                    arq_temp.nome_Arquivo = montarFormatoGD(matricula, "portariasComProcesso.pdf");
                    arq_temp.tipo_Arquivo = "portariasComProcesso";
                    list_arquivo.Add(arq_temp);
                }
                if (Session["postou_cis"].ToString().Equals((string)"sim"))
                {
                    Arquivo arq_temp = new Arquivo();
                    arq_temp.nome_Arquivo = montarFormatoGD(matricula, "cis.pdf");
                    arq_temp.tipo_Arquivo = "cis";
                    list_arquivo.Add(arq_temp);
                }
                if (Session["postou_avisoFerias"].ToString().Equals((string)"sim"))
                {
                    Arquivo arq_temp = new Arquivo();
                    arq_temp.nome_Arquivo = montarFormatoGD(matricula, "avisoFerias.pdf");
                    arq_temp.tipo_Arquivo = "avisoFerias";
                    list_arquivo.Add(arq_temp);
                }
                if (Session["postou_requerimentos"].ToString().Equals((string)"sim"))
                {
                    Arquivo arq_temp = new Arquivo();
                    arq_temp.nome_Arquivo = montarFormatoGD(matricula, "requerimentos.pdf");
                    arq_temp.tipo_Arquivo = "requerimentos";
                    list_arquivo.Add(arq_temp);
                }
                if (Session["postou_outros"].ToString().Equals((string)"sim"))
                {
                    Arquivo arq_temp = new Arquivo();
                    arq_temp.nome_Arquivo = montarFormatoGD(matricula, "outros.pdf");
                    arq_temp.tipo_Arquivo = "outros";
                    list_arquivo.Add(arq_temp);
                }

                // Verifica se existe documento pra adicionar ao banco
                if (list_arquivo.Count == 0)
                {
                    LabelErro.Text = "Nenhum documento adicionado";
                    ImageAttention.Visible = true;
                }
                else
                {
                    //Adicionando a lista dos arquivos aos documentos pra depois adicionar no banco
                    documento.arquivos = list_arquivo;

                    Adaptador adpt = new Adaptador();
                    try
                    {
                        adpt.InserirDocumento(documento);
                        //Após inserir o documento é inserido na base de LOG a mensagem de log do que foi feito.
                        Log log = new Log();
                        log.data_log = DateTime.Now;
                        log.tipo_acao_log = "Inserir";
                        log.usuario_log = (String)Session["usuario"];
                        log.mensagem_acao_log = "Matrícula " + documento.matricula_Colaborador + " (" + documento.nome_Colaborador + ")";
                        adpt.InserirLog(log);
                    }
                    catch (Exception ex)
                    {
                        LabelErro.Text = ex.Message;
                        ImageAttention.Visible = true;
                    }

                    //foreach (Arquivo arq in list_arquivo)
                    //{
                    //    versiona_indexa(arq.nome_Arquivo);
                    //}

                    mensagem = "Colaborador inserido com sucesso";
                    Session.Add("mensagem", mensagem);
                    Server.Transfer("listar.aspx");
                }
            }
        }

        /// <summary>
        /// Carrega a foto do colaborador no sistema.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
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

        /// <summary>
        /// Método que atualiza o atributo matricula assim que o TextBox é editado.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void TextBoxMatricula_TextChanged(object sender, EventArgs e)
        {
            matricula = TextBoxMatricula.Text;
        }

        /// <summary>
        /// Método usado para verificar se já existe a matrícula passada.
        /// </summary>
        /// <param name="source">The source of the event.</param>
        /// <param name="args">The <see cref="System.Web.UI.WebControls.ServerValidateEventArgs"/> instance containing the event data.</param>
        protected void CustomValidatorMatricula_ServerValidate(object source, ServerValidateEventArgs args)
        {
            Adaptador adpt = new Adaptador();
            if (adpt.existeMatricula(args.Value))
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }

        /// <summary>
        /// Método usado para verificar se já existe o CPF passado.
        /// </summary>
        /// <param name="source">The source of the event.</param>
        /// <param name="args">The <see cref="System.Web.UI.WebControls.ServerValidateEventArgs"/> instance containing the event data.</param>
        protected void CustomValidatorCPF_ServerValidate(object source, ServerValidateEventArgs args)
        {
            Adaptador adpt = new Adaptador();
            if (adpt.existeCPF(args.Value))
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }

      
       
    }
}
