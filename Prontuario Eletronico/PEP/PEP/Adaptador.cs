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

namespace PEP
{
    public class Adaptador: IAdaptador
    {
        /*
         * Método que realiza a pesquisa de um prontuário utilizando como parâmetro uma consulta lightbase.
         */ 
        public List<Prontuario> PesquisaPorCampo(string comando)
        {
            List<Prontuario> lista = new List<Prontuario>();
            IDataReader reader;
            IDbConnection minhaConexao = new LightBaseConnection("user=lbw;password=lbw;udb=DEFUDB;server=localhost");
            try
            {
                minhaConexao.Open();//abertura da conexão com o lightbase
                try
                {
                    IDbCommand meuComando = new LightBaseCommand(comando);//Criando a setando o comando de consulta
                    meuComando.Connection = minhaConexao;
                
                    reader = meuComando.ExecuteReader();
                    //Leitura dos registros retornados na consulta e carga na classe de prontuário
                    while (reader.Read())
                    {
                        Prontuario prontuario = new Prontuario();
                        Endereco enderecos = new Endereco();
                        Telefone telefones = new Telefone();
                        Medico medico = new Medico();
                        
                        prontuario.id = Convert.ToInt16(reader["id"]);
                        prontuario.arq_Arquivo = Convert.ToString(reader["arq_Arquivo"]);
                        prontuario.numero_Registro = Convert.ToString(reader["numero_Registro"]);
                        prontuario.nome_Paciente = Convert.ToString(reader["nome_Paciente"]);
                        prontuario.naturalidade = Convert.ToString(reader["naturalidade"]);
                        prontuario.data_Nascimento = Convert.ToDateTime(reader["data_Nascimento"]);
                        prontuario.sexo = Convert.ToString(reader["sexo"]);
                        prontuario.nome_Pai = Convert.ToString(reader["nome_Pai"]);
                        prontuario.nome_Mae = Convert.ToString(reader["nome_Mae"]);
                        prontuario.profissao = Convert.ToString(reader["profissao"]);
                        prontuario.pessoa_Responsavel = Convert.ToString(reader["pessoa_Responsavel"]);
                        prontuario.procedencia = Convert.ToString(reader["procedencia"]);
                        prontuario.nome_Clinica_Diagnostico = Convert.ToString(reader["nome_Clinica_Diagnostico"]);
                        prontuario.diagnostico = Convert.ToString(reader["diagnostico"]);
                        prontuario.cid = Convert.ToString(reader["cid"]);
                        prontuario.nome_Clinica_Internacao = Convert.ToString(reader["nome_Clinica_Internacao"]);
                        prontuario.diagnostico_Provisorio = Convert.ToString(reader["diagnostico_Provisorio"]);
                        prontuario.data_Internacao = Convert.ToDateTime(reader["data_Internacao"]);
                        prontuario.medico_Solicitante = Convert.ToString(reader["medico_Solicitante"]);
                        
                        //Tratamento do campo multivalorado Endereco
                        DataTable dt_Endereco = (DataTable)reader["endereco"];
                        string endereco = dt_Endereco.Rows[0]["endereco"].ToString();
                        string numero = dt_Endereco.Rows[0]["numero"].ToString();
                        string complemento = dt_Endereco.Rows[0]["complemento"].ToString();
                        string bairro = dt_Endereco.Rows[0]["bairro"].ToString();
                        string nome_Cidade = dt_Endereco.Rows[0]["nome_Cidade"].ToString();
                        string nome_Estado = dt_Endereco.Rows[0]["nome_Estado"].ToString();

                        enderecos.endereco = endereco;
                        enderecos.numero = numero;
                        enderecos.complemento = complemento;
                        enderecos.bairro = bairro;
                        enderecos.nome_Cidade = nome_Cidade;
                        enderecos.nome_Estado = nome_Estado;

                        //Tratamento do campo multivalorado Telefones.
                        DataTable dt_Telefones = (DataTable)reader["telefones"];
                        string[] dados_Telefone = { "", "", "" };
                        for (int i = 0; i < dt_Telefones.Rows.Count; i++)
                        {
                            dados_Telefone[i] = dt_Telefones.Rows[i]["numero_Telefone"].ToString();
                        }
                        telefones.numero_TelefoneFixo = dados_Telefone[0].ToString();
                        telefones.numero_TelefoneCelular = dados_Telefone[1].ToString();
                        telefones.numero_TelefoneComercial = dados_Telefone[2].ToString();

                        //Tratamento do campo multivalorado Médicos.
                        DataTable dt_Medico = (DataTable)reader["medicos"];
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

                        //Adicionando cada registro a uma lista.
                        lista.Add(prontuario);
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

        /*
         * Métodos que realiza uma consulta através de um campo específico.
         */ 
        public List<Prontuario> PesquisarCampos(string comando)
        {
            List<Prontuario> lista = new List<Prontuario>();
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
                        Prontuario prontuario = new Prontuario();
                        Endereco enderecos = new Endereco();
                        Telefone telefones = new Telefone();
                        Medico medico = new Medico();
                        prontuario.id = Convert.ToInt16(reader["id"]);
                        prontuario.arq_Arquivo = Convert.ToString(reader["arq_Arquivo"]);
                        prontuario.numero_Registro = Convert.ToString(reader["numero_Registro"]);
                        prontuario.nome_Paciente = Convert.ToString(reader["nome_Paciente"]);
                        prontuario.naturalidade = Convert.ToString(reader["naturalidade"]);
                        prontuario.data_Nascimento = Convert.ToDateTime(reader["data_Nascimento"]);
                        prontuario.sexo = Convert.ToString(reader["sexo"]);
                        prontuario.nome_Pai = Convert.ToString(reader["nome_Pai"]);
                        prontuario.nome_Mae = Convert.ToString(reader["nome_Mae"]);
                        prontuario.profissao = Convert.ToString(reader["profissao"]);
                        prontuario.pessoa_Responsavel = Convert.ToString(reader["pessoa_Responsavel"]);
                        prontuario.procedencia = Convert.ToString(reader["procedencia"]);
                        prontuario.nome_Clinica_Diagnostico = Convert.ToString(reader["nome_Clinica_Diagnostico"]);
                        prontuario.diagnostico = Convert.ToString(reader["diagnostico"]);
                        prontuario.cid = Convert.ToString(reader["cid"]);
                        prontuario.nome_Clinica_Internacao = Convert.ToString(reader["nome_Clinica_Internacao"]);
                        prontuario.diagnostico_Provisorio = Convert.ToString(reader["diagnostico_Provisorio"]);
                        prontuario.data_Internacao = Convert.ToDateTime(reader["data_Internacao"]);
                        prontuario.medico_Solicitante = Convert.ToString(reader["medico_Solicitante"]);

                        DataTable dt_Endereco = (DataTable)reader["endereco"];
                        string endereco = dt_Endereco.Rows[0]["endereco"].ToString();
                        string numero = dt_Endereco.Rows[0]["numero"].ToString();
                        string complemento = dt_Endereco.Rows[0]["complemento"].ToString();
                        string bairro = dt_Endereco.Rows[0]["bairro"].ToString();
                        string nome_Cidade = dt_Endereco.Rows[0]["nome_Cidade"].ToString();
                        string nome_Estado = dt_Endereco.Rows[0]["nome_Estado"].ToString();

                        enderecos.endereco = endereco;
                        enderecos.numero = numero;
                        enderecos.complemento = complemento;
                        enderecos.bairro = bairro;
                        enderecos.nome_Cidade = nome_Cidade;
                        enderecos.nome_Estado = nome_Estado;

                        DataTable dt_Telefones = (DataTable)reader["telefones"];
                        string[] dados_Telefone = { "", "", "" };
                        for (int i = 0; i < dt_Telefones.Rows.Count; i++)
                        {
                            dados_Telefone[i] = dt_Telefones.Rows[i]["numero_Telefone"].ToString();
                        }
                        telefones.numero_TelefoneFixo = dados_Telefone[0].ToString();
                        telefones.numero_TelefoneCelular = dados_Telefone[1].ToString();
                        telefones.numero_TelefoneComercial = dados_Telefone[2].ToString();

                        DataTable dt_Medico = (DataTable)reader["medicos"];
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

                        lista.Add(prontuario);
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

        /*
         * Método que retorna todos os registro da base.
         */  
        public List<Prontuario> Todos()
        {
            List<Prontuario> lista = new List<Prontuario>();
            IDataReader reader;
            IDbConnection minhaConexao = new LightBaseConnection("user=lbw;password=lbw;UDB=defudb;server=localhost");
            try
            {
                minhaConexao.Open();

                IDbCommand comando = new LightBaseCommand("select id, arq_Arquivo, numero_Registro, nome_Paciente, naturalidade, data_Nascimento, "
                                                        + "sexo, nome_Pai, nome_Mae, profissao, pessoa_Responsavel, endereco.endereco, "
                                                        + "endereco.numero, endereco.complemento, endereco.bairro, endereco.nome_Cidade, "
                                                        + "endereco.nome_Estado, telefones.numero_Telefone, procedencia, nome_Clinica_Diagnostico, "
                                                        + "diagnostico, cid, medicos.matricula_Medico, medicos.nome_Medico, nome_Clinica_Internacao, diagnostico_Provisorio, "
                                                        + "data_Internacao, medico_Solicitante from prontuario");
                comando.Connection = minhaConexao;
                reader = comando.ExecuteReader();
                
                while (reader.Read())
                {
                    Prontuario prontuario = new Prontuario();
                    Endereco enderecos = new Endereco();
                    Telefone telefones = new Telefone();
                    Medico medico = new Medico();
                    prontuario.id = Convert.ToInt16(reader["id"]);
                    prontuario.arq_Arquivo = Convert.ToString(reader["arq_Arquivo"]);
                    prontuario.numero_Registro = Convert.ToString(reader["numero_Registro"]);
                    prontuario.nome_Paciente = Convert.ToString(reader["nome_Paciente"]);
                    prontuario.naturalidade = Convert.ToString(reader["naturalidade"]);
                    prontuario.data_Nascimento = Convert.ToDateTime(reader["data_Nascimento"]);
                    prontuario.sexo = Convert.ToString(reader["sexo"]);

                    //Tratamento do campo sexo, pois sempre é grava um caractere que identifica se é homem ou mulher.
                    if (prontuario.sexo == "m")
                    {
                        prontuario.sexo = "Masculino";
                    }
                    else
                    {
                        prontuario.sexo = "Feminino";
                    }
                    prontuario.nome_Pai = Convert.ToString(reader["nome_Pai"]);
                    prontuario.nome_Mae = Convert.ToString(reader["nome_Mae"]);
                    prontuario.profissao = Convert.ToString(reader["profissao"]);
                    prontuario.pessoa_Responsavel = Convert.ToString(reader["pessoa_Responsavel"]);
                    prontuario.procedencia = Convert.ToString(reader["procedencia"]);
                    prontuario.nome_Clinica_Diagnostico = Convert.ToString(reader["nome_Clinica_Diagnostico"]);
                    prontuario.diagnostico = Convert.ToString(reader["diagnostico"]);
                    prontuario.cid = Convert.ToString(reader["cid"]);
                    prontuario.nome_Clinica_Internacao = Convert.ToString(reader["nome_Clinica_Internacao"]);
                    prontuario.diagnostico_Provisorio = Convert.ToString(reader["diagnostico_Provisorio"]);
                    prontuario.data_Internacao = Convert.ToDateTime(reader["data_Internacao"]);
                    prontuario.medico_Solicitante = Convert.ToString(reader["medico_Solicitante"]);

                    DataTable dt_Endereco = (DataTable)reader["endereco"];
                    string endereco = dt_Endereco.Rows[0]["endereco"].ToString();
                    string numero = dt_Endereco.Rows[0]["numero"].ToString();
                    string complemento = dt_Endereco.Rows[0]["complemento"].ToString();
                    string bairro = dt_Endereco.Rows[0]["bairro"].ToString();
                    string nome_Cidade = dt_Endereco.Rows[0]["nome_Cidade"].ToString();
                    string nome_Estado = dt_Endereco.Rows[0]["nome_Estado"].ToString();

                    enderecos.endereco = endereco;
                    enderecos.numero = numero;
                    enderecos.complemento = complemento;
                    enderecos.bairro = bairro;
                    enderecos.nome_Cidade = nome_Cidade;
                    enderecos.nome_Estado = nome_Estado;

                    DataTable dt_Telefones = (DataTable)reader["telefones"];
                    string[] dados_Telefone = { "", "", "" };
                    for (int i = 0; i < dt_Telefones.Rows.Count; i++)
                    {
                        dados_Telefone[i] = dt_Telefones.Rows[i]["numero_Telefone"].ToString();
                    }
                    telefones.numero_TelefoneFixo = dados_Telefone[0].ToString();
                    telefones.numero_TelefoneCelular = dados_Telefone[1].ToString();
                    telefones.numero_TelefoneComercial = dados_Telefone[2].ToString();

                    DataTable dt_Medico = (DataTable)reader["medicos"];
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

                    lista.Add(prontuario);
                }
                return lista;
            }
            finally
            {
                minhaConexao.Close();
            }
        }


        /*
         * Método que realiza uma consulta por uma coluna específica
         */ 
        public List<Prontuario> PorColuna(string coluna, string orderby)
        {
            List<Prontuario> lista = new List<Prontuario>();
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
                else if (coluna == "numero_Medico1")
                {
                    coluna = "numero_Medico";
                }
                else if (coluna == "numero_Medico2")
                {
                    coluna = "numero_Medico";
                }
                else if (coluna == "numero_Medico3")
                {
                    coluna = "numero_Medico";
                }
                
                minhaConexaoexao.Open();
                try
                {   
                    IDbCommand comando = new LightBaseCommand("select id, arq_Arquivo, numero_Registro, nome_Paciente, naturalidade, data_Nascimento, "
                                                            + "sexo, nome_Pai, nome_Mae, profissao, pessoa_Responsavel, endereco.endereco, "
                                                            + "endereco.numero, endereco.complemento, endereco.bairro, endereco.nome_Cidade, "
                                                            + "endereco.nome_Estado, telefones.numero_Telefone, medicos.matricula_Medico, medicos.nome_Medico, procedencia, "
                                                            + "nome_Clinica_Diagnostico, diagnostico, cid, nome_Clinica_Internacao, "
                                                            + "diagnostico_Provisorio, data_Internacao, medico_Solicitante from prontuario "
                                                            + "order by " + coluna + " " + orderby);
                    comando.Connection = minhaConexaoexao;
                    reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        Prontuario prontuario = new Prontuario();
                        Endereco enderecos = new Endereco();
                        Telefone telefones = new Telefone();
                        Medico medico = new Medico();
                        prontuario.id = Convert.ToInt16(reader["id"]);
                        prontuario.arq_Arquivo = Convert.ToString(reader["arq_Arquivo"]);
                        prontuario.numero_Registro = Convert.ToString(reader["numero_Registro"]);
                        prontuario.nome_Paciente = Convert.ToString(reader["nome_Paciente"]);
                        prontuario.naturalidade = Convert.ToString(reader["naturalidade"]);
                        prontuario.data_Nascimento = Convert.ToDateTime(reader["data_Nascimento"]);
                        prontuario.sexo = Convert.ToString(reader["sexo"]);
                        if (prontuario.sexo == "m")
                        {
                            prontuario.sexo = "Masculino";
                        }
                        else
                        {
                            prontuario.sexo = "Feminino";
                        }
                        prontuario.nome_Pai = Convert.ToString(reader["nome_Pai"]);
                        prontuario.nome_Mae = Convert.ToString(reader["nome_Mae"]);
                        prontuario.profissao = Convert.ToString(reader["profissao"]);
                        prontuario.pessoa_Responsavel = Convert.ToString(reader["pessoa_Responsavel"]);
                        prontuario.procedencia = Convert.ToString(reader["procedencia"]);
                        prontuario.nome_Clinica_Diagnostico = Convert.ToString(reader["nome_Clinica_Diagnostico"]);
                        prontuario.diagnostico = Convert.ToString(reader["diagnostico"]);
                        prontuario.cid = Convert.ToString(reader["cid"]);
                        prontuario.nome_Clinica_Internacao = Convert.ToString(reader["nome_Clinica_Internacao"]);
                        prontuario.diagnostico_Provisorio = Convert.ToString(reader["diagnostico_Provisorio"]);
                        prontuario.data_Internacao = Convert.ToDateTime(reader["data_Internacao"]);
                        prontuario.medico_Solicitante = Convert.ToString(reader["medico_Solicitante"]);

                        DataTable dt_Endereco = (DataTable)reader["endereco"];
                        string endereco = dt_Endereco.Rows[0]["endereco"].ToString();
                        string numero = dt_Endereco.Rows[0]["numero"].ToString();
                        string complemento = dt_Endereco.Rows[0]["complemento"].ToString();
                        string bairro = dt_Endereco.Rows[0]["bairro"].ToString();
                        string nome_Cidade = dt_Endereco.Rows[0]["nome_Cidade"].ToString();
                        string nome_Estado = dt_Endereco.Rows[0]["nome_Estado"].ToString();

                        enderecos.endereco = endereco;
                        enderecos.numero = numero;
                        enderecos.complemento = complemento;
                        enderecos.bairro = bairro;
                        enderecos.nome_Cidade = nome_Cidade;
                        enderecos.nome_Estado = nome_Estado;

                        DataTable dt_Telefones = (DataTable)reader["telefones"];
                        string[] dados_Telefone = { "", "", "" };
                        for (int i = 0; i < dt_Telefones.Rows.Count; i++)
                        {
                            dados_Telefone[i] = dt_Telefones.Rows[i]["numero_Telefone"].ToString();
                        }
                        telefones.numero_TelefoneFixo = dados_Telefone[0].ToString();
                        telefones.numero_TelefoneCelular = dados_Telefone[1].ToString();
                        telefones.numero_TelefoneComercial = dados_Telefone[2].ToString();

                        DataTable dt_Medico = (DataTable)reader["medicos"];
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

                        lista.Add(prontuario);
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

        /*
         * Método que atualiza os dados de um prontuário.
         */ 
        public void AtualizarProntuario(Prontuario prontuario)
        {
            Endereco enderecos = new Endereco();
            enderecos = prontuario.endereco;
            Telefone telefone = new Telefone();
            telefone = prontuario.telefone;
            Medico medico = new Medico();
            medico = prontuario.medico;

            IDbConnection minhaConexao = new LightBaseConnection("user=lbw;password=lbw;UDB=defudb;server=localhost");
            try
            {
                minhaConexao.Open();
                IDbCommand comando = new LightBaseCommand("update prontuario set arq_Arquivo=@arq_Arquivo, numero_Registro=@numero_Registro, "
                    + "nome_Paciente=@nome_Paciente, naturalidade=@naturalidade, data_Nascimento=@data_Nascimento, sexo=@sexo, nome_Pai=@nome_Pai, "
                    + "nome_Mae=@nome_Mae, profissao=@profissao, pessoa_Responsavel=@pessoa_Responsavel, procedencia=@procedencia, "
                    + "nome_Clinica_Diagnostico=@nome_Clinica_Diagnostico, diagnostico=@diagnostico, cid=@cid, "
                    + "nome_Clinica_Internacao=@nome_Clinica_Internacao, diagnostico_Provisorio=@diagnostico_Provisorio, "
                    + "data_Internacao=@data_Internacao, medico_Solicitante=@medico_Solicitante, endereco={{@endereco, @numero, @complemento, "
                    + "@bairro, @nome_Cidade, @nome_Estado, @cep}}, telefones={{@numero_telefone1}, {@numero_telefone2}, {@numero_telefone3}}, medicos={{@matricula_Medico1, @nome_medico1}, "
                    + "{@matricula_Medico2, @nome_medico2}, {@matricula_Medico3, @nome_medico3}, {@matricula_Medico4, @nome_Medico4}} where id=@id");
                comando.Parameters.Add(new LightBaseParameter("arq_Arquivo", prontuario.arq_Arquivo));
                comando.Parameters.Add(new LightBaseParameter("id", prontuario.id));
                comando.Parameters.Add(new LightBaseParameter("numero_Registro", prontuario.numero_Registro));
                comando.Parameters.Add(new LightBaseParameter("nome_Paciente", prontuario.nome_Paciente));
                comando.Parameters.Add(new LightBaseParameter("naturalidade", prontuario.naturalidade));
                comando.Parameters.Add(new LightBaseParameter("data_Nascimento", prontuario.data_Nascimento));
                comando.Parameters.Add(new LightBaseParameter("sexo", prontuario.sexo));
                comando.Parameters.Add(new LightBaseParameter("nome_Pai", prontuario.nome_Pai));
                comando.Parameters.Add(new LightBaseParameter("nome_Mae", prontuario.nome_Mae));
                comando.Parameters.Add(new LightBaseParameter("profissao", prontuario.profissao));
                comando.Parameters.Add(new LightBaseParameter("pessoa_Responsavel", prontuario.pessoa_Responsavel));
                comando.Parameters.Add(new LightBaseParameter("procedencia", prontuario.procedencia));
                comando.Parameters.Add(new LightBaseParameter("nome_Clinica_Diagnostico", prontuario.nome_Clinica_Diagnostico));
                comando.Parameters.Add(new LightBaseParameter("diagnostico", prontuario.diagnostico));
                comando.Parameters.Add(new LightBaseParameter("cid", prontuario.cid));
                comando.Parameters.Add(new LightBaseParameter("nome_Clinica_Internacao", prontuario.nome_Clinica_Internacao));
                comando.Parameters.Add(new LightBaseParameter("diagnostico_Provisorio", prontuario.diagnostico_Provisorio));
                comando.Parameters.Add(new LightBaseParameter("data_Internacao", prontuario.data_Internacao));
                comando.Parameters.Add(new LightBaseParameter("medico_Solicitante", prontuario.medico_Solicitante));
                comando.Parameters.Add(new LightBaseParameter("endereco", enderecos.endereco));
                comando.Parameters.Add(new LightBaseParameter("numero", enderecos.numero));
                comando.Parameters.Add(new LightBaseParameter("complemento", enderecos.complemento));
                comando.Parameters.Add(new LightBaseParameter("bairro", enderecos.bairro));
                comando.Parameters.Add(new LightBaseParameter("nome_Cidade", enderecos.nome_Cidade));
                comando.Parameters.Add(new LightBaseParameter("nome_Estado", enderecos.nome_Estado));
                comando.Parameters.Add(new LightBaseParameter("cep", enderecos.cep));

                comando.Parameters.Add(new LightBaseParameter("numero_Telefone1", telefone.numero_TelefoneFixo));
                comando.Parameters.Add(new LightBaseParameter("numero_Telefone2", telefone.numero_TelefoneCelular));
                comando.Parameters.Add(new LightBaseParameter("numero_Telefone3", telefone.numero_TelefoneComercial));

                comando.Parameters.Add(new LightBaseParameter("matricula_Medico1", medico.matricula_Medico1));
                comando.Parameters.Add(new LightBaseParameter("matricula_Medico2", medico.matricula_Medico2));
                comando.Parameters.Add(new LightBaseParameter("matricula_Medico3", medico.matricula_Medico3));
                comando.Parameters.Add(new LightBaseParameter("matricula_Medico4", medico.matricula_Medico4));
                comando.Parameters.Add(new LightBaseParameter("nome_Medico1", medico.nome_Medico1));
                comando.Parameters.Add(new LightBaseParameter("nome_Medico2", medico.nome_Medico2));
                comando.Parameters.Add(new LightBaseParameter("nome_Medico3", medico.nome_Medico3));
                comando.Parameters.Add(new LightBaseParameter("nome_Medico4", medico.nome_Medico4));

                comando.Connection = minhaConexao;
                comando.ExecuteNonQuery();
            }
            finally
            {
                minhaConexao.Close();
            }
        }

        /*
         * Método que insere um novo prontuário na base.
         */ 
        public void InserirProntuario(Prontuario prontuario)
        {
            IDbConnection minhaConexao = new LightBaseConnection("user=lbw;password=lbw;udb=defudb;server=localhost");
            try
            {
                minhaConexao.Open();

                IDbCommand meuComando = new LightBaseCommand();
                meuComando.CommandText = "insert into prontuario(arq_Arquivo, numero_Registro, nome_Paciente, naturalidade, data_Nascimento, sexo, "
                     + "nome_Pai, nome_Mae, profissao, pessoa_Responsavel, endereco, telefones, procedencia, nome_Clinica_Diagnostico, diagnostico, "
                     + "cid, medicos, nome_Clinica_Internacao, diagnostico_Provisorio, data_Internacao, medico_Solicitante)values (@arq_Arquivo, "
                     + "@numero_Registro, @nome_Paciente, @naturalidade, @data_Nascimento, @sexo, @nome_Pai, @nome_Mae, @profissao, @pessoa_Responsavel, "
                     + "{{@endereco, @numero, @complemento, @bairro, @nome_Cidade, @nome_Estado, @cep}}, {{@telefone1},{@telefone2}, {@telefone3}}, "
                     + "@procedencia, @nome_Clinica_Diagnostico, @diagnostico, @cid, {{@matricula_Medico1, @nome_Medico1}, {@matricula_Medico2, "
                     + "@nome_Medico2}, {@matricula_Medico3, @nome_Medico3}, {@matricula_Medico4, @nome_Medico4}}, @nome_Clinica_Internacao, "
                     + "@diagnostico_Provisorio, @data_Internacao, @medico_Solicitante)";
                
                meuComando.Connection = minhaConexao;

                Endereco enderecos = new Endereco();
                enderecos = prontuario.endereco;
                Telefone telefone = new Telefone();
                telefone = prontuario.telefone;
                Medico medico = new Medico();
                medico = prontuario.medico;
                meuComando.Parameters.Add(new LightBaseParameter("arq_Arquivo", prontuario.arq_Arquivo));
                meuComando.Parameters.Add(new LightBaseParameter("numero_Registro", prontuario.numero_Registro));
                meuComando.Parameters.Add(new LightBaseParameter("nome_Paciente", prontuario.nome_Paciente));
                meuComando.Parameters.Add(new LightBaseParameter("naturalidade", prontuario.naturalidade));
                meuComando.Parameters.Add(new LightBaseParameter("data_Nascimento", prontuario.data_Nascimento));
                meuComando.Parameters.Add(new LightBaseParameter("sexo", prontuario.sexo));
                meuComando.Parameters.Add(new LightBaseParameter("nome_Pai", prontuario.nome_Pai));
                meuComando.Parameters.Add(new LightBaseParameter("nome_Mae", prontuario.nome_Mae));
                meuComando.Parameters.Add(new LightBaseParameter("profissao", prontuario.profissao));
                meuComando.Parameters.Add(new LightBaseParameter("pessoa_Responsavel", prontuario.pessoa_Responsavel));
                meuComando.Parameters.Add(new LightBaseParameter("endereco", enderecos.endereco));
                meuComando.Parameters.Add(new LightBaseParameter("numero", enderecos.numero));
                meuComando.Parameters.Add(new LightBaseParameter("complemento", enderecos.complemento));
                meuComando.Parameters.Add(new LightBaseParameter("bairro", enderecos.bairro));
                meuComando.Parameters.Add(new LightBaseParameter("nome_Cidade", enderecos.nome_Cidade));
                meuComando.Parameters.Add(new LightBaseParameter("nome_Estado", enderecos.nome_Estado));
                meuComando.Parameters.Add(new LightBaseParameter("cep", prontuario.endereco.cep));
                meuComando.Parameters.Add(new LightBaseParameter("numero_Telefone1", telefone.numero_TelefoneFixo));
                meuComando.Parameters.Add(new LightBaseParameter("numero_Telefone2", telefone.numero_TelefoneCelular));
                meuComando.Parameters.Add(new LightBaseParameter("numero_Telefone3", telefone.numero_TelefoneComercial));
                meuComando.Parameters.Add(new LightBaseParameter("procedencia", prontuario.procedencia));
                meuComando.Parameters.Add(new LightBaseParameter("nome_Clinica_Diagnostico", prontuario.nome_Clinica_Diagnostico));
                meuComando.Parameters.Add(new LightBaseParameter("diagnostico", prontuario.diagnostico));
                meuComando.Parameters.Add(new LightBaseParameter("cid", prontuario.cid));
                meuComando.Parameters.Add(new LightBaseParameter("matricula_Medico1", medico.matricula_Medico1));
                meuComando.Parameters.Add(new LightBaseParameter("matricula_Medico2", medico.matricula_Medico2));
                meuComando.Parameters.Add(new LightBaseParameter("matricula_Medico3", medico.matricula_Medico3));
                meuComando.Parameters.Add(new LightBaseParameter("matricula_Medico4", medico.matricula_Medico4));
                meuComando.Parameters.Add(new LightBaseParameter("nome_Medico1", medico.nome_Medico1));
                meuComando.Parameters.Add(new LightBaseParameter("nome_Medico2", medico.nome_Medico2));
                meuComando.Parameters.Add(new LightBaseParameter("nome_Medico3", medico.nome_Medico3));
                meuComando.Parameters.Add(new LightBaseParameter("nome_Medico4", medico.nome_Medico4));

                meuComando.Parameters.Add(new LightBaseParameter("nome_Clinica_Internacao", prontuario.nome_Clinica_Internacao));
                meuComando.Parameters.Add(new LightBaseParameter("diagnostico_Provisorio", prontuario.diagnostico_Provisorio));
                meuComando.Parameters.Add(new LightBaseParameter("data_Internacao", prontuario.data_Internacao));
                meuComando.Parameters.Add(new LightBaseParameter("medico_Solicitante", prontuario.medico_Solicitante));

                meuComando.ExecuteNonQuery();
            }
            finally
            {
                minhaConexao.Close();
            }           
        }

        /*
         * Método que remove um prontuário da base.
         */ 
        public void RemoverProntuario(int id)
        {
            IDbConnection minhaConexaoexao = new LightBaseConnection("user=lbw;password=lbw;UDB=defudb;server=localhost");
            try
            {
                minhaConexaoexao.Open();
                IDbCommand comando = new LightBaseCommand("delete from prontuario where id=@id");
                comando.Parameters.Add(new LightBaseParameter("id", id));
                comando.Connection = minhaConexaoexao;
                comando.ExecuteNonQuery();
            }
            finally
            {
                minhaConexaoexao.Close();
            }
        }

        /*
         * Método que verifica as permissões de acesso de um usuário a aplicação.
         */ 
        public int EfetuaLogin(string nome, string senha)
        {
            GoldenAccess goldenAccess = new GoldenAccess("http://localhost:3271/GoldenAccess.soap");
            LightInfocon.GoldenAccess.General.User usuario = new LightInfocon.GoldenAccess.General.User(nome, senha); //Validação de usuário e senha
            try
            {
                usuario = goldenAccess.Authenticate(nome, senha); //Autenticação de usuário e senha.
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
                if (usuario.HasGroup("PTRIOADM"))
                {
                    return 1;//Será retornado 1 caso o usuário seja administrado do sistema.
                }
                else if (usuario.HasGroup("PTRIOLIM"))
                {
                    return 2;// Será retornado 2 caso o usuário tenha permissões limitadas a aplicação (Apenas consulta).
                }
                else
                {
                    return 3;// Será retornado 3 caso não seja um usuário do sistema.
                }
            }
            else
            {
                return 5;// Será retornado 5 caso o erro gerado não tenha sido identificado.
            }
        }

        /*
         * Método que vai alterar a senha de um usuário cadastrado no sistema.
         */ 
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
            if (usuarioGoldenAccess.IsAuthenticated && usuarioGoldenAccess.IsAdm)//Verica se o usuário está autenticado e tem permissão para modificação
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        /*
         * Método que vai consultar um prontuário através de seu identificador.
         */ 
        public Prontuario obterProntuarioPorId(string valor)
        {
            IDataReader reader;
            IDbConnection minhaConexao = new LightBaseConnection("user=lbw;password=lbw;udb=defudb;server=localhost");
            try
            {
                minhaConexao.Open();
                Prontuario prontuario = new Prontuario();
                try
                {
                    IDbCommand comando = new LightBaseCommand("select id, arq_Arquivo, numero_Registro, nome_Paciente, naturalidade, data_Nascimento,"
                                                    + "sexo, nome_Pai, nome_Mae, profissao, pessoa_Responsavel, endereco.endereco, "
                                                    + "endereco.numero, endereco.complemento, endereco.bairro, endereco.nome_Cidade, "
                                                    + "endereco.nome_Estado, telefones.numero_Telefone, procedencia, nome_Clinica_Diagnostico, "
                                                    + "diagnostico, cid, medicos.matricula_Medico, medicos.nome_Medico, nome_Clinica_Internacao, "
                                                    + "diagnostico_Provisorio, data_Internacao, medico_Solicitante from prontuario where id="+valor);
                    comando.Connection = minhaConexao;
                    reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        Endereco enderecos = new Endereco();
                        Telefone telefones = new Telefone();
                        Medico medico = new Medico();

                        prontuario.id = Convert.ToInt16(reader["id"]);
                        prontuario.arq_Arquivo = Convert.ToString(reader["arq_Arquivo"]);
                        prontuario.numero_Registro = Convert.ToString(reader["numero_Registro"]);
                        prontuario.nome_Paciente = Convert.ToString(reader["nome_Paciente"]);
                        prontuario.naturalidade = Convert.ToString(reader["naturalidade"]);
                        prontuario.data_Nascimento = Convert.ToDateTime(reader["data_Nascimento"]);
                        prontuario.sexo = Convert.ToString(reader["sexo"]);
                        prontuario.nome_Pai = Convert.ToString(reader["nome_Pai"]);
                        prontuario.nome_Mae = Convert.ToString(reader["nome_Mae"]);
                        prontuario.profissao = Convert.ToString(reader["profissao"]);
                        prontuario.pessoa_Responsavel = Convert.ToString(reader["pessoa_Responsavel"]);
                        prontuario.procedencia = Convert.ToString(reader["procedencia"]);
                        prontuario.nome_Clinica_Diagnostico = Convert.ToString(reader["nome_Clinica_Diagnostico"]);
                        prontuario.diagnostico = Convert.ToString(reader["diagnostico"]);
                        prontuario.cid = Convert.ToString(reader["cid"]);
                        prontuario.nome_Clinica_Internacao = Convert.ToString(reader["nome_Clinica_Internacao"]);
                        prontuario.diagnostico_Provisorio = Convert.ToString(reader["diagnostico_Provisorio"]);
                        prontuario.data_Internacao = Convert.ToDateTime(reader["data_Internacao"]);
                        prontuario.medico_Solicitante = Convert.ToString(reader["medico_Solicitante"]);

                        //Tratamento do campo multivalorado Endereco
                        DataTable dt_Endereco = (DataTable)reader["endereco"];
                        string endereco = dt_Endereco.Rows[0]["endereco"].ToString();
                        string numero = dt_Endereco.Rows[0]["numero"].ToString();
                        string complemento = dt_Endereco.Rows[0]["complemento"].ToString();
                        string bairro = dt_Endereco.Rows[0]["bairro"].ToString();
                        string nome_Cidade = dt_Endereco.Rows[0]["nome_Cidade"].ToString();
                        string nome_Estado = dt_Endereco.Rows[0]["nome_Estado"].ToString();

                        enderecos.endereco = endereco;
                        enderecos.numero = numero;
                        enderecos.complemento = complemento;
                        enderecos.bairro = bairro;
                        enderecos.nome_Cidade = nome_Cidade;
                        enderecos.nome_Estado = nome_Estado;

                        //Tratamento do campo multivalorado Telefones.
                        DataTable dt_Telefones = (DataTable)reader["telefones"];
                        string[] dados_Telefone = { "", "", "" };
                        for (int i = 0; i < dt_Telefones.Rows.Count; i++)
                        {
                            dados_Telefone[i] = dt_Telefones.Rows[i]["numero_Telefone"].ToString();
                        }
                        telefones.numero_TelefoneFixo = dados_Telefone[0].ToString();
                        telefones.numero_TelefoneCelular = dados_Telefone[1].ToString();
                        telefones.numero_TelefoneComercial = dados_Telefone[2].ToString();

                        //Tratamento do campo multivalorado Médicos.
                        DataTable dt_Medico = (DataTable)reader["medicos"];
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

                        prontuario.medico = medico;
                        prontuario.endereco = enderecos;
                        prontuario.telefone = telefones;
                    }
                    
                }
                catch (LightBaseException e)
                {
                    string erro = e.Message;
                }
                return prontuario;
            }
            finally
            {
                minhaConexao.Close();
            }
        }
    }
}