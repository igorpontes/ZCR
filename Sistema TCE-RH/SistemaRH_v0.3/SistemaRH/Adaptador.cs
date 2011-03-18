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
    public class Adaptador : IAdaptador
    {
        public List<Documento> PesquisaPorCampo(string comando)
        {
            List<Documento> lista = new List<Documento>();
            IDataReader reader;
            IDbConnection minhaConexao = new LightBaseConnection("user=lbw;password=lbw;udb=DEFUDB;server=localhost");
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
                        Arquivo arquivo = new Arquivo();

                        documento.id = Convert.ToInt16(reader["id"]);
                        documento.matricula_Colaborador = Convert.ToString(reader["matricula_Colaborador"]);
                        documento.nome_Colaborador = Convert.ToString(reader["nome_Colaborador"]);

                        DataTable dt_Arquivos = (DataTable)reader["arquivos"];
                        string[] dados_Arquivos = { "", "", "" };
                        for (int i = 0; i < dt_Arquivos.Rows.Count; i++)
                        {
                            dados_Arquivos[i] = dt_Arquivos.Rows[i]["nome_Arquivo"].ToString();
                            dados_Arquivos[i] = dt_Arquivos.Rows[i]["conteudo_Arquivo"].ToString();
                        }
                        arquivo.nome_Arquivo = dados_Arquivos[0].ToString();
                        arquivo.conteudo_Arquivo = dados_Arquivos[0].ToString();

                        lista.Add(documento);
                    }
                }
                catch (LightBaseException e)
                {
                    string texto = e.Message;
                }
                return lista;
            }
            finally
            {
                minhaConexao.Close();
            }
        }

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
                        Arquivo arquivo = new Arquivo();

                        documento.id = Convert.ToInt16(reader["id"]);
                        documento.matricula_Colaborador = Convert.ToString(reader["matricula_Colaborador"]);
                        documento.nome_Colaborador = Convert.ToString(reader["nome_Colaborador"]);

                        DataTable dt_Arquivos = (DataTable)reader["arquivos"];
                        string[] dados_Arquivos = { "", "", "" };
                        for (int i = 0; i < dt_Arquivos.Rows.Count; i++)
                        {
                            dados_Arquivos[i] = dt_Arquivos.Rows[i]["nome_Arquivo"].ToString();
                            dados_Arquivos[i] = dt_Arquivos.Rows[i]["conteudo_Arquivo"].ToString();
                        }
                        arquivo.nome_Arquivo = dados_Arquivos[0].ToString();
                        arquivo.conteudo_Arquivo = dados_Arquivos[0].ToString();


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

        public List<Documento> Todos()
        {
            List<Documento> lista = new List<Documento>();
            IDataReader reader;
            IDbConnection minhaConexao = new LightBaseConnection("user=lbw;password=lbw;UDB=defudb;server=localhost");
            try
            {
                minhaConexao.Open();

                IDbCommand comando = new LightBaseCommand("select id, matricula_Colaborador, nome_Colaborador, arquivos.nome_Arquivo, "
                                                        + "arquivos.conteudo_Arquivo from documento");
                comando.Connection = minhaConexao;
                reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Documento documento = new Documento();
                    Arquivo arquivo = new Arquivo();

                    documento.id = Convert.ToInt16(reader["id"]);
                    documento.matricula_Colaborador = Convert.ToString(reader["matricula_Colaborador"]);
                    documento.nome_Colaborador = Convert.ToString(reader["nome_Colaborador"]);

                    DataTable dt_Arquivos = (DataTable)reader["arquivos"];
                    string[] dados_Arquivos = { "", "", "" };
                    for (int i = 0; i < dt_Arquivos.Rows.Count; i++)
                    {
                        dados_Arquivos[i] = dt_Arquivos.Rows[i]["nome_Arquivo"].ToString();
                        dados_Arquivos[i] = dt_Arquivos.Rows[i]["conteudo_Arquivo"].ToString();
                    }
                    arquivo.nome_Arquivo = dados_Arquivos[0].ToString();
                    arquivo.conteudo_Arquivo = dados_Arquivos[0].ToString();



                    lista.Add(documento);
                }
                return lista;
            }
            finally
            {
                minhaConexao.Close();
            }
        }

        public List<Documento> PorColuna(string coluna)
        {
            //Ver como faz aqui pra nao precisar ordenar por colunas na parte dos documentos. Só por nome e matricula
            List<Documento> lista = new List<Documento>();
            IDataReader reader;
            IDbConnection minhaConexao = new LightBaseConnection("user=lbw;password=lbw;UDB=defudb;server=localhost");
            try
            {
                minhaConexao.Open();
                try
                {
                    IDbCommand comando = new LightBaseCommand("select id, matricula_Colaborador, nome_Colaborador, arquivos.nome_Arquivo, "
                                                        + "arquivos.conteudo_Arquivo from documento order by " + coluna);
                    comando.Connection = minhaConexao;
                    reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        Documento documento = new Documento();
                        Arquivo arquivo = new Arquivo();

                        documento.id = Convert.ToInt16(reader["id"]);
                        documento.matricula_Colaborador = Convert.ToString(reader["matricula_Colaborador"]);
                        documento.nome_Colaborador = Convert.ToString(reader["nome_Colaborador"]);

                        DataTable dt_Arquivos = (DataTable)reader["arquivos"];
                        string[] dados_Arquivos = { "", "", "" };
                        for (int i = 0; i < dt_Arquivos.Rows.Count; i++)
                        {
                            dados_Arquivos[i] = dt_Arquivos.Rows[i]["nome_Arquivo"].ToString();
                            dados_Arquivos[i] = dt_Arquivos.Rows[i]["conteudo_Arquivo"].ToString();
                        }
                        arquivo.nome_Arquivo = dados_Arquivos[0].ToString();
                        arquivo.conteudo_Arquivo = dados_Arquivos[0].ToString();

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

        public void AtualizarDocumento(Documento documento)
        {
            List<Arquivo> arquivo = new List<Arquivo>();
            arquivo = documento.arquivos;
            IDbConnection minhaConexao = new LightBaseConnection("user=lbw;password=lbw;UDB=defudb;server=localhost");
            try
            {
                minhaConexao.Open();
                IDbCommand meuComando = new LightBaseCommand();
                string comando = "update documento set matricula_Colaborador=@matricula_Colaborador, "
                    + "nome_Colaborador=@nome_Colaborador";

                meuComando.Connection = minhaConexao;
                for (int i = 0; i < arquivo.Count; i++)
                {
                    comando += ",";
                    if (i == 0)
                    {
                        comando += "arquivos={";
                    }
                    comando += "{@nome_Arquivo" + i + ",@conteudo_Arquivo" + i + ",@tipo_Arquivo" + i + "}";
                    if (arquivo.Count == (i + 1))
                    {
                        comando += "}";
                    }
                }
                comando += " where id=@id)";
                meuComando.CommandText = comando;

                meuComando.Parameters.Add(new LightBaseParameter("matricula_Colaborador", documento.matricula_Colaborador));
                meuComando.Parameters.Add(new LightBaseParameter("nome_Colaborador", documento.nome_Colaborador));
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

        public void InserirDocumento(Documento documento)
        {
            List<Arquivo> arquivo = new List<Arquivo>();
            arquivo = documento.arquivos;
            IDbConnection minhaConexao = new LightBaseConnection("user=lbw;password=lbw;udb=defudb;server=localhost");
            try
            {
                minhaConexao.Open();
                IDbCommand meuComando = new LightBaseCommand();
                string comando = "insert into documento (matricula_Colaborador,nome_Colaborador,arquivos)  "
                                + "values (@matricula_Colaborador,@nome_Colaborador";
                meuComando.Connection = minhaConexao;
                for (int i = 0; i < arquivo.Count; i++)
                {
                    comando += ",";
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
                meuComando.Parameters.Add(new LightBaseParameter("nome_Colaborador", documento.nome_Colaborador));
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
                else if (usuario.HasGroup("RHADM"))
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
                    IDbCommand meuComando = new LightBaseCommand("select id, arquivos from documento where id = " + valor);
                    meuComando.Connection = minhaConexao;
                    reader = meuComando.ExecuteReader();
                    while (reader.Read())
                    {
                        documento.id = Convert.ToInt32(reader["id"]);
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
        public List<Arquivo> RetornaArquivos(List<Arquivo> lista)
        {
            List<Arquivo> items = new List<Arquivo>();
            items = lista;
            return items;
        }
    }
}
