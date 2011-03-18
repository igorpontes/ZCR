using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using ceTe.DynamicPDF;
using ceTe.DynamicPDF.Merger;
using ceTe.DynamicPDF.PageElements;
using ceTe.DynamicPDF.IO;
using System.IO;
using SistemaRH.Properties;
using LightInfocon.Data.LightBaseProvider;
using System.Collections.Generic;


namespace SistemaRH
{
    /// <summary>
    /// Classe responsável pelo UserControl usado nas abas de tipos de documentos.
    /// </summary>
    public partial class tab_default : System.Web.UI.UserControl
    {
        string ext = ".pdf";
        String pathDir = HttpContext.Current.Server.MapPath("~/arquivos/");
        string diretorio;
        string id;
        string erro;
        string nomeArquivo;
        string matricula;
        Adaptador adpt = new Adaptador();
        List<Arquivo> lista = new List<Arquivo>();

        /// <summary>
        /// Método usado para carregar a aba do tipo especifico do documento e verificar se é um cadastro ou edição .
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["cadastro"].ToString().Equals((string)"nao"))
            {
                if ((String)Session["id"] != null)
                {
                    id = (String)Session["id"];

                    matricula = retornaMatricula(id);
                }
                else
                {
                    Server.Transfer("default.aspx");
                    erro = "Identificador do campo não encontrado";
                    Session.Add("erro", erro);
                }
                LabelArquivo.Text = "";
            }

            TableArquivo.Visible = false;
            ImageButtonDelete.Visible = false;

            if (Session["abaAtiva"].ToString().Equals((string)"TabPanelPessoais"))
            {
                LabelTipoProcesso.Visible = false;
                DropDownListTipoProcesso.Visible = false;
                if (Session["postou_docsPessoais"].ToString().Equals((string)"sim"))
                {
                    //verificar o nome do arquivo
                    if (Session["cadastro"].ToString().Equals((string)"nao"))
                    {
                        nomeArquivo = montarFormatoGD(matricula, "docsPessoais" + ext);
                        LabelArquivo.Text = nomeArquivo;
                    }
                    TableArquivo.Visible = true;
                    ImageButtonDelete.Visible = true;
                }
            }
            else if (Session["abaAtiva"].ToString().Equals((string)"TabPanelTitulacao"))
            {
                LabelTipoProcesso.Visible = true;
                DropDownListTipoProcesso.Visible = true;
                if (Session["postou_titulacoes"].ToString().Equals((string)"sim"))
                {
                    if (Session["cadastro"].ToString().Equals((string)"nao"))
                    {
                        nomeArquivo = montarFormatoGD(matricula, "titulacoes" + ext);
                        LabelArquivo.Text = nomeArquivo;
                    }

                    TableArquivo.Visible = true;
                    ImageButtonDelete.Visible = true;
                }
            }
            else if (Session["abaAtiva"].ToString().Equals((string)"TabPanelPortaria"))
            {
                LabelTipoProcesso.Visible = false;
                DropDownListTipoProcesso.Visible = false;
                if (Session["postou_portarias"].ToString().Equals((string)"sim"))
                {
                    if (Session["cadastro"].ToString().Equals((string)"nao"))
                    {
                        nomeArquivo = montarFormatoGD(matricula, "portarias" + ext);
                        LabelArquivo.Text = nomeArquivo;
                    }
                    TableArquivo.Visible = true;
                    ImageButtonDelete.Visible = true;
                }
            }
            else if (Session["abaAtiva"].ToString().Equals((string)"TabPanelPortariaComProcesso"))
            {

                LabelTipoProcesso.Visible = false;
                DropDownListTipoProcesso.Visible = false;

                if (Session["postou_portariasComProcesso"].ToString().Equals((string)"sim"))
                {
                    if (Session["cadastro"].ToString().Equals((string)"nao"))
                    {
                        //mudar o nome
                        nomeArquivo = montarFormatoGD(matricula, "portariasComProcesso" + ext);
                        LabelArquivo.Text = nomeArquivo;
                    }
                    TableArquivo.Visible = true;
                    ImageButtonDelete.Visible = true;
                }
            }
            else if (Session["abaAtiva"].ToString().Equals((string)"TabPanelCI"))
            {
                LabelTipoProcesso.Visible = false;
                DropDownListTipoProcesso.Visible = false;
                if (Session["postou_cis"].ToString().Equals((string)"sim"))
                {
                    if (Session["cadastro"].ToString().Equals((string)"nao"))
                    {
                        nomeArquivo = montarFormatoGD(matricula, "cis" + ext);
                        LabelArquivo.Text = nomeArquivo;
                    }
                    TableArquivo.Visible = true;
                    ImageButtonDelete.Visible = true;
                }
            }
            else if (Session["abaAtiva"].ToString().Equals((string)"TabPanelAviso"))
            {
                LabelTipoProcesso.Visible = false;
                DropDownListTipoProcesso.Visible = false;
                if (Session["postou_avisoFerias"].ToString().Equals((string)"sim"))
                {
                    if (Session["cadastro"].ToString().Equals((string)"nao"))
                    {
                        nomeArquivo = montarFormatoGD(matricula, "avisoFerias" + ext);
                        LabelArquivo.Text = nomeArquivo;
                    }
                    TableArquivo.Visible = true;
                    ImageButtonDelete.Visible = true;
                }
            }
            else if (Session["abaAtiva"].ToString().Equals((string)"TabPanelRequerimento"))
            {
                LabelTipoProcesso.Visible = false;
                DropDownListTipoProcesso.Visible = false;
                if (Session["postou_requerimentos"].ToString().Equals((string)"sim"))
                {
                    if (Session["cadastro"].ToString().Equals((string)"nao"))
                    {
                        nomeArquivo = montarFormatoGD(matricula, "requerimentos" + ext);
                        LabelArquivo.Text = nomeArquivo;
                    }
                    TableArquivo.Visible = true;
                    ImageButtonDelete.Visible = true;
                }
            }
            else if (Session["abaAtiva"].ToString().Equals((string)"TabPanelOutros"))
            {
                LabelTipoProcesso.Visible = false;
                DropDownListTipoProcesso.Visible = false;
                if (Session["postou_outros"].ToString().Equals((string)"sim"))
                {
                    if (Session["cadastro"].ToString().Equals((string)"nao"))
                    {
                        nomeArquivo = montarFormatoGD(matricula, "outros" + ext);
                        LabelArquivo.Text = nomeArquivo;
                    }
                    TableArquivo.Visible = true;
                    ImageButtonDelete.Visible = true;
                }
            }
        }

        /// <summary>
        /// Método usado para salvar o arquivo no disco e quando necessário fazer um merge no arquivo.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        protected void ImageButtonSalvar_Click(object sender, ImageClickEventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                string id = "";
                string nameFile = "", abaAtiva;
                abaAtiva = retornaAbaAtiva();
                try
                {
                    id = obterMatricula();
                }
                catch (Exception ex)
                {
                    LabelErro.Text = ex.Message;
                    ImageAttention.Visible = true;
                }

                //verificar se nao é uma titulaçao
                if (!abaAtiva.Equals("titulacoes"))
                {
                    nomeArquivo = montarFormatoGD(id, abaAtiva + ext);
                }
                else
                {
                    if (id.Length == 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), Guid.NewGuid().ToString(), "window.alert('Digite a matrícula!');", true);
                    }
                    else
                    {
                        nomeArquivo = adpt.montaFormatoTitulacao(FileUpload1.FileName.ToString(), id, DropDownListTipoProcesso.SelectedValue.ToString());
                    }

                }


                if (id.Length == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), Guid.NewGuid().ToString(), "window.alert('Digite a matrícula!');", true);
                }
                else
                {
                    if (Session["postou_" + abaAtiva].ToString().Equals((string)"sim") && !(abaAtiva.Equals("titulacoes")))
                    {
                        // Create a merge document and set it's properties
                        MergeDocument document = MergeDocument.Merge(pathDir + nomeArquivo, FileUpload1.PostedFile.FileName);
                        // Outputs the merged document
                        document.Draw(nomeArquivo);
                        System.IO.File.Delete(pathDir + nomeArquivo);
                        System.IO.File.Move(nomeArquivo, pathDir + nomeArquivo);
                    }
                    //se ainda nao postou o documento nao precisa fazer um merge. Ou é uma titulaçao que nao precisa de merge
                    else
                    {
                        try
                        {
                            FileUpload1.SaveAs(pathDir + nomeArquivo);
                            Session.Add("fileName", nomeArquivo);
                        }
                        catch (Exception ex)
                        {
                            LabelErro.Text = ex.Message;
                            ImageAttention.Visible = true;
                        }
                    }
                    TableArquivo.Visible = true;
                    GridView1.Visible = true;
                    LabelArquivo.Text = nomeArquivo;
                    //ImageButtonVer.Visible = true;
                    ImageButtonDelete.Visible = true;
                    //levantar flag dizendo que o arquivo foi postado
                    Session.Add("postou_" + abaAtiva, "sim");

                    Arquivo arq = new Arquivo();
                    arq.nome_Arquivo = nomeArquivo;
                    arq.tipo_Arquivo = abaAtiva;

                    List<Arquivo> lista = new List<Arquivo>();
                    lista = (List<Arquivo>)Session["arquivos"];
                    if (lista == null)
                    {
                        lista = new List<Arquivo>();
                    }
                    lista.Add(arq);
                    Session["arquivos"] = lista;

                    carrega_Grid("arquivos");
                }



            }
            else
            {
                //Mostra o Erro quando não tem arquivo selecionado
                LabelErro.Text = "Selecione o arquivo";
                ImageAttention.Visible = true;
            }
        }

        /// <summary>
        /// Método do botão usado para visualizar o arquivo selecionado.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        protected void ImageButtonVer_Click(object sender, ImageClickEventArgs e)
        {
            string nameFile = retornaAbaAtiva();
            string id = "";
            try
            {
                id = obterMatricula();
            }
            catch (Exception ex)
            {
                LabelErro.Text = ex.Message;
                ImageAttention.Visible = true;
            }
            string nome = montarFormatoGD(id, nameFile + ext);
            diretorio = pathDir;


            if (Directory.Exists(diretorio))
            {
                try
                {
                    System.Diagnostics.Process.Start(diretorio + nome);
                }
                catch (Exception ex)
                {
                    LabelErro.Text = ex.Message;
                    //throw;
                }

                TableArquivo.Visible = true;
                //ImageButtonVer.Visible = true;
                ImageButtonDelete.Visible = true;
            }
            else
            {
                LabelErro.Text = "Diretório " + diretorio + "  não encontrado";
                ImageAttention.Visible = true;
            }
        }

        /// <summary>
        /// Método do botão usado para deletar o arquivo selecionado.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        protected void ImageButtonDelete_Click(object sender, ImageClickEventArgs e)
        {
            string nameFile = retornaAbaAtiva();
            string id = "";
            try
            {
                id = obterMatricula();
            }
            catch (Exception ex)
            {
                LabelErro.Text = ex.Message;
                ImageAttention.Visible = true;
            }
            string nome = montarFormatoGD(id, nameFile);
            diretorio = pathDir;


            if (Directory.Exists(diretorio))
            {
                File.Delete(diretorio + nome + ".pdf");

                TableArquivo.Visible = false;
                //ImageButtonVer.Visible = false;
                ImageButtonDelete.Visible = false;
            }
            else
            {
                LabelErro.Text = "Diretório " + diretorio + "  não encontrado";
                ImageAttention.Visible = true;
            }

            if (Session["cadastro"].ToString().Equals((string)"nao"))
            {
                Log log = new Log();
                log.data_log = DateTime.Now;
                log.tipo_acao_log = "Excluir";
                log.usuario_log = (String)Session["usuario"];
                log.mensagem_acao_log = "Arquivo " + nome + ".pdf";
                adpt.InserirLog(log);
            }

            TableArquivo.Visible = false;
            //ImageButtonVer.Visible = false;
            ImageButtonDelete.Visible = false;
            Session.Add("postou_" + nameFile, "nao");
            LabelArquivo.Text = "";
        }

        /// <summary>
        /// Método usado para obter a matrícula do colaborador.
        /// </summary>
        /// <returns>a matrícula que está no TextBox.</returns>
        public string obterMatricula()
        {
            string matric = "";
            if (Session["cadastro"].ToString().Equals((string)"sim"))
            {
                matric = CadastroDocs.matricula;
            }
            else if (Session["cadastro"].ToString().Equals((string)"nao"))
            {
                matric = AlterarColaborador.matricula;
            }
            return matric;

        }

        /// <summary>
        /// Método usado para obter a matrícula do colaborador usando a base de dados.
        /// </summary>
        /// <param name="id1">O id do colaborador.</param>
        /// <returns>a matrícula buscada no banco de dados.</returns>
        public string retornaMatricula(string id1)
        {
            string temp;
            IDataReader reader;
            IDbConnection minhaConexao = new LightBaseConnection("user=lbw;password=lbw;udb=defudb;server=localhost");
            try
            {
                minhaConexao.Open();
                IDbCommand meuComando = new LightBaseCommand("select matricula_Colaborador from documento where id =" + id1);
                meuComando.Connection = minhaConexao;
                reader = meuComando.ExecuteReader();
                reader.Read();


                temp = Convert.ToString(reader["matricula_Colaborador"]);

            }
            finally
            {
                minhaConexao.Close();
            }
            return temp;

        }

        /// <summary>
        /// Método usado para montar o nome do arquivo.
        /// </summary>
        /// <param name="id">A matrícula do colaborador.</param>
        /// <param name="nome_Arquivo">O nome do arquivo.</param>
        /// <returns>retorna o comando tratado</returns>
        public string montarFormatoGD(string id, string nome_Arquivo)
        {
            return id + "_" + nome_Arquivo;
        }

        /// <summary>
        /// Método usado para retornar uma string com a aba ativa dos tipos de documentos.
        /// </summary>
        /// <returns>retorna a aba ativa</returns>
        protected String retornaAbaAtiva()
        {
            String nameFile = "";
            if (Session["abaAtiva"].ToString().Equals((string)"TabPanelPessoais"))
            {
                nameFile = "docsPessoais";
            }
            else if (Session["abaAtiva"].ToString().Equals((string)"TabPanelTitulacao"))
            {
                nameFile = "titulacoes";
            }
            else if (Session["abaAtiva"].ToString().Equals((string)"TabPanelPortaria"))
            {
                nameFile = "portarias";
            }
            else if (Session["abaAtiva"].ToString().Equals((string)"TabPanelPortariasComProcesso"))
            {
                nameFile = "portariasComProcesso";
            }
            else if (Session["abaAtiva"].ToString().Equals((string)"TabPanelCI"))
            {
                nameFile = "cis";
            }
            else if (Session["abaAtiva"].ToString().Equals((string)"TabPanelAviso"))
            {
                nameFile = "avisoFerias";
            }
            else if (Session["abaAtiva"].ToString().Equals((string)"TabPanelRequerimento"))
            {
                nameFile = "requerimentos";
            }
            else if (Session["abaAtiva"].ToString().Equals((string)"TabPanelOutros"))
            {
                nameFile = "outros";
            }

            return nameFile;
        }


        /// <summary>
        /// Método usado para deletar uma página especifica do documento.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        protected void ImageButtonDeletePagina_Click(object sender, ImageClickEventArgs e)
        {
            string nomeArquivo = LabelArquivo.Text;
            LabelErro.Text = "";
            ImageAttention.Visible = false;
            //verificar se o nome nao esta nulo
            if (nomeArquivo.Equals(""))
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), Guid.NewGuid().ToString(), "window.alert('Não existe arquivo');", true);
            }
            else
            {
                int page = Convert.ToInt16(TextBoxDeletePagina.Text);
                PdfDocument originalPDF = new PdfDocument(pathDir + nomeArquivo);  //specify original file
                int totalpages = originalPDF.Pages.Count;

                if ((page > totalpages) || (page <= 0))
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), Guid.NewGuid().ToString(), "window.alert('Página fora do limite!');", true);
                }
                else if (totalpages == 1)
                {
                    System.IO.File.Delete(pathDir + nomeArquivo);
                    TableArquivo.Visible = false;
                    string abaAtiva = retornaAbaAtiva();
                    Session.Add("postou_" + abaAtiva, "nao");
                    ScriptManager.RegisterStartupScript(Page, GetType(), Guid.NewGuid().ToString(), "window.alert('Página deletada com sucesso!');", true);
                }
                else if (totalpages != 1)
                {
                    if (page == totalpages)
                    {
                        MergeDocument smallerPDF = new MergeDocument(originalPDF, 1, page - 1);
                        smallerPDF.Draw(nomeArquivo);
                        System.IO.File.Delete(pathDir + nomeArquivo);
                        System.IO.File.Move(nomeArquivo, pathDir + nomeArquivo);
                    }
                    else
                    {
                        MergeDocument smallerPDF = new MergeDocument(originalPDF, 1, page - 1);
                        int pagesAppend = totalpages - page;
                        smallerPDF.Append(originalPDF, page + 1, pagesAppend);  //append pages deleted plus one until page count;
                        smallerPDF.Draw(nomeArquivo);
                        System.IO.File.Delete(pathDir + nomeArquivo);
                        System.IO.File.Move(nomeArquivo, pathDir + nomeArquivo);
                    }
                    ScriptManager.RegisterStartupScript(Page, GetType(), Guid.NewGuid().ToString(), "window.alert('Página deletada com sucesso!');", true);
                }

            }
        }

        /// <summary>
        /// Método usado para deletar um intervalo de páginas do documento.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        protected void ImageButtonDeleteIntervalo_Click(object sender, ImageClickEventArgs e)
        {
            string nomeArquivo = LabelArquivo.Text;
            LabelErro.Text = "";
            ImageAttention.Visible = false;
            //verificar se o nome nao esta nulo
            if (nomeArquivo.Equals(""))
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), Guid.NewGuid().ToString(), "window.alert('Não existe arquivo');", true);
            }
            else
            {
                int pageInicio = Convert.ToInt16(TextBoxDeleteInicio.Text);
                int pageFinal = Convert.ToInt16(TextBoxDeleteFinal.Text);
                PdfDocument originalPDF = new PdfDocument(pathDir + nomeArquivo);  //specify original file
                int totalpages = originalPDF.Pages.Count;

                if ((0 < pageInicio) && (pageInicio < totalpages) && (pageInicio < pageFinal) && (pageFinal <= totalpages))
                {
                    int temp = pageFinal - pageInicio + 1;
                    if (temp == totalpages)
                    {
                        System.IO.File.Delete(pathDir + nomeArquivo);
                        TableArquivo.Visible = false;
                        string abaAtiva = retornaAbaAtiva();
                        Session.Add("postou_" + abaAtiva, "nao");
                    }
                    else
                    {
                        MergeDocument smallerPDF = new MergeDocument(originalPDF, 1, pageInicio - 1);
                        int pagesAppend = totalpages - pageFinal;
                        smallerPDF.Append(originalPDF, pageFinal + 1, pagesAppend);  //append pages deleted plus one until page count;
                        smallerPDF.Draw(nomeArquivo);
                        System.IO.File.Delete(pathDir + nomeArquivo);
                        System.IO.File.Move(nomeArquivo, pathDir + nomeArquivo);
                    }
                    ScriptManager.RegisterStartupScript(Page, GetType(), Guid.NewGuid().ToString(), "window.alert('Páginas deletadas com sucesso!');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), Guid.NewGuid().ToString(), "window.alert('Erro no intervalo!');", true);
                }
            }
        }

        /// <summary>
        /// Método usado para carregar o grid com a lista dos documentos por opção de tipo de documento.
        /// </summary>
        /// <param name="opcao">A opção informa o tipo de documento a preencher.</param>
        protected void carrega_Grid(string opcao)
        {
            lista = (List<Arquivo>)Session[opcao];
            lista = adpt.RetornaArquivos(lista);
            GridView1.DataSource = lista;
            GridView1.DataBind();
        }

        /// <summary>
        /// Método do evento RowCommand que controla o GridView1.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewCommandEventArgs"/> instance containing the event data.</param>
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index, id;
            string nomeArquivo;
            if (e.CommandName == "Excluir")
            {
                index = Convert.ToInt32(e.CommandArgument) - (GridView1.PageSize * GridView1.PageIndex);
                //id = Convert.ToInt32(GridView1.DataKeys[index].Value);
                nomeArquivo = Convert.ToString(GridView1.DataKeys[index].Value);

            }

        }

        /// <summary>
        /// Método que retorna o tipo de titulação usada no DropDownList.
        /// </summary>
        /// <returns>Tipo de titulação.</returns>
        protected string retornaTipoTitulacao()
        {
            return DropDownListTipoProcesso.SelectedValue.ToString();
        }



    }
}