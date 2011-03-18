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
    public partial class listar : System.Web.UI.Page
    {
        string usuarioConectado;
        string senhaConectado;
        string diretorio;
        string coluna;
        string erro;

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
                Adaptador adpt = new Adaptador();
                GridView1.DataSource = adpt.Todos();

                GridView1.DataBind();
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            Adaptador adpt = new Adaptador();
            GridView1.DataSource = adpt.Todos();
            GridView1.DataBind();
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
                            }
                        }
                        catch (Exception ex)
                        {
                            LabelErro.Text = ex.Message;
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

                        Documento processo = new Documento();
                        processo = adpt.obterDocumentoPorId(id.ToString());
                        //testar se ta pegando os arquivos certos
                        if (processo.arquivos != null)
                        {
                            //loop que pega os nomes dos arquivos e deleta
                            foreach (var arq in processo.arquivos)
                            {
                                string nomeArquivoAntigo = arq.nome_Arquivo;
                                if (nomeArquivoAntigo != null)
                                {
                                    string diretorioRemover = Settings.Default.CaminhoDoRepositorioDocumento + nomeArquivoAntigo;
                                    FileInfo arquivoAntigo = new FileInfo(diretorioRemover);
                                    arquivoAntigo.Delete();
                                }
                            }
                        }
                        adpt.RemoverDocumento(id);
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

        protected void GridView1_Sorting1(object sender, GridViewSortEventArgs e)
        {
            coluna = e.SortExpression;
            GridView1.DataSource = bindGrid();
            GridView1.DataBind();
        }

        protected List<Documento> bindGrid()
        {
            List<Documento> lista = new List<Documento>();
            Adaptador adpt = new Adaptador();
            lista = adpt.PorColuna(coluna);
            return lista;
        }
    }
}
