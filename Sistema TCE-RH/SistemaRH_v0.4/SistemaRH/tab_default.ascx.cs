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
    public partial class tab_default : System.Web.UI.UserControl
    {
        string ext = ".pdf";
        String pathDir = HttpContext.Current.Server.MapPath("~/arquivos/");
        string diretorio;
        string id;
        string erro;
        string nomeArquivo;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["cadastro"].ToString().Equals((string)"nao"))
            {
                if ((String)Session["id"] != null)
                {
                    id = (String)Session["id"];
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
            ImageButtonVer.Visible = false;
            ImageButtonDelete.Visible = false;


            if (Session["abaAtiva"].ToString().Equals((string)"TabPanelPessoais"))
            {
                if (Session["postou_docsPessoais"].ToString().Equals((string)"sim"))
                {
                    //verificar o nome do arquivo
                    if (Session["cadastro"].ToString().Equals((string)"nao"))
                    {
                        nomeArquivo = montarFormatoGD(id, "docsPessoais" + ext);
                        LabelArquivo.Text = nomeArquivo;
                    }
                    TableArquivo.Visible = true;
                    ImageButtonVer.Visible = true;
                    ImageButtonDelete.Visible = true;
                }
            }
            else if (Session["abaAtiva"].ToString().Equals((string)"TabPanelTitulacao"))
            {
                if (Session["postou_titulacoes"].ToString().Equals((string)"sim"))
                {
                    if (Session["cadastro"].ToString().Equals((string)"nao"))
                    {
                        nomeArquivo = montarFormatoGD(id, "titulacoes" + ext);
                        LabelArquivo.Text = nomeArquivo;
                    }

                    TableArquivo.Visible = true;
                    ImageButtonVer.Visible = true;
                    ImageButtonDelete.Visible = true;
                }
            }
            else if (Session["abaAtiva"].ToString().Equals((string)"TabPanelPortaria"))
            {
                if (Session["postou_portarias"].ToString().Equals((string)"sim"))
                {
                    if (Session["cadastro"].ToString().Equals((string)"nao"))
                    {
                        nomeArquivo = montarFormatoGD(id, "portarias" + ext);
                        LabelArquivo.Text = nomeArquivo;
                    }
                    TableArquivo.Visible = true;
                    ImageButtonVer.Visible = true;
                    ImageButtonDelete.Visible = true;
                }
            }
            else if (Session["abaAtiva"].ToString().Equals((string)"TabPanelCI"))
            {
                if (Session["postou_cis"].ToString().Equals((string)"sim"))
                {
                    if (Session["cadastro"].ToString().Equals((string)"nao"))
                    {
                        nomeArquivo = montarFormatoGD(id, "cis" + ext);
                        LabelArquivo.Text = nomeArquivo;
                    }
                    TableArquivo.Visible = true;
                    ImageButtonVer.Visible = true;
                    ImageButtonDelete.Visible = true;
                }
            }
            else if (Session["abaAtiva"].ToString().Equals((string)"TabPanelAviso"))
            {
                if (Session["postou_avisoFerias"].ToString().Equals((string)"sim"))
                {
                    if (Session["cadastro"].ToString().Equals((string)"nao"))
                    {
                        nomeArquivo = montarFormatoGD(id, "avisoFerias" + ext);
                        LabelArquivo.Text = nomeArquivo;
                    }
                    TableArquivo.Visible = true;
                    ImageButtonVer.Visible = true;
                    ImageButtonDelete.Visible = true;
                }
            }
            else if (Session["abaAtiva"].ToString().Equals((string)"TabPanelRequerimento"))
            {
                if (Session["postou_requerimentos"].ToString().Equals((string)"sim"))
                {
                    if (Session["cadastro"].ToString().Equals((string)"nao"))
                    {
                        nomeArquivo = montarFormatoGD(id, "requerimentos" + ext);
                        LabelArquivo.Text = nomeArquivo;
                    }
                    TableArquivo.Visible = true;
                    ImageButtonVer.Visible = true;
                    ImageButtonDelete.Visible = true;
                }
            }
            else if (Session["abaAtiva"].ToString().Equals((string)"TabPanelOutros"))
            {
                if (Session["postou_outros"].ToString().Equals((string)"sim"))
                {
                    if (Session["cadastro"].ToString().Equals((string)"nao"))
                    {
                        nomeArquivo = montarFormatoGD(id, "outros" + ext);
                        LabelArquivo.Text = nomeArquivo;
                    }
                    TableArquivo.Visible = true;
                    ImageButtonVer.Visible = true;
                    ImageButtonDelete.Visible = true;
                }
            }
        }

        protected void ImageButtonSalvar_Click(object sender, ImageClickEventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                string id = "";
                string nameFile = "";
                try
                {
                    id = obterIdCadastrado();
                }
                catch (Exception ex)
                {
                    LabelErro.Text = ex.Message;
                    ImageAttention.Visible = true;
                }
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
                string nomeArquivo = montarFormatoGD(id, nameFile + ext);
                if (Session["postou_" + nameFile].ToString().Equals((string)"sim"))
                {
                    // Create a merge document and set it's properties
                    MergeDocument document = MergeDocument.Merge(pathDir + nomeArquivo, FileUpload1.PostedFile.FileName);
                    // Outputs the merged document
                    document.Draw(nomeArquivo);
                    System.IO.File.Delete(pathDir + nomeArquivo);
                    System.IO.File.Move(nomeArquivo, pathDir + nomeArquivo);
                }
                //se ainda nao postou o documento nao precisa fazer um merge
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
                LabelArquivo.Text = nomeArquivo;
                ImageButtonVer.Visible = true;
                ImageButtonDelete.Visible = true;
                //levantar flag dizendo que o arquivo foi postado
                Session.Add("postou_" + nameFile, "sim");

                Arquivo arq = new Arquivo();
                arq.nome_Arquivo = nomeArquivo;
                arq.tipo_Arquivo = nameFile;

                List<Arquivo> lista = new List<Arquivo>();
                lista = (List<Arquivo>)Session["arquivos"];
                if (lista == null)
                {
                    lista = new List<Arquivo>();
                }
                lista.Add(arq);
                Session["arquivos"] = lista;
            }
            else
            {
                //Mostra o Erro quando não tem arquivo selecionado
                LabelErro.Text = "Selecione o arquivo";
                ImageAttention.Visible = true;
            }
        }

        protected void ImageButtonVer_Click(object sender, ImageClickEventArgs e)
        {
            string nameFile = retornaAbaAtiva();
            string id = "";
            try
            {
                id = obterIdCadastrado();
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
                System.Diagnostics.Process.Start(diretorio + nome);
                TableArquivo.Visible = true;
                ImageButtonVer.Visible = true;
                ImageButtonDelete.Visible = true;
            }
            else
            {
                LabelErro.Text = "Diretório " + diretorio + "  não encontrado";
                ImageAttention.Visible = true;
            }
        }

        protected void ImageButtonDelete_Click(object sender, ImageClickEventArgs e)
        {
            string nameFile = retornaAbaAtiva();
            string id = "";
            try
            {
                id = obterIdCadastrado();
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
                File.Delete(diretorio + nome);

                TableArquivo.Visible = false;
                ImageButtonVer.Visible = false;
                ImageButtonDelete.Visible = false;
            }
            else
            {
                LabelErro.Text = "Diretório " + diretorio + "  não encontrado";
                ImageAttention.Visible = true;
            }

            File.Delete(pathDir + Session["fileName"].ToString());

            if (Session["cadastro"].ToString().Equals((string)"nao"))
            {
                Log log = new Log();
                log.data_log = DateTime.Now;
                log.tipo_acao_log = "Excluir";
                log.usuario_log = (String)Session["usuario"];
                log.mensagem_acao_log = "O usuário " + log.usuario_log + " deletou um arquivo do id " + id;
                Adaptador adpt = new Adaptador();
                adpt.InserirLog(log);
            }

            TableArquivo.Visible = false;
            ImageButtonVer.Visible = false;
            ImageButtonDelete.Visible = false;
            Session.Add("postou_" + nameFile, "nao");
            LabelArquivo.Text = "";
        }

        public string obterIdCadastrado()
        {
            IDataReader reader;
            IDbConnection con = new LightBaseConnection("user=lbw;password=lbw;udb=defudb;server=localhost");
            try
            {
                con.Open();
                IDbCommand comm = new LightBaseCommand("select last id from documento");
                comm.Connection = con;
                reader = comm.ExecuteReader();
                reader.Read();

                return reader["id"].ToString();
            }
            finally
            {
                con.Close();
            }
        }

        public string montarFormatoGD(string id, string nome_Arquivo)
        {
            if (id.Length == 0)
            {
                id = "0";
            }
            if (Session["cadastro"].ToString().Equals((string)"sim"))
            {
                int novoId = Convert.ToInt16(id) + 1;
                return novoId + "_" + nome_Arquivo;
            }
            else
            {
                int novoId = Convert.ToInt16(id);
                return novoId + "_" + nome_Arquivo;
            }
        }

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

      
        protected void ImageButtonDeletePagina_Click(object sender, ImageClickEventArgs e)
        {
            string nomeArquivo = LabelArquivo.Text;
            LabelErro.Text = "";
            ImageAttention.Visible = false;
            //verificar se o nome nao esta nulo
            if (nomeArquivo.Equals(""))
            {
                LabelErro.Text = "Não existe arquivo";
                ImageAttention.Visible = true;
            }
            else
            {
                int page = Convert.ToInt16(TextBoxDeletePagina.Text);
                PdfDocument originalPDF = new PdfDocument(pathDir + nomeArquivo);  //specify original file
                int totalpages = originalPDF.Pages.Count;

                if ((page > totalpages) || (page <= 0))
                {
                    LabelErro.Text = "Página fora do limite";
                    ImageAttention.Visible = true;
                }
                else if (page == totalpages)
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
            }
        }
    }
}