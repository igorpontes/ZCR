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
using System.Collections.Generic;

namespace SistemaRH
{
    public partial class tab_default : System.Web.UI.UserControl
    {
        string ext = ".pdf";
        String pathDir = HttpContext.Current.Server.MapPath("~/arquivos/");
        Adaptador adpt = new Adaptador();
        List<Arquivo> lista = new List<Arquivo>();

        protected void Page_Init(object sender, EventArgs e)
        {
            Session.Add("pathDir", pathDir);
            if (!IsPostBack)
            {
                lista = adpt.RetornaArquivos(lista);
                //GridView1.DataSource = lista;
                //GridView1.DataBind();
            }
        }
        //protected void Page_Unload(object sender, EventArgs e)
        //{
        //    this.lista = null;
        //    //Session
        //    GridView1.DataSource = lista;
        //    GridView1.DataBind();
        //}
        protected void carrega_Grid(string opcao)
        {
            lista = (List<Arquivo>)Session[opcao];
            lista = adpt.RetornaArquivos(lista);
            GridView1.DataSource = lista;
            GridView1.DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            ImageButtonVer.Visible = false;
            ImageButtonDelete.Visible = false;
            if (Session["abaAtiva"].ToString().Equals((string)"TabPanelPessoais"))
            {
                //lista = adpt.RetornaArquivos("docsPessoais");
                //lista = (List<Arquivo>)Session["docsPessoais"];
                //GridView1.DataSource = lista;
                //GridView1.DataBind();
                carrega_Grid("docsPessoais");
                if (Session["postou_docsPessoais"].ToString().Equals((string)"sim"))
                {
                    ImageButtonVer.Visible = true;
                    ImageButtonDelete.Visible = true;
                }
            }
            else if (Session["abaAtiva"].ToString().Equals((string)"TabPanelTitulacao"))
            {
                carrega_Grid("titulacoes");
                if (Session["postou_titulacoes"].ToString().Equals((string)"sim"))
                {
                    ImageButtonVer.Visible = true;
                    ImageButtonDelete.Visible = true;
                }
            }
            else if (Session["abaAtiva"].ToString().Equals((string)"TabPanelPortaria"))
            {
                carrega_Grid("portarias");
                if (Session["postou_portarias"].ToString().Equals((string)"sim"))
                {
                    ImageButtonVer.Visible = true;
                    ImageButtonDelete.Visible = true;
                }
            }
            else if (Session["abaAtiva"].ToString().Equals((string)"TabPanelCI"))
            {
                carrega_Grid("cis");
                if (Session["postou_cis"].ToString().Equals((string)"sim"))
                {
                    ImageButtonVer.Visible = true;
                    ImageButtonDelete.Visible = true;
                }
            }
            else if (Session["abaAtiva"].ToString().Equals((string)"TabPanelAviso"))
            {
                carrega_Grid("avisoFerias");
                if (Session["postou_avisoFerias"].ToString().Equals((string)"sim"))
                {
                    ImageButtonVer.Visible = true;
                    ImageButtonDelete.Visible = true;
                }
            }
            else if (Session["abaAtiva"].ToString().Equals((string)"TabPanelRequerimento"))
            {
                carrega_Grid("requerimentos");
                if (Session["postou_requerimentos"].ToString().Equals((string)"sim"))
                {
                    ImageButtonVer.Visible = true;
                    ImageButtonDelete.Visible = true;
                }
            }
            else if (Session["abaAtiva"].ToString().Equals((string)"TabPanelOutros"))
            {
                carrega_Grid("outros");
                if (Session["postou_outros"].ToString().Equals((string)"sim"))
                {
                    ImageButtonVer.Visible = true;
                    ImageButtonDelete.Visible = true;
                }
            }
        }

        protected void ImageButtonSalvar_Click(object sender, ImageClickEventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                //lembrar de adicionar o ID do cadastro ao nome na hora de adicionar ao banco
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

                if (Session["postou_" + nameFile].ToString().Equals((string)"sim"))
                {
                    string nomeArquivo = nameFile + ext;
                    // Create a merge document and set it's properties
                    MergeDocument document = MergeDocument.Merge(pathDir + nomeArquivo, FileUpload1.PostedFile.FileName);
                    //string nomeArquivo1 = FileUpload1.PostedFile.FileName.Substring(FileUpload1.PostedFile.FileName.LastIndexOf('\\') + 1);
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
                        FileUpload1.SaveAs(pathDir + nameFile + ext);
                        //FileUpload1.SaveAs("C:\\Temp\\" + nameFile + ext);
                        Session.Add("fileName", nameFile + ext);

                        
                    }
                    catch (Exception ex)
                    {
                        LabelErro.Text = ex.Message;
                    }
                }
                LabelArquivo.Text = nameFile + ext;
                ImageButtonVer.Visible = true;
                ImageButtonDelete.Visible = true;
                //levantar flag dizendo que o arquivo foi postado
                Session.Add("postou_" + nameFile, "sim");

                //List<Arquivo> lista = new List<Arquivo>();
                Arquivo arquivo = new Arquivo();
                arquivo.nome_Arquivo = nameFile + ext;
                arquivo.tipo_Arquivo = nameFile;
                
                lista = (List<Arquivo>)Session[nameFile];
                if (lista == null)
                {
                    lista = new List<Arquivo>();
                }
                lista.Add(arquivo);
                Session.Add(nameFile, lista);
                carrega_Grid(nameFile);
            }
            else
            {
                //Mostra o Erro quando não tem arquivo selecionado
                LabelErro.Text = "Selecione o arquivo";
            }
        }

        protected void ImageButtonVer_Click(object sender, ImageClickEventArgs e)
        {
            System.Diagnostics.Process.Start(Session["pathDir"].ToString() + Session["fileName"].ToString());
            ImageButtonVer.Visible = true;
            ImageButtonDelete.Visible = true;
        }

        protected void ImageButtonDelete_Click(object sender, ImageClickEventArgs e)
        {
            File.Delete(Session["pathDir"].ToString() + Session["fileName"].ToString());
            ImageButtonVer.Visible = false;
            ImageButtonDelete.Visible = false;
            string nameFile = Session["fileName"].ToString().Substring(0, Session["fileName"].ToString().IndexOf('.'));
            Session.Add("postou_" + nameFile, "nao");
        }
    }
}