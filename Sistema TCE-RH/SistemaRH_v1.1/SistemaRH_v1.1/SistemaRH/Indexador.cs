using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SistemaRH.Properties;
using LightInfocon.GoldenIndex.General;
using System.Collections.Generic;
using LightInfocon.Data.LightBaseProvider;

namespace SistemaRH
{
    /// <summary>
    /// Classe responsável pela indexaçao do conteúdo dos arquivos.
    /// </summary>
    public class Indexador
    {
        String diretorio = HttpContext.Current.Server.MapPath("~/arquivos/");

        /// <summary>
        /// Método usado para extrair o conteúdo de um arquivo específico.
        /// </summary>
        /// <param name="arq">O arquivo a ser indexado.</param>
        public void Indexe(Arquivo arq)
        {
            try
            {
                IGoldenIndex goldenIndex =
                    GoldenIndexClient.Instance(Settings.Default.MaquinaGoldenIndex,
                                               Settings.Default.PortaGoldenIndex,
                                               Settings.Default.UriGoldenIndex,
                                               Settings.Default.ProtocoloGoldenIndex);
                User usuarioGoldenIndex = GoldenIndexClient.Authenticate(Settings.Default.UsuarioGoldenIndex,
                                                                         Settings.Default.SenhaGoldenIndex, goldenIndex);

                //string extensao = versao.Extensao.StartsWith(".") ? versao.Extensao : string.Concat(".", versao.Extensao);
                //if (!goldenIndex.IsSupported(usuarioGoldenIndex, extensao))
                //{
                //    // Se não for um arquivo suportado, não faz nada
                //    return;
                //}

                string id = obterIdCadastrado();
                //List<Arquivo> lista = new List<Arquivo>();
                //lista = obterIdArquivos(id);

                //foreach (var arq in lista)
                //{


                    FileData arquivo = new FileData();
                    //arquivo.Id = count++;
                    arquivo.Url = diretorio + arq.nome_Arquivo;

                    //TESTE

                    CollectionFieldUpdatingParameters parameters = new CollectionFieldUpdatingParameters();
                    parameters.CollectionName = "arquivos";
                    parameters.ContentField = "conteudo_Arquivo"; // é o mesmo que a coleção neste caso, porque o multivalorado não está agrupado num grupo
                    parameters.Table = "documento";
                    //parameters.ParentField = "id";
                    //parameters.ParentFieldValue = id; // aqui supomos que haja um registro na base Pessoas cujo Id seja 12. é nesse registro que será adicionado uma nova linha na coleção "Documentos"
                    parameters.IdField = "id_arquivo";
                    parameters.IdFieldValue = arq.id_Arquivo.ToString();
                    
                    


                    arquivo.IndexerParameters = parameters;
                    //arquivo.Id = Convert.ToUInt32(arq.id_Arquivo);
                    
                    goldenIndex.SaveFile(usuarioGoldenIndex, arquivo);


               // }

                
            }
            catch (Exception exception)
            {
                string erro = exception.Message;
            }
        }

        /// <summary>
        /// Método usado para versionar um arquivo.
        /// </summary>
        /// <param name="nome_arquivo">O nome do arquivo.</param>
        public void versiona_indexa(string nome_arquivo)
        {
            Versao versao = new Versao();
            String diretorio = HttpContext.Current.Server.MapPath("~/arquivos/");
            versao.CaminhoDoArquivo = diretorio;
            versao.Extensao = ".pdf";
            versao.NomeDoArquivo = nome_arquivo;
            versao.Id = Convert.ToInt32(obterIdCadastrado());

            //Indexador indexador = new Indexador();
            //indexador.Indexe(versao);
        }

        /// <summary>
        /// Método usado para obters o último id cadastrado.
        /// </summary>
        /// <returns>o id encontrado.</returns>
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
        /// Método usado para obter os arquivos pelo id do documento.
        /// </summary>
        /// <param name="id">O id.</param>
        /// <returns></returns>
        public List<Arquivo> obterIdArquivos(string id)
        {
            IDataReader reader;
            IDbConnection con = new LightBaseConnection("user=lbw;password=lbw;udb=defudb;server=localhost");
            Documento documento = new Documento();
            try
            {
                con.Open();
                string comando = "select arquivos from documento where id = ";
                IDbCommand comm = new LightBaseCommand(comando + id);
                comm.Connection = con;
                reader = comm.ExecuteReader();
                reader.Read();


                DataTable dt_Arquivos = (DataTable)reader["arquivos"];
                //string[] dados_Arquivos = new string[dt_Arquivos.Rows.Count];
                List<Arquivo> list_arq = new List<Arquivo>();
                for (int i = 0; i < dt_Arquivos.Rows.Count; i++)
                {
                    Arquivo arquivo = new Arquivo();
                    arquivo.nome_Arquivo = dt_Arquivos.Rows[i]["nome_Arquivo"].ToString();
                    arquivo.id_Arquivo = Convert.ToInt32(dt_Arquivos.Rows[i]["id_arquivo"].ToString());
                    list_arq.Add(arquivo);
                }

                return list_arq;
            }
            finally
            {
                con.Close();
            }
        }
    }
}
