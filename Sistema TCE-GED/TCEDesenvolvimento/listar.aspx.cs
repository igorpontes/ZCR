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
using System.IO;
using LightInfocon.GoldenAccess.General;
using GED_TCESE.Properties;
using System.Collections.Generic;

namespace GED_TCESE
{
    public partial class WebForm7 : System.Web.UI.Page
    {
        string usuarioConectado;
        string senhaConectado;
        string diretorio;
        string coluna;
        string erro;
        string orderBy;
        string nome = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ImageButton lkCadastrar = (ImageButton)Master.FindControl("ImageButtonCadastrar");
                lkCadastrar.Visible = false;
                ImageButton lkListar = (ImageButton)Master.FindControl("ImageButtonListar");
                lkListar.Visible = false;
                ImageButton lkAjuda = (ImageButton)Master.FindControl("ImageButtonAjuda");
                lkAjuda.Visible = false;
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

                }
                else
                {
                    erro = "Usuário não autenticado";
                    Session.Add("erro", erro);
                    Server.Transfer("login.aspx");
                    Session.Abandon();
                }
                Adaptador adpt = new Adaptador();
                List<Processo> lista = new List<Processo>();
                lista = adpt.Todos();
                GridView1.DataSource = lista;
                GridView1.DataBind();
            }
        }

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
            }
            if (usuario.IsAuthenticated && !usuario.Disabled)
            {
                if (usuario.HasGroup("TCESEADM"))
                {
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
                            //diretorio = Server.MapPath("~\\arquivos\\jurisprudencia\\");
                            diretorio = Server.MapPath(Settings.Default.CaminhoDoRepositorioJurisprudencia);
                            //diretorio = "\\\\tce-s008\\GED_Arquivos\\jurisprudencia\\";
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

                    if (e.CommandName == "AbrirDecisao")
                    {
                        index = Convert.ToInt32(e.CommandArgument);
                        int id = Convert.ToInt32(GridView1.DataKeys[index].Value);
                        Adaptador adpt = new Adaptador();
                        Processo processo = new Processo();
                        processo = adpt.obterProcessoPorId(id.ToString());
                        diretorio = Server.MapPath(Settings.Default.CaminhoDoRepositorioDecisao);
                        string subDiretorio = processo.decisao.Substring(processo.decisao.IndexOf("\\DECISAO$") + 9);
                        nome = processo.decisao.Substring(processo.decisao.LastIndexOf('\\') + 1);
                        subDiretorio = subDiretorio.Replace(nome, "");
                        if (processo.decisao == null)
                        {
                            //Response.Write("<script>alert('Ainda não existe decisão para esse processo')</script>");
                            LabelErro.Text = "Não existe decisão para este processo";
                        }
                        else
                        {
                            //if (Directory.Exists(diretorio))
                            //{
                            Response.Clear();
                            Response.ClearHeaders();
                            Response.AddHeader("Content-Type", "application/pdf");
                            Response.AddHeader("Content-Disposition", "attachment; filename=" + nome);
                            FileStream file = new FileStream(diretorio + subDiretorio + nome, System.IO.FileMode.Open, FileAccess.Read);
                            byte[] bytes = new byte[Convert.ToInt32(file.Length)];
                            file.Read(bytes, 0, bytes.Length);
                            file.Close();
                            Response.OutputStream.Write(bytes, 0, bytes.GetUpperBound(0));
                            Response.Flush();
                            Response.Close();
                            //}
                        }
                        GridView1.Rows[index].Cells[1].Enabled = true;
                    }
                }
                else if (usuario.HasGroup("TCESELIM"))
                {
                    if (e.CommandName == "Abrir")
                    {
                        string nome = "";
                        try
                        {
                            index = Convert.ToInt32(e.CommandArgument);
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

                    if (e.CommandName == "Alterar")
                    {
                        LabelErro.Text = "Você não tem permissão para " + e.CommandName + " esse registro.";
                    }

                    if (e.CommandName == "Excluir")
                    {
                        LabelErro.Text = "Você não tem permissão para " + e.CommandName + " esse registro.";
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

        protected void LinkButtonVoltar_Click(object sender, EventArgs e)
        {
            Server.Transfer("default.aspx");
        }

        protected List<Processo> bindGrid()
        {
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
            lista = adpt.porColuna(null, coluna, orderBy);
            return lista;
        }

        protected void GridView1_Sorting1(object sender, GridViewSortEventArgs e)
        {
            coluna = e.SortExpression;
            GridView1.DataSource = bindGrid();
            GridView1.DataBind();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            Adaptador adpt = new Adaptador();
            GridView1.DataSource = adpt.Todos();
            GridView1.DataBind();
        }
    }
}
