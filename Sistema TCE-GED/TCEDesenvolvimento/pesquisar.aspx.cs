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
using System.Collections.Generic;
using LightInfocon.GoldenAccess.General;
using System.IO;
using GED_TCESE.Properties;
using System.Text.RegularExpressions;

namespace GED_TCESE
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        string erro;
        string usuarioConectado;
        string senhaConectado;
        string diretorio = "";
        string coluna;
        string comando;
        string orderBy;
        string pesquisa;
        string comandoMontado;
        string campos;
        string nome = "";

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
                    comandoMontado = comandoMontado.Substring(0, comandoMontado.LastIndexOf("E") - 1);
                }
            }
            return comandoMontado;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pesquisa = (String)Session["ComandoPesquisa"];
                campos = (String)Session["Campos"];
                string[] parametros = campos.Split(' ');
                for (int i = 0; i < parametros.Length; i++)
                {
                    if (i < parametros.Length)
                    {
                        TextBoxComandoPesquisa.Text += parametros[i] + " ";
                    }
                }
            }
            TextBoxComandoPesquisa.Focus();
            
            if (!IsPostBack)
            {
                ImageButton lkListar = (ImageButton)Master.FindControl("ImageButtonListar");
                lkListar.Visible = false;
                ImageButton lkCadastrar = (ImageButton)Master.FindControl("ImageButtonCadastrar");
                lkCadastrar.Visible = false;
                ImageButton lkAjuda = (ImageButton)Master.FindControl("ImageButtonAjuda");
                lkAjuda.Visible = false;
                comando = (String)Session["lista"];
                Adaptador adpt = new Adaptador();
                List<Processo> processos = new List<Processo>();
                GridView1.DataSource = adpt.PesquisarCampos(comando);
                GridView1.DataBind();
                usuarioConectado = (String)Session["usuario"];
                senhaConectado = (String)Session["senha"];
                GoldenAccess servicoDeAutenticacao = new GoldenAccess("http://localhost:3271/GoldenAccess.soap");
                User usuarioGoldenAccess = new User(usuarioConectado, senhaConectado);
                try
                {
                    usuarioGoldenAccess = servicoDeAutenticacao.Authenticate(usuarioConectado, senhaConectado);
                }
                catch (Exception ex)
                {
                    LabelErro.Text = ex.Message;
                }
                if (usuarioGoldenAccess.IsAuthenticated && !usuarioGoldenAccess.Disabled)
                {
                }
                else
                {
                    erro = "Usuário não autenticado";
                    Session.Add("erro", erro);
                    Server.Transfer("Login.aspx");
                    Session.Abandon();
                }
            }
        }

        protected void LinkButtonVoltar_Click(object sender, EventArgs e)
        {
            int modulo = (Int32)Session["modulo"];
            string pagina = (String)Session["pagina"];

            if ((modulo == 0 || modulo == 1 || modulo == 2) && (pagina == "buscaAvancada"))
            {
                Server.Transfer("buscaavancada.aspx");
            }
            else
            {
                Server.Transfer("default.aspx");
            }
        }

        protected void GridView1_RowCommand1(object sender, GridViewCommandEventArgs e)
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
            }
            if (usuario.IsAuthenticated && !usuario.Disabled)
            {
                if (usuario.HasGroup("TCESEADM"))
                {
                    if (e.CommandName == "Alterar")
                    {
                        index = Convert.ToInt32(e.CommandArgument);
                        string id = (String)GridView1.DataKeys[index].Value.ToString();
                        Session.Add("id", id);
                        Server.Transfer("Alterar.aspx");
                    }

                    if (e.CommandName == "Excluir")
                    {
                        index = Convert.ToInt32(e.CommandArgument);
                        int id = Convert.ToInt32(GridView1.DataKeys[index].Value);
                        Adaptador adpt = new Adaptador();

                        Processo processo = new Processo();
                        processo = adpt.obterProcessoPorId(id.ToString());

                        string nomeArquivoAntigo = processo.arq_Arquivo;
                        if (nomeArquivoAntigo != null)
                        {
                            string diretorioRemover = Settings.Default.CaminhoDoRepositorioJurisprudencia + nomeArquivoAntigo;
                            FileInfo arquivoAntigo = new FileInfo(diretorioRemover);
                            arquivoAntigo.Delete();
                        }

                        adpt.RemoveProcesso(id);
                    }

                    if (e.CommandName == "Abrir")
                    {
                        string nome = "";
                        try
                        {
                            index = Convert.ToInt32(e.CommandArgument) - (GridView1.PageSize * GridView1.PageIndex);
                            int id = Convert.ToInt32(GridView1.DataKeys[index].Value);
                            Adaptador adpt = new Adaptador();
                            Processo processo = new Processo();
                            processo = adpt.obterProcessoPorId(id.ToString());
                            nome = processo.arq_Arquivo;
                            diretorio = Server.MapPath(Settings.Default.CaminhoDoRepositorioJurisprudencia);
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
                            }
                        }
                        catch (Exception ex)
                        {
                            LabelErro.Text = ex.Message;
                        }
                    }

                    if (e.CommandName == "AbrirDecisao")
                    {
                        index = Convert.ToInt32(e.CommandArgument);
                        int id = Convert.ToInt32(GridView1.DataKeys[index].Value);
                        Adaptador adpt = new Adaptador();
                        Processo processo = new Processo();
                        processo = adpt.obterProcessoPorId(id.ToString());
                        diretorio = Server.MapPath(Settings.Default.CaminhoDoRepositorioDecisao);
                        //string subDiretorio = processo.decisao.Substring(processo.decisao.IndexOf("\\DECISAO$") + 9);
                        nome = processo.decisao.Substring(processo.decisao.LastIndexOf('\\') + 1);
                        //subDiretorio = subDiretorio.Replace(nome, "");
                        if (processo.decisao == null || processo.decisao == "")
                        {
                            //Response.Write("<script>alert('Ainda não existe decisão para esse processo')</script>");
                            LabelErro.Text = "Não existe decisão para este processo";
                        }
                        else
                        {
                            if (Directory.Exists(diretorio))
                            {
                                Response.Clear();
                                Response.ClearHeaders();
                                Response.AddHeader("Content-Type", "application/pdf");
                                Response.AddHeader("Content-Disposition", "attachment; filename=" + nome);
                                FileStream file = new FileStream(diretorio /*+ subDiretorio*/ + nome, System.IO.FileMode.Open, FileAccess.Read);
                                byte[] bytes = new byte[Convert.ToInt32(file.Length)];
                                file.Read(bytes, 0, bytes.Length);
                                file.Close();
                                Response.OutputStream.Write(bytes, 0, bytes.GetUpperBound(0));
                                Response.Flush();
                                Response.Close();
                            }
                        }
                        GridView1.Rows[index].Cells[1].Enabled = true;
                    }
                }
                else
                {
                    LabelErro.Text = "Você não tem permissão para " + e.CommandName + " esse registro.";
                }
            }
            else
            {
                LabelErro.Text = "Você não é usuário do sistema.";
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            comando = (String)Session["lista"];
            Adaptador adpt = new Adaptador();
            GridView1.DataSource = adpt.PesquisarCampos(comando);
            GridView1.DataBind();
        }

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            comando = (String)Session["lista"];
            coluna = e.SortExpression;
            GridView1.DataSource = bindGrid();
            GridView1.DataBind();
        }

        protected List<Processo> bindGrid()
        {
            comando = (String)Session["lista"];
            List<Processo> lista = new List<Processo>();
            Adaptador adpt = new Adaptador();
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
            lista = adpt.porColuna(comando, coluna, orderBy);
            return lista;
        }

        protected void ButtonPesquisaComando_Click(object sender, EventArgs e)
        {
            comando = (String)Session["lista"];
            campos = (String)Session["Campos"];
            
            if (IsPostBack)
            {
                Adaptador adpt2 = new Adaptador();
                if(campos == TextBoxComandoPesquisa.Text)
                {
                    GridView1.DataSource = adpt2.PesquisarCampos(comando);
                    GridView1.DataBind();
                }
                else
                {
                    Session["Campos"] = TextBoxComandoPesquisa.Text;
                    string parametros = montarComando(TextBoxComandoPesquisa.Text);
                    comando = "textsearch in tcejurisprudencia " + parametros;
                    GridView1.DataSource = adpt2.PesquisarCampos(comando);
                    GridView1.DataBind();
                }
            }
            Adaptador adpt = new Adaptador();
            List<Processo> lista = new List<Processo>();
            lista = adpt.PesquisarCampos(comando);
            if(lista.Count == 0)
            {
                LabelErro.Text = "Não foram encontrados resultados para essa pesquisa";
            }
            else
            {
                GridView1.DataSource = lista;
                GridView1.DataBind();
            }            
        }
    }
}
