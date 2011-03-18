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
using SistemaRH.Properties;
using LightInfocon.Data.LightBaseProvider;

namespace SistemaRH
{
    public partial class cadastro : System.Web.UI.Page
    {
        string mensagem = "";
        string nomeArquivo = "";

        public string obterIdCadastrado()
        {
            IDataReader reader;
            IDbConnection con = new LightBaseConnection("user=lbw;password=lbw;udb=defudb;server=localhost");
            try
            {
                con.Open();
                IDbCommand comm = new LightBaseCommand("select last id from pessoa");
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

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ImageButtonCadastrar_Click(object sender, ImageClickEventArgs e)
        {

            string ext = "";
            string id = "";
            string arquivo = "";
            int tamanho = 0;
            string nomeArquivoLBW = "";

            if (FileUploadArquivo.PostedFile.ContentLength == 0)
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
                arquivo = FileUploadArquivo.PostedFile.FileName;
                nomeArquivo = System.IO.Path.GetFileName(arquivo);
                tamanho = nomeArquivo.Length;
                nomeArquivoLBW = montarFormatoGD(id, nomeArquivo);
                Pessoa pessoa = new Pessoa();
                pessoa.arq_Arquivo = nomeArquivoLBW;
                
                pessoa.nome_Colaborador = TextBoxNome_Colaborador.Text;
                pessoa.naturalidade = TextBoxNaturalidade.Text;
                pessoa.data_Nascimento = Convert.ToDateTime(TextBoxData_Nascimento.Text);
                if (RadioButtonListSexo.SelectedIndex == 0)
                {
                    pessoa.sexo = 'm';
                }
                else
                {
                    pessoa.sexo = 'm';
                }
                pessoa.nome_Pai = TextBoxNome_Pai.Text;
                pessoa.nome_Mae = TextBoxNome_Mae.Text;
                pessoa.cargo = TextBoxCargo.Text;
                
                Endereco endereco = new Endereco();
                endereco.endereco = TextBoxEndereco.Text;
                endereco.numero = TextBoxNumero.Text;
                endereco.complemento = TextBoxComplemento.Text;
                endereco.bairro = TextBoxBairro.Text;
                endereco.cep = TextBoxCEP.Text;
                endereco.cidade = TextBoxCidade.Text;
                endereco.estado = TextBoxEstado.Text;
                pessoa.endereco = endereco;
                Telefone telefone = new Telefone();
                telefone.numero_TelefoneFixo = TextBoxTelefoneResidencial.Text;
                telefone.numero_TelefoneCelular = TextBoxTelefoneCelular.Text;
                pessoa.telefone = telefone;
              

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

                //FileUploadArquivo.PostedFile.SaveAs(Settings.Default.CaminhoDoRepositorioPessoa + nomeArquivoLBW);
                FileUploadArquivo.PostedFile.SaveAs(@"c:/temp/" + nomeArquivoLBW);

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
}
