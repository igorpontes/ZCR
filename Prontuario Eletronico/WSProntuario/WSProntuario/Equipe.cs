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

namespace WSProntuario
{
    public class Equipe
    {
        private Medicos medicos { get; set; }
        private Tecnicos tecnicos { get; set; }

        public List<Prontuario> consultaEquipe(AuthenticationSoapHeader authentication)
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
                    IDbCommand meuComando = new LightBaseCommand("select * from prontuario where medico.matricula_Medico=@matricula_Medico and numero_Registro=@numero_Registro");
                    meuComando.Parameters.Add(new LightBaseParameter("matricula_Medico", authentication.Matricula_Medico));
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
                        prontuario.tecnicos = tecnicos;
                        prontuario.medico = medico;
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