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

namespace GED_TCESE
{
    public class Adaptador : IAdaptador
    {
        public List<Processo> PesquisaPorCampo(string comando)
        {
            List<Processo> lista = new List<Processo>();
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
                        Processo processo = new Processo();
                        processo.id = Convert.ToInt16(reader["id"]);
                        processo.arq_Arquivo = Convert.ToString(reader["arq_Arquivo"]);
                        processo.decisao = Convert.ToString(reader["decisao"]);
                        processo.numero_Processo = Convert.ToString(reader["numero_Processo"]);
                        processo.ano_Processo = Convert.ToString(reader["Ano_Processo"]);
                        processo.origem = Convert.ToString(reader["origem"]);
                        processo.assunto = Convert.ToString(reader["assunto"]);
                        processo.descricao = Convert.ToString(reader["descricao"]);
                        DataTable dt = (DataTable)reader["interessados"];
                        string[] nome = { "", "", "", "" };
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            nome[i] = dt.Rows[i]["nome"].ToString();
                        }
                        processo.pessoa1 = nome[0].ToString();
                        processo.pessoa2 = nome[1].ToString();
                        processo.pessoa3 = nome[2].ToString();
                        processo.pessoa4 = nome[3].ToString();
                        lista.Add(processo);
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

        public List<Processo> PesquisarCampos(string comando)
        {
            List<Processo> lista = new List<Processo>();
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
                        Processo processo = new Processo();
                        processo.id = Convert.ToInt16(reader["id"]);
                        processo.decisao = Convert.ToString(reader["decisao"]);
                        processo.numero_Processo = Convert.ToString(reader["numero_Processo"]);
                        processo.ano_Processo = Convert.ToString(reader["ano_Processo"]);
                        processo.origem = Convert.ToString(reader["origem"]);
                        processo.assunto = Convert.ToString(reader["assunto"]);
                        processo.descricao = Convert.ToString(reader["descricao"]);
                        DataTable dt = (DataTable)reader["interessados"];
                        string[] nome = { "", "", "", "" };
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            nome[i] = dt.Rows[i]["nome"].ToString();
                        }
                        processo.pessoa1 = nome[0].ToString();
                        processo.pessoa2 = nome[1].ToString();
                        processo.pessoa3 = nome[2].ToString();
                        processo.pessoa4 = nome[3].ToString();
                        lista.Add(processo);
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

        public List<Processo>Todos()
        {
            List<Processo> lista = new List<Processo>();
            IDataReader reader;
            IDbConnection minhaConexao = new LightBaseConnection("user=lbw;password=lbw;UDB=defudb;server=localhost");
            try
            {
                minhaConexao.Open();

                IDbCommand comando = new LightBaseCommand("select id, arq_Arquivo, decisao, numero_Processo, ano_Processo, origem, assunto, descricao, interessados.nome from tcejurisprudencia");
                comando.Connection = minhaConexao;
                reader = comando.ExecuteReader();
                
                while (reader.Read())
                {
                    Processo processo = new Processo();
                    processo.id = Convert.ToInt32(reader["id"]);
                    processo.arq_Arquivo = Convert.ToString(reader["arq_Arquivo"]);
                    processo.decisao = Convert.ToString(reader["decisao"]);
                    processo.numero_Processo = Convert.ToString(reader["numero_Processo"]);
                    processo.ano_Processo = Convert.ToString(reader["ano_Processo"]);
                    processo.origem = Convert.ToString(reader["origem"]);
                    processo.assunto = Convert.ToString(reader["assunto"]);
                    processo.descricao = Convert.ToString(reader["descricao"]);
                    DataTable dt = (DataTable)reader["interessados"];
                    string[] nome = {"", "", "", ""};
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        nome[i] = dt.Rows[i]["nome"].ToString();
                    }
                    processo.pessoa1 = nome[0].ToString();
                    processo.pessoa2 = nome[1].ToString();
                    processo.pessoa3 = nome[2].ToString();
                    processo.pessoa4 = nome[3].ToString();
                    lista.Add(processo);
                }
                return lista;
            }
            finally
            {
                minhaConexao.Close();
            }
        }

        public List<Processo> porColuna(string select, string coluna, string orderBy)
        {
            if (select == null)
            {
                select = "select id, arq_Arquivo, decisao, numero_Processo, ano_Processo, origem, assunto, descricao, interessados.nome from tcejurisprudencia";
            }
            List<Processo> lista = new List<Processo>();
            IDataReader reader;
            IDbConnection minhaConexaoexao = new LightBaseConnection("user=lbw;password=lbw;UDB=defudb;server=localhost");
            try
            {
                if (coluna == "pessoa1")
                {
                    coluna = "nome";
                }
                else if (coluna == "pessoa2")
                {
                    coluna = "nome";
                }
                else if (coluna == "pessoa3")
                {
                    coluna = "nome";
                }
                else if (coluna == "pessoa4")
                {
                    coluna = "nome";
                }
                minhaConexaoexao.Open();
                try
                {
                    IDbCommand comando = new LightBaseCommand(select + " order by " + coluna + " " + orderBy);
                    comando.Connection = minhaConexaoexao;
                    reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        Processo processo = new Processo();
                        processo.id = Convert.ToInt32(reader["id"]);
                        processo.arq_Arquivo = Convert.ToString(reader["arq_Arquivo"]);
                        processo.decisao = Convert.ToString(reader["decisao"]);
                        processo.numero_Processo = Convert.ToString(reader["numero_Processo"]);
                        processo.ano_Processo = Convert.ToString(reader["ano_Processo"]);
                        processo.origem = Convert.ToString(reader["origem"]);
                        processo.assunto = Convert.ToString(reader["assunto"]);
                        processo.descricao = Convert.ToString(reader["descricao"]);
                        DataTable dt = (DataTable)reader["interessados"];
                        string[] nome = { "", "", "", "" };
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            nome[i] = dt.Rows[i]["nome"].ToString();
                        }
                        processo.pessoa1 = nome[0].ToString();
                        processo.pessoa2 = nome[1].ToString();
                        processo.pessoa3 = nome[2].ToString();
                        processo.pessoa4 = nome[3].ToString();
                        lista.Add(processo);
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
                minhaConexaoexao.Close();
            }
        }

        public void AtualizaProcesso(Processo processo)
        {
            IDbConnection minhaConexaoexao = new LightBaseConnection("user=lbw;password=lbw;UDB=defudb;server=localhost");
            try
            {
                minhaConexaoexao.Open();
                IDbCommand comando = new LightBaseCommand("update tcese set arq_Arquivo=@arq_Arquivo, numero_Processo=@numero_Processo, ano_Processo=@ano_Processo, origem=@origem, assunto=@assunto, descricao=@descricao, interessados={{@pessoa1}, {@pessoa2}, {@pessoa3}, {@pessoa4}} where id=@id");
                comando.Parameters.Add(new LightBaseParameter("arq_Arquivo", processo.arq_Arquivo));
                comando.Parameters.Add(new LightBaseParameter("decisao", processo.decisao));
                comando.Parameters.Add(new LightBaseParameter("numero_Processo", processo.numero_Processo));
                comando.Parameters.Add(new LightBaseParameter("ano_Processo", processo.ano_Processo));
                comando.Parameters.Add(new LightBaseParameter("origem", processo.origem));
                comando.Parameters.Add(new LightBaseParameter("assunto", processo.assunto));
                comando.Parameters.Add(new LightBaseParameter("descricao", processo.descricao));
                comando.Parameters.Add(new LightBaseParameter("pessoa1", processo.pessoa1));
                comando.Parameters.Add(new LightBaseParameter("pessoa2", processo.pessoa2));
                comando.Parameters.Add(new LightBaseParameter("pessoa3", processo.pessoa3));
                comando.Parameters.Add(new LightBaseParameter("pessoa4", processo.pessoa4));
                comando.Parameters.Add(new LightBaseParameter("id", processo.id));
                comando.Connection = minhaConexaoexao;
                comando.ExecuteNonQuery();
            }
            finally
            {
                minhaConexaoexao.Close();
            }
        }

        public void InsereProcesso(Processo processo)
        {
            IDbConnection minhaConexao = new LightBaseConnection("user=lbw;password=lbw;udb=defudb;server=localhost");
            try
            {
                minhaConexao.Open();

                IDbCommand meuComando = new LightBaseCommand();

                if (processo.qtdPessoas == 4)
                {
                    meuComando.CommandText = "tcese (arq_Arquivo:@arq_Arquivo, numero_Processo:@numero_Processo, ano_Processo:@ano_Processo, origem:@origem, assunto:@assunto, descricao:@descricao, interessados:{{@pessoa1}, {@pessoa2}, {@pessoa3}, {@pessoa4}})";
                }
                else if (processo.qtdPessoas == 3)
                {
                    meuComando.CommandText = "tcese (arq_Arquivo:@arq_Arquivo, numero_Processo:@numero_Processo, ano_Processo:@ano_Processo, origem:@origem, assunto:@assunto, descricao:@descricao, interessados:{{@pessoa1}, {@pessoa2}, {@pessoa3}})";
                }
                else if (processo.qtdPessoas == 2)
                {
                    meuComando.CommandText = "tcese (arq_Arquivo:@arq_Arquivo, numero_Processo:@numero_Processo, ano_Processo:@ano_Processo, origem:@origem, assunto:@assunto, descricao:@descricao, interessados:{{@pessoa1}, {@pessoa2}})";
                }
                else if (processo.qtdPessoas == 1)
                {
                    meuComando.CommandText = "tcese (arq_Arquivo:@arq_Arquivo, numero_Processo:@numero_Processo, ano_Processo:@ano_Processo, origem:@origem, assunto:@assunto, descricao:@descricao, interessados:{{@pessoa1}})";
                }
                meuComando.Connection = minhaConexao;
                meuComando.Parameters.Add(new LightBaseParameter("arq_Arquivo", processo.arq_Arquivo));
                meuComando.Parameters.Add(new LightBaseParameter("numero_Processo", processo.numero_Processo));
                meuComando.Parameters.Add(new LightBaseParameter("ano_Processo", processo.ano_Processo));
                meuComando.Parameters.Add(new LightBaseParameter("origem", processo.origem));
                meuComando.Parameters.Add(new LightBaseParameter("assunto", processo.assunto));
                meuComando.Parameters.Add(new LightBaseParameter("descricao", processo.descricao));
                meuComando.Parameters.Add(new LightBaseParameter("pessoa1", processo.pessoa1));
                meuComando.Parameters.Add(new LightBaseParameter("pessoa2", processo.pessoa2));
                meuComando.Parameters.Add(new LightBaseParameter("pessoa3", processo.pessoa3));
                meuComando.Parameters.Add(new LightBaseParameter("pessoa4", processo.pessoa4));
                meuComando.ExecuteNonQuery();
            }
            finally
            {
                minhaConexao.Close();
            }           
        }

        public void RemoveProcesso(int id)
        {
            IDbConnection minhaConexaoexao = new LightBaseConnection("user=lbw;password=lbw;UDB=defudb;server=localhost");
            try
            {
                minhaConexaoexao.Open();
                IDbCommand comando = new LightBaseCommand("delete from tcese where id=@id");
                comando.Parameters.Add(new LightBaseParameter("id", id));
                comando.Connection = minhaConexaoexao;
                comando.ExecuteNonQuery();
            }
            finally
            {
                minhaConexaoexao.Close();
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
            catch(Exception ex)
            {
                if (!usuario.IsAuthenticated)
                {
                    string erro = ex.Message;
                    return 4;
                }
            }
            if (usuario.IsAuthenticated)
            {
                if (usuario.HasGroup("TCESEADM"))
                {
                    return 1;
                }
                else if (usuario.HasGroup("TCESELIM"))
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

        public Processo obterProcessoPorId(string valor)
        {
            IDataReader reader;
            IDbConnection minhaConexao = new LightBaseConnection("user=lbw;password=lbw;udb=defudb;server=localhost");
            try
            {
                minhaConexao.Open();
                Processo processo = new Processo();
                try
                {
                    IDbCommand meuComando = new LightBaseCommand("select first id, arq_Arquivo, decisao from tcejurisprudencia where id = " + valor);
                    meuComando.Connection = minhaConexao;
                    reader = meuComando.ExecuteReader();
                    while (reader.Read())
                    {
                        processo.id = Convert.ToInt32(reader["id"]);
                        processo.arq_Arquivo = Convert.ToString(reader["arq_Arquivo"]);
                        processo.decisao = Convert.ToString(reader["decisao"]);
                    }
                }
                catch (LightBaseException e)
                {
                    string erro = e.Message;
                }
                return processo;
            }
            finally
            {
                minhaConexao.Close();
            }
        }
    }
}