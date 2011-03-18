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
using PEP.Properties;
using LightInfocon.GoldenAccess.General;
using System.IO;
using LightInfocon.Data.LightBaseProvider;

namespace PEP
{
    public partial class WebForm11 : System.Web.UI.Page
    {
        string usuarioConectado;
        string senhaConectado;
        string diretorio;
        string erro;
        string comando;
        string coluna;
        string orderBy;
        string comandoMontado;
        string campos;

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
            TextBoxComandoPesquisa.Focus();
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
                campos = (String)Session["campo"]; //Pegando o parâmetro de consulta da página principal.
                TextBoxComandoPesquisa.Text = campos;
                //Listando os registros retornados pela pesquisa específica.
                comando = (String)Session["lista"];
                Adaptador adpt = new Adaptador();
                List<Prontuario> prontuario = new List<Prontuario>();
                GridView1.DataSource = adpt.PesquisarCampos(comando);
                GridView1.DataBind();
            }
            else
            {
                LabelErro.Text = "Não foram retornados resultados para essa consulta.";
            }
        }

        /*
         * Método que trata o click em uma linha do grid.
         */ 
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
                    //Caso seja clicado no botão abrir.
                    if (e.CommandName == "Abrir")
                    {
                        string nome = "";
                        try
                        {
                            index = Convert.ToInt32(e.CommandArgument) - (GridView1.PageSize * GridView1.PageIndex);//pegando a linha clicada.
                            int id = Convert.ToInt32(GridView1.DataKeys[index].Value);//pegando o id da linha
                            Adaptador adpt = new Adaptador();
                            Prontuario prontuario = new Prontuario();
                            Medico medico = new Medico();
                            prontuario = adpt.obterProntuarioPorId(id.ToString());
                            medico = prontuario.medico;
                            nome = prontuario.arq_Arquivo;
                            //diretorio = Server.MapPath(Settings.Default.CaminhoDoRepositorioProntuario);
                            diretorio = Server.MapPath("~\\arquivos\\");//pegando o diretório onde o arquivo encontra-se
                            Medico medicosSolicitantes = new Medico();
                            medicosSolicitantes = medicoParticipante(prontuario.numero_Registro, usuarioConectado);
                            if (medicosSolicitantes.matricula_Medico1 == medico.matricula_Medico1 || medicosSolicitantes.matricula_Medico2 == medico.matricula_Medico2
                                || medicosSolicitantes.matricula_Medico3 == medico.matricula_Medico3 || medicosSolicitantes.matricula_Medico4 == medico.matricula_Medico4 || usuario.Login == "admpront")
                            {
                                //Abertura do arquivo anexado.
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
                                LabelErro.Text = "Você não tem permissão para consultar esse prontuário";
                            }
                        }
                        catch (Exception ex)
                        {
                            LabelErro.Text = ex.Message;
                        }
                    }

                    //Caso seja clicado no botão alterar
                    if (e.CommandName == "Alterar")
                    {
                        index = Convert.ToInt32(e.CommandArgument) - (GridView1.PageSize * GridView1.PageIndex);
                        int id = Convert.ToInt32(GridView1.DataKeys[index].Value);
                        Adaptador adpt = new Adaptador();
                        Prontuario prontuario = new Prontuario();
                        Medico medico = new Medico();
                        prontuario = adpt.obterProntuarioPorId(id.ToString());
                        medico = prontuario.medico;
                        Medico medicosSolicitantes = new Medico();
                        medicosSolicitantes = medicoParticipante(prontuario.numero_Registro, usuarioConectado);
                        if (medicosSolicitantes.matricula_Medico1 == medico.matricula_Medico1 || medicosSolicitantes.matricula_Medico2 == medico.matricula_Medico2
                            || medicosSolicitantes.matricula_Medico3 == medico.matricula_Medico3 || medicosSolicitantes.matricula_Medico4 == medico.matricula_Medico4 || usuario.Login == "admpront")
                        {
                            Session.Add("id", id.ToString());
                            Server.Transfer("alterarpaciente.aspx");//redirecionamento para a página de alteração de dados.
                        }
                        else 
                        {
                            LabelErro.Text = "Você não tem permissão para consultar esse prontuário";
                        }
                    }

                    //Caso seja clicado no botão excluir.
                    if (e.CommandName == "Excluir")
                    {
                        index = Convert.ToInt32(e.CommandArgument) - (GridView1.PageSize * GridView1.PageIndex); ;
                        int id = Convert.ToInt32(GridView1.DataKeys[index].Value);
                        Adaptador adpt = new Adaptador();
                        Prontuario prontuario = new Prontuario();
                        Medico medico = new Medico();
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
                                arquivoAntigo.Delete();//exclusão do arquivo
                            }

                            //recarga da página após a exclusão do registro.
                            adpt.RemoverProntuario(id);
                            GridView1.DataSource = adpt.Todos();
                            GridView1.DataBind();
                        }
                        else
                        {
                            LabelErro.Text = "Você não tem permissão para consultar esse prontuário";
                        }
                    }
                }
                //Caso o usuário não tenha permissão de modificar os dados.
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
                        //    diretorio = Server.MapPath("~\\arquivos\\");
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
        
        /*
         * Método que trata a paginação
         */ 
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            Adaptador adpt = new Adaptador();
            GridView1.DataSource = adpt.Todos();
            GridView1.DataBind();
        }

        /*
         * Método que recarrega o dados consultados
         */ 
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

        /*
         * Método que faz a ordenação dos registros por coluna.
         */ 
        protected void GridView1_Sorting1(object sender, GridViewSortEventArgs e)
        {
            coluna = e.SortExpression;
            GridView1.DataSource = bindGrid();
            GridView1.DataBind();
        }

        /*
         * Método que redireciona para a página inicial
         */ 
        protected void LinkButtonVoltar_Click(object sender, EventArgs e)
        {
            Server.Transfer("default.aspx");
        }

        protected void ButtonPesquisaComando_Click(object sender, EventArgs e)
        {
            LabelErro.Text = "";
            comando = (String)Session["lista"];
            campos = (String)Session["campo"];

            if (IsPostBack)
            {
                Adaptador adpt2 = new Adaptador();
                if (campos == TextBoxComandoPesquisa.Text)
                {
                    comando = comando.Replace("OU", "E");
                    GridView1.DataSource = adpt2.PesquisarCampos(comando);
                    GridView1.DataBind();
                }
                else
                {
                    Session["campo"] = TextBoxComandoPesquisa.Text;
                    string parametros = montarComando(TextBoxComandoPesquisa.Text);
                    comando = "textsearch in prontuario " + parametros;
                    GridView1.DataSource = adpt2.PesquisarCampos(comando);
                    GridView1.DataBind();
                }
            }
            Adaptador adpt = new Adaptador();
            List<Prontuario> lista = new List<Prontuario>();
            lista = adpt.PesquisarCampos(comando);
            if (lista.Count == 0)
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