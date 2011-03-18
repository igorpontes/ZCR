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
using GED_TCESE.Properties;

namespace GED_TCESE
{
    public partial class WebForm6 : System.Web.UI.Page
    {
        string erro;
        string usuarioConectado;
        string senhaConectado;
        string coluna;
        string comando;
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
                comando = (String)Session["lista"];
                AdaptadorProtocolo adpt = new AdaptadorProtocolo();
                List<Protocolo> protocolos = new List<Protocolo>();
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
                    //GridView1.DataSource = lista;
                    //GridView1.DataBind();
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
            string diretorio;

            if (e.CommandName == "Abrir")
            {
                string nome = "";
                try
                {
                    index = Convert.ToInt32(e.CommandArgument) - (GridView1.PageSize * GridView1.PageIndex);
                    int id = Convert.ToInt32(GridView1.DataKeys[index].Value);
                    AdaptadorProtocolo adpt = new AdaptadorProtocolo();
                    Protocolo protocolo = new Protocolo();
                    protocolo = adpt.obterProtocoloPorId(id.ToString());
                    nome = protocolo.arq_Arquivo;
                    //diretorio = Server.MapPath("~\\arquivos\\protocolos\\");
                    diretorio = Server.MapPath(Settings.Default.CaminhoDoRepositorioProtocolo);
                    if (Directory.Exists(diretorio))
                    {
                        Response.Clear();
                        Response.ClearHeaders();
                        Response.AddHeader("Content-Type", "image/tiff");
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

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            comando = (String)Session["lista"];
            GridView1.PageIndex = e.NewPageIndex;
            AdaptadorProtocolo adpt = new AdaptadorProtocolo();
            GridView1.DataSource = adpt.PesquisarCampos(comando);
            GridView1.DataBind();
        }

        protected List<Protocolo> bindGrid()
        {
            comando = (String)Session["lista"];
            List<Protocolo> lista = new List<Protocolo>();
            AdaptadorProtocolo adpt = new AdaptadorProtocolo();
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

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            comando = (String)Session["lista"];
            coluna = e.SortExpression;
            GridView1.DataSource = bindGrid();
            GridView1.DataBind();
        }
    }
}
