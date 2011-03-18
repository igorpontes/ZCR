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
    public partial class tab_editar : System.Web.UI.UserControl
    {
        String pathDir = HttpContext.Current.Server.MapPath("~/arquivos/");
        Adaptador adpt = new Adaptador();
        List<Arquivo> lista = new List<Arquivo>();
        string diretorio;

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                carrega_Grid("docsPessoais");
                carrega_Grid("titulacoes");
                carrega_Grid("portarias");
                carrega_Grid("cis");
                carrega_Grid("avisoFerias");
                carrega_Grid("requerimentos");
                carrega_Grid("outros");
                lista = adpt.RetornaArquivos(lista);
                Session.Add("carregou_grid", "nao");
            }
        }

        protected void carrega_Grid(string opcao)
        {
            lista = (List<Arquivo>)Session[opcao];
            lista = adpt.RetornaArquivos(lista);
            GridView1.DataSource = lista;
            GridView1.DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ImageButtonSalvar_Click(object sender, ImageClickEventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                //lembrar de adicionar o ID do cadastro ao nome na hora de adicionar ao banco
                String nameFile = "";
                nameFile = retornaAbaAtiva();

                string nomeArquivo1 = FileUpload1.PostedFile.FileName.Substring(FileUpload1.PostedFile.FileName.LastIndexOf('\\') + 1);
                try
                {
                    FileUpload1.SaveAs(pathDir + nomeArquivo1);
                }
                catch (Exception ex)
                {
                    LabelErro.Text = ex.Message;
                }
                //levantar flag dizendo que o arquivo foi postado
                Session.Add("postou_" + nameFile, "sim");

                Arquivo arquivo = new Arquivo();
                arquivo.nome_Arquivo = nomeArquivo1;
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

        protected void GridView1_RowCommand1(object sender, GridViewCommandEventArgs e)
        {
            int index;

            if (e.CommandName == "Abrir")
            {
                string nome = "";
                try
                {
                    index = Convert.ToInt32(e.CommandArgument);
                    //int id = Convert.ToInt32(GridView1.DataKeys[index].Value);

                    string nameFile = retornaAbaAtiva();
                    lista = (List<Arquivo>)Session[nameFile];
                    if (lista == null)
                    {
                        LabelErro.Text = "Erro. Lista vazia!";
                    }
                    nome = lista.ElementAt(index).nome_Arquivo;
                    diretorio = pathDir;


                    if (Directory.Exists(diretorio))
                    {
                        System.Diagnostics.Process.Start(diretorio + nome);
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

            if (e.CommandName == "Excluir")
            {
                index = Convert.ToInt32(e.CommandArgument); ;
                //int id = Convert.ToInt32(GridView1.DataKeys[index].Value);
                string nameFile = retornaAbaAtiva();
                lista = (List<Arquivo>)Session[nameFile];
                if (lista == null)
                {
                    LabelErro.Text = "Erro. Lista vazia!";
                }
                string nome = lista.ElementAt(index).nome_Arquivo;
                diretorio = pathDir;

                //testar se ta pegando os arquivos certos
                FileInfo arquivoAntigo = new FileInfo(diretorio + nome);
                arquivoAntigo.Delete();

                lista.RemoveAt(index);
                Session.Add(nameFile, lista);
                carrega_Grid(nameFile);
            }
        }
    }
}