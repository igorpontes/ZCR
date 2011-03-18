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
using PEP.Properties;
using LightInfocon.Data.LightBaseProvider;
using LightInfocon.GoldenAccess.General;

namespace PEP
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        string mensagem = "";
        string nomeArquivo = "";
        string erro;

        //Localizando o último id cadastrado na base.
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

        //Localizando o último número de registro cadastrado na base.
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

        //Montagem de formato com id do registro + nome do arquivo que será anexado.
        public string montarFormatoGD(string id, string nome_Arquivo)
        {
            int novoId = Convert.ToInt16(id) + 1;
            return novoId + "_" + nome_Arquivo;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ImageButton lkPrincipal = (ImageButton)Master.FindControl("ImageButtonPrincipal");
            lkPrincipal.Visible = true;
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
                //Caso não seja o mesmo usuário, este será redirecionado para o login e a sessão será limpa.
                erro = "Usuário não autenticado";
                Session.Add("erro", erro);
                Session.Abandon();
                Server.Transfer("login.aspx");
            }
        }

        protected void ImageButtonCadastrar_Click(object sender, ImageClickEventArgs e)
        {
            string ext = "";
            string id = "";
            string arquivo = "";
            int tamanho = 0;
            string nomeArquivoLBW = "";

            //Obrigando o usuário a selecionar um arquivo.
            if (FileUploadArquivo.PostedFile.ContentLength == 0)
            {
                LabelErro.Text = "É necessário selecionar um arquivo";
            }
            else
            {
                try
                {
                    id = obterIdCadastrado();
                    nomeArquivoLBW = obterUltimoRegistroCadastrado();
                }
                catch (Exception ex)
                {
                    LabelErro.Text = ex.Message;
                }
                arquivo = FileUploadArquivo.PostedFile.FileName;
                nomeArquivo = System.IO.Path.GetFileName(arquivo);
                tamanho = nomeArquivo.Length;
                nomeArquivoLBW = nomeArquivoLBW + "_prontuario.pdf";
                Prontuario prontuario = new Prontuario();
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
                    prontuario.sexo = "f";
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
                medico.nome_Medico1 = TextBoxMedico_Solicitante1.Text;
                medico.nome_Medico2 = TextBoxMedico_Solicitante2.Text;
                medico.nome_Medico3 = TextBoxMedico_Solicitante3.Text;
                medico.nome_Medico4 = TextBoxMedico_Solicitante4.Text;
                prontuario.medico = medico;
                prontuario.nome_Clinica_Internacao = TextBoxNome_Clinica_Internacao.Text;
                prontuario.diagnostico_Provisorio = TextBoxDiagnostico_Provisorio.Text;
                prontuario.data_Internacao = Convert.ToDateTime(TextBoxData_Internacao.Text);
                prontuario.medico_Solicitante = TextBoxNome_Medico.Text;

                Adaptador adpt = new Adaptador();
                try
                {
                    adpt.InserirProntuario(prontuario);//disparando o método de inserção de prontuário.
                }
                catch (Exception ex)
                {
                    LabelErro.Text = ex.Message;
                }

                Versao versao = new Versao();//classe que trata os dados do arquivo que será anexado.

                FileUploadArquivo.PostedFile.SaveAs(Settings.Default.CaminhoDoRepositorioProntuario + nomeArquivoLBW);//salvando um cópia do arquivo que será usado como repositório.

                versao.CaminhoDoArquivo = Settings.Default.CaminhoDoRepositorioProntuario;
                versao.Extensao = ext;
                versao.NomeDoArquivo = nomeArquivoLBW;

                versao.Id = Convert.ToInt32(obterIdCadastrado());

                Indexador indexador = new Indexador();
                indexador.Indexe(versao);//método que prepara o arquivo para ser indexado.

                mensagem = "Registro inserido com sucesso";
                Session.Add("mensagem", mensagem);
                Server.Transfer("Listar.aspx");
            }
        }
    }
}