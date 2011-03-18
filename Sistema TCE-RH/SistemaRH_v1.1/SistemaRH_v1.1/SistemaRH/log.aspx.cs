using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using LightInfocon.GoldenAccess.General;
using System.Collections.Generic;
using System.IO;
using SistemaRH.Properties;


namespace SistemaRH
{
    /// <summary>
    /// Classe responsável pela listagem do LOG do sistema
    /// </summary>
    public partial class WebFormLog : System.Web.UI.Page
    {
        string usuarioConectado;
        string senhaConectado;
        string coluna;
        string erro;
        string orderBy;
        string comandoMontado;
        string campos;
        string comando;
        List<Log> listaLog = new List<Log>();

        /// <summary>
        /// Método que verifica se o usuário está autenticado e preenche o grid com a listagem de LOG.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                usuarioConectado = (String)Session["usuario"];
                senhaConectado = (String)Session["senha"];
                GoldenAccess goldenAccess = new GoldenAccess("http://localhost:3271/GoldenAccess.soap");
                User usuario = new User(usuarioConectado, senhaConectado);
                try
                {
                    usuario = goldenAccess.Authenticate(usuarioConectado, senhaConectado);
                }
                catch (Exception ex)
                {
                    
                }
                if (usuario.IsAuthenticated && !usuario.Disabled)
                {

                }
                else
                {
                    erro = "Usuário não autenticado";
                    Session.Add("erro", erro);
                    Server.Transfer("Login.aspx");
                    Session.Abandon();
                }
                Session.Add("coluna", "id");
                Session.Add("orderBy", "desc");
                Session.Add("campos", null);
                comando = "textsearch in log ";
                Session.Add("pesquisaLOG", comando);
                Adaptador adpt = new Adaptador();
                listaLog = adpt.TodosLogs();
                Session.Add("listaLog", listaLog);
                //GridView1.DataSource = bindGrid("sort");
                GridView1.DataSource = listaLog;
                GridView1.DataBind();
                
                
            }
            listaLog = (List<Log>)Session["listaLog"];
        }



        /// <summary>
        /// Método usado para mudar a página do GridView1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewPageEventArgs"/> instance containing the event data.</param>
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //comando = (String)Session["pesquisaLOG"];
            GridView1.PageIndex = e.NewPageIndex;
            //GridView1.DataSource = bindGrid("indexchange");
            GridView1.DataSource = listaLog;
            GridView1.DataBind();
        }

        /// <summary>
        /// Método usado para ordenar GridView1 control usando as classes internas de ordenação.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewSortEventArgs"/> instance containing the event data.</param>
        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            comando = (String)Session["pesquisaLOG"];
            coluna = e.SortExpression;
            Session.Add("coluna", coluna);
            if (coluna == "usuario_log")
            {
                if ((String)Session["orderBy"] == "asc")
                {
                    Session["orderBy"] = "desc";
                    listaLog.Sort(new LogSort_Usuario_DESC());
                }
                else
                {
                    Session["orderBy"] = "asc";
                    listaLog.Sort(new LogSort_Usuario_ASC());
                }
               
            }
            else if (coluna == "tipo_acao_log")
            {
                if ((String)Session["orderBy"] == "asc")
                {
                    Session["orderBy"] = "desc";
                    listaLog.Sort(new LogSort_Tipo_DESC());
                }
                else
                {
                    Session["orderBy"] = "asc";
                    listaLog.Sort(new LogSort_Tipo_ASC());
                }
            }
            else if (coluna == "mensagem_acao_log")
            {
                if ((String)Session["orderBy"] == "asc")
                {
                    Session["orderBy"] = "desc";
                    listaLog.Sort(new LogSort_Mensagem_DESC());
                }
                else
                {
                    Session["orderBy"] = "asc";
                    listaLog.Sort(new LogSort_Mensagem_ASC());
                }
            }
            else if (coluna == "data_log")
            {
                if ((String)Session["orderBy"] == "asc")
                {
                    Session["orderBy"] = "desc";
                    listaLog.Sort(new LogSort_Data_DESC());
                }
                else
                {
                    Session["orderBy"] = "asc";
                    listaLog.Sort(new LogSort_Data_ASC());
                }
            }

            //GridView1.DataSource = bindGrid("sort");
            GridView1.DataSource = listaLog;
            GridView1.DataBind();
        }

        /// <summary>
        /// Método usado para fazer pesquisa dentro da lista de LOGs.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ButtonEnviarPesquisa_Click(object sender, EventArgs e)
        {
            comando = (String)Session["pesquisaLOG"];
            campos = (String)Session["campos"];
            Adaptador adpt = new Adaptador();
            if (IsPostBack)
            {
                
                if (campos == TextBoxPesquisa.Text)
                {
                    GridView1.DataSource = adpt.PesquisarCamposLOG(comando);
                    GridView1.DataBind();
                }
                else
                {
                    if (campos == null)
                    {
                        campos = TextBoxPesquisa.Text;
                    }
                    else
                    {
                        campos += " " + TextBoxPesquisa.Text;
                    }
                    Session["campos"] = campos;
                    string parametros = montarComando(campos);
                    //string parametros = TextBoxPesquisa.Text;
                    if (TextBoxPesquisa.Text == null)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), Guid.NewGuid().ToString(), "window.alert('Parâmetro Inválido.');", true);
                    }
                    else
                    {
                        comando = "textsearch in log " + montaParametros(parametros);
                        listaLog = adpt.PesquisarCamposLOG(comando);
                        GridView1.DataSource = listaLog;
                        GridView1.DataBind();
                        Session["pesquisaLOG"] = comando;
                        Session["listaLog"] = listaLog;
                    }

                }
            }
        }

        /// <summary>
        /// Método usado para montar parte de um comando da LightBase com os parametros passados.
        /// </summary>
        /// <param name="parametros">Parametros de pesquisa.</param>
        /// <returns></returns>
        public string montaParametros(string parametros)
        {
            string comandoNovo = "("+parametros + ")[usuario_log] OU (" + parametros + ")[tipo_acao_log] OU (" + parametros + ")[mensagem_acao_log] OU (" + parametros + ")[data_log] ";

            return comandoNovo;
        }

        /// <summary>
        /// Método usado para montar o comando de pesquisa.
        /// </summary>
        /// <param name="pComando">A string a ser tratada.</param>
        /// <returns>a string tratada do comando.</returns>
        public string montarComando(string pComando)
        {
            string[] nomes = pComando.Split(' ');
            for (int i = 0; i < nomes.Length; i++)
            {
                if (nomes[i].Length > 2)
                {
                    comandoMontado = comandoMontado + " " + "\"" + nomes[i] + "\"" + " E";
                }


                if (i == nomes.Length - 1 && pComando.Length >= 2)
                {
                    try
                    {
                        comandoMontado = comandoMontado.Substring(0, comandoMontado.LastIndexOf("E") - 1);
                    }
                    catch (Exception ex)
                    {
                        string temp = ex.Message;
                    }
                }
            }
            return comandoMontado;
        }

        /// <summary>
        /// Método usado para fazer um filtro por um intervalo de datas.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        protected void ImageButtonPesquisaIntervaloDatas_Click(object sender, ImageClickEventArgs e)
        {
            string dataInicio, dataFinal;
            dataInicio = TextBoxDataInicio.Text;
            dataFinal = TextBoxDataFinal.Text;
            DateTime inicio, final;
            IFormatProvider theCultureInfo = new System.Globalization.CultureInfo("en-US", true);
            inicio = DateTime.ParseExact(dataInicio, "dd/MM/yyyy", theCultureInfo);
            final = DateTime.ParseExact(dataFinal, "dd/MM/yyyy", theCultureInfo);

            GridView1.DataSource = intervaloDatas(listaLog, inicio, final);
            GridView1.DataBind();
        }

        /// <summary>
        /// Método usado para retornar uma lista filtrada com o intervalo de datas.
        /// </summary>
        /// <param name="lista">Lista de LOG.</param>
        /// <param name="dataInicio">A data inicial.</param>
        /// <param name="dataFinal">A data final.</param>
        /// <returns>a lista filtrada</returns>
        protected List<Log> intervaloDatas(List<Log> lista, DateTime dataInicio, DateTime dataFinal)
        {
            List<Log> listaTemp = new List<Log>();
            foreach (var item in lista)
            {
                if ((item.data_log.CompareTo(dataInicio) >= 0) && (item.data_log.CompareTo(dataFinal) <= 0))
	            {
                    listaTemp.Add(item);
	            } 
            }
            Session["listaLog"] = listaTemp;
            return listaTemp;
        }
        
    }

    #region Internal Classes Comparer
    /// <summary>
    /// Realiza Comparações pelo Usuario Ascendente
    /// </summary>
    internal class LogSort_Usuario_ASC : IComparer<Log>
    {
        public int Compare(Log x, Log y)
        {
            return x.usuario_log.CompareTo(y.usuario_log);
        }
    }

    /// <summary>
    /// Realiza Comparações pelo Usuario Descendente
    /// </summary>
    internal class LogSort_Usuario_DESC : IComparer<Log>
    {
        public int Compare(Log x, Log y)
        {
            return y.usuario_log.CompareTo(x.usuario_log);
        }
    }

    /// <summary>
    /// Realiza Comparações pelo Tipo Ascendente
    /// </summary>
    internal class LogSort_Tipo_ASC : IComparer<Log>
    {
        public int Compare(Log x, Log y)
        {
            return x.tipo_acao_log.CompareTo(y.tipo_acao_log);
        }
    }

    /// <summary>
    /// Realiza Comparações pelo Tipo Descendente
    /// </summary>
    internal class LogSort_Tipo_DESC : IComparer<Log>
    {
        public int Compare(Log x, Log y)
        {
            return y.tipo_acao_log.CompareTo(x.tipo_acao_log);
        }
    }

    /// <summary>
    /// Realiza Comparações pela Mensagem Ascendente
    /// </summary>
    internal class LogSort_Mensagem_ASC : IComparer<Log>
    {
        public int Compare(Log x, Log y)
        {
            return x.mensagem_acao_log.CompareTo(y.mensagem_acao_log);
        }
    }

    /// <summary>
    /// Realiza Comparações pela Mensagem Descendente
    /// </summary>
    internal class LogSort_Mensagem_DESC : IComparer<Log>
    {
        public int Compare(Log x, Log y)
        {
            return y.mensagem_acao_log.CompareTo(x.mensagem_acao_log);
        }
    }

    /// <summary>
    /// Realiza Comparações pela Data Ascendente
    /// </summary>
    internal class LogSort_Data_ASC : IComparer<Log>
    {
        public int Compare(Log x, Log y)
        {
            IFormatProvider theCultureInfo = new System.Globalization.CultureInfo("en-US", true);
            DateTime dt1 = x.data_log;
            DateTime dt2 = y.data_log;
            return dt1.CompareTo(dt2);
        }
    }

    /// <summary>
    /// Realiza Comparações pela Data Descendente
    /// </summary>
    internal class LogSort_Data_DESC : IComparer<Log>
    {
        public int Compare(Log x, Log y)
        {
            IFormatProvider theCultureInfo = new System.Globalization.CultureInfo("en-US", true);
            DateTime dt1 = x.data_log;
            DateTime dt2 = y.data_log;
            return dt2.CompareTo(dt1);
        }
    }

    #endregion

    #region Nao Usando

     
        //protected List<Log> bindGrid(string opcao)
        //{
            
        //    comando = (String)Session["pesquisaLOG"];
        //    List<Log> lista = new List<Log>();
        //    Adaptador adpt = new Adaptador();
        //    if (opcao == "sort")
        //    {
        //        if ((String)Session["orderBy"] == null)
        //        {
        //            orderBy = "desc";
        //            Session.Add("orderBy", orderBy);
        //        }
        //        else
        //        {
        //            if ((String)Session["coluna"] != "id")
        //            {
        //                if ((String)Session["orderBy"] == "asc")
        //                {
        //                    orderBy = "desc";
        //                    Session["orderBy"] = orderBy;
        //                }
        //                else
        //                {
        //                    orderBy = "asc";
        //                    Session["orderBy"] = orderBy;
        //                }
        //            }
        //            else
        //            {
        //                orderBy = "desc";
        //                Session["orderBy"] = orderBy;
        //            }
                    
        //        }

        //        //coluna = (String)Session["coluna"];
        //        if (coluna == null)
        //        {
        //            coluna = "id";
        //        }
        //        //lista = adpt.PorColunaLOG(null, coluna, orderBy);
        //    }
        //    else if (opcao == "indexchange")
        //    {
        //        orderBy = (String)Session["orderBy"];
        //    }


        //    coluna = (String)Session["coluna"];
        //    if (coluna == null)
        //    {
        //        coluna = "id";
        //    }
        //    lista = adpt.PorColunaLOG(comando, coluna, orderBy);
        //    return lista;
        //}

    #endregion
}
