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
using System.IO;

namespace GED_TCESE
{
    public partial class WebForm11 : System.Web.UI.Page
    {
        string nomeArquivo;
        string nomeArquivoAntigo;
        string arquivo;
        string nomeArquivoLBW;
        string mensagem;
        string erro;
        string extensao;
        int tamanho;
        string id;

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

        protected void Page_Load(object sender, EventArgs e)
        {
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
                Server.Transfer("Login.aspx");
                erro = "Usuário não autenticado";
                Session.Add("erro", erro);
                Session.Abandon();
            }

            if ((String)Session["id"] != null)
            {
                id = (String)Session["id"];
            }
            else
            {
                Server.Transfer("Default.aspx");
                erro = "Identificador do campo não encontrado";
                Session.Add("erro", erro);
            }

            if (!Page.IsPostBack)
            {
                MontaTela(id);
            }
        }

        public void MontaTela(string id)
        {
            IDataReader reader;
            IDbConnection minhaConexao = new LightBaseConnection("user=lbw;password=lbw;udb=defudb;server=localhost");
            try
            {
                minhaConexao.Open();

                IDbCommand meuComando = new LightBaseCommand("select arq_Arquivo, numero_Processo, ano_Processo, origem, descricao, assunto, interessados.nome from tcese where id =" + id);
                meuComando.Connection = minhaConexao;
                reader = meuComando.ExecuteReader();

                while (reader.Read())
                {
                    nomeArquivoAntigo = Convert.ToString(reader["arq_Arquivo"]);
                    TextBoxNumeroProcesso.Text = Convert.ToString(reader["numero_Processo"]);
                    TextBoxAnoProcesso.Text = Convert.ToString(reader["ano_Processo"]);
                    TextBoxOrigem.Text = Convert.ToString(reader["origem"]);
                    TextBoxAssunto.Text = Convert.ToString(reader["assunto"]);
                    TextBoxDescricao.Text = Convert.ToString(reader["descricao"]);
                    DataTable dt = (DataTable)reader["interessados"];
                    string[] nome = { "", "", "", "" };
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        nome[i] = dt.Rows[i]["nome"].ToString();
                    }
                    TextBoxPessoa1.Text = nome[0].ToString();
                    TextBoxPessoa2.Text = nome[1].ToString();
                    TextBoxPessoa3.Text = nome[2].ToString();
                    TextBoxPessoa4.Text = nome[3].ToString();
                }
            }
            finally
            {
                minhaConexao.Close();
            }
        }

        protected void ImageButtonEnviar_Click(object sender, ImageClickEventArgs e)
        {
            if (FileUpload.PostedFile.ContentLength == 0)
            {
                Response.Write("<script>alert('Favor selecionar arquivo')</script>");
            }
            else
            {
                arquivo = FileUpload.PostedFile.FileName;
                nomeArquivo = System.IO.Path.GetFileName(arquivo);
                int pos = 0;
                pos = nomeArquivo.LastIndexOf('.');
                extensao = nomeArquivo.Substring(pos).ToLower();
                tamanho = nomeArquivo.Length;
                nomeArquivoLBW = id + "_" + "0001" + "_" + "arq_Arquivo" + extensao;
                Processo processo = new Processo();
                processo.id = Convert.ToInt16(id);
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
                Adaptador adt = new Adaptador();
                try
                {
                    Processo proc = new Processo();
                    proc = adt.obterProcessoPorId(id);
                    nomeArquivoAntigo = proc.arq_Arquivo;
                    if (nomeArquivoAntigo != null)
                    {
                        string diretorioRemover = Settings.Default.CaminhoDoRepositorioJurisprudencia + nomeArquivoAntigo;
                        FileInfo arquivoAntigo = new FileInfo(diretorioRemover);
                        arquivoAntigo.Delete();
                    }
                    adt.AtualizaProcesso(processo);
                }
                catch (Exception ex)
                {
                    LabelErro.Text = ex.Message;
                }

                Versao versao = new Versao();

                FileUpload.PostedFile.SaveAs(Settings.Default.CaminhoDoRepositorioJurisprudencia + nomeArquivoLBW);

                versao.CaminhoDoArquivo = Settings.Default.CaminhoDoRepositorioJurisprudencia;
                versao.Extensao = extensao;
                versao.NomeDoArquivo = nomeArquivoLBW;

                versao.Id = processo.id;

                Indexador indexador = new Indexador();
                indexador.Indexe(versao);

                mensagem = "Registro modificado com sucesso";
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
