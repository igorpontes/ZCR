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
using System.Collections.Generic;
using LightInfocon.Data.LightBaseProvider;
using LightInfocon.GoldenAccess.General;

namespace SistemaRH
{
    /// <summary>
    /// Classe usada como repositório de métodos comuns.
    /// </summary>
    public class Adaptador : IAdaptador
    {
        /// <summary>
        /// Método usado para pesquisar um comando na base de Documentos.
        /// </summary>
        /// <param name="comando">O script de comando.</param>
        /// <returns>lista de documentos filtrados pelo comando.</returns>
        public List<Documento> PesquisarCampos(string comando)
        {
            List<Documento> lista = new List<Documento>();
            IDataReader reader;
            IDbConnection minhaConexao = new LightBaseConnection("user=lbw;password=lbw;UDB=defudb;server=localhost");
            try
            {
                minhaConexao.Open();
                try
                {
                    IDbCommand meuComando = new LightBaseCommand(comando);
                    meuComando.Connection = minhaConexao;
                    reader = meuComando.ExecuteReader();

                    while (reader.Read())
                    {
                        Documento documento = new Documento();

                        documento.id = Convert.ToInt16(reader["id"]);
                        documento.matricula_Colaborador = Convert.ToString(reader["matricula_Colaborador"]);
                        documento.nome_Colaborador = Convert.ToString(reader["nome_Colaborador"]);
                        documento.cpf_Colaborador = Convert.ToString(reader["cpf_Colaborador"]);

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
                        documento.arquivos = list_arq;
                        lista.Add(documento);
                    }
                }
                catch (LightBaseException e)
                {
                    string erro = e.Message;
                }
                return lista;
            }
            finally
            {
                minhaConexao.Close();
            }
        }

        /// <summary>
        /// Método usado para retornar toda a base de Documentos.
        /// </summary>
        /// <returns>retorna a base de Documentos.</returns>
        public List<Documento> Todos()
        {
            List<Documento> lista = new List<Documento>();
            IDataReader reader;
            IDbConnection minhaConexao = new LightBaseConnection("user=lbw;password=lbw;UDB=defudb;server=localhost");
            try
            {
                minhaConexao.Open();
                IDbCommand comando = new LightBaseCommand("select id, matricula_Colaborador, nome_Colaborador, cpf_Colaborador, arquivos.nome_Arquivo, "
                                                        + "arquivos.conteudo_Arquivo, arquivos.tipo_Arquivo from documento");
                comando.Connection = minhaConexao;
                reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Documento documento = new Documento();
                    List<Arquivo> list_arq = new List<Arquivo>();

                    documento.id = Convert.ToInt16(reader["id"]);
                    documento.matricula_Colaborador = Convert.ToString(reader["matricula_Colaborador"]);
                    documento.cpf_Colaborador = Convert.ToString(reader["cpf_Colaborador"]);
                    documento.nome_Colaborador = Convert.ToString(reader["nome_Colaborador"]);

                    DataTable dt_Arquivos = (DataTable)reader["arquivos"];
                    for (int i = 0; i < dt_Arquivos.Rows.Count; i++)
                    {
                        Arquivo arquivo = new Arquivo();
                        arquivo.nome_Arquivo = dt_Arquivos.Rows[i]["nome_Arquivo"].ToString();
                        arquivo.conteudo_Arquivo = dt_Arquivos.Rows[i]["conteudo_Arquivo"].ToString();
                        arquivo.tipo_Arquivo = dt_Arquivos.Rows[i]["tipo_Arquivo"].ToString();
                        list_arq.Add(arquivo);
                    }
                    documento.arquivos = list_arq;
                    lista.Add(documento);
                }
                return lista;
            }
            finally
            {
                minhaConexao.Close();
            }
        }

        /// <summary>
        /// Método usado para listar a base ordenada por coluna.
        /// </summary>
        /// <param name="select">O comando select.</param>
        /// <param name="coluna">A coluna a ser ordenada.</param>
        /// <param name="orderBy">A ordem, se ascendente ou descendente.</param>
        /// <returns></returns>
        public List<Documento> PorColuna(string select, string coluna, string orderBy)
        {
            if (select == null)
            {
                select = "select id, matricula_Colaborador, nome_Colaborador, cpf_Colaborador, arquivos.nome_Arquivo, "
                                                        + "arquivos.conteudo_Arquivo, arquivos.tipo_Arquivo from documento";
            }
            List<Documento> lista = new List<Documento>();
            IDataReader reader;
            IDbConnection minhaConexao = new LightBaseConnection("user=lbw;password=lbw;UDB=defudb;server=localhost");
            try
            {
                minhaConexao.Open();
                try
                {
                    IDbCommand comando = new LightBaseCommand(select + " order by " + coluna + " " + orderBy);
                    comando.Connection = minhaConexao;
                    reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        Documento documento = new Documento();

                        documento.id = Convert.ToInt16(reader["id"]);
                        documento.matricula_Colaborador = Convert.ToString(reader["matricula_Colaborador"]);
                        documento.nome_Colaborador = Convert.ToString(reader["nome_Colaborador"]);
                        documento.cpf_Colaborador = Convert.ToString(reader["cpf_Colaborador"]);

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
                        documento.arquivos = list_arq;
                        lista.Add(documento);
                    }
                }
                catch (LightBaseException e)
                {
                    string erro = e.Message;
                }
                return lista;
            }
            finally
            {
                minhaConexao.Close();
            }
        }

        /// <summary>
        /// Método usado para atualizar a base de documento.
        /// </summary>
        /// <param name="documento">O documento a ser atualizado.</param>
        /// <param name="id">O id do documento na base.</param>
        public void AtualizarDocumento(Documento documento, string id)
        {
            List<Arquivo> arquivo = new List<Arquivo>();
            arquivo = documento.arquivos;
            IDbConnection minhaConexao = new LightBaseConnection("user=lbw;password=lbw;UDB=defudb;server=localhost");
            try
            {
                minhaConexao.Open();
                IDbCommand meuComando = new LightBaseCommand();
                string comando = "update documento set matricula_Colaborador=@matricula_Colaborador, "
                    + "foto=@foto, nome_Colaborador=@nome_Colaborador, cpf_Colaborador=@cpf_Colaborador";

                meuComando.Connection = minhaConexao;
                if (arquivo.Count == 0)
                {
                    IDbCommand meuComandoDelete = new LightBaseCommand();
                    string comandoDelete = "delete from documento.arquivos where id=@id";
                    meuComandoDelete.CommandText = comandoDelete;
                    meuComandoDelete.Parameters.Add(new LightBaseParameter("id", id));
                    meuComandoDelete.Connection = minhaConexao;
                    meuComandoDelete.ExecuteNonQuery();
                }

                for (int i = 0; i < arquivo.Count; i++)
                {
                    
                    comando += ",";
                    if (i == 0)
                    {
                        comando += " arquivos={";
                    }
                    comando += "{@nome_Arquivo" + i + ",@conteudo_Arquivo" + i + ",@tipo_Arquivo" + i + "}";
                    if (arquivo.Count == (i + 1))
                    {
                        comando += "}";
                    }
                }
                comando += " where id=@id";
                meuComando.CommandText = comando;

                meuComando.Parameters.Add(new LightBaseParameter("id", id));
                meuComando.Parameters.Add(new LightBaseParameter("matricula_Colaborador", documento.matricula_Colaborador));
                meuComando.Parameters.Add(new LightBaseParameter("foto", documento.foto));
                meuComando.Parameters.Add(new LightBaseParameter("nome_Colaborador", documento.nome_Colaborador));
                meuComando.Parameters.Add(new LightBaseParameter("cpf_Colaborador", documento.cpf_Colaborador));
                int count = 0;
                foreach (var arq in arquivo)
                {
                    meuComando.Parameters.Add(new LightBaseParameter("nome_Arquivo" + count, arq.nome_Arquivo));
                    meuComando.Parameters.Add(new LightBaseParameter("conteudo_Arquivo" + count, arq.conteudo_Arquivo));
                    meuComando.Parameters.Add(new LightBaseParameter("tipo_Arquivo" + count, arq.tipo_Arquivo));
                    count++;
                }
                meuComando.ExecuteNonQuery();
            }
            finally
            {
                minhaConexao.Close();
            }
        }

        /// <summary>
        /// Método usado para inserir um documento na base.
        /// </summary>
        /// <param name="documento">O documento a ser inserido.</param>
        public void InserirDocumento(Documento documento)
        {
            List<Arquivo> arquivo = new List<Arquivo>();
            arquivo = documento.arquivos;
            IDbConnection minhaConexao = new LightBaseConnection("user=lbw;password=lbw;udb=defudb;server=localhost");
            try
            {
                minhaConexao.Open();
                IDbCommand meuComando = new LightBaseCommand();
                string comando = "insert into documento (matricula_Colaborador, foto, nome_Colaborador, cpf_Colaborador, arquivos)  "
                                + "values (@matricula_Colaborador, @foto, @nome_Colaborador, @cpf_Colaborador";
                //string comando = "insert into documento (matricula_Colaborador, foto, nome_Colaborador, cpf_Colaborador)  "
                //                + "values (@matricula_Colaborador, @foto, @nome_Colaborador, @cpf_Colaborador";
                meuComando.Connection = minhaConexao;
                for (int i = 0; i < arquivo.Count; i++)
                {
                    comando += ", ";
                    if (i == 0)
                    {
                        comando += "{";
                    }
                    comando += "{@nome_Arquivo" + i + ",@conteudo_Arquivo" + i + ",@tipo_Arquivo" + i + "}";
                    if (arquivo.Count == (i + 1))
                    {
                        comando += "}";
                    }
                }
                comando += ")";
                meuComando.CommandText = comando;
                meuComando.Parameters.Add(new LightBaseParameter("matricula_Colaborador", documento.matricula_Colaborador));
                meuComando.Parameters.Add(new LightBaseParameter("foto", documento.foto));
                meuComando.Parameters.Add(new LightBaseParameter("nome_Colaborador", documento.nome_Colaborador));
                meuComando.Parameters.Add(new LightBaseParameter("cpf_Colaborador", documento.cpf_Colaborador));
                int count = 0;
                foreach (var arq in arquivo)
                {
                    meuComando.Parameters.Add(new LightBaseParameter("nome_Arquivo" + count, arq.nome_Arquivo));
                    meuComando.Parameters.Add(new LightBaseParameter("conteudo_Arquivo" + count, arq.conteudo_Arquivo));
                    meuComando.Parameters.Add(new LightBaseParameter("tipo_Arquivo" + count, arq.tipo_Arquivo));
                    //versiona_indexa(arq.nome_Arquivo);
                    count++;
                }
                meuComando.ExecuteNonQuery();

                
                //começa o versionamento:
                //posso versionar e depois pegar um list dos conteudos e colocar na lista documentos.arquivos.conteudo
                //e apos isso eu faço um update na base
                Indexador indexador = new Indexador();
                List<Arquivo> lista = new List<Arquivo>();
                string id = obterIdCadastrado();
                lista = obterIdArquivos(id);
                foreach (Arquivo arq in lista)
                {
                    indexador.Indexe(arq);
                }


                //faz update dos campos apos versionamento

            }
            finally
            {
                minhaConexao.Close();
            }
        }

        /// <summary>
        /// Método usado para remover determinado documento.
        /// </summary>
        /// <param name="id">O id do documento a ser removido.</param>
        public void RemoverDocumento(int id)
        {
            IDbConnection minhaConexao = new LightBaseConnection("user=lbw;password=lbw;UDB=defudb;server=localhost");
            try
            {
                minhaConexao.Open();
                IDbCommand comando = new LightBaseCommand("delete from documento where id=@id");
                comando.Parameters.Add(new LightBaseParameter("id", id));
                comando.Connection = minhaConexao;
                comando.ExecuteNonQuery();
            }
            finally
            {
                minhaConexao.Close();
            }
        }

        /// <summary>
        /// Método que retorna o tipo de usuário no login.
        /// </summary>
        /// <param name="nome">O username.</param>
        /// <param name="senha">A senha.</param>
        /// <returns></returns>
        public int EfetuaLogin(string nome, string senha)
        {
            GoldenAccess goldenAccess = new GoldenAccess("http://localhost:3271/GoldenAccess.soap");
            LightInfocon.GoldenAccess.General.User usuario = new LightInfocon.GoldenAccess.General.User(nome, senha);
            try
            {
                usuario = goldenAccess.Authenticate(nome, senha);
            }
            catch (Exception ex)
            {
                if (!usuario.IsAuthenticated)
                {
                    string erro = ex.Message;
                    return 4;
                }
            }
            if (usuario.IsAuthenticated)
            {
                if (usuario.HasGroup("RHADM"))
                {
                    return 1;
                }
                else if (usuario.HasGroup("RHLIM"))
                {
                    return 2;
                }
                else
                {
                    return 3;
                }
            }
            else
            {
                return 5;
            }
        }

        /// <summary>
        /// Método usado para alterar a senha do usuário que esta logado.
        /// </summary>
        /// <param name="nome">O username.</param>
        /// <param name="novaSenha">A nova senha.</param>
        /// <returns>um booleano informando se foi auterada.</returns>
        public bool AlterarSenha(string nome, string novaSenha)
        {
            GoldenAccess servicoDeAutenticacao = new GoldenAccess("http://localhost:3271/GoldenAccess.soap");
            LightInfocon.GoldenAccess.General.User usuarioGoldenAccess = new LightInfocon.GoldenAccess.General.User(nome, novaSenha);
            try
            {
                usuarioGoldenAccess = servicoDeAutenticacao.Authenticate(nome, novaSenha);
            }
            catch (Exception e)
            {
                string erro = e.Message;
            }
            if (usuarioGoldenAccess.IsAuthenticated && usuarioGoldenAccess.IsAdm)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Método usado para obter o documento pelo id.
        /// </summary>
        /// <param name="valor">O id.</param>
        /// <returns></returns>
        public Documento obterDocumentoPorId(string valor)
        {
            IDataReader reader;
            IDbConnection minhaConexao = new LightBaseConnection("user=lbw;password=lbw;udb=defudb;server=localhost");
            try
            {
                minhaConexao.Open();
                Documento documento = new Documento();
                try
                {
                    IDbCommand meuComando = new LightBaseCommand("select id, matricula_Colaborador,nome_Colaborador, arquivos from documento where id = " + valor);
                    meuComando.Connection = minhaConexao;
                    reader = meuComando.ExecuteReader();
                    while (reader.Read())
                    {
                        documento.id = Convert.ToInt32(reader["id"]);
                        documento.matricula_Colaborador = Convert.ToString(reader["matricula_Colaborador"]);
                        documento.nome_Colaborador = Convert.ToString(reader["nome_Colaborador"]);
                        //ver como fazer conversao pra adicionar cada nome de arquivos a uma lista de arquivos
                        //eu faço um data table pra jogar os arquivos...ver medoto TODOS
                        DataTable dt_Arquivos = (DataTable)reader["arquivos"];
                        //string[] dados_Arquivos = new string[dt_Arquivos.Rows.Count];
                        List<Arquivo> list_arq = new List<Arquivo>();
                        for (int i = 0; i < dt_Arquivos.Rows.Count; i++)
                        {
                            Arquivo arquivo = new Arquivo();
                            arquivo.nome_Arquivo = dt_Arquivos.Rows[i]["nome_Arquivo"].ToString();
                            arquivo.conteudo_Arquivo = dt_Arquivos.Rows[i]["conteudo_Arquivo"].ToString();
                            arquivo.tipo_Arquivo = dt_Arquivos.Rows[i]["tipo_Arquivo"].ToString();
                            list_arq.Add(arquivo);
                        }
                        documento.arquivos = list_arq;
                    }
                }
                catch (LightBaseException e)
                {
                    string erro = e.Message;
                }
                return documento;
            }
            finally
            {
                minhaConexao.Close();
            }
        }

        //public List<Arquivo> RetornaArquivos(List<Arquivo> lista)
        //{
        //    List<Arquivo> items = new List<Arquivo>();

        //    //items = SessionPageStatePersister.
        //    for (int i = 0; i < 3; i++)
        //    {
        //        Arquivo arq = new Arquivo();
        //        arq.nome_Arquivo = "cpf" + i + ".pdf";
        //        items.Add(arq);
        //    }
        //    return items;
        //}

        /// <summary>
        /// Método usado para retornar a lista de arquivos passada.
        /// </summary>
        /// <param name="lista">Lista de arquivos.</param>
        /// <returns></returns>
        public List<Arquivo> RetornaArquivos(List<Arquivo> lista)
        {
            List<Arquivo> items = new List<Arquivo>();
            items = lista;
            return items;
        }

        /// <summary>
        /// Método que retorna toda a base de LOGs.
        /// </summary>
        /// <returns>a base de LOGs</returns>
        public List<Log> TodosLogs()
        {
            List<Log> lista = new List<Log>();
            IDataReader reader;
            IDbConnection minhaConexao = new LightBaseConnection("user=lbw;password=lbw;UDB=defudb;server=localhost");
            try
            {
                minhaConexao.Open();
                IDbCommand comando = new LightBaseCommand("select id, usuario_log , tipo_acao_log , mensagem_acao_log , data_log  from log");
                comando.Connection = minhaConexao;
                reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Log log = new Log();

                    log.id = Convert.ToInt16(reader["id"]);
                    log.usuario_log = Convert.ToString(reader["usuario_log"]);
                    log.tipo_acao_log = Convert.ToString(reader["tipo_acao_log"]);
                    log.mensagem_acao_log = Convert.ToString(reader["mensagem_acao_log"]);
                    System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("pt-BR");
                    log.data_log = Convert.ToDateTime(reader["data_log"], culture); 
                    lista.Add(log); 
                }
                return lista;
            }
            finally
            {
                minhaConexao.Close();
            }
        }

        /// <summary>
        /// Método usado para inserir um log na base de LOGs.
        /// </summary>
        /// <param name="log">O log a ser inserido.</param>
        public void InserirLog(Log log)
        {
            IDbConnection minhaConexao = new LightBaseConnection("user=lbw;password=lbw;udb=defudb;server=localhost");
            try
            {
                minhaConexao.Open();
                IDbCommand meuComando = new LightBaseCommand();
                string comando = "insert into log (usuario_log, tipo_acao_log, mensagem_acao_log, data_log)  "
                                + "values (@usuario_log, @tipo_acao_log, @mensagem_acao_log, @data_log)";
                meuComando.Connection = minhaConexao;
                meuComando.CommandText = comando;

                meuComando.Parameters.Add(new LightBaseParameter("usuario_log", log.usuario_log));
                meuComando.Parameters.Add(new LightBaseParameter("tipo_acao_log", log.tipo_acao_log));
                meuComando.Parameters.Add(new LightBaseParameter("mensagem_acao_log", log.mensagem_acao_log));
                meuComando.Parameters.Add(new LightBaseParameter("data_log", log.data_log));
                meuComando.ExecuteNonQuery();
            }
            finally
            {
                minhaConexao.Close();
            }
        }

        /// <summary>
        /// Método que ordena o LOG por coluna.
        /// </summary>
        /// <param name="select">O comando select.</param>
        /// <param name="coluna">A coluna a ser ordenada.</param>
        /// <param name="orderBy">O tipo da ordem.</param>
        /// <returns></returns>
        public List<Log> PorColunaLOG(string select, string coluna, string orderBy)
        {
            if (select == null)
            {
                select = "select id, usuario_log , tipo_acao_log , mensagem_acao_log , data_log  from log";
            }
            List<Log> lista = new List<Log>();
            IDataReader reader;
            IDbConnection minhaConexao = new LightBaseConnection("user=lbw;password=lbw;UDB=defudb;server=localhost");
            try
            {
                minhaConexao.Open();
                try
                {
                    IDbCommand comando = new LightBaseCommand(select + " order by " + coluna + " " + orderBy);
                    comando.Connection = minhaConexao;
                    reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        Log log = new Log();
                        log.id = Convert.ToInt16(reader["id"]);
                        log.usuario_log = Convert.ToString(reader["usuario_log"]);
                        log.tipo_acao_log = Convert.ToString(reader["tipo_acao_log"]);
                        log.mensagem_acao_log = Convert.ToString(reader["mensagem_acao_log"]);
                        System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("pt-BR");
                        log.data_log = Convert.ToDateTime(reader["data_log"].ToString(), culture);
                        lista.Add(log);
                    }
                }
                catch (LightBaseException e)
                {
                    string erro = e.Message;
                }
                return lista;
            }
            finally
            {
                minhaConexao.Close();
            }
        }

        /// <summary>
        /// Método usado para pesquisar determinado campo na base de LOG.
        /// </summary>
        /// <param name="comando">O comando a ser executado.</param>
        /// <returns></returns>
        public List<Log> PesquisarCamposLOG(string comando)
        {
            List<Log> lista = new List<Log>();
            IDataReader reader;
            IDbConnection minhaConexao = new LightBaseConnection("user=lbw;password=lbw;UDB=defudb;server=localhost");
            try
            {
                minhaConexao.Open();
                try
                {
                    IDbCommand meuComando = new LightBaseCommand(comando);
                    meuComando.Connection = minhaConexao;
                    reader = meuComando.ExecuteReader();

                    while (reader.Read())
                    {
                        Log log = new Log();
                        log.id = Convert.ToInt16(reader["id"]);
                        log.usuario_log = Convert.ToString(reader["usuario_log"]);
                        log.tipo_acao_log = Convert.ToString(reader["tipo_acao_log"]);
                        log.mensagem_acao_log = Convert.ToString(reader["mensagem_acao_log"]);
                        System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("pt-BR");
                        log.data_log = Convert.ToDateTime(reader["data_log"].ToString(), culture);
                        lista.Add(log);
                    }
                }
                catch (LightBaseException e)
                {
                    string erro = e.Message;
                }
                return lista;
            }
            finally
            {
                minhaConexao.Close();
            }
        }

        /// <summary>
        /// Método usado para verificar se existe a matricula.
        /// </summary>
        /// <param name="matricula">A matricula.</param>
        /// <returns>um booleado verificando se existe a matricula passada.</returns>
        public bool existeMatricula(string matricula)
        {
            IDataReader reader;
            IDbConnection minhaConexao = new LightBaseConnection("user=lbw;password=lbw;UDB=defudb;server=localhost");
            string command = "textsearch in documento " + matricula + "[matricula_Colaborador]";
            bool retorno = false;
            try
            {
                minhaConexao.Open();
                IDbCommand comando = new LightBaseCommand(command);
                comando.Connection = minhaConexao;
                reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    retorno = true;
                    break;
                }
                return retorno;
            }
            finally
            {
                minhaConexao.Close();
            }
        }

        /// <summary>
        ///  Método usado para verificar se existe um CPF.
        /// </summary>
        /// <param name="cpf">O CPF.</param>
        /// <returns>um booleado verificando se existe o CPF passado.</returns>
        public bool existeCPF(string cpf)
        {
            IDataReader reader;
            IDbConnection minhaConexao = new LightBaseConnection("user=lbw;password=lbw;UDB=defudb;server=localhost");
            string command = "textsearch in documento " + cpf + "[cpf_Colaborador]";
            bool retorno = false;
            try
            {
                minhaConexao.Open();
                IDbCommand comando = new LightBaseCommand(command);
                comando.Connection = minhaConexao;
                reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    retorno = true;
                    break;
                }
                return retorno;
            }
            finally
            {
                minhaConexao.Close();
            }
        }

        /// <summary>
        /// Método usado para verificar se existe um username já usado.
        /// </summary>
        /// <param name="login">O username.</param>
        /// <returns>um booleado verificando se existe o username passado.</returns>
        public bool existeLogin(string login)
        {
            IDataReader reader;
            IDbConnection minhaConexao = new LightBaseConnection("user=lbw;password=lbw;UDB=defudb;server=localhost");
            string command = "textsearch in usuario " + login + "[login_Usuario]";
            bool retorno = false;
            try
            {
                minhaConexao.Open();
                IDbCommand comando = new LightBaseCommand(command);
                comando.Connection = minhaConexao;
                reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    retorno = true;
                    break;
                }
                return retorno;
            }
            finally
            {
                minhaConexao.Close();
            }
        }

        /// <summary>
        /// Método usado para inserir um usuário no sistema e na base de usuario.
        /// </summary>
        /// <param name="usuario">O usuario a ser inserido.</param>
        public void addUsuario(Usuario usuario)
        {
            List<Permissoes> permissoes = new List<Permissoes>();
            permissoes = usuario.permissoes;
            IDbConnection minhaConexao = new LightBaseConnection("user=lbw;password=lbw;udb=defudb;server=localhost");
            try
            {
                minhaConexao.Open();
                IDbCommand meuComando = new LightBaseCommand();
                string comando = "insert into usuario (matricula_Usuario, login_Usuario, senha_Usuario, permissoes)  "
                                + "values (@matricula_Usuario, @login_Usuario, @senha_Usuario";
                meuComando.Connection = minhaConexao;
                for (int i = 0; i < permissoes.Count; i++)
                {
                    comando += ", ";
                    if (i == 0)
                    {
                        comando += "{";
                    }
                    comando += "{@opcao_permissao" + i + ",@tipo_permissao" + i + "}";
                    if (permissoes.Count == (i + 1))
                    {
                        comando += "}";
                    }

                    
                }
                comando += ")";
                meuComando.CommandText = comando;
                meuComando.Parameters.Add(new LightBaseParameter("matricula_Usuario", usuario.matricula));
                meuComando.Parameters.Add(new LightBaseParameter("login_Usuario", usuario.login));
                meuComando.Parameters.Add(new LightBaseParameter("senha_Usuario", usuario.password));
                int count = 0;
                foreach (var perm in permissoes)
                {
                    meuComando.Parameters.Add(new LightBaseParameter("opcao_permissao" + count, perm.opcao));
                    meuComando.Parameters.Add(new LightBaseParameter("tipo_permissao" + count, perm.tipo_permissao));
                    count++;
                }
                meuComando.ExecuteNonQuery();

                /***************** Insere usuario no goldenAcess *********************/
                comando = "insert into GoldenUsers ( Login, PassWord, Name, Disabled, NeedChangePassword, Type, RegistrationDate, RegistrationTime, OrganizationArea, IdOrganizationArea )" +
                          "values( @Login, @PassWord, @Name, @Disabled, @NeedChangePassword, @Type, @RegistrationDate, @RegistrationTime, @OrganizationArea, @IdOrganizationArea)";

                meuComando.CommandText = comando;
                //string data = DateTime.Now.ToString("dd/MM/yyyy");
                meuComando.Parameters.Add(new LightBaseParameter("Login", usuario.login));
                meuComando.Parameters.Add(new LightBaseParameter("PassWord", usuario.password));
                meuComando.Parameters.Add(new LightBaseParameter("Name", usuario.login + "_" +usuario.matricula));
                meuComando.Parameters.Add(new LightBaseParameter("Disabled", false));
                meuComando.Parameters.Add(new LightBaseParameter("NeedChangePassword", false));
                meuComando.Parameters.Add(new LightBaseParameter("Type", "USER"));
                meuComando.Parameters.Add(new LightBaseParameter("RegistrationDate", DateTime.Now.ToShortDateString()));
                meuComando.Parameters.Add(new LightBaseParameter("RegistrationTime", DateTime.Now.ToShortTimeString()));
                meuComando.Parameters.Add(new LightBaseParameter("OrganizationArea", "root"));
                meuComando.Parameters.Add(new LightBaseParameter("IdOrganizationArea", 1));
                meuComando.ExecuteNonQuery();

                /***************** Coloca usuario em um grupo *********************/
                comando = "update GoldenUsers set Groups = {@Grupo} where Login = @Login";
                meuComando.CommandText = comando;
                meuComando.Parameters.Add(new LightBaseParameter("Login", usuario.login));
                //string[] grupos = new string[2];
                //grupos[0] = "TODOS";
                //grupos[1] = "RHLIM";

                meuComando.Parameters.Add(new LightBaseParameter("Grupo", "RHLIM , TODOS"));
                meuComando.ExecuteNonQuery();
                
            }
            finally
            {
                minhaConexao.Close();
            }
        }

        /// <summary>
        /// Método usado para retornar o usuario através do seu username.
        /// </summary>
        /// <param name="login">O username.</param>
        /// <returns></returns>
        public Usuario retornaUsuario(string login)
        {
            Usuario usuario = new Usuario();
            
            IDataReader reader;
            IDbConnection minhaConexao = new LightBaseConnection("user=lbw;password=lbw;UDB=defudb;server=localhost");
            try
            {
                minhaConexao.Open();
                IDbCommand comando = new LightBaseCommand("select permissoes from usuario where login_Usuario = @login_Usuario");
                comando.Parameters.Add(new LightBaseParameter("login_Usuario", login));
                comando.Connection = minhaConexao;
                reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    DataTable dt_Permissoes = (DataTable)reader["permissoes"];
                    List<Permissoes> listaPermissoes = new List<Permissoes>();
                    for (int i = 0; i < dt_Permissoes.Rows.Count; i++)
                    {
                        Permissoes perm = new Permissoes();
                        perm.opcao = dt_Permissoes.Rows[i]["opcao_permissao"].ToString();
                        perm.tipo_permissao = Convert.ToInt16(dt_Permissoes.Rows[i]["tipo_permissao"].ToString());
                        listaPermissoes.Add(perm);
                    }
                    usuario.permissoes = listaPermissoes;
                }
            }
            finally
            {
                minhaConexao.Close();
            }

            return usuario;
        }

        /// <summary>
        /// Método usado para retornar uma lista de LOGs filtrada por um intervalo de datas.
        /// </summary>
        /// <param name="inicio">The inicio.</param>
        /// <param name="final">The final.</param>
        /// <returns></returns>
        public List<Log> intervaloDatasLogs(string inicio, string final)
        {

            List<Log> lista = new List<Log>();
            IDataReader reader;
            IDbConnection minhaConexao = new LightBaseConnection("user=lbw;password=lbw;UDB=defudb;server=localhost");
            try
            {
                minhaConexao.Open();
                IDbCommand comando = new LightBaseCommand();
                string comandoData = "select id, usuario_log , tipo_acao_log , mensagem_acao_log , data_log  from log where data_log >= @dataInicio and data_log <= @dataFinal";
                comando.CommandText = comandoData;
                
                comando.Parameters.Add(new LightBaseParameter("dataInicio", inicio));
                comando.Parameters.Add(new LightBaseParameter("dataFinal", final));
                comando.Connection = minhaConexao;
                reader = comando.ExecuteReader();

                //Fazer parameters pra passar o intervalor de datas pra fazer a busca
               
                while (reader.Read())
                {
                    Log log = new Log();

                    log.id = Convert.ToInt16(reader["id"]);
                    log.usuario_log = Convert.ToString(reader["usuario_log"]);
                    log.tipo_acao_log = Convert.ToString(reader["tipo_acao_log"]);
                    log.mensagem_acao_log = Convert.ToString(reader["mensagem_acao_log"]);
                    log.data_log = Convert.ToDateTime(reader["data_log"]);
                    lista.Add(log);
                }
                return lista;
            }
            finally
            {
                minhaConexao.Close();
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
        /// Método usado para obter o último id cadastrado.
        /// </summary>
        /// <returns>uma string com o ultimo id cadastrado.</returns>
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
        /// Método usado para obter os arquivos através do id do documento.
        /// </summary>
        /// <param name="id">O id do documento.</param>
        /// <returns>a lista com os arquivos do determinado documento.</returns>
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

        /// <summary>
        /// Método usado para montar o formato do nome dos arquivos de titulação.
        /// </summary>
        /// <param name="nomeArquivo">O nome do arquivo original.</param>
        /// <param name="matricula">A matricula.</param>
        /// <param name="tipo">O tipo de titulação.</param>
        /// <returns>o nome formatado.</returns>
        public string montaFormatoTitulacao(string nomeArquivo, string matricula, string tipo)
        {
            nomeArquivo = nomeArquivo.Substring(0, nomeArquivo.LastIndexOf(".pdf"));
            nomeArquivo = matricula + "-" + nomeArquivo + "-" + tipo + ".pdf";
            return nomeArquivo;
        }

    }
}
