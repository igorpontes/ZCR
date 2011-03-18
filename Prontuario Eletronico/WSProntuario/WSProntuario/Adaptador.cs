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
using System.Security.Cryptography;
using System.Text;
using System.Data.SqlClient;

namespace WSProntuario
{
    public class Adaptador : IAdaptador
    {
        /// <summary>
        /// Método que gerar numero aleatorio
        /// </summary>
        /// <returns>int</returns>
        private int GerarNrAleatorio()
        {
            Random rnd = new Random();
            return rnd.Next();
        }

        /// <summary>
        /// Método que calcula hash dinâmica
        /// </summary>
        /// <param name="input">por exemplo: password</param>
        /// <returns>string</returns>
        private string CalcularHash(string input)
        {
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            StringBuilder str = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                str.Append(hash[i].ToString("X2"));
            }
            return str.ToString();
        }

        public object AutenticarUsuario(AuthenticationSoapHeader authentication)
        {
            bool achou = false;
            if (authentication != null && authentication.Matricula_Medico != null && authentication.Numero_Registro != null && authentication.DevToken != null)
            {
                IDbConnection minhaConexao = new LightBaseConnection("user=lbw;password=lbw;udb=DEFUDB;server=zdoc01");
                IDataReader reader;

                try
                {
                    minhaConexao.Open();
                    IDbCommand meuComando = new LightBaseCommand("select * from prontuario where medico.matricula_Medico=@matricula_Medico and numero_Registro=@numero_Registro");
                    meuComando.Parameters.Add(new LightBaseParameter("matricula_Medico", authentication.Matricula_Medico));
                    meuComando.Parameters.Add(new LightBaseParameter("numero_Registro", authentication.Numero_Registro));
                    meuComando.Connection = minhaConexao;
                    reader = meuComando.ExecuteReader();
                    while (reader.Read())
                    {
                        achou = true;
                    }
                }
                catch (Exception ex)
                {
                    string erro = ex.StackTrace;
                }
                finally
                {
                    minhaConexao.Close();
                }
            }
            return achou;
        }


        /*
         * Método que vai consultar um prontuário através de seu número de registro.
         */
        public List<Prontuario> obterProntuarioPorRegistro(AuthenticationSoapHeader authentication)
        {
            IDataReader reader;
            IDbConnection minhaConexao = new LightBaseConnection("user=lbw;password=lbw;udb=defudb;server=zdoc01");
            List<Prontuario> lista = new List<Prontuario>();
            try
            {
                minhaConexao.Open();
                Prontuario prontuario = new Prontuario();
                try
                {
                    IDbCommand meuComando = new LightBaseCommand("select * from prontuario where numero_Registro=@numero_Registro");
                    meuComando.Parameters.Add(new LightBaseParameter("numero_Registro", authentication.Numero_Registro));
                    meuComando.Connection = minhaConexao;
                    reader = meuComando.ExecuteReader();

                    while (reader.Read())
                    {
                        Endereco enderecos = new Endereco();
                        Telefones telefones = new Telefones();
                        Medicos medico = new Medicos();
                        Tecnicos tecnicos = new Tecnicos();

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
                        string cep = dt_Endereco.Rows[0]["cep"].ToString();

                        enderecos.endereco = endereco;
                        enderecos.numero = numero;
                        enderecos.complemento = complemento;
                        enderecos.bairro = bairro;
                        enderecos.nome_Cidade = nome_Cidade;
                        enderecos.nome_Estado = nome_Estado;
                        enderecos.cep = cep;

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

                        //Tratamento do campo multivalorado Técnicos.
                        DataTable dt_Tecnico = (DataTable)reader["tecnicos"];
                        string[] matriculas_Tecnico = { "", "", "", "" };
                        string[] nomes_Tecnico = { "", "", "", "" };
                        for (int i = 0; i < dt_Tecnico.Rows.Count; i++)
                        {
                            matriculas_Tecnico[i] = dt_Tecnico.Rows[i]["matricula_Tecnico"].ToString();
                            nomes_Tecnico[i] = dt_Tecnico.Rows[i]["nome_Tecnico"].ToString();
                        }
                        tecnicos.matricula_Tecnico1 = matriculas_Tecnico[0].ToString();
                        tecnicos.matricula_Tecnico2 = matriculas_Tecnico[1].ToString();
                        tecnicos.matricula_Tecnico3 = matriculas_Tecnico[2].ToString();
                        tecnicos.matricula_Tecnico4 = matriculas_Tecnico[3].ToString();
                        tecnicos.nome_Tecnico1 = nomes_Tecnico[0].ToString();
                        tecnicos.nome_Tecnico2 = nomes_Tecnico[1].ToString();
                        tecnicos.nome_Tecnico3 = nomes_Tecnico[2].ToString();
                        tecnicos.nome_Tecnico4 = nomes_Tecnico[3].ToString();

                        prontuario.medico = medico;
                        prontuario.tecnicos = tecnicos;
                        prontuario.endereco = enderecos;
                        prontuario.telefone = telefones;
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

        public bool validaAcesso(AuthenticationSoapHeader authentication)
        {
            IDbConnection minhaConexao = new LightBaseConnection("user=lbw;password=lbw;udb=DEFUDB;server=zdoc01");
            IDataReader reader;
            bool achou = false;
            try
            {
                minhaConexao.Open();
                IDbCommand meuComando = new LightBaseCommand("select * from prontuario where medico.matricula_Medico=@matricula_Medico and numero_Registro=@numero_Registro");
                meuComando.Parameters.Add(new LightBaseParameter("matricula_Medico", authentication.Matricula_Medico));
                meuComando.Parameters.Add(new LightBaseParameter("numero_Registro", authentication.Numero_Registro));
                meuComando.Connection = minhaConexao;
                reader = meuComando.ExecuteReader();
                while (reader.Read())
                {
                    achou = true;
                }
            }
            catch (Exception ex)
            {
                string erro = ex.StackTrace;
            }
            finally
            {
                minhaConexao.Close();
            }
            return achou;
        }


        public bool validaAcessoMedico(AuthenticationSoapHeader authentication)
        {
            IDbConnection minhaConexao = new LightBaseConnection("user=lbw;password=lbw;udb=DEFUDB;server=zdoc01");
            IDataReader reader;
            bool achou = false;
            try
            {
                minhaConexao.Open();
                IDbCommand meuComando = new LightBaseCommand("select * from prontuario where medico.matricula_Medico=@matricula_Medico");
                meuComando.Parameters.Add(new LightBaseParameter("matricula_Medico", authentication.Matricula_Medico));
                meuComando.Connection = minhaConexao;
                reader = meuComando.ExecuteReader();
                while (reader.Read())
                {
                    achou = true;
                }
            }
            catch (Exception ex)
            {
                string erro = ex.StackTrace;
            }
            finally
            {
                minhaConexao.Close();
            }
            return achou;
        }

        public List<Prontuario> consultarEquipe(AuthenticationSoapHeader authentication)
        {
            List<Prontuario> lista = new List<Prontuario>();
            Equipe equipe = new Equipe();
            lista = equipe.consultaEquipe(authentication);
            return lista;
        }


        /*
         * Método que vai consultar um prontuário através de seu número de registro.
         */
        public List<Prontuario> listarProntuarios(AuthenticationSoapHeader authentication)
        {
            IDataReader reader;
            IDbConnection minhaConexao = new LightBaseConnection("user=lbw;password=lbw;udb=defudb;server=zdoc01");
            List<Prontuario> lista = new List<Prontuario>();
            try
            {
                minhaConexao.Open();
                try
                {
                    IDbCommand meuComando = new LightBaseCommand("select * from prontuario where medico.matricula_Medico=@matricula_Medico");
                    meuComando.Parameters.Add(new LightBaseParameter("matricula_Medico", authentication.Matricula_Medico));
                    meuComando.Connection = minhaConexao;
                    reader = meuComando.ExecuteReader();

                    while (reader.Read())
                    {
                        Prontuario prontuario = new Prontuario();
                        Endereco enderecos = new Endereco();
                        Telefones telefones = new Telefones();
                        Medicos medico = new Medicos();
                        Tecnicos tecnicos = new Tecnicos();

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
                        string cep = dt_Endereco.Rows[0]["cep"].ToString();

                        enderecos.endereco = endereco;
                        enderecos.numero = numero;
                        enderecos.complemento = complemento;
                        enderecos.bairro = bairro;
                        enderecos.nome_Cidade = nome_Cidade;
                        enderecos.nome_Estado = nome_Estado;
                        enderecos.cep = cep;

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


                        //Tratamento do campo multivalorado Técnicos.
                        DataTable dt_Tecnico = (DataTable)reader["tecnicos"];
                        string[] matriculas_Tecnico = { "", "", "", "" };
                        string[] nomes_Tecnico = { "", "", "", "" };
                        for (int i = 0; i < dt_Tecnico.Rows.Count; i++)
                        {
                            matriculas_Tecnico[i] = dt_Tecnico.Rows[i]["matricula_Tecnico"].ToString();
                            nomes_Tecnico[i] = dt_Tecnico.Rows[i]["nome_Tecnico"].ToString();
                        }
                        tecnicos.matricula_Tecnico1 = matriculas_Tecnico[0].ToString();
                        tecnicos.matricula_Tecnico2 = matriculas_Tecnico[1].ToString();
                        tecnicos.matricula_Tecnico3 = matriculas_Tecnico[2].ToString();
                        tecnicos.matricula_Tecnico4 = matriculas_Tecnico[3].ToString();
                        tecnicos.nome_Tecnico1 = nomes_Tecnico[0].ToString();
                        tecnicos.nome_Tecnico2 = nomes_Tecnico[1].ToString();
                        tecnicos.nome_Tecnico3 = nomes_Tecnico[2].ToString();
                        tecnicos.nome_Tecnico4 = nomes_Tecnico[3].ToString();

                        prontuario.medico = medico;
                        prontuario.tecnicos = tecnicos;
                        prontuario.endereco = enderecos;
                        prontuario.telefone = telefones;
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
    }
}