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
    public class Adaptador: IAdaptador
    {
        public List<Pessoa> PesquisaPorCampo(string comando)
        {
            List<Pessoa> lista = new List<Pessoa>();
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
                        Pessoa pessoa = new Pessoa();
                        Endereco endereco = new Endereco();
                        Telefone telefones = new Telefone();
                        
                        pessoa.id = Convert.ToInt16(reader["id"]);
                        pessoa.arq_Arquivo = Convert.ToString(reader["arq_Arquivo"]);
                        pessoa.nome_Colaborador = Convert.ToString(reader["nome_Colaborador"]);
                        pessoa.naturalidade = Convert.ToString(reader["naturalidade"]);
                        pessoa.data_Nascimento = Convert.ToDateTime(reader["data_Nascimento"]);
                        pessoa.sexo = Convert.ToChar(reader["sexo"]);
                        pessoa.nome_Pai = Convert.ToString(reader["nome_Pai"]);
                        pessoa.nome_Mae = Convert.ToString(reader["nome_Mae"]);
                        pessoa.cargo = Convert.ToString(reader["cargo"]);
                        DataTable dt_Enderecos = (DataTable)reader["endereco"];
                        string[] dados_Endereco = { "", "", "" };
                        for (int i = 0; i < dt_Enderecos.Rows.Count; i++)
                        {
                            dados_Endereco[i] = dt_Enderecos.Rows[i]["endereco"].ToString();
                            dados_Endereco[i] = dt_Enderecos.Rows[i]["numero"].ToString();
                            dados_Endereco[i] = dt_Enderecos.Rows[i]["complemento"].ToString();
                            dados_Endereco[i] = dt_Enderecos.Rows[i]["bairro"].ToString();
                            dados_Endereco[i] = dt_Enderecos.Rows[i]["cidade"].ToString();
                            dados_Endereco[i] = dt_Enderecos.Rows[i]["estado"].ToString();
                        }
                        endereco.endereco = dados_Endereco[0].ToString();
                        endereco.numero = dados_Endereco[0].ToString();
                        endereco.complemento = dados_Endereco[0].ToString();
                        endereco.bairro = dados_Endereco[0].ToString();
                        endereco.cidade = dados_Endereco[0].ToString();
                        endereco.estado = dados_Endereco[0].ToString();
                        DataTable dt_Telefones = (DataTable)reader["telefones"];
                        string[] dados_Telefone = { "", "", "" };
                        for (int i = 0; i < dt_Telefones.Rows.Count; i++)
                        {
                            dados_Telefone[i] = dt_Telefones.Rows[i]["numero_Telefone"].ToString();
                        }
                        telefones.numero_TelefoneFixo = dados_Telefone[0].ToString();
                        telefones.numero_TelefoneCelular = dados_Telefone[1].ToString();
                        

                        lista.Add(pessoa);
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

        public List<Pessoa> PesquisarCampos(string comando)
        {
            List<Pessoa> lista = new List<Pessoa>();
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
                        Pessoa pessoa = new Pessoa();
                        Endereco endereco = new Endereco();
                        Telefone telefones = new Telefone();
                        
                        pessoa.id = Convert.ToInt16(reader["id"]);
                        pessoa.arq_Arquivo = Convert.ToString(reader["arq_Arquivo"]);
                        
                        pessoa.nome_Colaborador = Convert.ToString(reader["nome_Colaborador"]);
                        pessoa.naturalidade = Convert.ToString(reader["naturalidade"]);
                        pessoa.data_Nascimento = Convert.ToDateTime(reader["data_Nascimento"]);
                        pessoa.sexo = Convert.ToChar(reader["sexo"]);
                        pessoa.nome_Pai = Convert.ToString(reader["nome_Pai"]);
                        pessoa.nome_Mae = Convert.ToString(reader["nome_Mae"]);
                        pessoa.cargo = Convert.ToString(reader["cargo"]);
                       
                        DataTable dt_Endereco = (DataTable)reader["endereco"];
                        string[] dados_Endereco = { "", "", "" };
                        for (int i = 0; i < dt_Endereco.Rows.Count; i++)
                        {
                            dados_Endereco[i] = dt_Endereco.Rows[i]["endereco"].ToString();
                            dados_Endereco[i] = dt_Endereco.Rows[i]["numero"].ToString();
                            dados_Endereco[i] = dt_Endereco.Rows[i]["complemento"].ToString();
                            dados_Endereco[i] = dt_Endereco.Rows[i]["bairro"].ToString();
                            dados_Endereco[i] = dt_Endereco.Rows[i]["cidade"].ToString();
                            dados_Endereco[i] = dt_Endereco.Rows[i]["estado"].ToString();
                        }
                        endereco.endereco = dados_Endereco[0].ToString();
                        endereco.numero = dados_Endereco[0].ToString();
                        endereco.complemento = dados_Endereco[0].ToString();
                        endereco.bairro = dados_Endereco[0].ToString();
                        endereco.cidade = dados_Endereco[0].ToString();
                        endereco.estado = dados_Endereco[0].ToString();

                        DataTable dt_Telefones = (DataTable)reader["telefones"];
                        string[] dados_Telefone = { "", "", "" };
                        for (int i = 0; i < dt_Telefones.Rows.Count; i++)
                        {
                            dados_Telefone[i] = dt_Telefones.Rows[i]["numero_Telefone"].ToString();
                        }
                        telefones.numero_TelefoneFixo = dados_Telefone[0].ToString();
                        telefones.numero_TelefoneCelular = dados_Telefone[1].ToString();

                       
                        lista.Add(pessoa);
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

        public List<Pessoa> Todos()
        {
            List<Pessoa> lista = new List<Pessoa>();
            IDataReader reader;
            IDbConnection minhaConexao = new LightBaseConnection("user=lbw;password=lbw;UDB=defudb;server=localhost");
            try
            {
                minhaConexao.Open();

                IDbCommand comando = new LightBaseCommand("select id, arq_Arquivo, nome_Colaborador, naturalidade, data_Nascimento, "
                                                        + "sexo, nome_Pai, nome_Mae, cargo, endereco.endereco, "
                                                        + "endereco.numero, endereco.complemento, endereco.bairro, endereco.cidade, "
                                                        + "endereco.estado, telefones.numero_Telefone from pessoa");
                comando.Connection = minhaConexao;
                reader = comando.ExecuteReader();
                
                while (reader.Read())
                {
                    Pessoa pessoa = new Pessoa();
                    Endereco endereco = new Endereco();
                    Telefone telefones = new Telefone();
                   
                    pessoa.id = Convert.ToInt16(reader["id"]);
                    pessoa.arq_Arquivo = Convert.ToString(reader["arq_Arquivo"]);
                    
                    pessoa.nome_Colaborador = Convert.ToString(reader["nome_Colaborador"]);
                    pessoa.naturalidade = Convert.ToString(reader["naturalidade"]);
                    pessoa.data_Nascimento = Convert.ToDateTime(reader["data_Nascimento"]);
                    pessoa.sexo = Convert.ToChar(reader["sexo"]);
                    pessoa.nome_Pai = Convert.ToString(reader["nome_Pai"]);
                    pessoa.nome_Mae = Convert.ToString(reader["nome_Mae"]);
                    pessoa.cargo = Convert.ToString(reader["cargo"]);
                    
                    DataTable dt_Endereco = (DataTable)reader["endereco"];
                    string[] dados_Endereco = { "", "", "" };
                    for (int i = 0; i < dt_Endereco.Rows.Count; i++)
                    {
                        dados_Endereco[i] = dt_Endereco.Rows[i]["endereco"].ToString();
                        dados_Endereco[i] = dt_Endereco.Rows[i]["numero"].ToString();
                        dados_Endereco[i] = dt_Endereco.Rows[i]["complemento"].ToString();
                        dados_Endereco[i] = dt_Endereco.Rows[i]["bairro"].ToString();
                        dados_Endereco[i] = dt_Endereco.Rows[i]["cidade"].ToString();
                        dados_Endereco[i] = dt_Endereco.Rows[i]["estado"].ToString();
                    }
                    endereco.endereco = dados_Endereco[0].ToString();
                    endereco.numero = dados_Endereco[0].ToString();
                    endereco.complemento = dados_Endereco[0].ToString();
                    endereco.bairro = dados_Endereco[0].ToString();
                    endereco.cidade = dados_Endereco[0].ToString();
                    endereco.estado = dados_Endereco[0].ToString();

                    DataTable dt_Telefones = (DataTable)reader["telefones"];
                    string[] dados_Telefone = { "", "", "" };
                    for (int i = 0; i < dt_Telefones.Rows.Count; i++)
                    {
                        dados_Telefone[i] = dt_Telefones.Rows[i]["numero_Telefone"].ToString();
                    }
                    telefones.numero_TelefoneFixo = dados_Telefone[0].ToString();
                    telefones.numero_TelefoneCelular = dados_Telefone[1].ToString();

                   

                    lista.Add(pessoa);
                }
                return lista;
            }
            finally
            {
                minhaConexao.Close();
            }
        }

        public List<Pessoa> PorColuna(string coluna)
        {
            List<Pessoa> lista = new List<Pessoa>();
            IDataReader reader;
            IDbConnection minhaConexaoexao = new LightBaseConnection("user=lbw;password=lbw;UDB=defudb;server=localhost");
            try
            {
                if (coluna == "numero_Telefone1")
                {
                    coluna = "numero_Telefone";
                }
                else if (coluna == "numero_Telefone2")
                {
                    coluna = "numero_Telefone";
                }
                else if (coluna == "numero_Telefone3")
                {
                    coluna = "numero_Telefone";
                }
                
                
                minhaConexaoexao.Open();
                try
                {
                    IDbCommand comando = new LightBaseCommand("select id, arq_Arquivo,  nome_Colaborador, naturalidade, data_Nascimento, "
                                                            + "sexo, nome_Pai, nome_Mae, cargo, endereco.endereco, "
                                                            + "endereco.numero, endereco.complemento, endereco.bairro, endereco.cidade, "
                                                            + "endereco.estado, telefones.numero_Telefone from pessoa "
                                                            + "order by " + coluna);
                    comando.Connection = minhaConexaoexao;
                    reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        Pessoa pessoa = new Pessoa();
                        Endereco endereco = new Endereco();
                        Telefone telefones = new Telefone();
                       
                        pessoa.id = Convert.ToInt16(reader["id"]);
                        pessoa.arq_Arquivo = Convert.ToString(reader["arq_Arquivo"]);
                        
                        pessoa.nome_Colaborador = Convert.ToString(reader["nome_Colaborador"]);
                        pessoa.naturalidade = Convert.ToString(reader["naturalidade"]);
                        pessoa.data_Nascimento = Convert.ToDateTime(reader["data_Nascimento"]);
                        pessoa.sexo = Convert.ToChar(reader["sexo"]);
                        pessoa.nome_Pai = Convert.ToString(reader["nome_Pai"]);
                        pessoa.nome_Mae = Convert.ToString(reader["nome_Mae"]);
                        pessoa.cargo = Convert.ToString(reader["cargo"]);
                        
                        DataTable dt_Endereco = (DataTable)reader["endereco"];
                        string[] dados_Endereco = { "", "", "" };
                        for (int i = 0; i < dt_Endereco.Rows.Count; i++)
                        {
                            dados_Endereco[i] = dt_Endereco.Rows[i]["endereco"].ToString();
                            dados_Endereco[i] = dt_Endereco.Rows[i]["numero"].ToString();
                            dados_Endereco[i] = dt_Endereco.Rows[i]["complemento"].ToString();
                            dados_Endereco[i] = dt_Endereco.Rows[i]["bairro"].ToString();
                            dados_Endereco[i] = dt_Endereco.Rows[i]["cidade"].ToString();
                            dados_Endereco[i] = dt_Endereco.Rows[i]["estado"].ToString();
                        }
                        endereco.endereco = dados_Endereco[0].ToString();
                        endereco.numero = dados_Endereco[0].ToString();
                        endereco.complemento = dados_Endereco[0].ToString();
                        endereco.bairro = dados_Endereco[0].ToString();
                        endereco.cidade = dados_Endereco[0].ToString();
                        endereco.estado = dados_Endereco[0].ToString();

                        DataTable dt_Telefones = (DataTable)reader["telefones"];
                        string[] dados_Telefone = { "", "", "" };
                        for (int i = 0; i < dt_Telefones.Rows.Count; i++)
                        {
                            dados_Telefone[i] = dt_Telefones.Rows[i]["numero_Telefone"].ToString();
                        }
                        telefones.numero_TelefoneFixo = dados_Telefone[0].ToString();
                        telefones.numero_TelefoneCelular = dados_Telefone[1].ToString();
                        

                        lista.Add(pessoa);
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

        public void AtualizarPessoa(Pessoa pessoa)
        {
            Endereco endereco = new Endereco();
            endereco = pessoa.endereco;
            Telefone telefone = new Telefone();
            telefone = pessoa.telefone;
            IDbConnection minhaConexaoexao = new LightBaseConnection("user=lbw;password=lbw;UDB=defudb;server=localhost");
            try
            {
                minhaConexaoexao.Open();
                IDbCommand comando = new LightBaseCommand("update pessoa set arq_Arquivo=@arq_Arquivo, "
                    + "nome_Colaborador=@nome_Colaborador, naturalidade=@naturalidade, data_Nascimento=@data_Nascimento, sexo=@sexo, nome_Pai=@nome_Pai, "
                    + "nome_Mae=@nome_Mae, cargo=@cargo, endereco={{@endereco, @numero, @complemento, "
                    + "@bairro, @cidade, @estado}}, telefones={{@numero_telefone1, @numero_telefone2, @numero_telefone3}} where id=@id");
                comando.Parameters.Add(new LightBaseParameter("arq_Arquivo", pessoa.arq_Arquivo));
                comando.Parameters.Add(new LightBaseParameter("id", pessoa.id));
                
                comando.Parameters.Add(new LightBaseParameter("nome_Colaborador", pessoa.nome_Colaborador));
                comando.Parameters.Add(new LightBaseParameter("naturalidade", pessoa.naturalidade));
                comando.Parameters.Add(new LightBaseParameter("data_Nascimento", pessoa.data_Nascimento));
                comando.Parameters.Add(new LightBaseParameter("sexo", pessoa.sexo));
                comando.Parameters.Add(new LightBaseParameter("nome_Pai", pessoa.nome_Pai));
                comando.Parameters.Add(new LightBaseParameter("nome_Mae", pessoa.nome_Mae));
                comando.Parameters.Add(new LightBaseParameter("cargo", pessoa.cargo));
                
                comando.Parameters.Add(new LightBaseParameter("endereco.endereco", endereco.endereco));
                comando.Parameters.Add(new LightBaseParameter("endereco.numero", endereco.numero));
                comando.Parameters.Add(new LightBaseParameter("endereco.complemento", endereco.complemento));
                comando.Parameters.Add(new LightBaseParameter("endereco.bairro", endereco.bairro));
                comando.Parameters.Add(new LightBaseParameter("endereco.cidade", endereco.cidade));
                comando.Parameters.Add(new LightBaseParameter("endereco.estado", endereco.estado));

                comando.Parameters.Add(new LightBaseParameter("numero_Telefone1", telefone.numero_TelefoneFixo));
                comando.Parameters.Add(new LightBaseParameter("numero_Telefone2", telefone.numero_TelefoneCelular));

               

                comando.Connection = minhaConexaoexao;
                comando.ExecuteNonQuery();
            }
            finally
            {
                minhaConexaoexao.Close();
            }
        }

        public void InserirPessoa(Pessoa pessoa)
        {
            IDbConnection minhaConexao = new LightBaseConnection("user=lbw;password=lbw;udb=defudb;server=localhost");
            try
            {
                minhaConexao.Open();

                IDbCommand meuComando = new LightBaseCommand();

                meuComando.CommandText = "insert into pessoa (arq_Arquivo,nome_Colaborador,naturalidade,data_Nascimento,sexo,nome_Pai,nome_Mae, cargo,"
                        +" endereco,telefones)  "
                        + "values ( @arq_Arquivo , @nome_Colaborador, @naturalidade, @data_Nascimento, @sexo, "
                        + "@nome_Pai, @nome_Mae, @cargo,{{@endereco,@numero,@complemento,@bairro,@estado,@cidade}}, {{@numero_Telefone1},{@numero_Telefone2}}) ";
                    
        
                meuComando.Connection = minhaConexao;
                Endereco endereco = new Endereco();
                endereco = pessoa.endereco;
                Telefone telefone = new Telefone();
                telefone = pessoa.telefone;
               
                meuComando.Parameters.Add(new LightBaseParameter("arq_Arquivo", pessoa.arq_Arquivo));
                meuComando.Parameters.Add(new LightBaseParameter("nome_Colaborador", pessoa.nome_Colaborador));
                meuComando.Parameters.Add(new LightBaseParameter("naturalidade", pessoa.naturalidade));
                meuComando.Parameters.Add(new LightBaseParameter("data_Nascimento", pessoa.data_Nascimento));
                meuComando.Parameters.Add(new LightBaseParameter("sexo", pessoa.sexo));
                meuComando.Parameters.Add(new LightBaseParameter("nome_Pai", pessoa.nome_Pai));
                meuComando.Parameters.Add(new LightBaseParameter("nome_Mae", pessoa.nome_Mae));
                meuComando.Parameters.Add(new LightBaseParameter("cargo", pessoa.cargo));
                meuComando.Parameters.Add(new LightBaseParameter("endereco", endereco.endereco));
                meuComando.Parameters.Add(new LightBaseParameter("numero", endereco.numero));
                meuComando.Parameters.Add(new LightBaseParameter("complemento", endereco.complemento));
                meuComando.Parameters.Add(new LightBaseParameter("bairro", endereco.bairro));
                meuComando.Parameters.Add(new LightBaseParameter("cidade", endereco.cidade));
                meuComando.Parameters.Add(new LightBaseParameter("estado", endereco.estado));
                meuComando.Parameters.Add(new LightBaseParameter("cep", pessoa.endereco.cep));
                meuComando.Parameters.Add(new LightBaseParameter("numero_Telefone1", telefone.numero_TelefoneFixo));
                meuComando.Parameters.Add(new LightBaseParameter("numero_Telefone2", telefone.numero_TelefoneCelular));
          
                meuComando.ExecuteNonQuery();
            }
            finally
            {
                minhaConexao.Close();
            }           
        }

        public void RemoverPessoa(int id)
        {
            IDbConnection minhaConexaoexao = new LightBaseConnection("user=lbw;password=lbw;UDB=defudb;server=localhost");
            try
            {
                minhaConexaoexao.Open();
                IDbCommand comando = new LightBaseCommand("delete from pessoa where id=@id");
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

        public Pessoa obterPessoaPorId(string valor)
        {
            IDataReader reader;
            IDbConnection minhaConexao = new LightBaseConnection("user=lbw;password=lbw;udb=defudb;server=localhost");
            try
            {
                minhaConexao.Open();
                Pessoa pessoa = new Pessoa();
                try
                {
                    IDbCommand meuComando = new LightBaseCommand("select id, arq_Arquivo from pessoa where id = " + valor);
                    meuComando.Connection = minhaConexao;
                    reader = meuComando.ExecuteReader();
                    while (reader.Read())
                    {
                        pessoa.id = Convert.ToInt32(reader["id"]);
                        pessoa.arq_Arquivo = Convert.ToString(reader["arq_Arquivo"]);
                    }
                }
                catch (LightBaseException e)
                {
                    string erro = e.Message;
                }
                return pessoa;
            }
            finally
            {
                minhaConexao.Close();
            }
        }
    }
}