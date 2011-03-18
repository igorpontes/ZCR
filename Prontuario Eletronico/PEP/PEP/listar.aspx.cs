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
using PEP.Properties;
using LightInfocon.Data.LightBaseProvider;

namespace PEP
{
    public partial class WebForm9 : System.Web.UI.Page
    {

        string usuarioConectado;
        string senhaConectado;
        string diretorio;
        string coluna;
        string erro;
        string orderBy;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ImageButton lkCadastrar = (ImageButton)Master.FindControl("ImageButtonCadastrar");
                lkCadastrar.Visible = true;
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
                GridView1.DataSource = adpt.Todos();

                GridView1.DataBind();
            }
        }

        private Medico medicoParticipante(string numeroRegistro, string matriculaMedico)
        {
            IDbConnection minhaConexaoSolicitante = new LightBaseConnection(Settings.Default.conexaoLBW);
            IDataReader readerSolicitante;
            Medico medico = new Medico();

            try
            {
                minhaConexaoSolicitante.Open();
                IDbCommand meuComandoSolicitante = new LightBaseCommand("select medicos.matricula_Medico, medicos.nome_Medico from prontuario where numero_Registro=@numero_Registro and medico.matricula_Medico=@matricula_Medico");
                meuComandoSolicitante.Parameters.Add(new LightBaseParameter("numero_Registro", numeroRegistro));
                meuComandoSolicitante.Parameters.Add(new LightBaseParameter("matricula_Medico", matriculaMedico));
                meuComandoSolicitante.Connection = minhaConexaoSolicitante;
                readerSolicitante = meuComandoSolicitante.ExecuteReader();

                while (readerSolicitante.Read())
                {
                    DataTable dt_Medico = (DataTable)readerSolicitante["medicos"];
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
                }
                return medico;
            }
            finally
            {
                minhaConexaoSolicitante.Close();
            }
        }

        protected void LinkButtonVoltar_Click(object sender, EventArgs e)
        {
            Server.Transfer("default.aspx");    
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
                if (usuario.HasGroup("PTRIOADM"))
                {
                    if (e.CommandName == "Abrir")
                    {
                        string nome = "";
                        try
                        {
                            index = Convert.ToInt32(e.CommandArgument) - (GridView1.PageSize * GridView1.PageIndex);
                            int id = Convert.ToInt32(GridView1.DataKeys[index].Value);
                            Adaptador adpt = new Adaptador();
                            Prontuario prontuario = new Prontuario();
                            Medico medico = new Medico();
                            prontuario = adpt.obterProntuarioPorId(id.ToString());
                            medico = prontuario.medico;
                            nome = prontuario.arq_Arquivo;
                            //diretorio = Server.MapPath(Settings.Default.CaminhoDoRepositorioProntuario);
                            diretorio = Server.MapPath("~\\arquivos\\");
                            Medico medicosSolicitantes = new Medico();
                            medicosSolicitantes = medicoParticipante(prontuario.numero_Registro, usuarioConectado);
                            if(medicosSolicitantes.matricula_Medico1 == medico.matricula_Medico1 || medicosSolicitantes.matricula_Medico2 == medico.matricula_Medico2
                                || medicosSolicitantes.matricula_Medico3 == medico.matricula_Medico3 || medicosSolicitantes.matricula_Medico4 == medico.matricula_Medico4 || usuario.Login == "admpront")
                            {
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
                            else
                            {
                                LabelErro.Text = "Você não tem permissão para " + e.CommandName + " esse prontuário";
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
                        Prontuario prontuario = new Prontuario();
                        Adaptador adpt = new Adaptador();
                        prontuario = adpt.obterProntuarioPorId(id.ToString());
                        Medico medico = new Medico();
                        Medico medicosSolicitantes = new Medico();
                        medicosSolicitantes = medicoParticipante(prontuario.numero_Registro, usuarioConectado);
                        if (medicosSolicitantes.matricula_Medico1 == medico.matricula_Medico1 || medicosSolicitantes.matricula_Medico2 == medico.matricula_Medico2
                            || medicosSolicitantes.matricula_Medico3 == medico.matricula_Medico3 || medicosSolicitantes.matricula_Medico4 == medico.matricula_Medico4 || usuario.Login == "admpront")
                        {
                            Session.Add("id", id.ToString());
                            Server.Transfer("alterarpaciente.aspx");
                        }
                        else
                        {
                            LabelErro.Text = "Você não possui permissão para " + e.CommandName + " esse prontuário";
                        }
                    }

                    if (e.CommandName == "Excluir")
                    {
                        index = Convert.ToInt32(e.CommandArgument) - (GridView1.PageSize * GridView1.PageIndex); ;
                        int id = Convert.ToInt32(GridView1.DataKeys[index].Value);
                        Medico medico = new Medico();
                        Adaptador adpt = new Adaptador();
                        Prontuario prontuario = new Prontuario();
                        prontuario = adpt.obterProntuarioPorId(id.ToString());
                        Medico medicosSolicitantes = new Medico();
                        medicosSolicitantes = medicoParticipante(prontuario.numero_Registro, usuarioConectado);
                        if (medicosSolicitantes.matricula_Medico1 == medico.matricula_Medico1 || medicosSolicitantes.matricula_Medico2 == medico.matricula_Medico2
                            || medicosSolicitantes.matricula_Medico3 == medico.matricula_Medico3 || medicosSolicitantes.matricula_Medico4 == medico.matricula_Medico4 || usuario.Login == "admpront")
                        {
                            string nomeArquivoAntigo = prontuario.arq_Arquivo;
                            if (nomeArquivoAntigo != null)
                            {
                                string diretorioRemover = Settings.Default.CaminhoDoRepositorioProntuario + nomeArquivoAntigo;
                                FileInfo arquivoAntigo = new FileInfo(diretorioRemover);
                                arquivoAntigo.Delete();
                            }

                            adpt.RemoverProntuario(id);
                            GridView1.DataSource = adpt.Todos();
                            GridView1.DataBind();
                        }
                        else
                        {
                            LabelErro.Text = "Você não possui permissão para " + e.CommandName + " esse prontuário";
                        }
                    }
                }
                else if (usuario.HasGroup("PTRIOLIM"))
                {
                    if (e.CommandName == "Abrir")
                    {
                        LabelErro.Text = "Você não tem permissão para " + e.CommandName + " esse prontuário.";
                        //string nome = "";
                        //try
                        //{
                        //    index = Convert.ToInt32(e.CommandArgument);
                        //    int id = Convert.ToInt32(GridView1.DataKeys[index].Value);
                        //    Adaptador adpt = new Adaptador();
                        //    Prontuario processo = new Prontuario();
                        //    processo = adpt.obterProntuarioPorId(id.ToString());
                        //    nome = processo.arq_Arquivo;
                        //    diretorio = Server.MapPath(Settings.Default.CaminhoDoRepositorioProntuario);
                        //    if (Directory.Exists(diretorio))
                        //    {
                        //        Response.Clear();
                        //        Response.ClearHeaders();
                        //        Response.AddHeader("Content-Type", "application/pdf");
                        //        Response.AddHeader("Content-Disposition", "attachment; filename=" + nome);

                        //        FileStream file = new FileStream(diretorio + nome, System.IO.FileMode.Open, FileAccess.Read);
                        //        byte[] bytes = new byte[Convert.ToInt32(file.Length)];
                        //        file.Read(bytes, 0, bytes.Length);
                        //        file.Close();

                        //        Response.OutputStream.Write(bytes, 0, bytes.GetUpperBound(0));

                        //        Response.Flush();
                        //        Response.Close();
                        //    }
                        //    else
                        //    {
                        //        LabelErro.Text = "Diretório " + diretorio + "  não encontrado";
                        //    }
                        //}
                        //catch (Exception ex)
                        //{
                        //    LabelErro.Text = ex.Message;
                        //}
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

        protected List<Prontuario> bindGrid()
        {
            List<Prontuario> lista = new List<Prontuario>();
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
            lista = adpt.PorColuna(coluna, orderBy);
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
