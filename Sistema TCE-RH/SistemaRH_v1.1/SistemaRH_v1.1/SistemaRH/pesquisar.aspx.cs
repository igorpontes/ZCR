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
    /// Classe responsável pela página que lista a pesquisa feita.
    /// </summary>
    public partial class Pesquisar : System.Web.UI.Page
    {
        string comando;
        string usuarioConectado;
        string senhaConectado;
        string diretorio;
        string coluna;
        string erro;
        string orderBy;
        string comandoMontado;
        string campos;

        /// <summary>
        /// Método Load que garante o usuário autenticado e preenche o GridView com a pesquisa realizada.
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
                    LabelErro.Text = ex.Message;
                    ImageAttention.Visible = true;
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
                comando = (String)Session["pesquisa"];
                Session.Add("coluna", null);
                //Session.Add("orderBy", "desc");
                Session.Add("campos", null);
                Adaptador adpt = new Adaptador();
                GridView1.DataSource = adpt.PesquisarCampos(comando);
                GridView1.DataBind();
            }
        }



        /// <summary>
        /// Método do evento RowCommand que controla o GridView.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewCommandEventArgs"/> instance containing the event data.</param>
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index;

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
                LabelErro.Text = ex.Message;
                ImageAttention.Visible = true;
            }
            if (usuario.IsAuthenticated && !usuario.Disabled)
            {
                if (usuario.HasGroup("RHADM"))
                {
                    if (e.CommandName == "Abrir")
                    {
                        string nome = "";
                        try
                        {
                            index = Convert.ToInt32(e.CommandArgument) - (GridView1.PageSize * GridView1.PageIndex);
                            int id = Convert.ToInt32(GridView1.DataKeys[index].Value);
                            Adaptador adpt = new Adaptador();
                            Documento processo = new Documento();
                            processo = adpt.obterDocumentoPorId(id.ToString());
                            //nome = processo.arq_Arquivo;
                            diretorio = Server.MapPath(Settings.Default.CaminhoDoRepositorioDocumento);
                            if (Directory.Exists(diretorio))
                            {
                                Response.Clear();
                                Response.ClearHeaders();
                                Response.AddHeader("Content-Type", "application/pdf");
                                Response.AddHeader("Content-Disposition", "attachment; filename=" + nome);

                                FileStream file = new FileStream(diretorio + nome, System.IO.FileMode.Open, FileAccess.Read);
                                byte[] bytes = new byte[Convert.ToInt32(file.Length)];
                                file.Read(bytes, 0, bytes.Length);
                                file.Close();

                                Response.OutputStream.Write(bytes, 0, bytes.GetUpperBound(0));

                                Response.Flush();
                                Response.Close();
                            }
                            else
                            {
                                LabelErro.Text = "Diretório " + diretorio + "  não encontrado";
                                ImageAttention.Visible = true;
                            }
                        }
                        catch (Exception ex)
                        {
                            LabelErro.Text = ex.Message;
                            ImageAttention.Visible = true;
                        }
                    }

                    if (e.CommandName == "Alterar")
                    {
                        index = Convert.ToInt32(e.CommandArgument) - (GridView1.PageSize * GridView1.PageIndex);
                        int id = Convert.ToInt32(GridView1.DataKeys[index].Value);
                        Session.Add("id", id.ToString());
                        Server.Transfer("alterarColaborador.aspx");
                    }

                    if (e.CommandName == "Excluir")
                    {
                        index = Convert.ToInt32(e.CommandArgument) - (GridView1.PageSize * GridView1.PageIndex); ;
                        int id = Convert.ToInt32(GridView1.DataKeys[index].Value);
                        Adaptador adpt = new Adaptador();

                        Documento documento = new Documento();
                        documento = adpt.obterDocumentoPorId(id.ToString());
                        //testar se ta pegando os arquivos certos
                        if (documento.arquivos != null)
                        {
                            //loop que pega os nomes dos arquivos e deleta
                            foreach (var arq in documento.arquivos)
                            {
                                string nomeArquivoAntigo = arq.nome_Arquivo;
                                if (nomeArquivoAntigo != null)
                                {
                                    string diretorioRemover = HttpContext.Current.Server.MapPath("~/arquivos/") + nomeArquivoAntigo;
                                    FileInfo arquivoAntigo = new FileInfo(diretorioRemover);
                                    arquivoAntigo.Delete();
                                }
                            }
                        }
                        adpt.RemoverDocumento(id);

                        Log log = new Log();
                        log.data_log = DateTime.Now;
                        log.tipo_acao_log = "Excluir";
                        log.usuario_log = (String)Session["usuario"];
                        log.mensagem_acao_log = "O usuário " + log.usuario_log + " deletou o colaborador de matrícula " + documento.matricula_Colaborador;
                        adpt.InserirLog(log);

                        GridView1.DataSource = adpt.Todos();
                        GridView1.DataBind();
                    }
                }
                else if (usuario.HasGroup("RHLIM"))
                {
                    if (e.CommandName == "Abrir")
                    {
                        string nome = "";
                        try
                        {
                            index = Convert.ToInt32(e.CommandArgument);
                            int id = Convert.ToInt32(GridView1.DataKeys[index].Value);
                            Adaptador adpt = new Adaptador();
                            Documento processo = new Documento();
                            processo = adpt.obterDocumentoPorId(id.ToString());
                            //ver como faço pra colocar o nome do arquivo dos documentos(titulacoes , CIs...)
                            //nome = processo.arq_Arquivo;
                            diretorio = Server.MapPath(Settings.Default.CaminhoDoRepositorioDocumento);
                            if (Directory.Exists(diretorio))
                            {
                                Response.Clear();
                                Response.ClearHeaders();
                                Response.AddHeader("Content-Type", "application/pdf");
                                Response.AddHeader("Content-Disposition", "attachment; filename=" + nome);

                                FileStream file = new FileStream(diretorio + nome, System.IO.FileMode.Open, FileAccess.Read);
                                byte[] bytes = new byte[Convert.ToInt32(file.Length)];
                                file.Read(bytes, 0, bytes.Length);
                                file.Close();

                                Response.OutputStream.Write(bytes, 0, bytes.GetUpperBound(0));

                                Response.Flush();
                                Response.Close();
                            }
                            else
                            {
                                LabelErro.Text = "Diretório " + diretorio + "  não encontrado";
                                ImageAttention.Visible = true;
                            }
                        }
                        catch (Exception ex)
                        {
                            LabelErro.Text = ex.Message;
                            ImageAttention.Visible = true;
                        }
                    }

                    if (e.CommandName == "Alterar")
                    {
                        LabelErro.Text = "Você não tem permissão para " + e.CommandName + " esse registro.";
                        ImageAttention.Visible = true;
                    }

                    if (e.CommandName == "Excluir")
                    {
                        LabelErro.Text = "Você não tem permissão para " + e.CommandName + " esse registro.";
                        ImageAttention.Visible = true;
                    }
                }
                else
                {
                    LabelErro.Text = "Você não tem permissão para " + e.CommandName + " esse registro.";
                    ImageAttention.Visible = true;
                }
            }
            else
            {
                LabelErro.Text = "Você não é usuário do sistema.";
                ImageAttention.Visible = true;
            }
        }

        /// <summary>
        /// Método usado para ordenar o GridView.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewSortEventArgs"/> instance containing the event data.</param>
        protected void GridView1_Sorting1(object sender, GridViewSortEventArgs e)
        {
            comando = (String)Session["pesquisa"];
            coluna = e.SortExpression;
            Session.Add("coluna", coluna);
            GridView1.DataSource = bindGrid("sort");
            GridView1.DataBind();
        }

        /// <summary>
        /// Método usado para mudar o indice do GridView.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewPageEventArgs"/> instance containing the event data.</param>
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            comando = (String)Session["pesquisa"];
            GridView1.PageIndex = e.NewPageIndex;
            Adaptador adpt = new Adaptador();
            //GridView1.DataSource = adpt.PesquisarCampos(comando);
            GridView1.DataSource = bindGrid("indexchange");
            GridView1.DataBind();
        }

        /// <summary>
        /// Método usado para retornar uma lista ordenada ou uma lista com um indice diferente.
        /// </summary>
        /// <param name="opcao">Opção escolhida se é de ordenaçao ou mudança de indice.</param>
        /// <returns>a lista modificada.</returns>
        protected List<Documento> bindGrid(string opcao)
        {
            comando = (String)Session["pesquisa"];
            List<Documento> lista = new List<Documento>();
            Adaptador adpt = new Adaptador();
            if (opcao == "sort")
            {
                if ((String)Session["orderBy"] == null)
                {
                    orderBy = "asc";
                    Session.Add("orderBy", orderBy);
                }
                else
                {
                    if ((String)Session["orderBy"] == "asc")
                    {
                        orderBy = "desc";
                        Session["orderBy"] = orderBy;
                    }
                    else
                    {
                        orderBy = "asc";
                        Session["orderBy"] = orderBy;
                    }
                }
            }
            else if (opcao == "indexchange")
            {
                orderBy = (String)Session["orderBy"];
            }


            coluna = (String)Session["coluna"];
            if (coluna == null)
            {
                coluna = "id";
            }
            lista = adpt.PorColuna(comando, coluna, orderBy);
            return lista;
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
        /// Método usado para processar a pesquisa dentro da lista de Colaboradores.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ButtonEnviarPesquisa_Click(object sender, EventArgs e)
        {
            comando = (String)Session["pesquisa"];
            campos = (String)Session["campos"];
            Adaptador adpt = new Adaptador();
            if (IsPostBack)
            {
                if (campos == TextBoxPesquisa.Text)
                {
                    GridView1.DataSource = adpt.PesquisarCampos(comando);
                    GridView1.DataBind();
                }
                else
                {
                    Session["campos"] = TextBoxPesquisa.Text;
                    string parametros = montarComando(TextBoxPesquisa.Text);
                    if (parametros == null)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), Guid.NewGuid().ToString(), "window.alert('Parâmetro Inválido.');", true);
                    }
                    else
                    {
                        comando += " E " + parametros;
                        GridView1.DataSource = adpt.PesquisarCampos(comando);
                        GridView1.DataBind();
                    }
                   
                }
            }

            //List<Documento> lista = new List<Documento>();
            //lista = adpt.PesquisarCampos(comando);

            //GridView1.DataSource = lista;
            //GridView1.DataBind();
        }
    }
}
