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

namespace GED_TCESE 
{
    public class AdaptadorDespesa : IAdaptadorDespesa
    {
        public List<Despesa> Todos()
        {
            List<Despesa> lista = new List<Despesa>();
            IDataReader reader;
            IDbConnection minhaConexao = new LightBaseConnection("user=lbw;password=lbw;udb=defudb;server=localhost");
            try
            {
                minhaConexao.Open();
                IDbCommand meuComando = new LightBaseCommand("select id, arq_Arquivo, documento1, documento2, documento3, documento4, documento5, documento6 from folder245_indices5");
                meuComando.Connection = minhaConexao;
                reader = meuComando.ExecuteReader();

                while (reader.Read())
                {
                    Despesa despesa = new Despesa();
                    despesa.id = Convert.ToInt32(reader["id"]);
                    despesa.arq_Arquivo = Convert.ToString(reader["arq_Arquivo"]);
                    despesa.documento1 = Convert.ToString(reader["documento1"]);
                    despesa.documento2 = Convert.ToString(reader["documento2"]);
                    despesa.documento3 = Convert.ToString(reader["documento3"]);
                    despesa.documento4 = Convert.ToString(reader["documento4"]);
                    despesa.documento5 = Convert.ToString(reader["documento5"]);
                    despesa.documento6 = Convert.ToString(reader["documento6"]);
                    lista.Add(despesa);
                }
                return lista;
            }
            finally
            {
                minhaConexao.Close();
            }
        }

        public List<Despesa> porColuna(string select, string coluna, string orderBy)
        {
            List<Despesa> lista = new List<Despesa>();
            IDataReader reader;
            IDbConnection minhaConexao = new LightBaseConnection("user=lbw;password=lbw;udb=defudb;server=localhost");
            try
            {
                minhaConexao.Open();
                try
                {
                    IDbCommand meuComando = new LightBaseCommand("select id, arq_Arquivo, documento1, documento2, documento3, documento4, documento5, documento6 from folder245_indices5 order by " + coluna);
                    meuComando.Connection = minhaConexao;
                    reader = meuComando.ExecuteReader();

                    while (reader.Read())
                    {
                        Despesa despesa = new Despesa();
                        despesa.id = Convert.ToInt32(reader["id"]);
                        despesa.arq_Arquivo = Convert.ToString(reader["arq_Arquivo"]);
                        despesa.documento1 = Convert.ToString(reader["documento1"]);
                        despesa.documento2 = Convert.ToString(reader["documento2"]);
                        despesa.documento3 = Convert.ToString(reader["documento3"]);
                        despesa.documento4 = Convert.ToString(reader["documento4"]);
                        despesa.documento5 = Convert.ToString(reader["documento5"]);
                        despesa.documento6 = Convert.ToString(reader["documento6"]);
                        lista.Add(despesa);
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

        public List<Despesa> PesquisaPorCampo(string comando)
        {
            List<Despesa> lista = new List<Despesa>();
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
                        Despesa despesa = new Despesa();
                        despesa.id = Convert.ToInt32(reader["id"]);
                        despesa.arq_Arquivo = Convert.ToString(reader["arq_Arquivo"]);
                        despesa.documento1 = Convert.ToString(reader["documento1"]);
                        despesa.documento2 = Convert.ToString(reader["documento2"]);
                        despesa.documento3 = Convert.ToString(reader["documento3"]);
                        despesa.documento4 = Convert.ToString(reader["documento4"]);
                        despesa.documento5 = Convert.ToString(reader["documento5"]);
                        despesa.documento6 = Convert.ToString(reader["documento6"]);
                        lista.Add(despesa);
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

        public Despesa obterDespesaPorId(string valor)
        {
            IDataReader reader;
            IDbConnection minhaConexao = new LightBaseConnection("user=lbw;password=lbw;udb=defudb;server=localhost");
            try
            {
                minhaConexao.Open();
                Despesa despesa = new Despesa();
                try
                {
                    IDbCommand meuComando = new LightBaseCommand("select id, arq_Arquivo from folder245_indices5 where id = " + valor);
                    meuComando.Connection = minhaConexao;
                    reader = meuComando.ExecuteReader();
                    while (reader.Read())
                    {
                        despesa.id = Convert.ToInt32(reader["id"]);
                        despesa.arq_Arquivo = Convert.ToString(reader["arq_Arquivo"]);
                    }
                }
                catch (LightBaseException e)
                {
                    string erro = e.Message;
                }
                return despesa;
            }
            finally
            {
                minhaConexao.Close();
            }
        }

        public List<Despesa> PesquisarCampos(string comando)
        {
            List<Despesa> lista = new List<Despesa>();
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
                        Despesa despesa = new Despesa();
                        despesa.id = Convert.ToInt16(reader["id"]);
                        despesa.arq_Arquivo = Convert.ToString(reader["arq_Arquivo"]);
                        despesa.documento1 = Convert.ToString(reader["documento1"]);
                        despesa.documento2 = Convert.ToString(reader["documento2"]);
                        despesa.documento3 = Convert.ToString(reader["documento3"]);
                        despesa.documento4 = Convert.ToString(reader["documento4"]);
                        despesa.documento5 = Convert.ToString(reader["documento5"]);
                        despesa.documento6 = Convert.ToString(reader["documento6"]);
                        lista.Add(despesa);
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