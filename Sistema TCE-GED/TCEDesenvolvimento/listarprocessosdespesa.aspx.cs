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
using System.Collections.Generic;
using GED_TCESE.Properties;

namespace GED_TCESE
{
    public partial class WebForm8 : System.Web.UI.Page
    {
        string erro;
        string usuarioConectado;
        string senhaConectado;
        string coluna;
        string orderBy;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ImageButton lkListar = (ImageButton)Master.FindControl("ImageButtonListar");
                lkListar.Visible = false;
                ImageButton lkCadastrar = (ImageButton)Master.FindControl("ImageButtonCadastrar");
                lkCadastrar.Visible = false;
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
                AdaptadorDespesa adpt = new AdaptadorDespesa();
                GridView1.DataSource = adpt.Todos();
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
                    //if (e.CommandName == "Alterar")
                    //{
                    //    index = Convert.ToInt32(e.CommandArgument);
                    //    string id = (String)GridView1.DataKeys[index].Value.ToString();
                    //    Session.Add("id", id);
                    //    Server.Transfer("Alterar.aspx");
                    //}

                    //if (e.CommandName == "Excluir")
                    //{
                    //    index = Convert.ToInt32(e.CommandArgument);
                    //    int id = Convert.ToInt32(GridView1.DataKeys[index].Value);
                    //    Adaptador adpt = new Adaptador();
                    //    adpt.RemoveProcesso(id);
                    //}

                    if (e.CommandName == "Abrir")
                    {
                        string nome = "";
                        string diretorio;
                        try
                        {
                            index = Convert.ToInt32(e.CommandArgument) - (GridView1.PageSize * GridView1.PageIndex);
                            int id = Convert.ToInt32(GridView1.DataKeys[index].Value);
                            AdaptadorDespesa adpt = new AdaptadorDespesa();
                            Despesa despesa = new Despesa();
                            despesa = adpt.obterDespesaPorId(id.ToString());
                            nome = despesa.arq_Arquivo;
                            //diretorio = Server.MapPath("~\\arquivos\\processosdespesa\\");
                            diretorio = Server.MapPath(Settings.Default.CaminhoDoRepositorioProcessoDespesa);
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

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            coluna = e.SortExpression;
            GridView1.DataSource = bindGrid();
            GridView1.DataBind();
        }

        protected List<Despesa> bindGrid()
        {
            List<Despesa> lista = new List<Despesa>();
            AdaptadorDespesa adpt = new AdaptadorDespesa();
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

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            AdaptadorDespesa adpt = new AdaptadorDespesa();
            GridView1.DataSource = adpt.Todos();
            GridView1.DataBind();
        }
    }
}
