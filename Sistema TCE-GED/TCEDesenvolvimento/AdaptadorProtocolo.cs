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
    public class AdaptadorProtocolo : IAdaptadorProtocolo
    {
        public List<Protocolo> Todos()
        {
            List<Protocolo> lista = new List<Protocolo>();
            IDataReader reader;
            IDbConnection minhaConexao = new LightBaseConnection("user=lbw;password=lbw;udb=defudb;server=localhost");
            try
            {
                minhaConexao.Open();
                IDbCommand meuComando = new LightBaseCommand("select id, arq_Arquivo, documento1, documento2, documento3, documento4, documento5, documento6 from folder245_indices6");
                meuComando.Connection = minhaConexao;
                reader = meuComando.ExecuteReader();

                while (reader.Read())
                {
                    Protocolo protocolo = new Protocolo();
                    protocolo.id = Convert.ToInt32(reader["id"]);
                    protocolo.arq_Arquivo = Convert.ToString(reader["arq_Arquivo"]);
                    protocolo.documento1 = Convert.ToString(reader["documento1"]);
                    protocolo.documento2 = Convert.ToString(reader["documento2"]);
                    protocolo.documento3 = Convert.ToString(reader["documento3"]);
                    protocolo.documento4 = Convert.ToString(reader["documento4"]);
                    protocolo.documento5 = Convert.ToString(reader["documento5"]);
                    protocolo.documento6 = Convert.ToString(reader["documento6"]);
                    lista.Add(protocolo);
                }
                return lista;
            }
            finally
            {
                minhaConexao.Close();
            }
        }

        public List<Protocolo> porColuna(string select, string coluna, string orderBy)
        {
            List<Protocolo> lista = new List<Protocolo>();
            IDataReader reader;
            IDbConnection minhaConexao = new LightBaseConnection("user=lbw;password=lbw;udb=defudb;server=localhost");
            try
            {
                minhaConexao.Open();
                try
                {
                    IDbCommand meuComando = new LightBaseCommand("select id, arq_Arquivo, documento1, documento2, documento3, documento4, documento5, documento6 from folder245_indices6 order by " + coluna);
                    meuComando.Connection = minhaConexao;
                    reader = meuComando.ExecuteReader();

                    while (reader.Read())
                    {
                        Protocolo protocolo = new Protocolo();
                        protocolo.id = Convert.ToInt32(reader["id"]);
                        protocolo.arq_Arquivo = Convert.ToString(reader["arq_Arquivo"]);
                        protocolo.documento1 = Convert.ToString(reader["documento1"]);
                        protocolo.documento2 = Convert.ToString(reader["documento2"]);
                        protocolo.documento3 = Convert.ToString(reader["documento3"]);
                        protocolo.documento4 = Convert.ToString(reader["documento4"]);
                        protocolo.documento5 = Convert.ToString(reader["documento5"]);
                        protocolo.documento6 = Convert.ToString(reader["documento6"]);
                        lista.Add(protocolo);
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

        public List<Protocolo> PesquisaPorCampo(string comando)
        {
            List<Protocolo> lista = new List<Protocolo>();
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
                        Protocolo protocolo = new Protocolo();
                        protocolo.id = Convert.ToInt32(reader["id"]);
                        protocolo.arq_Arquivo = Convert.ToString(reader["arq_Arquivo"]);
                        protocolo.documento1 = Convert.ToString(reader["documento1"]);
                        protocolo.documento2 = Convert.ToString(reader["documento2"]);
                        protocolo.documento3 = Convert.ToString(reader["documento3"]);
                        protocolo.documento4 = Convert.ToString(reader["documento4"]);
                        protocolo.documento5 = Convert.ToString(reader["documento5"]);
                        protocolo.documento6 = Convert.ToString(reader["documento6"]);
                        lista.Add(protocolo);
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

        public Protocolo obterProtocoloPorId(string valor)
        {
            IDataReader reader;
            IDbConnection minhaConexao = new LightBaseConnection("user=lbw;password=lbw;udb=defudb;server=localhost");
            try
            {
                minhaConexao.Open();
                Protocolo protocolo = new Protocolo();
                try
                {
                    IDbCommand meuComando = new LightBaseCommand("select id, arq_Arquivo from folder245_indices6 where id = " + valor);
                    meuComando.Connection = minhaConexao;
                    reader = meuComando.ExecuteReader();
                    while (reader.Read())
                    {
                        protocolo.id = Convert.ToInt32(reader["id"]);
                        protocolo.arq_Arquivo = Convert.ToString(reader["arq_Arquivo"]);
                    }
                }
                catch (LightBaseException e)
                {
                    string erro = e.Message;
                }
                return protocolo;
            }
            finally
            {
                minhaConexao.Close();
            }
        }

        public List<Protocolo> PesquisarCampos(string comando)
        {
            List<Protocolo> lista = new List<Protocolo>();
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
                        Protocolo protocolo = new Protocolo();
                        protocolo.id = Convert.ToInt16(reader["id"]);
                        protocolo.arq_Arquivo = Convert.ToString(reader["arq_Arquivo"]);
                        protocolo.documento1 = Convert.ToString(reader["documento1"]);
                        protocolo.documento2 = Convert.ToString(reader["documento2"]);
                        protocolo.documento3 = Convert.ToString(reader["documento3"]);
                        protocolo.documento4 = Convert.ToString(reader["documento4"]);
                        protocolo.documento5 = Convert.ToString(reader["documento5"]);
                        protocolo.documento6 = Convert.ToString(reader["documento6"]);
                        lista.Add(protocolo);
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