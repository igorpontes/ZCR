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
using PEP.Properties;
using System.IO;

namespace PEP
{
    public partial class WebForm10 : System.Web.UI.Page
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
                IDbCommand comm = new LightBaseCommand("select last id from prontuario");
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

        public string obterUltimoRegistroCadastrado()
        {
            IDataReader reader;
            IDbConnection con = new LightBaseConnection("user=lbw;password=lbw;udb=defudb;server=localhost");
            try
            {
                con.Open();
                IDbCommand comm = new LightBaseCommand("select last numero_Registro from prontuario");
                comm.Connection = con;
                reader = comm.ExecuteReader();
                reader.Read();
                return reader["numero_Registro"].ToString();
            }
            finally
            {
                con.Close();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
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
                TextBoxNumero_Registro.Focus();
            }
            else
            {
                erro = "Usuário não autenticado";
                Session.Add("erro", erro);
                Session.Abandon();
                Server.Transfer("login.aspx");
            }

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

            //Caso esteja sendo aberta pela primeira vez, a página buscará os valores dos campos.
            if (!Page.IsPostBack)
            {
                MontaTela(id);
            }
        }

        /*
         * Método que vai carregar os dados do registro selecionado na listagem na tela.
         */ 
        public void MontaTela(string id)
        {
            IDataReader reader;
            IDbConnection minhaConexao = new LightBaseConnection("user=lbw;password=lbw;udb=defudb;server=localhost");
            try
            {
                minhaConexao.Open();

                IDbCommand meuComando = new LightBaseCommand("select arq_Arquivo, numero_registro, nome_Paciente, naturalidade, "
                    + " data_Nascimento, sexo, nome_Pai, nome_Mae, profissao, pessoa_Responsavel, endereco.endereco, endereco.numero, "
                    + "endereco.complemento, endereco.bairro, endereco.nome_Cidade, endereco.nome_Estado, endereco.Cep, telefones.numero_Telefone, "
                    + "procedencia, nome_Clinica_Diagnostico, diagnostico, cid, medicos.matricula_Medico, medicos.nome_Medico, nome_Clinica_Internacao, diagnostico_Provisorio, "
                    + "data_Internacao, medico_Solicitante from prontuario where id =" + id);
                meuComando.Connection = minhaConexao;
                reader = meuComando.ExecuteReader();

                while (reader.Read())
                {
                    TextBoxNumero_Registro.Text = Convert.ToString(reader["numero_Registro"]);
                    TextBoxNome_Paciente.Text = Convert.ToString(reader["nome_Paciente"]);
                    TextBoxNaturalidade.Text = Convert.ToString(reader["naturalidade"]);
                    TextBoxData_Nascimento.Text = Convert.ToDateTime(reader["data_Nascimento"]).ToShortDateString();
                    string sexo = Convert.ToString(reader["sexo"]);
                    if (sexo == "m")
                    {
                        RadioButtonListSexo.Items[0].Selected = true;
                    }
                    else
                    {
                        RadioButtonListSexo.Items[1].Selected = true;
                    }
                    TextBoxNome_Pai.Text = Convert.ToString(reader["nome_Pai"]);
                    TextBoxNome_Mae.Text = Convert.ToString(reader["nome_Mae"]);
                    TextBoxProfissao.Text = Convert.ToString(reader["profissao"]);
                    TextBoxPessoa_Responsavel.Text = Convert.ToString(reader["pessoa_Responsavel"]);
                    TextBoxProcedencia.Text = Convert.ToString(reader["procedencia"]);
                    TextBoxNome_Clinica_Diagnostico.Text = Convert.ToString(reader["nome_Clinica_Diagnostico"]);
                    TextBoxDiagnostico.Text = Convert.ToString(reader["diagnostico"]);
                    TextBoxCID.Text = Convert.ToString(reader["cid"]);
                    TextBoxNome_Clinica_Internacao.Text = Convert.ToString(reader["nome_Clinica_Internacao"]);
                    TextBoxDiagnostico_Provisorio.Text = Convert.ToString(reader["diagnostico_Provisorio"]);
                    TextBoxData_Internacao.Text = Convert.ToDateTime(reader["data_Internacao"]).ToShortDateString();
                    TextBoxNome_Medico.Text = Convert.ToString(reader["medico_Solicitante"]);

                    DataTable dt_Enderecos = (DataTable)reader["endereco"];
                    Endereco endereco = new Endereco();
                    for (int i = 0; i < dt_Enderecos.Rows.Count; i++)
                    {
                        endereco.endereco = dt_Enderecos.Rows[i]["endereco"].ToString();
                        endereco.numero = dt_Enderecos.Rows[i]["numero"].ToString();
                        endereco.complemento = dt_Enderecos.Rows[i]["complemento"].ToString();
                        endereco.bairro = dt_Enderecos.Rows[i]["bairro"].ToString();
                        endereco.nome_Cidade = dt_Enderecos.Rows[i]["nome_Cidade"].ToString();
                        endereco.nome_Estado = dt_Enderecos.Rows[i]["nome_Estado"].ToString();
                        endereco.cep = dt_Enderecos.Rows[i]["cep"].ToString();
                    }
                    TextBoxEndereco.Text = endereco.endereco;
                    TextBoxNumero.Text = endereco.numero;
                    TextBoxComplemento.Text = endereco.complemento;
                    TextBoxBairro.Text = endereco.bairro;
                    TextBoxCidade.Text = endereco.nome_Cidade;
                    TextBoxEstado.Text = endereco.nome_Estado;
                    TextBoxCEP.Text = endereco.cep;

                    DataTable dt_Telefones = (DataTable)reader["telefones"];
                    string[] dados_Telefone = { "", "", "" };
                    for (int i = 0; i < dt_Telefones.Rows.Count; i++)
                    {
                        dados_Telefone[i] = dt_Telefones.Rows[i]["numero_Telefone"].ToString();
                    }
                    TextBoxTelefoneResidencial.Text = dados_Telefone[0].ToString();
                    TextBoxTelefoneCelular.Text = dados_Telefone[1].ToString();
                    TextBoxTelefoneComercial.Text = dados_Telefone[2].ToString();

                    DataTable dt_Medico = (DataTable)reader["medicos"];
                    Medico medico = new Medico();
                    string[] matriculas_Medico = { "", "", "", "" };
                    string[] nomes_Medico = { "", "", "", "" };
                    for (int i = 0; i < dt_Medico.Rows.Count; i++)
                    {
                        matriculas_Medico[i] = dt_Medico.Rows[i]["matricula_Medico"].ToString();
                        nomes_Medico[i] = dt_Medico.Rows[i]["nome_Medico"].ToString();
                    }
                    medico.matricula_Medico1 = matriculas_Medico[0].ToString();
                    medico.matricula_Medico2 = matriculas_Medico[1].ToString();
                    medico.matricula_Medico3 = matriculas_Medico[2].ToString();
                    medico.matricula_Medico4 = matriculas_Medico[3].ToString();
                    medico.nome_Medico1 = nomes_Medico[0].ToString();
                    medico.nome_Medico2 = nomes_Medico[1].ToString();
                    medico.nome_Medico3 = nomes_Medico[2].ToString();
                    medico.nome_Medico4 = nomes_Medico[3].ToString();
                    TextBoxMedico_Solicitante1.Text = medico.nome_Medico1;
                    TextBoxMedico_Solicitante2.Text = medico.nome_Medico2;
                    TextBoxMedico_Solicitante3.Text = medico.nome_Medico3;
                    TextBoxMedico_Solicitante4.Text = medico.nome_Medico4;
                }
            }
            finally
            {
                minhaConexao.Close();
            }
        }

        /*
         * Método que vai alterar os dados do prontuário, após o click do botão enviar.
         */ 
        protected void ImageButtonAlterar_Click(object sender, ImageClickEventArgs e)
        {
            if (FileUploadArquivo.PostedFile.ContentLength == 0)
            {
                Response.Write("<script>alert('Favor selecionar arquivo')</script>");
            }
            else
            {
                arquivo = FileUploadArquivo.PostedFile.FileName;
                nomeArquivo = System.IO.Path.GetFileName(arquivo);
                extensao = nomeArquivo.Substring(nomeArquivo.LastIndexOf('.'));
                tamanho = nomeArquivo.Length;
                Prontuario prontuario = new Prontuario();
                Adaptador adpt = new Adaptador();
                string id = (String)Session["id"];
                prontuario = adpt.obterProntuarioPorId(id);
                prontuario.id = Convert.ToInt16(id);
                nomeArquivoAntigo = prontuario.arq_Arquivo;
                nomeArquivoLBW = prontuario.numero_Registro + "_prontuario.pdf";
                prontuario.arq_Arquivo = nomeArquivoLBW;
                prontuario.numero_Registro = TextBoxNumero_Registro.Text;
                prontuario.nome_Paciente = TextBoxNome_Paciente.Text;
                prontuario.naturalidade = TextBoxNaturalidade.Text;
                prontuario.data_Nascimento = Convert.ToDateTime(TextBoxData_Nascimento.Text);
                if (RadioButtonListSexo.SelectedIndex == 0)
                {
                    prontuario.sexo = "m";
                }
                else
                {
                    prontuario.sexo = "m";
                }
                prontuario.nome_Pai = TextBoxNome_Pai.Text;
                prontuario.nome_Mae = TextBoxNome_Mae.Text;
                prontuario.profissao = TextBoxProfissao.Text;
                prontuario.pessoa_Responsavel = TextBoxPessoa_Responsavel.Text;
                Endereco endereco = new Endereco();
                endereco.endereco = TextBoxEndereco.Text;
                endereco.numero = TextBoxNumero.Text;
                endereco.complemento = TextBoxComplemento.Text;
                endereco.bairro = TextBoxBairro.Text;
                endereco.cep = TextBoxCEP.Text;
                endereco.nome_Cidade = TextBoxCidade.Text;
                endereco.nome_Estado = TextBoxEstado.Text;
                prontuario.endereco = endereco;
                Telefone telefone = new Telefone();
                telefone.numero_TelefoneFixo = TextBoxTelefoneResidencial.Text;
                telefone.numero_TelefoneCelular = TextBoxTelefoneCelular.Text;
                telefone.numero_TelefoneComercial = TextBoxTelefoneComercial.Text;
                prontuario.telefone = telefone;
                prontuario.procedencia = TextBoxProcedencia.Text;
                prontuario.nome_Clinica_Diagnostico = TextBoxNome_Clinica_Diagnostico.Text;
                prontuario.diagnostico = TextBoxDiagnostico.Text;
                prontuario.cid = TextBoxCID.Text;
                Medico medico = new Medico();
                medico = prontuario.medico;
                medico.nome_Medico1 = TextBoxMedico_Solicitante1.Text;
                medico.nome_Medico2 = TextBoxMedico_Solicitante2.Text;
                medico.nome_Medico3 = TextBoxMedico_Solicitante3.Text;
                medico.nome_Medico4 = TextBoxMedico_Solicitante4.Text;
                prontuario.medico = medico;
                prontuario.nome_Clinica_Internacao = TextBoxNome_Clinica_Internacao.Text;
                prontuario.diagnostico_Provisorio = TextBoxDiagnostico_Provisorio.Text;
                prontuario.data_Internacao = Convert.ToDateTime(TextBoxData_Internacao.Text);
                prontuario.medico_Solicitante = TextBoxNome_Medico.Text;
                Adaptador adt = new Adaptador();
                try
                {
                    if (nomeArquivoAntigo != null)
                    {
                        string diretorioRemover = Settings.Default.CaminhoDoRepositorioProntuario + nomeArquivoAntigo;
                        FileInfo arquivoAntigo = new FileInfo(diretorioRemover);
                        arquivoAntigo.Delete();
                    }
                    adt.AtualizarProntuario(prontuario);
                }
                catch (Exception ex)
                {
                    LabelErro.Text = ex.Message;
                }

                Versao versao = new Versao();

                FileUploadArquivo.PostedFile.SaveAs(Settings.Default.CaminhoDoRepositorioProntuario + nomeArquivoLBW);

                versao.CaminhoDoArquivo = Settings.Default.CaminhoDoRepositorioProntuario;
                versao.Extensao = extensao;
                versao.NomeDoArquivo = nomeArquivoLBW;

                versao.Id = prontuario.id;

                Indexador indexador = new Indexador();
                indexador.Indexe(versao);

                mensagem = "Registro modificado com sucesso";
                Session.Add("mensagem", mensagem);
                Server.Transfer("Listar.aspx");
            }
        }
    }
}